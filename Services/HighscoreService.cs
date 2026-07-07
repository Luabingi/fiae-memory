using FiaeMemory.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace FiaeMemory.Services;

public class HighscoreService
{
    private readonly IJSRuntime _js;
    private const string StorageKey = "fiae_memory_highscores";
    private List<HighscoreEntry> _scores = new();

    public HighscoreService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task LoadAsync()
    {
        try
        {
            var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
            if (!string.IsNullOrEmpty(json))
                _scores = JsonSerializer.Deserialize<List<HighscoreEntry>>(json) ?? new();
        }
        catch
        {
            _scores = new();
        }
    }

    public async Task AddScoreAsync(HighscoreEntry entry)
    {
        _scores.Add(entry);
        _scores = _scores.OrderByDescending(s => s.Score).Take(50).ToList();
        await SaveAsync();
    }

    public List<HighscoreEntry> GetTopScores(string? category = null, int count = 10)
    {
        var query = category == null || category == "Alle"
            ? _scores
            : _scores.Where(s => s.Category == category);
        return query.OrderByDescending(s => s.Score).Take(count).ToList();
    }

    public async Task ClearAsync()
    {
        _scores.Clear();
        await SaveAsync();
    }

    private async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(_scores);
        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }
}
