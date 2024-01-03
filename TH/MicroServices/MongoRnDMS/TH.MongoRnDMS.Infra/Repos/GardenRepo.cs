using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.App;
using TH.MongoRnDMS.Core;

namespace TH.MongoRnDMS.Infra
{
    public class GardenRepo : Repo<Garden>, IGardenRepo
    {
        public GardenRepo(MongoRnDDbContext dbContext) : base(dbContext)
        {
        }
    }
}