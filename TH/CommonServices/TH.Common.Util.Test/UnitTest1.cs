namespace TH.Common.Util.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                var emp= new Employee();
                emp.FirstName = "Tanvir";
                emp.Surname = "Hossain";
                //Util.TryClassPropertyNames(emp);

                Type t = typeof(Employee);

                foreach (var prop in t.GetProperties())
                {
                    var name = prop.Name;
                }

                var name1 = emp.FirstName;
                var name2 = emp.FirstName.ToString();
                var name3 = emp.FirstName.GetType();
            }
            catch (Exception)
            {
                ;
            }
        }
    }

    public class Employee
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }        
    }
}