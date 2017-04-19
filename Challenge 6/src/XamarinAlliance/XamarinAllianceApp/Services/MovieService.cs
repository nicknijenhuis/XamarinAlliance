using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using XamarinAllianceApp.Helpers;
using XamarinAllianceApp.Models;

namespace XamarinAllianceApp.Services
{
    public class MovieService
    {
        private MobileServiceClient Client;
        private IMobileServiceTable<Movie> MovieTable;

        public MovieService()
        {
        	Client = new MobileServiceClient(Constants.MobileServiceClientUrl);
        	MovieTable = Client.GetTable<Movie>();
        }

        /// <summary>
        /// Get the list of movies
        /// </summary>
        /// <returns>ObservableCollection of Character objects</returns>
        public async Task<ObservableCollection<Movie>> GetMoviesAsync()
        {
        	try
        	{
                var query = MovieTable.OrderBy(c => c.Title);
        		var movies = await query.ToListAsync();

                return new ObservableCollection<Movie>(movies);
        	}
        	catch (Exception)
        	{
        		throw;
        	}
        }
    }
}
