using Chat.Infrastructure.Helper.Abtractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Helper.ModelSection
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string? CollectionName { get; set; }
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}
