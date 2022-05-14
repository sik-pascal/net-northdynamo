using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using NorthDynamo.Lambda.Api.Core;
using static NorthDynamo.Lambda.Api.Infra.DynamoDb.NorthwindItem;

namespace NorthDynamo.Lambda.Api.Infra.DynamoDb;

public class CustomerRepository : ICustomerRepository
{
    private static readonly DynamoDBOperationConfig IgnoreNullConfig = new() { IgnoreNullValues = true };

    private readonly IDynamoDBContext _context;

    public CustomerRepository(IDynamoDBContext context) =>
        _context = context;

    private static async IAsyncEnumerable<dynamic> Resolve<T>(AsyncSearch<T> search)
    {
        while (!search.IsDone)
            yield return await search.GetNextSetAsync() ?? Enumerable.Empty<T>();
    }

    public IAsyncEnumerable<dynamic> FindCustomers() => Resolve(
        _context.FromQueryAsync<NorthwindItem>(new QueryOperationConfig {
            IndexName = Idx.CategoryByActive,
            Filter = new QueryFilter()
                .With(q => q.AddCondition(Att.Category, QueryOperator.Equal, Pfx.Customer))
                .With(q => q.AddCondition(Att.Active, QueryOperator.Equal, true))
        })
    );

    async Task<dynamic> ICustomerRepository.FindCustomer(string customerId) =>
        await _context.LoadAsync<NorthwindItem>(
            GetPk(customerId),
            GetSk(customerId)
        );

    IAsyncEnumerable<dynamic> ICustomerRepository.FindCustomerOrders(string customerId) => Resolve(
        _context.QueryAsync<NorthwindItem>(
            GetPk(customerId), QueryOperator.BeginsWith, Arr(Pfx.Order)
        )
    );

    IAsyncEnumerable<dynamic> ICustomerRepository.FindCustomerAddress(string customerId) => Resolve(
        _context.QueryAsync<NorthwindItem>(
            GetPk(customerId), QueryOperator.BeginsWith, Arr(Pfx.Address)
        )
    );

    public async Task<dynamic> Sync(INorthwindDao<CustomerDto> dao)
    {
        var customers = await dao.Fetch();
        var items = customers
            .Where(cu => !string.IsNullOrEmpty(cu.CustomerId))
            .Select((cu, i) => new NorthwindItem {
                Pk = GetPk(cu.CustomerId!),
                Sk = GetSk(cu.CustomerId!),
                Id = cu.CustomerId,
                CustomerId = cu.CustomerId,
                Name = cu.ContactName,
                Phone = cu.Phone,
                Address = new AddressItem {
                    Id = GetUuid(),
                    StreetWithNr = cu.Address,
                    PostalCode = cu.PostalCode,
                    City = cu.City,
                    Region = cu.Region,
                    Country = cu.Country,
                },
                Active = cu.City switch {
                    "Berlin" => false,
                    "London" => null,
                    _ => true
                }, //test data
                Closed = i % 2 == 0,
                Category = Pfx.Customer,
                CreatedAt = GetTimestamp(),
            });

        var batch = _context.CreateBatchWrite<NorthwindItem>(IgnoreNullConfig);
        batch.AddPutItems(items);
        await batch.ExecuteAsync();

        return items.Count();
    }
}
