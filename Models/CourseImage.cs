namespace NETCoreDemo.Models;

public class CourseImage : BaseModel
{
    public string Url { get; set; } = null!;
    public string MimeType { get; set; } = null!;
    public int Size { get; set; }
}