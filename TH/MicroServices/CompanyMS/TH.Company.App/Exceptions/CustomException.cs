using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.CompanyMS.App
{
    public class CustomException(string message) : Exception(message);
}