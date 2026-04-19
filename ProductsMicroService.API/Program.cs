using ProductsMicroService.API.ApiEndpoints;
using ProductsMicroService.API.Middleware;
using ProductsMicroService.BusinessLogicLayer;
using ProductsMicroService.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();
builder.Services.AddControllers();
// Endpoints API explorer is required for Swagger to discover the API endpoints and generate documentation.
// It provides metadata about the API routes, parameters, and responses, enabling Swagger to create an interactive API documentation interface.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure JSON serialization to use string representation for enums
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

var app = builder.Build();
app.UseExceptionHandlingMiddleware();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductApiEndpoints();

app.Run();
