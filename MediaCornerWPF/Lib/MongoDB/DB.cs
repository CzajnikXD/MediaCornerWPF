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

        public static void InitDB()
        {

           ConnectionString = "mongodb+srv://admin:ZAQ123wsx@projects.gkf3nwl.mongodb.net/";

            var settings = MongoClientSettings.FromConnectionString(ConnectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);
            DbName = client.GetDatabase("MediaCorner");
            UsersCollection = DbName.GetCollection<UsersModel>("Users");
        }

        public static bool AuthorizeUser(string username, string password)
        {
            Debug.WriteLine("xd");


            var user = UsersCollection.Find(x => x.username == username && x.password == password).FirstOrDefault();

            Debug.WriteLine(user.username);

            if (user != null)
            {
                LoggedUser.InitUser(user._id.ToString(), user.username, user.password);
                return true;
            }
            return false;
        }
    }
}
