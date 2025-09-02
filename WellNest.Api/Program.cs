using WellNest.Api.Endpoints;
using WellNest.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHabitRepository, InMemoryHabitRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapGet("/healthcheck", () =>
    Results.Ok(new { status = "ok", service = "WellNest.Api" })
);

app.MapHabitEndpoints();

app.Run();