using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

var devCORS = "AllowLocahostHostOrigins";

var prodCORS = "AllowFrontendHostOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: devCORS,policy =>
    {
        policy.WithOrigins(builder.Configuration.GetValue<string>("AllowedHosts:Dev"))//Only the local front end address can use it http://localhost:3000
        .WithHeaders(HeaderNames.ContentType)//Allow content type (to use jsons)
        .WithMethods("POST", "GET", "PUT", "DELETE");//Allow all methods

    });
    options.AddPolicy(name: prodCORS, policy =>
    {
        policy.WithOrigins(builder.Configuration.GetValue<string>("AllowedHosts:Prod"))//Only the deployment frontend address can use it https://minicore-front-hiriart.herokuapp.com
        .WithHeaders(HeaderNames.ContentType)//Allow content type (to use jsons)
        .WithMethods("POST", "GET", "PUT", "DELETE");//Allow all methods
    });
});

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

if (app.Environment.IsProduction())//Use the appropriate CORS policy, after UseRouting, before UseAuthentication
{
    app.UseCors(prodCORS);
}
else if (app.Environment.IsDevelopment())
{
    app.UseCors(devCORS);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
