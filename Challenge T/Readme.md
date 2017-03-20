# Coding Challenge T: Building a Techorama 2017 conference app 

## Introduction

We're excited to announce another partnership, this time with the amazing [Techorama](http://www.techorama.be) team! The team has provided us with a mobile backend that gives access to the entire agenda and speaker information for their upcoming conference.  

**The goal of this challenge is to use the mobile backend to build a great Techorama conference app, that will allow the attendees to check the conference agenda on their mobile device or get details about speakers or sessions.**


* [Challenge description](#description)
* [Connecting to the Azure Mobile App](#connecttobackend)
* [Resources](#resources)
* [Challenge completion](#completion)
* [Getting help](#gethelp)


## <a name="description"></a>Challenge Description

The mobile backend has been implemented using [**Azure Mobile Apps**](https://azure.microsoft.com/en-us/services/app-service/mobile/) so you can build on the knowledge you gained from [Challenge 3](https://github.com/msdxbelux/XamarinAlliance/tree/master/Challenge%203) to connect your Xamarin application to this mobile backend.

To make sure your app adheres to the Techorama design, you can find some **visual assets** in the [*assets*](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%20T/assets) folder.

Where can you find the mobile backend: [https://msft-xa-trm-api-euwe-prd.azurewebsites.net](https://msft-xa-trm-api-euwe-prd.azurewebsites.net).

When you navigate to the backend in the browser, you'll be presented with the swagger information that describes the layout of the data and also allows you to try out the queries. As you can see, you can retrieve information about sessions, speakers, room and tracks.

![Swagger](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%20T/images/xa_cht_swagger.jpg)

**Important**, the operations are provided as **custom APIs** in the Azure Mobile App and not as table APIs (see details below).

> TIP: take a look at the source code for the [Xamarin Evolve 2016](https://github.com/xamarinhq/app-evolve) mobile app for inspiration on what you could build.


## <a name="connecttobackend"></a>Connecting to the Azure Mobile App 

To connect to an Azure Mobile App backend, we have a client SDK for you. We'll be using the SDK for Xamarin, but there are versions available for the different client development options.

To install the Azure Mobile App SDK for Xamarin, first **add the [Microsoft.Azure.Mobile.Client](https://www.nuget.org/packages/Microsoft.Azure.Mobile.Client/) NuGet package**. This package provides the *MobileServiceClient* class to connect to the backend.

**Important**, make sure to add the NuGet package to all platform projects as well as the portable class library!

We then need to **instantiate and configure the *MobileServiceClient*** to connect to our endpoint by providing it the URL to our Azure Mobile App:

```csharp
string mobileServiceClientUrl = "https://msft-xa-trm-api-euwe-prd.azurewebsites.net";
MobileServiceClient Client = new MobileServiceClient(mobileServiceClientUrl);
```

To make sure that we can **serialize the data coming from the server**, we need the classes that represent the data tables in the mobile backend. Below you find the *Session* class. 

```csharp
public class Session
{
    Int32 id;
    string title;
    string description;
    string speaker;
    string room;
    string track;
    DateTime start;
    DateTime end;

    [JsonProperty(PropertyName = "id")]
    public int Id { get { return id; } set { id = value; } }
    [JsonProperty(PropertyName = "title")]
    public string Title { get { return title; } set { title = value; } }
    [JsonProperty(PropertyName = "description")]
    public string Description { get { return description; } set { description = value; } }
    [JsonProperty(PropertyName = "speaker")]
    public string Speaker { get { return speaker; } set { speaker = value; } }
    [JsonProperty(PropertyName = "room")]
    public string Room { get { return room; } set { room = value; } }
    [JsonProperty(PropertyName = "track")]
    public string Track { get { return track; } set { track = value; } }
    [JsonProperty(PropertyName = "start")]
    public DateTime Start { get { return start; } set { start = value; } }
    [JsonProperty(PropertyName = "end")]
    public DateTime End { get { return end; } set { end = value; } }
}
```

To speed up the process of creating the model classes, you can invoke the API, grab the JSON result, and then generate the C# classes from the JSON directly in Visual Studio. To do this, copy the JSON to the clipboard and in Visual Studio > select Edit > Paste Special > **Paste JSON as classes**.

![Paste JSON as classes](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%20T/images/xa_paste_classes.jpg)

To get the JSON, you can invoke the mobile backend, which is just a REST-based service, using a tool like [Postman](https://www.getpostman.com/). Make sure to set the **ZUMO-API-VERSION** header to **2.0.0**. For example, to get the Sessions information, use the following request:

```
GET http://msft-xa-trm-api-euwe-prd.azurewebsites.net/api/Sessions
HEADERS: ZUMO-API-VERSION: 2.0.0 
```


Once we're connected to the mobile backend, we can start querying the data. As mentioned before, the data is exposed through [custom APIs](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-dotnet-how-to-use-client-library#a-namecustomapiawork-with-a-custom-api). To invoke a custom API, you'll need to call into the *InvokeApiAsync* method on the mobile client:

```csharp
var result = await Client.InvokeApiAsync<Session>("Sessions", System.Net.Http.HttpMethod.Get, null);
```


## <a name="resources"></a>Resources

* https://developer.xamarin.com/guides/xamarin-forms/cloud-services/consuming/azure/
* https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-dotnet-how-to-use-client-library#a-namecustomapiawork-with-a-custom-api 
* https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started


## <a name="completion"></a>Challenge Completion

Through the T Challenge we're looking for a Cross Platform application (across iOS, Android and Windows) that leverages the API.

### The T Challenge winner will receive 2 tickets to attend Techorama 2017 (May 22nd - 24th in Antwerp) and the application will be available to use for all attendees of Techorama 2017.

To win the prize you should provide your application by Friday April 28th via this link: [Challenge T Submission](https://www.instant.ly/s/or7k6)

All submissions will be reviewed by a jury made up of a Microsoft employee and the organisers of Techorama. 

Prizes are not exchangeable and only open to participants that can attend Techorama 2017 in Belgium.

## <a name="gethelp"></a>Getting help

* Check the [Xamarin Forums](https://forums.xamarin.com/)
* Tweet hashtag [#XamarinAlliance](https://twitter.com/hashtag/xamarinalliance)
* Questions or issues? Check out the [FAQ](https://github.com/msdxbelux/XamarinAlliance/blob/master/FAQ.md) or [log an issue](https://github.com/msdxbelux/XamarinAlliance/issues)
