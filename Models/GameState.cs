namespace FiaeMemory.Models;

public enum GamePhase
{
    NotStarted,
    Playing,
    Paused,
    Finished
}

public class GameState
{
    public GamePhase Phase { get; set; } = GamePhase.NotStarted;
    public List<MemoryCard> Cards { get; set; } = new();
    public MemoryCard? FirstSelected { get; set; }
    public MemoryCard? SecondSelected { get; set; }
    public int Moves { get; set; }
    public int MatchedPairs { get; set; }
    public int TotalPairs { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    public bool IsChecking { get; set; }

    public bool IsComplete => MatchedPairs == TotalPairs && TotalPairs > 0;
}
