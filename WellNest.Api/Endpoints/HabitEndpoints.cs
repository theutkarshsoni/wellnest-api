using Microsoft.AspNetCore.Mvc;
using WellNest.Api.Contracts;
using WellNest.Api.Domain;
using WellNest.Api.Services;

namespace WellNest.Api.Endpoints;

public static class HabitEndpoints
{
    public static IEndpointRouteBuilder MapHabitEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/habits")
                          .WithTags("Habits");

        // GET /habits?includeArchived=false
        group.MapGet("/", async ([FromServices] IHabitRepository repo, [FromQuery] bool includeArchived, CancellationToken ct) =>
        {
            var items = await repo.ListAsync(includeArchived, ct);
            var response = items.Select(ToResponse);
            return Results.Ok(response);
        });

        // GET /habits/{id}
        group.MapGet("/{id:guid}", async ([FromServices] IHabitRepository repo, Guid id, CancellationToken ct) =>
        {
            var habit = await repo.GetAsync(id, ct);
            return habit is null ? Results.NotFound() : Results.Ok(ToResponse(habit));
        });

        // POST /habits
        group.MapPost("/", async ([FromServices] IHabitRepository repo, [FromBody] CreateHabitRequest req, CancellationToken ct) =>
        {
            try
            {
                var habit = new Habit(req.Name, req.Description, req.Frequency);
                var created = await repo.AddAsync(habit, ct);
                return Results.Created($"/habits/{created.Id}", ToResponse(created));
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        // PUT /habits/{id}
        group.MapPut("/{id:guid}", async ([FromServices] IHabitRepository repo, Guid id, [FromBody] UpdateHabitRequest req, CancellationToken ct) =>
        {
            var existing = await repo.GetAsync(id, ct);
            if (existing is null) return Results.NotFound();

            try
            {
                existing.Update(req.Name, req.Description, req.Frequency);
                var ok = await repo.UpdateAsync(existing, ct);
                return ok ? Results.Ok(ToResponse(existing)) : Results.NotFound();
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        // DELETE /habits/{id}  (archive semantics)
        group.MapDelete("/{id:guid}", async ([FromServices] IHabitRepository repo, Guid id, CancellationToken ct) =>
        {
            var ok = await repo.ArchiveAsync(id, ct);
            return ok ? Results.NoContent() : Results.NotFound();
        });

        return routes;
    }

    private static HabitResponse ToResponse(Habit h) =>
        new(h.Id, h.Name, h.Description, h.Frequency, h.CreatedAtUtc, h.IsArchived);
}
