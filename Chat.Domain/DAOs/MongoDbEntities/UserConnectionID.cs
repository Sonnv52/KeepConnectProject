using Chat.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.DAOs.MongoDbEntities
{
    public class UserConnectionID : BaseEntity
    {
        [BsonId]
        public string? Id { get; set; }

        [BsonElement("userid")]
        [Required]
        public string? UserId { get; set; }

        [BsonElement("connectionhubid")]
        public string? ConnectionHubId { get; set; }
    }
}
