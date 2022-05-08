using Domain.DataAccessContracts;
using EfcData;

using (UserContext ctx = new()) { ctx.Seed(); }

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPostDao, PostSqliteDAO>();
builder.Services.AddScoped<IUserDao, UserSqliteDAO>();
builder.Services.AddScoped<PostContext>();
builder.Services.AddScoped<UserContext>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();