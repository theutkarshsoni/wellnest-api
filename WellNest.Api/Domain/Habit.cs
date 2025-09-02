namespace WellNest.Api.Domain;

public sealed class Habit
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public string Frequency { get; private set; } = "daily";
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    public bool IsArchived { get; private set; }

    private Habit() { } // for serializers/ORM

    public Habit(string name, string? description, string frequency)
    {
        Update(name, description, frequency);
    }

    public void Update(string name, string? description, string frequency)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        if (frequency is not ("daily" or "weekly")) throw new ArgumentException("Frequency must be 'daily' or 'weekly'");
        Name = name.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        Frequency = frequency;
    }

    public void Archive() => IsArchived = true;
}
