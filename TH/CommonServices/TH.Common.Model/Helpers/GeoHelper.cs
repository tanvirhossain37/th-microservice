namespace TH.Common.Model;

public class GeoHelper
{
    private readonly HttpClient _httpClient;
    public GeoHelper()
    {
        _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(5)
        };
    }

    private async Task<string> GetIpAddress()
    {
        var ipAddress = await _httpClient.GetAsync("http://ipinfo.io/ip");
        if (ipAddress.IsSuccessStatusCode)
        {
            var json = await ipAddress.Content.ReadAsStringAsync();
            return json.ToString();
        }

        return string.Empty;
    }

    public async Task<string> GetGeoInfo()
    {
        var ipAddress = await GetIpAddress();
        var response = await _httpClient.GetAsync($"http://api.ipstack.com/" + ipAddress + "?access_key=a772621c35b2d9c0bf42015b813b4907");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return json.ToString();
        }

        return null;
    }
}