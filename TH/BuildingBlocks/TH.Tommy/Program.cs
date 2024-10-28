namespace TH.Tommy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Todo();
        }

        private static void Todo()
        {
            TommyService tommyService = new TommyService();
            //Company
            ////tommyService.CreateBE("TH.CompanyMS","C:\\Users\\Tanvir Hossain\\Desktop\\work\\th-microservice\\th-microservice\\TH\\MicroServices\\CompanyMS\\TH.Company.App\\Models");
            ////tommyService.CreateGateway("5002", "C:\\Users\\Tanvir Hossain\\Desktop\\work\\th-microservice\\th-microservice\\TH\\MicroServices\\CompanyMS\\TH.Company.API");
            //tommyService.CreateFE("TH.CompanyMS", "C:\\Users\\Tanvir Hossain\\Desktop\\work\\th-microservice\\th-microservice\\TH\\MicroServices\\CompanyMS\\TH.Company.App\\Models","C:\\Users\\Tanvir Hossain\\Desktop\\work\\th-microservice\\th-microservice\\TH\\MicroServices\\CompanyMS\\TH.Company.API\\Controllers" );

            //Address
            tommyService.CreateBE("TH.AddressMS", "C:\\Users\\Tanvir Hossain\\Desktop\\work\\th-microservice\\th-microservice\\TH\\MicroServices\\AddressMS\\TH.AddressMS.App\\Models");
            //tommyService.CreateGateway("5003", "C:\\Users\\Tanvir Hossain\\Desktop\\work\\th-microservice\\th-microservice\\TH\\MicroServices\\AddressMS\\TH.AddressMS.App");
            //tommyService.CreateFE("TH.AddressMS", "C:\Users\Tanvir Hossain\Desktop\work\th-microservice\th-microservice\TH\MicroServices\AddressMS\TH.AddressMS.App\Models", "C:\Users\Tanvir Hossain\Desktop\work\th-microservice\th-microservice\TH\MicroServices\AddressMS\TH.AddressMS.API\Controllers");
        }
    }
}
