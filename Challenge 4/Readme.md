# Coding Challenge 4: Authenticating users

## Introduction
Welcome to already the fourth #XamarinAlliance coding challenge. In the previous challenge you created a connected mobile application by reading the application data from a cloud-hosted backend. So far our application did not require users to authenticate to work with the application.

**The goal of this coding challenge will be to add user authentication to the mobile app**. For example, you could require that users first register before they get access to the application functionality. Because we don't want to implement a user identity store, we will allow users to **authenticate using existing authentication providers**, such as Facebook, Twitter, Microsoft Account, etcetera. 

By completing this coding challenge, you will have learned how to add authentication using existing authentication providers to your Xamarin application.

* [Challenge description](#description)
* [Getting started](#gettingstarted)
* [Implementing authentication](#implementauth)
* [Resources](#resources)
* [Challenge completion](#completion)
* [Getting help](#gethelp)


## <a name="description"></a>Challenge Description

In this challenge we will use [**Azure Mobile Apps**](https://azure.microsoft.com/en-us/services/app-service/mobile/) to authenticate users to our Xamarin application. It allows your application to log in users with no code changes required on the app backend. It uses federated identity, in which a 3rd party identity provider stores accounts and authenticates users, and the application uses this identity instead of its own. So you don't have to create your own username/database store as part of the application. Azure App Service, of which Mobile Apps is a part, currently supports 5 identity providers: Azure Active Directory, Facebook, Google, Microsoft Account and Twitter.

In this challenge, we'll **first configure the Azure Mobile App to enable authentication** and configure one or multiple identity providers. Then we'll **modify the Xamarin App** to use the identity provider to login users. If you don't have your own Azure Mobile App, you can again **use a shared Azure Mobile App** which has authentication enabled and configured for a number of providers.

These are the criteria for completing this challenge:

1. **Modify the Xamarin App to enable authentication using one or more identity providers.**  
  
  

## <a name="gettingstarted"></a>Getting Started

***Source code***

We'll continue with the application from the [third challenge](https://github.com/msdxbelux/XamarinAlliance/tree/master/Challenge%203).  If you have not yet completed it, you can either complete that one first, or grab the completed sources from the [GitHub repository](https://github.com/msdxbelux/XamarinAlliance/tree/master/Challenge%203). 

If you're building your own application, you can continue working on that and add user authentication to it.


***Azure Mobile App***

Similar to the previous challenge, we have setup a shared Azure Mobile App for you which hosts the application data for the Xamarin Alliance template application and which has been configured with the following identity providers: Twitter, Facebook and Google.

The endpoint for the shared Azure Mobile App is [https://xamarinalliancesecurebackend.azurewebsites.net](https://xamarinalliancesecurebackend.azurewebsites.net).

Alternatively, if you have your own Azure Mobile App to host the application data, you'll need to configure user authentication (see below).


## <a name="implementauth"></a>Implementing user authentication in the Xamarin application

Because we're using Azure Mobile Apps we can easily integrate user authentication by only making code changes in the Xamarin application. It supports **two authenticate flows**: *client-managed* and *server-managed* flow. The server-managed flow provides the simplest authentication experience, as it relies on the provider's web authentication interface. The client-managed flow allows for deeper integration with device-specific capabilities as it relies on provider-specific device-specific SDKs.

In this challenge we'll be using the simpler, **server-managed authentication flow**. In a server flow, Azure App Service manages the OAuth authentication flow by displaying the sign-in page of the selected provider. Once the identity provider returns, Azure App Service generates an App Service authentication token.

Adding the authentication to the Xamarin application consists of the following steps:

1. Configure the identity providers on the Azure Mobile App
2. Add authentication to the portable class library project
3. Add authentication to the platform-specific project(s)

In the following paragraphs we'll be going into more depth on how to implement authentication.
  


### Configure identity providers on the Azure Mobile App

To configure authentication on the Azure Mobile App, you need to go through a few steps:

1. Open the [Azure portal](https://portal.azure.com) and navigate to your Azure Mobile App
2. Click Settings > Authentication/Authorization
3. Make sure to switch **App Service Authentication** to **ON**
4. Configure the identity providers you wish to enable

![App Service Authentication](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%204/images/xa_ch4_appservice_auth.jpg)

By default, the Azure Mobile App accepts anonymous requests. There are two options to refuse anonymous requests:

* Configure the Azure Mobile App to not allow anonymous requests but instead require the user to sign in. This can be configured in the Azure portal (see screenshot above).
* Update the mobile service implementation code (see the [online documentation](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started-users#restrict-permissions-to-authenticated-users))
  
  
If you have not implemented your own Azure Mobile App, make sure to update the current Azure Mobile Service to use the secured shared Azure Mobile App that we provided for you:

[https://xamarinalliancesecurebackend.azurewebsites.net](https://xamarinalliancesecurebackend.azurewebsites.net)



### Add authentication to the portable class library project

To sign in a user with App Service authentication we need to invoke the [*LoginAsync*](https://msdn.microsoft.com/library/azure/dn268341(v=azure.10).aspx) extension method on the *MobileServiceClient* class. This method takes the type of identity provider as an input parameter to choose between one of the five providers that are supported (e.g. Facebook, Twitter).

First we'll define an **IAuthenticate** interface in the portable class library. We're doing this because we have a platform-specific implementation for authenticating with the backend. Using this interface in the portable class library we can implement most of the functionality in a platform agnostic manner.

```csharp
public interface IAuthenticate
 {
     Task<bool> Authenticate();
 }
 ```

To track the authentication, you can then create a static instance of *IAuthenticate* in the *App* class:

```csharp
public static IAuthenticate Authenticator { get; private set; }

 public static void Init(IAuthenticate authenticator)
 {
     Authenticator = authenticator;
 } 
 ```

On the main page of the application we can then perform the actual authentication, for example in response to a button click event:

```csharp
async void loginButton_Clicked(object sender, EventArgs e)
 {
     if (App.Authenticator != null)
         authenticated = await App.Authenticator.Authenticate();

     // Set syncItems to true to synchronize the data on startup when offline is enabled.
     if (authenticated == true)
         await RefreshItems(true, syncItems: false);
 }
 ```

This completes the platform independent code for authenticating users. It's up to you to define some user interface for letting users sign into the application. This could be a login button on the main page, or you could have a separate login screen appear upon launcing the application.



### Add authentication to the platform-specific project

So far, we have not actually implemented the authentication process, we have just invoked our *IAuthenticate.Authenticate* method.  The next step is now to **implement this interface in the platform-specific project**. As an example, we'll show how to do this for an Android application.

In the Android project, update the **MainActivity** class to implement the **IAuthenticate** interface:


```csharp
public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IAuthenticate
```

Now implement this interface in the *MainActivity* class:

```csharp
// Define an authenticated user.
 private MobileServiceUser user;

 public async Task<bool> Authenticate()
 {
     var success = false;
     var message = string.Empty;
     try
     {
         // Sign in with Facebook login using a server-managed flow.
         user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this,
             MobileServiceAuthenticationProvider.Facebook);
         if (user != null)
         {
             message = string.Format("you are now signed-in as {0}.",
                 user.UserId);
             success = true;
         }
     }
     catch (Exception ex)
     {
         message = ex.Message;
     }

     // Display the success or failure message.
     AlertDialog.Builder builder = new AlertDialog.Builder(this);
     builder.SetMessage(message);
     builder.SetTitle("Sign-in result");
     builder.Create().Show();

     return success;
 }
 ```

As you can see from the above code fragment, we're using the Facebook identity provider; this is specified using the **MobileServiceAuthenticationProvider.Facebook** enumeration value. You can specify another value if you prefer another identity provider. The shared Azure Mobile App has support for Facebook, Twitter or Google.

Finally, we need to **initialize the *IAuthenticate* instance**. This is done by adding the following code to the **OnCreate** method of the *MainActivity* class.

```csharp
// Initialize the authenticator before loading the app.
App.Init((IAuthenticate)this);
```

If you now launch the app, the data should not load until you sign into the application, for example using a sign in button.

![User authentication](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%204/images/xa_ch4_authentication.jpg)


## <a name="resources"></a>Resources

* https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-auth
* https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started-users
* https://azure.microsoft.com/en-us/services/app-service/mobile/


## <a name="completion"></a>Challenge Completion

You have unlocked this challenge when:

1. **You have modified the Xamarin App to enable authentication using one or more identity providers.**  

> TIP: you could add a registration page to your application to allow new users to register themselves. Taking it even further, you can have users select their favorite characters, sessions, recipes, etcetera.

When you have completed your coding challenge, collect your badge and feel free to tweet about it using the [#XamarinAlliance](https://twitter.com/hashtag/xamarinalliance) hashtag.



## <a name="gethelp"></a>Getting help

* Check the [Xamarin Forums](https://forums.xamarin.com/)
* Tweet hashtag [#XamarinAlliance](https://twitter.com/hashtag/xamarinalliance)
* Questions or issues? Check out the [FAQ](https://github.com/msdxbelux/XamarinAlliance/blob/master/FAQ.md) or [log an issue](https://github.com/msdxbelux/XamarinAlliance/issues)
