using ProductsMicroService.API.Middleware;
using ProductsMicroService.BusinessLogicLayer;
using ProductsMicroService.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();
builder.Services.AddControllers();
//builder.Services.AddAutoMapper();

var app = builder.Build();
app.UseExceptionHandlingMiddleware();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
