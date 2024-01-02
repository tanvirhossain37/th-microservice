using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.MongoRnDMS.Core
{
    public class BaseEntity
    {
        [BsonId]
        public string Id { get; set; } = null!;
    }
}
