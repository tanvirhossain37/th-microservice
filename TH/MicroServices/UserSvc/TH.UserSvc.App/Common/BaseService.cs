using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.UserSvc.App
{
    public abstract class BaseService : IBaseService
    {
        public abstract void Dispose();
    }
}