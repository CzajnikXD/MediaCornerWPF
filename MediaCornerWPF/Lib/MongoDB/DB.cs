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

        /// <summary>
        /// Funkcja inicjalizująca połączenie z bazą danych MongoDB.
        /// </summary>
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

        /// <summary>
        /// Funkcja autoryzująca użytkownika na podstawie nazwy użytkownika i hasła.
        /// </summary>
        /// <param name="username">
        ///     Nazwa użytkownika do autoryzacji.
        /// </param>
        /// <param name="password">
        ///     Hasło użytkownika do autoryzacji.
        /// </param>
        /// <returns>
        ///     True - jeśli autoryzacja się powiodła.
        ///     False - jeśli autoryzacja się nie powiodła.
        /// </returns>
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

        /// <summary>
        /// Funkcja pobierająca nazwę użytkownika na podstawie identyfikatora użytkownika.
        /// </summary>
        /// <param name="UserId">
        ///     Identyfikator użytkownika.
        /// </param>
        /// <returns>
        ///     Nazwa użytkownika.
        /// </returns>
        public static string GetUsername(string UserId)
        {
            var user = UsersCollection.Find(x => x._id.ToString() == UserId).FirstOrDefault();

            return user.username;
        }

        /// <summary>
        /// Funkcja dodająca film do listy "do obejrzenia" użytkownika.
        /// </summary>
        /// <param name="userId">
        ///     Identyfikator użytkownika.
        /// </param>
        /// <param name="movieId">
        ///     Identyfikator filmu.
        /// </param>
        public static void AddToWatchlist(string userId, int movieId)
        {
            var alreadyWatchlisted = WatchlistCollection.Find(x => x.UsersId == userId && x.MovieId == movieId).FirstOrDefault();

            if (alreadyWatchlisted != null)
            {
                return;
            }

            var watchlist = new WatchlistedModel
            {
                UsersId = userId,
                MovieId = movieId
            };

            WatchlistCollection.InsertOne(watchlist);
        }

        /// <summary>
        /// Funkcja usuwająca film z listy "do obejrzenia" użytkownika.
        /// </summary>
        /// <param name="userId">
        ///     Identyfikator użytkownika.
        /// </param>
        /// <param name="movieId">
        ///     Identyfikator filmu.
        /// </param>
        public static void RemoveFromWatchlist(string userId, int movieId)
        {
            var watchlist = WatchlistCollection.Find(x => x.UsersId == userId && x.MovieId == movieId).FirstOrDefault();

            if (watchlist == null)
            {
                return;
            }

            WatchlistCollection.DeleteOne(x => x.UsersId == userId && x.MovieId == movieId);
        }

        /// <summary>
        /// Funkcja pobierająca listę "do obejrzenia" użytkownika.
        /// </summary>
        /// <param name="userId">
        ///     Identyfikator użytkownika.
        /// </param>
        /// <returns>
        ///     Lista obiektów WatchlistedModel reprezentujących filmy do obejrzenia.
        /// </returns>
        public static List<WatchlistedModel> GetWatchlist(string userId)
        {
            var watchlist = WatchlistCollection.Find(x => x.UsersId == userId).ToList();

            return watchlist;
        }

        /// <summary>
        /// Funkcja pobierająca listę "do obejrzenia" innych użytkowników.
        /// </summary>
        /// <param name="userId">
        ///     Identyfikator aktualnego użytkownika.
        /// </param>
        /// <returns>
        ///     Lista obiektów WatchlistedModel reprezentujących filmy do obejrzenia innych użytkowników.
        /// </returns>
        public static List<WatchlistedModel> GetOthersWatchlist(string userId)
        {
            var watchlist = WatchlistCollection.Find(x => x.UsersId != userId).ToList();

            return watchlist;
        }
    }
}
