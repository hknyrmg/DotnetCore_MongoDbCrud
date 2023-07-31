namespace DotNetCore_MongoDb.Models;

public class ChessPieceDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CollectionName { get; set; } = null!;

}
