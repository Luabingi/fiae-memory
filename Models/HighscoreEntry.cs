namespace FiaeMemory.Models;

public class HighscoreEntry
{
    public string PlayerName { get; set; } = string.Empty;
    public int Moves { get; set; }
    public TimeSpan Duration { get; set; }
    public int CardCount { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime PlayedAt { get; set; } = DateTime.Now;

    public int Score => CalculateScore();

    private int CalculateScore()
    {
        if (Moves == 0) return 0;
        var timeBonus = Math.Max(0, 3000 - (int)Duration.TotalSeconds * 10);
        var moveBonus = Math.Max(0, 2000 - Moves * 20);
        return timeBonus + moveBonus + CardCount * 50;
    }
}
