using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Diagnostics;
using NorthDynamo.Lambda.Api;
using NorthDynamo.Lambda.Api.Core;
using NorthDynamo.Lambda.Api.Infra.DataSource;
using NorthDynamo.Lambda.Api.Infra.DynamoDb;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.Get<ProgramSettings>();
var container = builder.Services;

container.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
container.AddAWSService<IAmazonDynamoDB>();
container.AddSingleton<IDynamoDBContext>(_ => new DynamoDBContext(_.GetService<IAmazonDynamoDB>()));
container.AddScoped<ICustomerRepository, CustomerRepository>();

container.AddHttpClient<INorthwindDao<CustomerDto>, NorthwindDao>(
    NorthwindDao.Config(settings)
);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseExceptionHandler("/exception");

app.Map("/exception", (HttpContext ctx) => {
    var error = ctx.Features.Get<IExceptionHandlerFeature>()!.Error;
    return Results.Problem(
        title: error.GetType().Name,
        detail: error.Message,
        statusCode: StatusCodes.Status500InternalServerError
    );
});

app.MapPut("/customers/sync", async (INorthwindDao<CustomerDto> dao, ICustomerRepository repo) => {
    var result = await repo.Sync(dao);
    Results.Ok(result);
});

app.MapGet("/customers/{id}", async (ICustomerRepository repo, string id) => {
    var customer = await repo.FindCustomer(id);
    return customer ? Results.Ok(customer) : Results.NotFound();
});

app.Run();
