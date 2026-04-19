using ProductsMicroService.API.ApiEndpoints;
using ProductsMicroService.API.Middleware;
using ProductsMicroService.BusinessLogicLayer;
using ProductsMicroService.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();
builder.Services.AddControllers();
//builder.Services.AddAutoMapper();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductApiEndpoints();

app.Run();
