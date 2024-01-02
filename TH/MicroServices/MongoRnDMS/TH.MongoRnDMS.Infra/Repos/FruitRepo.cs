using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.App;
using TH.MongoRnDMS.Core;

namespace TH.MongoRnDMS.Infra
{
    public class FruitRepo : Repo<Fruit>, IFruitRepo
    {
    }
}