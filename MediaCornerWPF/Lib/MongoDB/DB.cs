using MediaCornerWPF.Lib.MongoDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;


namespace MediaCornerWPF.Lib.MongoDB
{
    public class DB
    {
        public static string ConnectionString { get; set; }

        public static IMongoDatabase DbName { get; set; }
        public static IMongoCollection<UsersModel> UsersCollection { get; set; }
        public static IMongoCollection<WatchlistedModel> WatchlistCollection { get; set; }

        public static void InitDB()
        {
            Debug.WriteLine("DB INIT");

           ConnectionString = "mongodb+srv://admin:ZAQ123wsx@projects.gkf3nwl.mongodb.net/";

            var settings = MongoClientSettings.FromConnectionString(ConnectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);
            DbName = client.GetDatabase("MediaCorner");
            UsersCollection = DbName.GetCollection<UsersModel>("Users");
            WatchlistCollection = DbName.GetCollection<WatchlistedModel>("Watchlist");
        }

        public static bool AuthorizeUser(string username, string password)
        {
            var user = UsersCollection.Find(x => x.username == username && x.password == password).FirstOrDefault();

            if (user != null)
            {
                LoggedUser.InitUser(user._id.ToString(), user.username, user.password);
                return true;
            }
            return false;
        }

        public static string GetUsername(string UserId)
        {
            var user = UsersCollection.Find(x => x._id.ToString() == UserId).FirstOrDefault();

            return user.username;
        }

        public static void AddToWatchlist(string userId, int movieId)
        {
            var alreadyWatchlisted = WatchlistCollection.Find(x => x.UsersId == userId && x.MovieId == movieId).FirstOrDefault();

            if (alreadyWatchlisted != null) {
                return;
            }

            var watchlist = new WatchlistedModel
            {
                UsersId = userId,
                MovieId = movieId
            };

            WatchlistCollection.InsertOne(watchlist);
        }

        public static void RemoveFromWatchlist(string userId, int movieId)
        {
            var watchlist = WatchlistCollection.Find(x => x.UsersId == userId && x.MovieId == movieId).FirstOrDefault();

            if (watchlist == null)
            {
                return;
            }

            WatchlistCollection.DeleteOne(x => x.UsersId == userId && x.MovieId == movieId);
        }

        public static List<WatchlistedModel> GetWatchlist(string userId)
        {
            var watchlist = WatchlistCollection.Find(x => x.UsersId == userId).ToList();

            return watchlist;
        }

        public static List<WatchlistedModel> GetOthersWatchlist(string userId)
        {
            var watchlist = WatchlistCollection.Find(x => x.UsersId != userId).ToList();

            return watchlist;
        }
    }
}
