using EmptyAspMvcAuth.Services;

var builder = WebApplication.CreateBuilder(args);

//services
builder.Services.AddControllers();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<DataBaseService>();

var app = builder.Build();

app.MapControllers();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Do you need it?
app.MapGet("/", () => "hello wolrd");

app.Run();
