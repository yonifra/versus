using System;
using System.Collections.Generic;
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

           // SimulateData();
        }

        public async void AddInitialData()
        {
            var ronaldo = new VsEntity
            {
                Description = "Cristiano Ronaldo is also one of the best",
                ImageUrl =
                    @"http://www.speechonfashion.com/wp-content/uploads/2016/03/fotos-cristiano-ronaldo-2009-cabelo.jpg",
                Name = "Cristiano Ronaldo",
                Competition = "Messi vs. Ronaldo"
            };

            var competition = new VsCompetition
            {
                BackdropUrl = @"http://www.google.com",
                Description = "This is a cool competition between two soccer legends",
                EndingDate = DateTime.Now.AddDays(10),
                StartedBy = "yonifra",
                Category = "Soccer",
                Name = "Messi vs. Ronaldo"
            };

            var messi = new VsEntity
                            {
                                Description = "Lionel Messi is considered one of the best in the world",
                                ImageUrl = @"http://img.uefa.com/imgml/TP/players/1/2016/324x324/95803.jpg",
                                Name = "Lionel Messi",
                                Competition = "Messi vs. Ronaldo"
                            };

            var cat = new Category
            {
                BackdropUrl = @"http://www.google.com",
                Description = "Best competitions from the soccer world",
                Name = "Soccer",
                SmallIconUrl = @"http://path.to.icon"
            };

            await _client.PushAsync(CategoriesName, cat);
            await _client.PushAsync(EntitiesName, messi);
            await _client.PushAsync(EntitiesName, ronaldo);
            await _client.PushAsync(CompetitionsName, competition);

            UpdateVote("Lionel Messi", "Messi vs. Ronaldo");
            UpdateVote("Cristiano Ronaldo", "Messi vs. Ronaldo");
        }

        private async void SimulateData()
        {
            await _client.DeleteAsync(CategoriesName);
            await _client.DeleteAsync(CompetitionsName);
            await _client.DeleteAsync(EntitiesName);

            AddInitialData();
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
