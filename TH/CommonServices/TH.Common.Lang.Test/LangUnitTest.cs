namespace TH.Common.Lang.Test
{
    [TestClass]
    public class LangUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                var result1=Lang.GetString("title");
                Lang.ChangeLanguage("bn-BD");
                var result2 = Lang.GetString("title");
            }
            catch (Exception ex)
            {
                ;
            }
        }
    }
}