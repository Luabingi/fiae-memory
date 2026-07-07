namespace FiaeMemory.Models;

public class MemoryCard
{
    public int Id { get; set; }
    public int MatchingId { get; set; }
    public string Content { get; set; } = string.Empty;
    public CardType Type { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsFaceUp { get; set; }
    public bool IsMatched { get; set; }
    public bool IsShaking { get; set; }
}

public enum CardType
{
    Question,
    Answer
}
