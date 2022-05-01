using Amazon.DynamoDBv2.DataModel;

namespace NorthDynamo.Lambda.Api.Infra.DynamoDb;

[DynamoDBTable(TableName)]
public partial class NorthwindItem
{
    [DynamoDBHashKey(Att.Pk)]
    public string Pk { get; set; } = null!;

    [DynamoDBRangeKey(Att.Sk)]
    public string Sk { get; set; } = null!;

    [DynamoDBProperty(Att.Id)]
    public string? Id { get; set; }

    [DynamoDBProperty(Att.Active)]
    public bool? Active { get; set; }

    [DynamoDBProperty("closed")]
    public bool Closed { get; set; }

    [DynamoDBProperty(Att.Stage)]
    public string? Stage { get; set; }

    [DynamoDBProperty(Att.Category)]
    public string? Category { get; set; }

    [DynamoDBProperty(Att.CreatedAt)]
    public long CreatedAt { get; set; }

    [DynamoDBProperty(Att.CustomerId)]
    public string? CustomerId { get; set; }

    [DynamoDBProperty(Att.OrderId)]
    public string? OrderId { get; set; }

    [DynamoDBProperty(Att.Name)]
    public string? Name { get; set; }

    [DynamoDBProperty(Att.Phone)]
    public string? Phone { get; set; }

    [DynamoDBProperty(Att.Address)]
    public AddressItem? Address { get; set; }
}
