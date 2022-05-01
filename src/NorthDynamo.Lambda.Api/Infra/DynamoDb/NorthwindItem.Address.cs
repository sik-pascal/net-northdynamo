using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.DataModel;

namespace NorthDynamo.Lambda.Api.Infra.DynamoDb;

public partial class NorthwindItem
{
    public class AddressItem
    {
        [DynamoDBProperty(Att.Id)]
        public string? Id { get; set; }

        [DynamoDBProperty(Att.StreetWithNr)]
        public string? StreetWithNr { get; set; }

        [DynamoDBProperty(Att.City)]
        public string? City { get; set; }

        [DynamoDBProperty(Att.Region)]
        public string? Region { get; set; }

        [DynamoDBProperty(Att.PostalCode)]
        public string? PostalCode { get; set; }

        [DynamoDBProperty(Att.Country)]
        public string? Country { get; set; }
    }
}
