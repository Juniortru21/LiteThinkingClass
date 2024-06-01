using System.ComponentModel;
using Azure.Storage.Blobs;

namespace Application.Common.Utils;
public class Document<T> where T : notnull
{
    public static FileStream ImportCsv(string routeFrom)
    {
        FileInfo file = new(routeFrom);
        string uploadfile = file.FullName;
        var fileStream = new FileStream(uploadfile, FileMode.Open);

        return fileStream;
    }

    public static async Task SaveToCsv(IEnumerable<T> reportData, string path)
    {
        var lines = new List<string>();
        IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
        var header = string.Join(",", props.ToList().Select(x => x.Name));
        lines.Add(header);
        var valueLines = reportData
                        .Select(row => string.Join(",", header.Split(',')
                                                   .Select(a => row?.GetType()?.GetProperty(a)?.GetValue(row, null))));
        lines.AddRange(valueLines);
        await File.WriteAllLinesAsync(path, [.. lines]);
    }
}
