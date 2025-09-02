namespace WellNest.Api.Contracts;

public sealed record CreateHabitRequest(string Name, string? Description, string Frequency = "daily");
public sealed record UpdateHabitRequest(string Name, string? Description, string Frequency = "daily");

public sealed record HabitResponse(
    Guid Id,
    string Name,
    string? Description,
    string Frequency,
    DateTime CreatedAtUtc,
    bool IsArchived
);
