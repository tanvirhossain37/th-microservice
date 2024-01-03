using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.App;

namespace TH.MongoRnDMS.Infra
{
    public class UoW : IUoW
    {
        private readonly MongoRnDDbContext _dbContext;
        public IGardenRepo GardenRepo { get; set; }
        public ITreeRepo TreeRepo { get; set; }
        public IFruitRepo FruitRepo { get; set; }

        public UoW(MongoRnDDbContext dbContext, IGardenRepo gardenRepo, ITreeRepo treeRepo, IFruitRepo fruitRepo)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            GardenRepo = gardenRepo ?? throw new ArgumentNullException(nameof(gardenRepo));
            TreeRepo = treeRepo ?? throw new ArgumentNullException(nameof(treeRepo));
            FruitRepo = fruitRepo ?? throw new ArgumentNullException(nameof(fruitRepo));
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}