using Amazon.DynamoDBv2.DocumentModel;

namespace NorthDynamo.Lambda.Api.Infra.DynamoDb;

public static class QueryFilterExtensions
{
    public static QueryFilter With(this QueryFilter self, Action<QueryFilter> setter)
    {
        setter(self);
        return self;
    }
}
