using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinAllianceApp.Helpers;
using XamarinAllianceApp.Models;
using XamarinAllianceApp.Services;

namespace XamarinAllianceApp.ViewModels
{
    public class CharacterListViewModel : BaseViewModel
    {
        CharacterService service;

        public ObservableRangeCollection<Grouping<string, Character>> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public CharacterListViewModel()
        {
            service = new CharacterService();
            Items = new ObservableRangeCollection<Grouping<string, Character>>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await service.GetCharactersAsync();

                Dictionary<string, List<Character>> characterDictionary = new Dictionary<string, List<Character>>();

                foreach(var item in items)
                {
                    foreach(var appearance in item.Appearances)
                    {
                        if (!characterDictionary.ContainsKey(appearance.Title))
                            characterDictionary.Add(appearance.Title, new List<Character>());

                        characterDictionary[appearance.Title].Add(item);
                    }
                }

                var groupedItems = from item in characterDictionary.OrderBy(x => x.Key)
                    select new Grouping<string, Character>(item.Key, item.Value);

                Items.AddRange(groupedItems);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
