using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TH.CompanyMS.App;

public static class Extensions
{
    public static T To<T>(this object input)
    {
        if (input is null)
        {
            return default;
        }

        return (T)Convert.ChangeType(input, typeof(T));
    }

    public static string Serialize(this object input)
    {
        if (input is null)
        {
            return "";
        }

        return JsonConvert.SerializeObject(input);
    }

    public static T Deserialize<T>(this string input)
    {
        if (input is null)
        {
            return default;
        }

        return (T)JsonConvert.DeserializeObject<T>(input);
    }
}