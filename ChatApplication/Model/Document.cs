using ChatApplication.Contracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApplication.Model
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
    }
}
