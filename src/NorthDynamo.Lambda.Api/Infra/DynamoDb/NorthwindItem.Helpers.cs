namespace NorthDynamo.Lambda.Api.Infra.DynamoDb;

public partial class NorthwindItem
{
    public static T[] Arr<T>(params T[] items) => items;
    public static string GetUuid() => Guid.NewGuid().ToString("D");
    public static long GetTimestamp() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public static string GetPk(string customerId) => $"{Pfx.Customer}#{customerId}";
    public static string GetSk(string customerId) => $"{Pfx.Customer}#{customerId}";
    public static string GetAddressSk(string addressId) => $"{Pfx.Address}#{addressId}";
}
