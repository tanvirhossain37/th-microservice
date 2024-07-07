namespace TH.Tommy;

public class BaseService
{
    public string SourceRoot { get; set; }
    public string TommyRoot { get; set; }
    public string ResultDestRoot { get; set; }
    private string _desktopPath { get; set; }
    private readonly string _title = "Tommy -  V-1";
    protected readonly string Dash = "========================================================================================================================";

    public BaseService()
    {
        var info = new DirectoryInfo(".");

        //desktop
        _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        SourceRoot = $"{_desktopPath}\\Tommy\\Source";
        ResultDestRoot = $"{_desktopPath}\\Tommy\\Result";

        //C:\Users\Tanvir Hossain\Desktop\work\th-microservice\th-microservice\TH\BuildingBlocks\TH.Tommy\bin\Debug\net8.0
        TommyRoot = info.FullName.Replace("\\bin\\Debug\\net8.0", "");

        Console.Title = _title;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{_title,60}");
        Console.ResetColor();
        //Console.Beep();

        Console.WriteLine(Dash);
        Console.WriteLine();

        Console.WriteLine($"Dest Root: {ResultDestRoot}");
        Console.WriteLine();
        Console.WriteLine(Dash);
        Console.WriteLine();

        Util.DeleteDirectory(ResultDestRoot, true);
        Util.CreateDirectory(ResultDestRoot);
    }
}