namespace TH.DeepAIMS.API;

public interface IDeepAIService
{
    public Task<string> GenerateImageAsync(ImageInputModel model);
}