using DotNetCore_MongoDb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNetCore_MongoDb.Services;

public class PiecesService
{
    private readonly IMongoCollection<ChessPiece> _piecesCollection;

    public PiecesService(
        IOptions<ChessPieceDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(
            options.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            options.Value.DatabaseName);

        _piecesCollection = mongoDatabase.GetCollection<ChessPiece>(
            options.Value.CollectionName);
    }

    public async Task<List<ChessPiece>> GetAsync() =>
        await _piecesCollection.Find(_ => true).ToListAsync();

    public async Task<ChessPiece?> GetAsync(string id) =>
        await _piecesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(ChessPiece newBook) =>
        await _piecesCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, ChessPiece updatedBook) =>
        await _piecesCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _piecesCollection.DeleteOneAsync(x => x.Id == id);
}