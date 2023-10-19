var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string _policyName = "CorsPolicy";
string _anotherPolicy = "AnotherCorsPolicy";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: _policyName, builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
    opt.AddPolicy(name: _anotherPolicy, builder =>
    {
        builder.WithOrigins("https://localhost:7056")
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(_policyName);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
