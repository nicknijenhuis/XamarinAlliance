# Coding Challenge 3: Adding a cloud-based backend

## Introduction
Welcome to the third #XamarinAlliance coding challenge. If you have been using our template application you may have noticed that the data for our application is stored inside the mobile application in a JSON document.

The advantage of this approach is that the application can work perfectly offline and doesn't suffer from slow connectivity. On the other hand, sharing data across users or devices is not possible and if the data changes, you need to go through publishing an application update in the app store.

**The goal of this coding challenge will be to build a connected mobile app and move the data for the application to the cloud**. This will allow you to easily change the application data centrally; optionally you could add offline capabilities to overcome network connectivity issues.

By completing this coding challenge, you will have learned how to connect your Xamarin cross-platform application to the cloud.


* [Challenge description](#description)
* [Getting started](#gettingstarted)
* [Creating an Azure Mobile App](#createazure)
* [Connecting to the Azure Mobile App](#connecttobackend)
* [Resources](#resources)
* [Challenge completion](#completion)
* [Getting help](#gethelp)


## <a name="description"></a>Challenge Description

As mentioned above, the goal of this challenge is to build a connected mobile application. You will connect your mobile application to a cloud-based backend, hosted in the cloud using [**Azure Mobile Apps**](https://azure.microsoft.com/en-us/services/app-service/mobile/). 

With Azure Mobile Apps you can build a mobile backend in the cloud. One of the key features is the ability to expose your application data as a REST-based service. But more than just providing you a place to host the data, Azure Mobile Apps gives you a lot more features, which we'll explore later.

In this challenge we will **replace the JSON file** inside the mobile app with an Azure Mobile App that exposes the same data as a **REST service**. We will then **connect the Xamarin app** to this backend.

These are the criteria for completing this challenge:

1. **The Xamarin application connects to an Azure Mobile App backend.**
2. **Optionally, you have built your own Azure Mobile App.**

If you have been using the template application, we already have an Azure Mobile App instance available for you to connect the Xamarin application to. You can find the sources for the mobile backend in our GitHub repo, if you want to host it in your own Azure subscription. Alternatively, you can create your own Azure Mobile App yourself.



## <a name="gettingstarted"></a>Getting Started

***Source code***

We'll continue with the application from the [second challenge](https://github.com/msdxbelux/XamarinAlliance/tree/master/Challenge%202).  If you have not yet completed it, you can either complete that one first, or grab the completed sources from the [GitHub repository](https://github.com/msdxbelux/XamarinAlliance/tree/master/Challenge%202). 

If you're building your own application, you can continue working on that and move the application data to an Azure Mobile App in the cloud.


***Azure subscription***

If don't yet have an Azure subscription, there are multiple ways to get one:

* If you have an MSDN subscription, you have free Azure credits - [activate them](https://azure.microsoft.com/en-us/pricing/member-offers/msdn-benefits/)!
* Get your free Azure credits with [Visual Studio Dev Essentials](https://www.visualstudio.com/dev-essentials/)
* Get a 30-day [Azure free trial](https://azure.microsoft.com/en-us/free/)

If you just want to connect to our Xamarin Alliance Mobile App, you can find it here: [http://xamarinalliancebackend.azurewebsites.net](http://xamarinalliancebackend.azurewebsites.net/)


## <a name="createazure"></a>Creating an Azure Mobile App

The first step to moving your data to the cloud is to create an Azure Mobile App. Once you have an Azure subscription (see above), this process will take just a few minutes to provision.

The first step is to **provision a new Azure Mobile App** in the [Azure portal](https://portal.azure.com). Follow these steps to provision a new Azure Mobile App:

1. Open the [Azure portal](https://portal.azure.com).
2. Click +NEW and type Mobile Apps in the search box.
3. Select *Mobile App* and click Create.
4. Choose an app name for the mobile backend - this will provide you the endpoint URL for your Azure Mobile App.
5. Choose a resource group name or use an existing one - a resource group is a deployment grouping and facilitates deleting related resources.
6. Select a location for the backend.
7. Click Create to start the provisioning - this might take a few minutes to complete.

The second step is to **configure the server project**: select *App Deployment > Quickstart* and choose *Xamarin.Forms*.

![Configure server quickstart](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%203/images/xa_azure_configure_server.jpg)

The next step is to **create an Azure SQL Database** to host the application data.  Follow the instructions in the portal to create a new database.

Finally, you need to **specify the programming language** you want to use to implement the mobile backend; you choose between .NET and Node.js. For the Xamarin Alliance template application we have chosen to implement the mobile backend in .NET. You can find the source code of the backend in the [GitHub repo](https://github.com/msdxbelux/XamarinAlliance/tree/master/Challenge%203).

If you want to use a Node.js backend, check the [Azure online documentation](https://docs.microsoft.com/en-gb/azure/app-service-mobile/app-service-mobile-node-backend-how-to-use-server-sdk) for more details.

If you're using a .NET backend, you can **download a starter project** and open it in Visual Studio. The backend for an Azure Mobile App is actually an ASP.NET Web API project, that exposes a REST-based service.

![Download C# backend](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%203/images/xa_azure_download_csharp.jpg)

You can now modify the mobile backend to match the data for your application. Take a look at the mobile backend we created for the Xamarin Alliance template application, as it demonstrates how to deal with tables that have relationships between each other (foreign keys).

Once you have completed the mobile backend, you can publish the Visual Studio project to your Azure Mobile App:

1. In Visual Studio, righ-click the project and click **Publish**.
2. If asked to select a publish target, click **Microsoft Azure App Service** > Next (you may need to sign-in with your Azure credentials).
3. Choose your subscription and select your Azure Mobile App > Click OK.
4. Verify the publish profile information and click **Publish**.

When the backend is published successfully, a browser window will open and you'll see the landing page for the mobile app. Note down the URL of the Azure Mobile App endpoint, as you'll need this for wiring up the Xamarin app to the mobile backend.

More information about creating a mobile backend using .NET, can be found in the [Azure online documentation](https://docs.microsoft.com/en-gb/azure/app-service-mobile/app-service-mobile-dotnet-backend-how-to-use-server-sdk).



## <a name="connecttobackend"></a>Connecting to the Azure Mobile App 

Now that we have our mobile backend up and running, we need to connect the Xamarin mobile application to this backend. To connect to an Azure Mobile App backend, we have a client SDK for you. We'll be using the SDK for Xamarin, but there are versions available for the different client development options.

To install the Azure Mobile App SDK for Xamarin, first **add the [Microsoft.Azure.Mobile.Client](https://www.nuget.org/packages/Microsoft.Azure.Mobile.Client/) NuGet package**. This package provides the *MobileServiceClient* class to connect to the backend.

> **Important:** you need to add the NuGet package to both the portable class library project, as well as the platform-specific projects.

We then need to **instantiate and configure the *MobileServiceClient*** to connect to our endpoint by providing it the URL to our Azure Mobile App:

```csharp
string mobileServiceClientUrl = "http://xamarinalliancebackend.azurewebsites.net";
MobileServiceClient Client = new MobileServiceClient(mobileServiceClientUrl);
```

To make sure that we can **serialize the data coming from the server**, we need the classes that represent the data tables in the mobile backend. If you're using a .NET backend, you can reuse that classes from the backend. Below you find the *Movie* class.

```csharp
public class Movie
{
    Int32 id;
    string title;
    string description;
    ICollection<Character> characters;

    [JsonProperty(PropertyName = "id")]
    public Int32 Id
    {
        get { return id; }
        set { id = value; }
    }

    [JsonProperty(PropertyName = "title")]
    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    [JsonProperty(PropertyName = "description")]
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public string Version { get; set; }
}
```

Alternatively, if you have been using a JSON backend, you'll need to create the corresponding classes in the Xamarin app. Note that you can generate the C# classes for a fragment of JSON in Visual Studio. Copy the JSON to the clipboard and in Visual Studio > select Edit > Paste Special > **Paste JSON as classes**.

![Paste JSON as classes](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%203/images/xa_paste_classes.jpg)

To get the JSON, you can invoke the mobile backend, which is just a REST-based service, using a tool like [Postman](https://www.getpostman.com/). Make sure to set the **ZUMO-API-VERSION** header to **2.0.0**. For example, to get the Character table content, use the following request:

```
GET http://xamarinalliancebackend.azurewebsites.net/tables/character
HEADERS: ZUMO-API-VERSION: 2.0.0 
```


Once we're connected to the backend, we can start querying the tabular data. Before we can query the data, we **get a reference to the table** using the *GetTable* method. The *Character* class represents the Model class for the specific table as defined in the Azure Mobile App backend.

```csharp
IMobileServiceTable<Character> CharacterTable = Client.GetTable<Character>();
```

Now we can **query the data using the LINQ** syntax:

```csharp
var characters = await CharacterTable.ToListAsync();
```

So far, we have only read data from the backend but the SDK also supports **making modifications** (insert, update, delete) to the data:

```csharp
await CharacterTable.InsertAsync(newCharacter);
await CharacterTable.UpdateAsync(modifiedCharacter);
await CharacterTable.DeleteAsync(badCharacter);
```

The thing remaining is to **remove the JSON file** from the Xamarin application and to replace the call to read the data from the file by a call to **get the data from the mobile backend**.

If you launch the app, you should see the same data as before, only the data is now coming from the Azure Mobile App. If you modify the data in the database and refresh the mobile client, you should see the updates appear in the app.


## <a name="resources"></a>Resources

* https://developer.xamarin.com/guides/xamarin-forms/cloud-services/consuming/azure/
* https://docs.microsoft.com/en-gb/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started
* https://azure.microsoft.com/en-us/services/app-service/mobile/


## <a name="completion"></a>Challenge Completion

You have unlocked this challenge when:

1. **The Xamarin application connects to an Azure Mobile App backend.**
2. **Optionally, you have built your own Azure Mobile App.**

If you want to add offline capabilities to your mobile app, take a look at the [steps required](https://docs.microsoft.com/en-gb/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started-offline-data) to do so.

When you have completed your coding challenge, collect your badge and feel free to tweet about it using the [#XamarinAlliance](https://twitter.com/hashtag/xamarinalliance) hashtag.



## <a name="gethelp"></a>Getting help

* Check the [Xamarin Forums](https://forums.xamarin.com/)
* Tweet hashtag [#XamarinAlliance](https://twitter.com/hashtag/xamarinalliance)
* Questions or issues? Check out the [FAQ](https://github.com/msdxbelux/XamarinAlliance/blob/master/FAQ.md) or [log an issue](https://github.com/msdxbelux/XamarinAlliance/issues)
