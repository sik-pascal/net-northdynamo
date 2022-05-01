using NorthDynamo.Lambda.Api.Core;

namespace NorthDynamo.Lambda.Api.Infra.DataSource;

public class NorthwindDao : INorthwindDao<CustomerDto>
{
    private readonly HttpClient _client;

    protected NorthwindDao(HttpClient client) =>
        _client = client;

    public async Task<IEnumerable<CustomerDto>> Fetch() =>
        await _client.GetFromJsonAsync<List<CustomerDto>>("/Customers") ?? Enumerable.Empty<CustomerDto>();

    public static Action<HttpClient> Config(INorthwindDaoSettings settings) => http =>
    {
        http.BaseAddress = settings.BaseUrl;
        http.Timeout = settings.Timeout;
    };
}
