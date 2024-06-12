using Microsoft.VisualBasic.ApplicationServices;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCornerWPF.Lib.MongoDB.Models
{
    public class UsersModel
    {
        public BsonObjectId _id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
