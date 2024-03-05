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
                //var result1=LangObsolete.GetString("title");
                //LangObsolete.ChangeLanguage("bn-BD");
                //var result2 = LangObsolete.GetString("title");

                //var result3 = LangObsolete.GetString("title3");

                var result1 = Lang.Find("title");
                Lang.SetCultureCode("bn-BD");
                var result2 = Lang.Find("title");

                var result3 = Lang.Find("title3");
            }
            catch (Exception ex)
            {
                ;
            }
        }
    }
}