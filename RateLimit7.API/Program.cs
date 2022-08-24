using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


// ? ConcurrencyLimiterOptions

app.UseRateLimiter(new RateLimiterOptions
{
    GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        if (context.Request.Path == "/noRateLimiter")
            return RateLimitPartition.CreateNoLimiter<string>("UnlimitedRequests"); //Rate Limit'ten etkilenmeyecek


        return RateLimitPartition.CreateConcurrencyLimiter<string>("GeneralLimit",
            _ => new ConcurrencyLimiterOptions(1, QueueProcessingOrder.NewestFirst, 10));
    }),
    RejectionStatusCode = 429 //RateLimit aşıldığında dönecek statusCode u belirttik.
});
// ? ConcurrencyLimiterOptions

//? TokenLimiterOptions

app.UseRateLimiter(new RateLimiterOptions
{
    GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        return RateLimitPartition.CreateTokenBucketLimiter<string>("TokenBucketLimit",
            _ => new TokenBucketRateLimiterOptions(10, QueueProcessingOrder.NewestFirst, 0, TimeSpan.FromSeconds(10),
                10));
    }),
    RejectionStatusCode = 429  
});
// ? TokenLimiterOptions


app.UseAuthorization();

app.MapControllers();

app.Run();