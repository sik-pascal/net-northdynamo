namespace NorthDynamo.Lambda.Api.Core;

public interface INorthwindDao<TDto> where TDto : NorthwindDto
{
    Task<IEnumerable<TDto>> Fetch();
}
