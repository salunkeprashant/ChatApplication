using ChatApplication.Helper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ChatApplication.Model
{
    [BsonCollection("Users")]
    public class User : Document
    {
        public PersonalInformation PersonalInformation { get; set; }

        public Address Addresses { get; set; }

        public string Username { get; set; }
    }

    public class PersonalInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long ContactNumber { get; set; }
        public string Email { get; set; }
    }

    public class Address
    {
        public string Name { get; set; }

        [BsonElement("Address")]
        public string AddressLine { get; set; }
    }
}
