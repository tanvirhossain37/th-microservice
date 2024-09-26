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
            tommyService.CreateBE("TH.CompanyMS","C:\\Users\\Tanvir Hossain\\Desktop\\work\\th-microservice\\th-microservice\\TH\\MicroServices\\CompanyMS\\TH.Company.App\\Models");
            tommyService.CreateGateway("5002", "C:\\Users\\Tanvir Hossain\\Desktop\\work\\th-microservice\\th-microservice\\TH\\MicroServices\\CompanyMS\\TH.Company.API");
            tommyService.CreateFE("TH.CompanyMS", "C:\\Users\\Tanvir Hossain\\Desktop\\work\\th-microservice\\th-microservice\\TH\\MicroServices\\CompanyMS\\TH.Company.App\\Models","C:\\Users\\Tanvir Hossain\\Desktop\\work\\th-microservice\\th-microservice\\TH\\MicroServices\\CompanyMS\\TH.Company.API\\Controllers" );
        }
    }
}
