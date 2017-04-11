using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using XamarinAllianceApp.Helpers;
using XamarinAllianceApp.Models;

namespace XamarinAllianceApp.Services
{
    public class CharacterService
    {
        private MobileServiceClient Client;
        private IMobileServiceTable<Character> CharacterTable;

        public CharacterService()
        {
            Client = new MobileServiceClient(Constants.MobileServiceClientUrl);
            CharacterTable = Client.GetTable<Character>();
        }

        /// <summary>
        /// Get the list of characters
        /// </summary>
        /// <returns>ObservableCollection of Character objects</returns>
        public async Task<ObservableCollection<Character>> GetCharactersAsync()
        {
            try
            {
                var query = CharacterTable.OrderBy(c => c.Name);
                var characters = await query.ToListAsync();

                return new ObservableCollection<Character>(characters);
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
