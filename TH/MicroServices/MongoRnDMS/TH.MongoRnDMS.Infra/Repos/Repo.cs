using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.App;

namespace TH.MongoRnDMS.Infra
{
    public class Repo<T> : IRepo<T> where T : class
    {
    }
}