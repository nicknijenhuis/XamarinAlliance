#Frequently Asked Technical Questions & Solutions

Navigate using the quicklinks below to some of the solutions to questions we've received:

[Installation Guide](#installation-guide)

[The Magic of the Clean](#the-magic-of-the-clean)

[Be careful with long paths](https://github.com/msdxbelux/XamarinAlliance/blob/master/FAQ.md#be-careful-with-long-paths)

[Java SDK Directory](https://github.com/msdxbelux/XamarinAlliance/blob/master/FAQ.md#java-sdk-directory)

[Android.Views.InflateException](https://github.com/msdxbelux/XamarinAlliance/blob/master/FAQ.md#androidviewsinflateexception)

[Codesigning Certificate for IOS in Visual Studio](https://github.com/msdxbelux/XamarinAlliance/blob/master/FAQ.md#codesigning-certificate-for-ios-in-visual-studio)

[Windows temporary key issues (Visual Studio 2017 RC)](#windows-temporary-key-issues-visual-studio-2017-rc)

[Project fails with mulitple errros regarding missing mscorlib reference](#project-fails-with-multiple-errors-regarding-missing-mscorlib-reference)

[EmbeddedResource is not supported](#embeddedresource-is-not-supported)

###Installation Guide
It’s very important to have in mind all the packages and plugins that you need to implement in the Visual Studio, to develop multi-platform apps without having any problem. If you don’t install all the components correctly, you’ll suffer a lot of errors in the compilation and deployment because Xamarin has a lot of dependencies, like Windows 10 SDK or Android SDK. So, these are the packages that you must install in the Visual Studio or Xamarin Studio:

![Image 1](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%201.png)


![Image 2](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%202.png)

![Image 3](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%203.png)

![Image 4](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%204.png)

![Image 5](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%205.png)

If your Visual Studio is already installed, you can go to Programs and Features in the Control Pane, and right click in Visual Studio. Select Change and then you can install all the packages missed.


###The Magic of the Clean
There are several problems in Xamarin that are solved with the ‘Clean Solution’ option. For example, we can solve this typical issue, just by cleaning the solution:

![Image 6](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%206.png)

![Image 7](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%207.png)

![Image 8](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%208.png)

![Image 9](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%209.png)

![Image 10](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2010.png)

![Image 11](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2011.png)

###Be careful with long paths
There is a limitation in the number of characters in the paths of a Xamarin project. It is a common error to save the project in long paths, such as desktop. The solution is very simple, we just must save it in the root directory, in C:/
This is the error:
The "CreateAdditionalLibraryResourceCache" task failed unexpectedly.System.IO.PathTooLongException: The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters.   at System.IO.Path.LegacyNormalizePath(String path, Boolean fullCheck, Int32 maxPathLength, Boolean expandShortPaths)   at System.IO.Path.NormalizePath(String path, Boolean fullCheck, Int32 maxPathLength, Boolean expandShortPaths)   at System.IO.Path.InternalGetDirectoryName(String path)   at Xamarin.Android.Tools.Files.CopyIfChanged(String source, String destination)   at Xamarin.Android.Tasks.CreateAdditionalLibraryResourceCache.Execute()   at Microsoft.Build.BackEnd.TaskExecutionHost.Microsoft.Build.BackEnd.ITaskExecutionHost.Execute()   at Microsoft.Build.BackEnd.TaskBuilder.<ExecuteInstantiatedTask>d__26.MoveNext()

![Image 12](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2012.png)

We save the project in a smaller path:

![Image 13](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2013.png)

Clean the project:

![Image 14](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2014.png)

And rebuild it:

![Image 15](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2015.png)

No errors!

![Image 16](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2016.png)

####Java SDK Directory
You must be sure that the latest Java SDK version is installed in your computer and the path that points to that SDK is correctly specified in the Visual Studio Features. The error is this: (the version is outdated)
![Image 17](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2017.png)

So first of all, go to the Oracle WebPage: 
http://www.oracle.com/technetwork/java/javase/downloads/jre8-downloads-2133155.html
Download the versión that fits your PC and install it. The version would be probably the 
Windows x64	62.66 MB  	jre-8u121-windows-x64.exe 
If you have a Windows.
Uninstall the older version:
#
![Image 18](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2018.png)

And set the path of the new one in the Visual Studio:

![Image 19](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2019.png)
![Image 20](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2020.png)
![Image 21](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2021.png)

Check it:

![Image 22](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2022.png)

Rebuild and…..

![Image 23](https://github.com/msdxbelux/XamarinAlliance/blob/master/Images/Image%2023.png)


###Android.Views.InflateException

Error: Android.Views.InflateException: Binary XML file line #1: Error inflating class android.support.v7.widget.Toolbar

Aditional details: This happens when creating a new project, before editing any of the code. 

Solution: In the Android Project: 

1st Step: Remove Xamarin Forms and all its dependencies with Nuget Manager

2nd Step: Restart Visual Studio

3rd Step: Clean the project

4th Step: Rebuild the project

#Codesigning Certificate for IOS in Visual Studio

Error: No valid iOS code signing keys found in keychain. You need to request a codesigning certificate from https://developer.apple.com

Solution:

1st Step: Inside the IOS solution, open the Info.plist file. We insert our Bundle ID. If it has underline symbols it will transform them into dashes.

2nd Step: In the same file, change the deployment target to the version our device is running.

3rd Step: Open the IOS solution project options and revise the following:
-	Configuration: Debug
-	Platform: Your device (Simulator by default)
-	SDK Version: Default.
-	Linker Options: Link all assemblies 
-	Supported Architectures: ARMv7 (Should work from iOS 6 onwards)

4th Step: Revise the iOS Bundle Signing
-	Identity: Developer
-	Provisioning profile: Automatic

5th Step: Open Xcode, create a new project where the name and organization make the same Bundle ID as step 1.

6th Step: Change deployment target to our connected device.

7th Step: Verify that Automatically Manage Signing is checked. Provisioning Profile: XCode Managed Profile should appear.

8th Step: You can view the profiles created (Normal and interface test) in two ways.
-	Preferences  Accounts  Details
-	In the directory: ~/Library/MobileDevice/Provisioning Profiles

9th Step: Run the application selecting your device as target. When it finishes building it will ask you to go to your device and accept the certificate.

10th Step: Stop the running application and close XCode.

11th Step: Return to Visual Studio. Open the iOS project build configuration and select your device.

###Windows Temporary Key Issues (Visual Studio 2017 RC)
On loading a new project, I get this error:
"Unable to copy the file 'Windows_TemporaryKey.pfx' from the project template to the project. Cannot find file 'C:\Path\Windows_TemporaryKey.pfx'"

Solution:

Manually add a UWP project to the Forms solution or use the MasterDetail template instead of the blank one. You will most likely get an error about a failure to add the Microsoft.NETCore.UniversalWindowsPlatform NuGet package to the UWP project, but it will be created and then you can just install the missing package.

###Project fails with multiple errors regarding missing mscorlib reference

Solution: 

Open your Android project .csproj file and add a reference to it with 

```
<Reference Include="mscorlib" />
```

###EmbeddedResource is not supported
When building the solution for the first time in Visual Studio 2015, you may get the error stating 'EmbeddedResource is not supported by one or more of the project's targets'.

Solution:

Open the solution in Visual Studio 2015, running as Administrator (right-click the VS 2015 icon and select 'Run as Administrator'). Build the solution and you should no longer get these errors. In the future, you can open the solution with Visual Studio 2015 as a regular user.

