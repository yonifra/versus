using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using Newtonsoft.Json;
using Versus.Portable.Entities;

namespace Versus.Portable.Data
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

        public static FirebaseManager Instance => _instance ?? (_instance = new FirebaseManager());

        private FirebaseManager()
        {
            var config = new FirebaseConfig
            {
                AuthSecret = FirebaseSecret,
                BasePath = BasePath
            };

            _client = new FirebaseClient(config);
        }

        public FirebaseClient Client => _client;

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
            var competitions = await GetAllCompetitions();

            if (entities.Any(e => e.Value.Name == entity))
            {
                // Entity is valid
                foreach (var c in competitions.Where(c => c.Value.Name == competition))
                {

                    if (c.Value.CompetitorName1 == entity)
                    {
                        // TODO: Here you should validate if this user has logged in and if he's already
                        // casted his vote
                        c.Value.CompetitorScore1++;
                        UpdateCompetition(c.Value, c.Key);
                    }
                    else if (c.Value.CompetitorName2 == entity)
                    {
                        c.Value.CompetitorScore2++;
                        UpdateCompetition(c.Value, c.Key);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the vote. Position 1 updates the left side, position 2 updates the right side
        /// </summary>
        /// <returns>The vote.</returns>
        /// <param name="position">Position.</param>
        /// <param name="competition">Competition.</param>
        public async void UpdateVote(int position, string competition)
        {
            var competitions = await GetAllCompetitions();

            // Entity is valid
            foreach (var c in competitions.Where(c => c.Value.Name == competition))
            {
                switch (position)
                {
                    case 1:
                        // TODO: Here you should validate if this user has logged in and if he's already
                        // casted his vote
                        c.Value.CompetitorScore1++;
                        UpdateCompetition(c.Value, c.Key);
                        break;
                    case 2:
                        c.Value.CompetitorScore2++;
                        UpdateCompetition(c.Value, c.Key);
                        break;
                }
            }
        }

        private static async void UpdateCompetition(VsCompetition value, string key)
        {
            if (string.IsNullOrEmpty(value.CompetitorName1))
            {
                value.CompetitorName1 = " ";
            }

            if (string.IsNullOrEmpty(value.CompetitorName2))
            {
                value.CompetitorName2 = " ";
            }

            await _client.UpdateAsync($"{CompetitionsName}/{key}", value);
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

        public async Task<IEnumerable<VsCompetition>> GetCompetitions(string categoryName)
        {
            var response = await _client.GetAsync(CompetitionsName);

            var dict = JsonConvert.DeserializeObject<Dictionary<string, VsCompetition>>(response.Body);

            return dict.Values.Where(c => string.Equals(c.Category, categoryName, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<VsCompetition> GetCompetition(string competitionName)
        {
            var response = await _client.GetAsync(CompetitionsName);

            var dict = JsonConvert.DeserializeObject<Dictionary<string, VsCompetition>>(response.Body);

            return dict.Values.FirstOrDefault(c => string.Equals(c.Name, competitionName, StringComparison.CurrentCultureIgnoreCase));
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

        public async Task<VsEntity> GetEntityByName(string name)
        {
            var dic = await GetAllEntities();

            return dic.Values.First(e => string.Equals(e.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
