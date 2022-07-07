using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

var frontCORS = "AllowLocahostHostOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: frontCORS, policy =>
    {
        policy.WithOrigins(builder.Configuration.GetValue<string>("AllowedHosts").Split(";"))//Only the front end address can use it
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

app.UseCors(frontCORS);

app.UseAuthorization();

app.MapControllers();

app.Run();
