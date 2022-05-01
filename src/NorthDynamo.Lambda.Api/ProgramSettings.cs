using NorthDynamo.Lambda.Api.Infra.DataSource;

namespace NorthDynamo.Lambda.Api;

public class ProgramSettings : INorthwindDaoSettings
{
    public string NorthwindDaoBaseUrl { get; set; } = null!;

    public int NorthwindDaoTimeoutMs { get; set; }

    Uri INorthwindDaoSettings.BaseUrl => new(NorthwindDaoBaseUrl);

    TimeSpan INorthwindDaoSettings.Timeout => TimeSpan.FromMilliseconds(NorthwindDaoTimeoutMs);
}
