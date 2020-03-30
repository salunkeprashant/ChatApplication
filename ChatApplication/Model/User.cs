using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ChatApplication.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty]
        public string PersonalInformation { get; set; }

        public Address Addresses { get; set; }

        public string Username { get; set; }
    }

    public class Address
    {
        public string Name { get; set; }

        [BsonElement("Address")]
        public string AddressLine { get; set; }
    }
}
