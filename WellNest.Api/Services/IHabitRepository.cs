using WellNest.Api.Domain;

namespace WellNest.Api.Services;

public interface IHabitRepository
{
    Task<Habit?> GetAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Habit>> ListAsync(bool includeArchived, CancellationToken ct);
    Task<Habit> AddAsync(Habit habit, CancellationToken ct);
    Task<bool> UpdateAsync(Habit habit, CancellationToken ct);
    Task<bool> ArchiveAsync(Guid id, CancellationToken ct);
}
