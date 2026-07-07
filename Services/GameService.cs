using FiaeMemory.Models;

namespace FiaeMemory.Services;

public class GameService
{
    private readonly CardRepository _repo;
    public GameState State { get; private set; } = new();
    public event Action? OnStateChanged;

    public GameService(CardRepository repo)
    {
        _repo = repo;
    }

    public void StartGame(string category, int pairCount)
    {
        var pairs = _repo.GetPairs(category, pairCount);
        var allCards = new List<MemoryCard>();

        foreach (var (q, a) in pairs)
        {
            allCards.Add(q);
            allCards.Add(a);
        }

        State = new GameState
        {
            Phase = GamePhase.Playing,
            Cards = allCards.OrderBy(_ => Random.Shared.Next()).ToList(),
            TotalPairs = pairs.Count,
            StartTime = DateTime.Now
        };

        Notify();
    }

    public async Task SelectCard(MemoryCard card)
    {
        if (State.IsChecking) return;
        if (card.IsMatched || card.IsFaceUp) return;
        if (State.Phase != GamePhase.Playing) return;

        card.IsFaceUp = true;

        if (State.FirstSelected == null)
        {
            State.FirstSelected = card;
            Notify();
            return;
        }

        State.SecondSelected = card;
        State.Moves++;
        State.IsChecking = true;
        Notify();

        await Task.Delay(900);

        if (State.FirstSelected.MatchingId == State.SecondSelected.Id)
        {
            State.FirstSelected.IsMatched = true;
            State.SecondSelected.IsMatched = true;
            State.MatchedPairs++;
        }
        else
        {
            State.FirstSelected.IsShaking = true;
            State.SecondSelected.IsShaking = true;
            Notify();
            await Task.Delay(500);
            State.FirstSelected.IsFaceUp = false;
            State.SecondSelected.IsFaceUp = false;
            State.FirstSelected.IsShaking = false;
            State.SecondSelected.IsShaking = false;
        }

        State.FirstSelected = null;
        State.SecondSelected = null;
        State.IsChecking = false;

        if (State.IsComplete)
        {
            State.Phase = GamePhase.Finished;
            State.ElapsedTime = DateTime.Now - State.StartTime;
        }

        Notify();
    }

    public void UpdateTimer()
    {
        if (State.Phase == GamePhase.Playing)
        {
            State.ElapsedTime = DateTime.Now - State.StartTime;
            Notify();
        }
    }

    private void Notify() => OnStateChanged?.Invoke();
}
