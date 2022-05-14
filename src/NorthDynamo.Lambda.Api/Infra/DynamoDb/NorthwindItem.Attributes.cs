namespace NorthDynamo.Lambda.Api.Infra.DynamoDb;

public partial class NorthwindItem
{
    public const string TableName = "northwind-data";

    public static class Att
    {
        public const string Pk = "pk";
        public const string Sk = "sk";
        public const string Id = "id";
        public const string Active = "active";
        public const string Stage = "stage";
        public const string Category = "category";
        public const string PreJoined = "pre_joined";
        public const string CreatedAt = "created_at";
        public const string Name = "name";
        public const string Phone = "phone";
        public const string LastName = "last_name";
        public const string CustomerId = "customer_id";
        public const string OrderId = "order_id";
        public const string Address = "address";
        public const string StreetWithNr = "street_with_nr";
        public const string City = "city";
        public const string Region = "region";
        public const string PostalCode = "postal_code";
        public const string Country = "country";
    }

    public static class Pfx
    {
        public const string Customer = "cu";
        public const string Address = "adr";
        public const string Order = "ord";
    }

    public static class Idx
    {
        public const string CategoryByActive = $"{Att.Category}-{Att.Active}-index";
    }
}
