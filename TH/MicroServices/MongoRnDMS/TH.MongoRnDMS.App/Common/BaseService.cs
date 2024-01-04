using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.MongoRnDMS.App
{
    public abstract class BaseService : IBaseService
    {
        protected readonly IUoW UoW;

        protected BaseService(IUoW uoW)
        {
            UoW = uoW;
        }

        public abstract void Dispose();
    }
}