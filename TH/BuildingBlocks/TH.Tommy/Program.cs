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
            tommyService.CreateBE("TH.Company", "");
        }
    }
}
