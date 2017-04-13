# Coding Challenge 5: Connecting to Azure Storage

## Introduction
Welcome to the fifth #XamarinAlliance coding challenge. In the third challenge we already used the cloud to store structured, tabular data. In this challenge you'll use the cloud for storing and retrieving non-relational data, for example images or documents.

**The goal of this coding challenge will be to retrieve non-relational data from the cloud**. 

By completing this coding challenge, you will have learned how to retrieve data from Azure Storage and use it in your Xamarin application.

> **IMPORTANT:** This is the final challenge for **receiving your official Xamarin Alliance Diploma**! Check below what is needed to [receive credit](#receivecredit).


* [Challenge description](#description)
* [Getting started](#gettingstarted)
* [Introduction to Azure Storage](#introstorage)
* [Connecting to Azure Storage from Xamarin](#connectstorage)
* [Resources](#resources)
* [Receive Credit](#receivecredit)
* [Challenge completion](#completion)
* [Getting help](#gethelp)


## <a name="description"></a>Challenge Description

In this challenge you'll need to retrieve an image from an Azure Storage account and use it inside the Xamarin application. The image file is stored as a blob on Blob storage and will be secured using a Shared Access Signature.

By storing the data outside of the mobile application you can easily modify or add data without having to publish an update of the application to the app store.


These are the criteria for completing this challenge:

1. **Modify the Xamarin App to retrieve a SAS token from the mobile backend.**  
2. **Modify the Xamarin App to retrieve an image from Azure Blob Storage using the SAS token.** 
3. **Get credit for participating in the Xamarin Alliance.**


## <a name="gettingstarted"></a>Getting Started

***Source code***

You can continue with the application from the [third challenge](https://github.com/msdxbelux/XamarinAlliance/tree/master/Challenge%203) or [fourth challenge](https://github.com/msdxbelux/XamarinAlliance/tree/master/Challenge%204).

If you're building your own application, you can continue working on it and add the retrieval of the image file.


***Azure Storage account***

We have set up a shared Azure Storage account for you which contains an image file for you to include in your application. The Azure Storage account is available at [https://xamarinalliance.blob.core.windows.net](https://xamarinalliance.blob.core.windows.net).

Alternatively, you can create your own Azure Storage account. Follow [these instructions](https://docs.microsoft.com/en-us/azure/storage/storage-create-storage-account#create-a-storage-account) to create an Azure Storage account. 


## <a name="introstorage"></a>Introduction to Azure Storage

### What is Azure Storage?
With Azure Mobile Apps you can store structured data and implement CRUD operations against tabular data stores, but it is less geared towards storing and managing large binary data, such as documents or image files. On the other hand, [**Microsoft Azure Storage**](https://docs.microsoft.com/en-us/azure/storage/storage-introduction) is a massively scalable, elastic cloud storage solution, which provides the following four services: Blob storage, Table storage, Queue storage, and File storage. In this challenge we will be using **Blob storage**. Blob Storage **stores unstructured object data**. A blob can be any type of text or binary data, such as a document, media file, or application installer. Blob storage is also referred to as Object storage.

With Blob storage accounts we distinguish between two **access tiers**: 

* A **Hot** access tier which indicates that the objects in the storage account will be more frequently accessed. This allows you to store data at a lower access cost.
* A **Cool** access tier which indicates that the objects in the storage account will be less frequently accessed. This allows you to store data at a lower data storage cost.

For data to be used by our mobile application we will be using the hot access tier because it needs to be accessible at any time.

Every blob is organized into a container. Containers also provide a useful way to assign security policies to groups of objects. A storage account can contain any number of containers, and a container can contain any number of blobs, up to the 500 TB capacity limit of the storage account.


### Security options for Azure Storage

By default only the storage account owner can access resources in the storage account. For the security of your data, every request made against resources in your account must be authenticated. Authentication relies on a **Shared Key model**. Blobs can also be configured to support anonymous authentication.

If you need to allow users controlled access to your storage resources, then you can create a **shared access signature (SAS)**. A SAS is a token that can be appended to a URL that enables delegated access to a storage resource. A SAS contains the permissions and a validity period of time for the access.

Finally, you can specify that a container and its blobs, or a specific blob, are available for public access. When you indicate that a container or blob is public, anyone can read it anonymously; no authentication is required. Public containers and blobs are useful for exposing resources such as media and documents that are hosted on websites.

In this challenge **we will be using shared access signatures** for providing access to the data in Azure Storage. Note that in this specific case we could make the blobs public because there is no private information contained in them.

In the diagram below you can see the architecture for getting a SAS token and retrieving a blob from Azure Storage.

![Storage Token Diagram](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%205/images/xa_storage_token_diagram.jpg)


## <a name="connectstorage"></a>Connecting to Azure Storage from Xamarin

As mentioned before, we will be retrieving an image file (Xamarin Alliance logo) from Azure Blob Storage. To authenticate to the storage account, we will be using a SAS token.

The sample image file is available on Azure Blob Storage here: [https://xamarinalliance.blob.core.windows.net/images/XAMARIN-Alliance-logo.png](https://xamarinalliance.blob.core.windows.net/images/XAMARIN-Alliance-logo.png)

To implement this scenario, we'll need to perform the following steps:

1. Add the Azure Storage Client SDK NuGet package to the Xamarin application
2. Invoke the mobile backend to retrieve a SAS token
3. Connect to the Storage account using the Azure Storage client SDK, providing the SAS token
4. Download the image file

### Add the Azure Storage Client SDK 

To interact with Azure Storage there is a client SDK, for which there is a NuGet package: **WindowsAzure.Storage**.
> **IMPORTANT:** make sure to use version **7.2.1** of the NuGet package because there is an issue with the v8 release that results in a 'Method not implemented exception' at runtime.

![Azure Storage Client SDK](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%205/images/xa_azurestorage_nuget.jpg)

### Retrieve the SAS token

The SAS token is generated on the mobile backend. In the shared mobile backend we've added a **custom API** that allows creating a new SAS token. The endpoint for retrieving the SAS token for the shared Azure Storage account (xamarinalliance.blob.core.windows.net) is:

[http://xamarinalliancebackend.azurewebsites.net/api/StorageToken/CreateToken](http://xamarinalliancebackend.azurewebsites.net/api/StorageToken/CreateToken)

Alternatively, if you're using the secure backend, the endpoint is available at:

[http://xamarinalliancesecurebackend.azurewebsites.net/api/StorageToken/CreateToken](http://xamarinalliancesecurebackend.azurewebsites.net/api/StorageToken/CreateToken)


To invoke this custom API from the Xamarin application, you can leverage the *InvokeApiAsync* method on the *MobileServiceClient* instance:

```csharp
var client = new MobileServiceClient(Constants.MobileServiceClientUrl);
var token = await client.InvokeApiAsync("/api/StorageToken/CreateToken");
```

The SAS token is then returned as a string value.

If you have built your own mobile backend, you can find instruction on how to create a custom api in the [online documentation](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-dotnet-backend-how-to-use-server-sdk#how-to-define-a-custom-api-controller).


### Connect to the Storage Account using the SAS token

Now we'll use the Azure Storage client SDK to connect to Azure Storage from our application. Note that we need to provide the SAS token and also specify the Azure Storage account name - for the shared storage account this is **xamarinbackend**.

```csharp
string storageAccountName = "xamarinalliance";

StorageCredentials credentials = new StorageCredentials(token);

CloudStorageAccount account = new CloudStorageAccount(credentials, storageAccountName, null, true);

var client = account.CreateCloudBlobClient();
```

### Download the image file

The final step is to download the blob from our storage account. The full URL for the image blob is [https://xamarinalliance.blob.core.windows.net/images/XAMARIN-Alliance-logo.png](https://xamarinalliance.blob.core.windows.net/images/XAMARIN-Alliance-logo.png). As can be derived from this URL, the image is located in a container named **images**.

```csharp
var container = client.GetContainerReference("images");
var blob = container.GetBlobReference("XAMARIN-Alliance-logo.png");

MemoryStream stream = new MemoryStream();

await blob.DownloadToStreamAsync(stream);
```

Now you have the actual blob downloaded in a *Stream* object, which can then be leveraged in the application.


## <a name="resources"></a>Resources

* https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-dotnet-how-to-use-client-library#a-namecustomapiawork-with-a-custom-api
* https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-shared-access-signature-part-1 
* https://developer.xamarin.com/guides/xamarin-forms/cloud-services/storage/azure-storage/
* https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-blob-storage
* https://azure.microsoft.com/en-us/services/app-service/mobile/


## <a name="receivecredit"></a>Receive Credit

To receive credit for the Xamarin Alliance and get your diploma, you will need to provide us with a **unique identifier (GUID)** through our [**online submission form**](https://aka.ms/xamarinalliancediploma). 

To get this unique identifier, you'll need to invoke a custom API on the mobile backend:

```csharp
var client = new MobileServiceClient(Constants.MobileServiceClientUrl);
var guid = await client.InvokeApiAsync("/api/XamarinAlliance/ReceiveCredit");
```

Once you have received this GUID, you can **submit it online** in the [submission form](https://aka.ms/xamarinalliancediploma). This will be your ticket to get your diploma.

Diplomas will be distributed at the end of each month for all challenges completed before the end of June 2017.


## <a name="completion"></a>Challenge Completion

You have unlocked this challenge when you:

1. **Modify the Xamarin App to retrieve a SAS token from the mobile backend.**  
2. **Modify the Xamarin App to retrieve an image from Azure Blob Storage using the SAS token.**
3. **Get credit for participating in the Xamarin Alliance.**
 

When you have completed your coding challenge, collect your badge and feel free to tweet about it using the [#XamarinAlliance](https://twitter.com/hashtag/xamarinalliance) hashtag.



## <a name="gethelp"></a>Getting help

* Check the [Xamarin Forums](https://forums.xamarin.com/)
* Tweet hashtag [#XamarinAlliance](https://twitter.com/hashtag/xamarinalliance)
* Questions or issues? Check out the [FAQ](https://github.com/msdxbelux/XamarinAlliance/blob/master/FAQ.md) or [log an issue](https://github.com/msdxbelux/XamarinAlliance/issues)
