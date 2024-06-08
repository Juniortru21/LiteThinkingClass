namespace Infrastructure.CosmosDB;
public class Settings
{
    public required string Uri { get; set; }
    public required string Key { get; set; }
    public required string DatabaseName {  get; set; }
    public required string Container {  get; set; }
}
