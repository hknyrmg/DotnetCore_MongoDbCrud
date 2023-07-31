

using DotNetCore_MongoDb.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetCore_MongoDb.Models
{
    public class ChessPiece
    {
        [BsonId] //to make this property the document's primary key.
        [BsonRepresentation(BsonType.ObjectId)] //  to allow passing the parameter as type string instead of an ObjectId structure. Mongo handles the conversion from string to ObjectId
        public string? Id { get; set; }
         
        [BsonElement("Name")] // The attribute's value of Name represents the property name in the MongoDB collection.
        public string PieceName { get; set; } = null!;

        public decimal Value { get; set; }

        public MoveType MoveType { get; set; }

    }
}
