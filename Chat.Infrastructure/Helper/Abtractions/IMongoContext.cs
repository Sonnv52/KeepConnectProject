using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Helper.Abtractions
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }
}
