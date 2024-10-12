namespace TH.DeepAIMS.API;

public class DeepAiService : IDeepAIService
{
    public async Task<string> GenerateImageAsync(ImageInputModel model)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Api-Key", "7cc5129a-c960-43e2-bb61-95e78a5767f4");

        var formData = new MultipartFormDataContent();
        formData.Add(new ByteArrayContent(File.ReadAllBytes(model.Image)), "image");

        var response = await client.PostAsync("https://api.deepai.org/api/deepdream", formData);//waifu2x  anime-generator
        var result = await response.Content.ReadAsStringAsync();

        return result;
    }
}