namespace TH.Common.Model;

public class Print
{
    public PrintPageSizeEnum PageSize { get; set; }
    public PrintLayoutEnum Layout { get; set; }
    public bool IncludeLogo { get; set; }
    public bool IncludeHeader { get; set; }

    public bool IncludePrintBy { get; set; }
    public bool IncludePageNumber { get; set; }
    public bool IncludeFooter { get; set; }

    public float MarginLeft { get; set; }
    public float MarginRight { get; set; }
    public float MarginTop { get; set; }
    public float MarginBottom { get; set; }

    public Print()
    {
        PageSize = PrintPageSizeEnum.A4;
        Layout = PrintLayoutEnum.Portrait;

        IncludeLogo = true;
        IncludeHeader = true;

        IncludeFooter = true;

        IncludePrintBy = true;
        IncludePageNumber = true;

        MarginLeft = 50;
        MarginRight = 50; //35
        MarginTop = 35; //34
        MarginBottom = 35; //90
    }
}