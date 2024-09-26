namespace TH.Io;

public class CustomFile
{
    public string Name { get; set; }
    public string NewName { get; set; }
    public long Size { get; set; }
    public long DocTypeId { get; set; }
    public string Type { get; set; }
    public string FileData { get; set; }
    public DateTime? LastModifiedDate { get; set; }

    public IList<string> Tags { get; set; }

    //addi
    public string Id { get; set; }
    public string Note { get; set; }
    public long? Version { get; set; }
    public DateTime? CreatedDate { get; set; }
    public bool IsFolder { get; set; }
    public string FileTypeName { get; set; }

    public IList<string> Parents { get; set; }

    public CustomFile()
    {
        Tags = new List<string>();
        Parents = new List<string>();
    }
}