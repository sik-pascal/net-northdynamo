namespace NorthDynamo.Lambda.Api.Infra.DataSource;

public interface INorthwindDaoSettings
{
    Uri BaseUrl { get; }

    TimeSpan Timeout { get; }
}
