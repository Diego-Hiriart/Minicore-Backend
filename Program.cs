using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

var AllowFrontendHostOrigins = "AllowFrontendHostOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowFrontendHostOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")//Only the front end address can use it //https://minicore-front-hiriart.herokuapp.com/
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

app.UseCors(AllowFrontendHostOrigins);//Use the CORS policy, after UseRouting, before UseAuthentication

app.UseAuthorization();

app.MapControllers();

app.Run();
