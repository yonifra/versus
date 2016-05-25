using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using Newtonsoft.Json;
using Versus.Common.Entities;

namespace Versus.Common.Data
{
    public class FirebaseManager
    {
        private static FirebaseManager _instance;
        private const string BasePath = "https://vs-app.firebaseio.com/";
        private const string FirebaseSecret = "0MyNdWBFTqAfUdRyr801j6lfWvSRb157WpWv9Q9n";
        private static FirebaseClient _client;
        private const string CategoriesName = "categories";
        private const string CompetitionsName = "competitions";
        private const string EntitiesName = "entities";

        public static FirebaseManager Instance
        {
            get { return _instance ?? (_instance = new FirebaseManager()); }
        }

        private FirebaseManager()
        {
            var config = new FirebaseConfig
            {
                AuthSecret = FirebaseSecret,
                BasePath = BasePath
            };

            _client = new FirebaseClient(config);
        }

        /// <summary>
        /// Deletes ALL the data from the server. USE WITH CAUTION!
        /// </summary>
        private async void DeleteAllData()
        {
            await DeleteNode(CategoriesName);
            await DeleteNode(CompetitionsName);
            await DeleteNode(EntitiesName);
        }

        public async Task<bool> DeleteNode(string nodeName)
        {
            var response = await _client.DeleteAsync(nodeName);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async void UpdateVote(string entity, string competition)
        {
            var entities = await GetAllEntities();

            foreach (var e in entities)
            {
                if (e.Value.Name == entity && e.Value.Competition == competition)
                {
                    var newItem = e.Value;
                    newItem.Votes++;
                    await _client.UpdateAsync(EntitiesName + "/" + e.Key, newItem);
                    break;
                }
            }
        }

        public async Task<bool> AddCompetition(VsCompetition competition)
        {
            var response = await _client.PushAsync(CompetitionsName, competition);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> AddEntity(VsEntity entity)
        {
            var response = await _client.PushAsync(EntitiesName, entity);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> AddCategory(Category category)
        {
            var response = await _client.PushAsync(CategoriesName, category);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<Dictionary<string, VsCompetition>> GetAllCompetitions()
        {
            var response = await _client.GetAsync(CompetitionsName);

            return JsonConvert.DeserializeObject<Dictionary<string, VsCompetition>>(response.Body);
        }

        public async Task<Dictionary<string, VsEntity>> GetAllEntities()
        {
            var response = await _client.GetAsync(EntitiesName);

            return JsonConvert.DeserializeObject<Dictionary<string, VsEntity>>(response.Body);
        }

        public async Task<Dictionary<string, Category>> GetAllCategories()
        {
            var response = await _client.GetAsync(CategoriesName);

            return JsonConvert.DeserializeObject<Dictionary<string, Category>>(response.Body);
        }
    }
}
