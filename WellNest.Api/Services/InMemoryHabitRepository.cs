using System.Collections.Concurrent;
using WellNest.Api.Domain;

namespace WellNest.Api.Services;

public sealed class InMemoryHabitRepository : IHabitRepository
{
    private readonly ConcurrentDictionary<Guid, Habit> _store = new();

    public Task<Habit?> GetAsync(Guid id, CancellationToken ct)
        => Task.FromResult(_store.TryGetValue(id, out var h) ? h : null);

    public Task<IReadOnlyList<Habit>> ListAsync(bool includeArchived, CancellationToken ct)
    {
        var list = includeArchived
            ? _store.Values.ToList()
            : _store.Values.Where(h => !h.IsArchived).ToList();
        return Task.FromResult((IReadOnlyList<Habit>)list);
    }

    public Task<Habit> AddAsync(Habit habit, CancellationToken ct)
    {
        _store[habit.Id] = habit;
        return Task.FromResult(habit);
    }

    public Task<bool> UpdateAsync(Habit habit, CancellationToken ct)
    {
        if (!_store.ContainsKey(habit.Id)) return Task.FromResult(false);
        _store[habit.Id] = habit;
        return Task.FromResult(true);
    }

    public Task<bool> ArchiveAsync(Guid id, CancellationToken ct)
    {
        if (!_store.TryGetValue(id, out var existing)) return Task.FromResult(false);
        existing.Archive();
        _store[id] = existing;
        return Task.FromResult(true);
    }
}
