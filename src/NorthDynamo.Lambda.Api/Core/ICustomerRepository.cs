namespace NorthDynamo.Lambda.Api.Core;

public interface ICustomerRepository
{
    Task<dynamic> Sync(INorthwindDao<CustomerDto> dao);

    Task<dynamic> FindCustomer(string customerId);

    IAsyncEnumerable<dynamic> FindCustomers();

    IAsyncEnumerable<dynamic> FindCustomerOrders(string customerId);

    IAsyncEnumerable<dynamic> FindCustomerAddress(string customerId);
}
