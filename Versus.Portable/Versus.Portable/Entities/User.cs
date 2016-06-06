using System;
using System.Collections.Generic;

namespace Versus.Portable.Entities
{
    public class User
    {
        public string Username { get; private set; }
        public DateTime JoinedDate { get; set; }
        public List<VsCompetition> FavoriteCompetitions { get; set; }
        public List<User> Friends { get; set; }
        public Tuple<int, int> LikesDislikes { get; set; }

        public User(string username)
        {
            LikesDislikes = new Tuple<int, int>(0, 0);
            JoinedDate = DateTime.Now;
            FavoriteCompetitions = new List<VsCompetition>();
            Friends = new List<User>();
            Username = username;
        }
    }
}
