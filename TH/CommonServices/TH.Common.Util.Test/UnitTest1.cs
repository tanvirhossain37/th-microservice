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
                var emp = new Employee();
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

        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                var email1 = Util.TryIsValidEmail("");
                var email2 = Util.TryIsValidEmail(null);
                var email3 = Util.TryIsValidEmail("   tanvir@gmail.com  ");
                var email4 = Util.TryIsValidEmail("Tanvir@gmail.com");
                var email5 = Util.TryIsValidEmail("tanvir@gmail.co");
                var email6 = Util.TryIsValidEmail("tanvir@gmail.co.co");
                var email7 = Util.TryIsValidEmail("tanvirgmail");
            }
            catch (Exception e)
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