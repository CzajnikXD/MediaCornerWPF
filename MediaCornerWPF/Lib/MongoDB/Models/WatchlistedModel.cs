using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCornerWPF.Lib.MongoDB.Models
{
    public class WatchlistedModel
    {
        public BsonObjectId _id { get; set; }
        public string UsersId { get; set; }
        public int MovieId { get; set; }
    }
}
