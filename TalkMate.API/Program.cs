using TalkMate.Application;
using TalkMate.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ---------------------
// اضافه کردن سرویس‌ها
// ---------------------
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------
// اضافه کردن CORS
// ---------------------
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

await app.Services.InitialiseDatabaseAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();   // ← این بخش نبود، مشکل همین است

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
