using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.MongoRnDMS.App
{
    public interface IUoW : IDisposable
    {
        IGardenRepo GardenRepo { get; set; }
        ITreeRepo TreeRepo { get; set; }
        IFruitRepo FruitRepo { get; set; }

        int Commit();
    }
}