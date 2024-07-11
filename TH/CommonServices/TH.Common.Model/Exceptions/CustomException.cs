using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Common.Model
{
    public class CustomException(string message) : Exception(message);
}