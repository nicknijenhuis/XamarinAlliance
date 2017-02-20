# Coding Challenge 2: Adding pages and navigation

## Introduction
Welcome to the second #XamarinAlliance challenge, and this time it's a real **coding** challenge. If you have completed the [first challenge](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%201/Readme.md), you're developer machine should be all set to start writing some code.

* [Challenge description](#description)
* [Page navigation](#pagenavigation)
* [Hierarchical navigation](#hierarchicalnavigation)
* [Master-Detail navigation](#masterdetailnavigation)
* [Challenge completion](#completion)
* [Getting help](#gethelp)


## <a name="description"></a>Challenge Description

If have been using the Xamarin Alliance Template App, you noticed that the app only contains a single page. This is ok to start with, but is a somewhat limiting experience, especially as we want to display more details about the items in the listview. Therefore, we will be extending the app with additional pages and a navigation mechanism. 
If you have been using your own app, you might have already unlocked this challenge; do read on, because maybe you'll learn something new or get some inspiration for a new feature.

The goal of this coding challenge is improve the user experience of our app by **adding multiple pages and implement navigation**. To unlock this challenge, you will need to:
1. **have multiple pages in your app,**
2. **add navigation to the different pages.**

We'll take a look at how you can add multiple pages to your Xamarin app and how to navigation between the different pages. As we have been using Xamarin.Forms for the template app, we'll explain these concepts using Xamarin.Forms, although you're free to implement a native app too.

By the time we release the third challenge, we'll also publish the updated source code for the Xamarin Alliance template app.


## <a name="pagenavigation"></a>Page Navigation

With Xamarin.Forms there are a number of different ways to implement [page navigation](https://developer.xamarin.com/guides/xamarin-forms/user-interface/navigation/). 

![Navigation experiences](https://github.com/msdxbelux/XamarinAlliance/blob/master/Challenge%202/images/xa_navigation.jpg)

For this challenge, we'll consider only two of them:
* Hierarchical navigation using the *NavigationPage* class
* Master-Detail pages using the *MasterDetailPage* class

For our Xamarin Alliance template app, we'll add a detail page that provides the details of a Star Wars character. This page will be called upon tapping the corresponding item in *ListView* on the main page.

> **TIP:** you could also add a carousel page to allowing scrolling through the list of characters.

### <a name="hierarchicalnavigation"></a>Hierarchical navigation

The *NavigationPage* class provides a hierarchival navigation experience where you can navigate through pages, both forwards and backwards. This class uses a stack (LIFO) of Page objects. To move to another page, you *push* a new page on the stack; when navigating backwards, you *pop* the current page from the stack and the new topmost page becomes the active page. It's recommended that the pages that are pushed on the navigation stack are *ContentPage* instances only.

The first page on the navigation stack is called the *root page* of the application. This is accomplished as follows (Page1Xaml is of type *ContentPage*):
```csharp
public App ()
{
  MainPage = new NavigationPage (new Page1Xaml ());
}
```

If we now want to navigate to another page from inside the Page1Xaml page (e.g. when tapping a button), we can leverage the *Navigation* property of the current page and invoke the *PushAsync* method on it, providing the target *ContentPage* instance:

```csharp
async void OnNextPageButtonClicked (object sender, EventArgs e)
{
  await Navigation.PushAsync (new Page2Xaml ());
}
```

To programmatically return to the previous page in the navigation stack, we again leverage the *Navigation* property on the current page, but this time, we invoke the *PopAsync* method:

```csharp
async void OnPreviousPageButtonClicked (object sender, EventArgs e)
{
  await Navigation.PopAsync ();
}
```

Note that you can push multiple pages onto the navigation stack before 'walking back the stack'. If you want to return straight to the root page of the stack, you can use the *Navigation.PopToRootAsync()' method.

In our Xamarin Alliance template app, we need to navigate to the character detail page when tapping on an entry in the list of characters on the main page. As shown above, we'll first create a root *NavigationPage* and push our main page on the stack. When an item is tapped, we push a character detail *ContentPage* on the stack.

```csharp
async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
{
    var item = args.SelectedItem as Item;
    if (item == null)
        return;

    await Navigation.PushAsync(new CharacterDetailPage(item));

    // Manually deselect item
    ItemsListView.SelectedItem = null;
}
```

As you notice from the code fragment above, we need to provide information about the selected item to the character detail page. One way to achieve this, is to pass the information to the constructor of the target page.

To navigate back from the character detail page to the main page, we would need to *pop* the page from the stack. However, if we use the default platform navigation controls (platform specific), no additional code is required in the detail page. If, on the other hand, we want to add an explicit control to navigate back, we'd have to invoke the *Navigation.PopAsync()* method.



More details about hierarchical navigation can be found [here](https://developer.xamarin.com/guides/xamarin-forms/user-interface/navigation/hierarchical/).


### <a name="masterdetailnavigation"></a>Master-Detail pages



## <a name="completion"></a>Challenge Completion

You have unlocked this challenge when you:
1. **have multiple pages in your app,**
2. **add navigation to the different pages.**

When you have completed your coding challenge, feel free to tweet about using the [#XamarinAlliance](https://twitter.com/hashtag/xamarinalliance) hashtag.

## <a name="gethelp"></a>Getting help

* Check the [Xamarin Forums](https://forums.xamarin.com/)
* Tweet hashtag [#XamarinAlliance](https://twitter.com/hashtag/xamarinalliance)
* Create an issue in the [GitHub repo](https://github.com/msdxbelux/XamarinAlliance/issues) - you can add labels and milestones to provide additional context


## Hidden Gem

A great resource to get started with building Xamarin.Forms apps is this [free ebook](https://developer.xamarin.com/guides/xamarin-forms/creating-mobile-apps-xamarin-forms/). Make sure to download it and enjoy the read!
