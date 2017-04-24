# Coding Challenge 6: Advanced UI Enhancements - List grouping

## Introduction
Welcome to the sixth #XamarinAlliance challenge, and this time a **coding** challenge that will enhance the current application UI.

* [Challenge description](#description)
* [Grouping](#grouping)
* [Design](#design)
* [Challenge completion](#completion)
* [Getting help](#gethelp)


## <a name="description"></a>Challenge Description

While using the Xamarin Alliance Template app, you'll see that we present all characters on the main page in a list. This works great and was enough to start off with.
But often we like to show some separation, mostly done through data grouping.

In our case we can alter the main page and use each character’s appearence to separate them based on which movie they act in, this by showing the movie title as a group header.

So the goal of this coding challenge is to enhance the current list implementation and add a grouping feature, based on the already acquired character data.

We'll take a look at what our options are on adding grouping,  whether you start out with a new app or whether you have to work from an already existing data stream.
After that we'll dive into the XAML to show you how to enable the grouping feature and what options you have to change the default design.

The goal of this coding challenge is improve the user experience of our app by **adding grouping to our character list overview**. To unlock this challenge, you will need to:

1. **reformatted the data from the service to a list of lists,**
2. **redesigned the ListView to use and show the grouped data.**

![Challenge 6 outcome](https://github.com/Depechie/XamarinAlliance/blob/master/Challenge%206/images/xa_screenshot1.png)

## <a name="grouping"></a>Grouping

Often, large sets of data can become unwieldy when presented in a continuously scrolling list. Enabling grouping can improve the user experience in these cases by better organizing the content and activating platform-specific controls that make navigating data easier.

When grouping is activated for a ListView, a header row is added for each group. How and on what you group your data is totally up to you, but some steps need to be taken care off before the ListView will render this content.
So to enable grouping you'll need to go through the following:

* Create a list of lists (a list of groups, each group being a list of elements).
* Set the ListView's ItemsSource to that list.
* Set IsGroupingEnabled to true.
* Set GroupDisplayBinding to bind to the property of the groups that is being used as the title of the group.
* [Optional] Set GroupShortNameBinding to bind to the property of the groups that is being used as the short name for the group. The short name is used for the jump lists (right-side column on iOS - see example below, tile grid on Windows Phone).

![Jump list iOS](https://github.com/Depechie/XamarinAlliance/blob/master/Challenge%206/images/xa_screenshot2.png)

The first requirement, creating a list of lists, can be done in 2 different ways. Either your data service already provides a correct grouped stream, or you'll need to manipulate the given data into a correct format.

In this case, the current data service gives a list of characters with a list of movie appearances. But for our challenge we want to group our characters per movie, so the lists need to be in a different order.

To help out with reformatting the data, we have a small helper class that makes everything super easy to use and set up.
```csharp
public class Grouping<K, T> : ObservableCollection<T>
{
  public K Key { get; private set; }
public Grouping(K key, IEnumerable<T> items)
  {
      Key = key;
      foreach (var item in items)
        this.Items.Add(item);
  }
}
```

Grouping class seems simple, but is very extensible, especially if using data binding.
Notice that a Grouping is really just an ObservableCollection of type T. However, each Grouping also has a Key.

Now this key is of type K, which means it could be anything including a complex data structure. This means you could in theory bind your header to a data structure of K and then bind multiple controls to multiple properties, which is very cool.

But for this example though we will just make K a string, because all we want to show for now is the movie title in which the character appeared.

You'll first need to change the current Items property in the CharacterListViewModel from
```csharp
public ObservableRangeCollection<Character> Items { get; set; }
```
To
```csharp
public ObservableRangeCollection<Grouping<string, Character>> Items { get; set; } 
```
That way we tackled the fist item of our todo, creating a list of lists.

> **TIP:** Even though the Grouping class is very straightforward, you'll still need to do all the hard work yourself in code, to actually have a list of movies with each movie having a list of characters.

Best place to do this, is in the ExecuteLoadItemsCommand also in the CharacterListViewModel.
After you got all the characters, be sure to iterate them and keep track of each movie available in the appearances list and by doing this, create a new list per movie and add the characters.
Be sure to use the Grouping class and have the movie title as the key field.

### <a name="design"></a>Design

This will tell the ListView to treat the given ItemSource as a Grouped one, meaning it will handle the List with Lists scenario. It also tells it what data part to use for the Group header.
The code snippet above will already give us the correct solution for this challenge, but will render each group header in an OS default look and feel.
Although this can be enough for some apps, we would like to format our headers in a nicer way and platform independent ( so same look and feel for each platform ).

To do this, you'll need to add an extra DataTemplate, but this time not for an ItemTemplate, but one for a GroupHeaderTemplate.

By using a DataTemplate you'll have all the freedom of designing your group header, as long as you bind to the data that is availabe in the Group. So in our case we are just limited to one string called Key.
You have several options to present this Key field, I would suggest a TextCell or a Label.

For a good example on this take a look [here](https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/customizing-list-appearance/#Grouping).

## <a name="completion"></a>Challenge Completion

You have unlocked this challenge when you:

1. **reformatted the data from the service to a list of lists,**
2. **redesigned the ListView to use and show the grouped data.**

When you have completed your coding challenge, feel free to tweet about using the [#XamarinAlliance](https://twitter.com/hashtag/xamarinalliance) hashtag.

## <a name="gethelp"></a>Getting help

* Check the [Xamarin Forums](https://forums.xamarin.com/)
* Tweet hashtag [#XamarinAlliance](https://twitter.com/hashtag/xamarinalliance)
* Questions or issues? Check out the [FAQ](https://github.com/msdxbelux/XamarinAlliance/blob/master/FAQ.md) or [log an issue](https://github.com/msdxbelux/XamarinAlliance/issues)
