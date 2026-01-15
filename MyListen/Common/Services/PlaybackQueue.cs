using MyArchitecture;

namespace MyListen.Common.Services;

public sealed class PlaybackQueue 
{
    EnqueueList? songCollection;

    readonly Queue<Guid> playbackQueue = []; 
    readonly Stack<Guid> history = [];
    readonly Random random = new();
    bool isLooped = false;


    Guid _currentSongId;
    public Guid CurrentSongId
    {
        get => _currentSongId;
        set
        {
            _currentSongId = value;
            OnCurrentSongIdChanged(value);
        }
    }

    public void SetPlaybackQueue(EnqueueList songCollection)
    {
        this.songCollection = songCollection;
        FillPlaybackQueue(this.songCollection.Songs);
        history.Clear();
    }

    void FillPlaybackQueue(IReadOnlyList<Guid> songCollection)
    {
        FillFromSongList(songCollection);
        CurrentSongId = playbackQueue.First();
    }

    void FillFromSongList(IReadOnlyList<Guid> songCollection)
    {
        playbackQueue.Clear();
        foreach (Guid id in songCollection)
        {
            playbackQueue.Enqueue(id);
        }
        OnPlaybackQueueChanged([.. playbackQueue]);
    }

    public Result<Guid> MoveTo(Guid id)
    {
        if (!playbackQueue.Contains(id)) return Result<Guid>.Fail("L'identifiant est introuvable dans la playlist.");

        while (playbackQueue.Peek() != id)
        {
            playbackQueue.Dequeue();
        } 
        
        CurrentSongId = playbackQueue.Dequeue();
        return Result<Guid>.Ok(CurrentSongId);
    }

    public Result SetShuffledPlaybackQueue(EnqueueList songCollection, Guid StartedSong)
    {
        if (!songCollection.Songs.Contains(StartedSong)) return Result.Fail("L'identifiant de la musique de départ est introuvable dans la collection.");
        this.songCollection = songCollection;
        history.Clear();
        return ShufflePlayBackQueue(StartedSong);
    }


    public Result ShufflePlayBackQueue(Guid? startedSong = null)
    {
        if (songCollection is null) return Result.Fail("La collection est vide.");

        List<Guid> songIds = [.. songCollection.Songs];
        List<Guid> shuffledList = [];

        if (startedSong is Guid realId)
        {
            songIds.Remove(realId);
            shuffledList.Add(realId);
        }

        while (songIds.Count > 0)
        {
            int randomIndex = random.Next(songIds.Count);
            shuffledList.Add(songIds.ElementAt(randomIndex));
            songIds.RemoveAt(randomIndex);
        }

        FillPlaybackQueue(shuffledList);
        OnIsShuffled(true);
        return Result.Ok();
    }

    public Result OrderPlaybackQueue()
    {
        if (songCollection is null) return Result.Fail("La collection est vide.");
        FillPlaybackQueue(songCollection.Songs);
        OnIsShuffled(false);
        return Result.Ok();
    }

    public Result<Guid> NextSong()
    {
        if (playbackQueue.Count == 0) Result<Guid>.Fail("La file de lecture est vide.");
        if (isLooped) return Result<Guid>.Ok(LoopNext());
        else return Result<Guid>.Ok(OrderNext());
    }

    public Guid LoopNext()
    {
        history.Push(CurrentSongId);
        CurrentSongId = playbackQueue.Dequeue();
        playbackQueue.Enqueue(CurrentSongId);
        return CurrentSongId;
    }

    public Guid OrderNext()
    {
        history.Push(CurrentSongId);
        CurrentSongId = playbackQueue.Dequeue();
        return CurrentSongId;
    }

    public Result<Guid> PreviousSong()
    {
        if (!history.TryPop(out var id)) return Result<Guid>.Fail("Il n'y a pas de musique précédente dans l'historique.");
        CurrentSongId = id;
        return Result<Guid>.Ok(CurrentSongId);
    }

    public void EnableLoop()
    {
        isLooped = true;
        OnIsLoopedStateChanged(true);
    }

    public void DisableLoop()
    {
        isLooped = false;
        OnIsLoopedStateChanged(false);
    }

    public event EventHandler<bool>? IsShuffledStateChanged;
    void OnIsShuffled(bool newState)
    {
        IsShuffledStateChanged?.Invoke(this, newState);
    }

    public event EventHandler<bool>? IsLoopedStateChanged;
    void OnIsLoopedStateChanged(bool newState)
    {
        IsLoopedStateChanged?.Invoke(this, newState);
    }

    public event EventHandler<Guid>? CurrentSongIdChanged;
    void OnCurrentSongIdChanged(Guid newId)
    {
        CurrentSongIdChanged?.Invoke(this, newId);
    }

    public event EventHandler<IReadOnlyList<Guid>>? PlaybackQueueChanged;
    void OnPlaybackQueueChanged(IReadOnlyList<Guid> newQueue)
    {
        PlaybackQueueChanged?.Invoke(this, newQueue);
    }
}

public sealed class EnqueueList
{
    public IReadOnlyList<Guid> Songs { get; }

    private EnqueueList(IReadOnlyList<Guid> songs)
    {
        Songs = songs;
    }

    public static Result<EnqueueList> FromSongs(IReadOnlyList<Guid> songs)
    {
        if (songs.Count == 0) Result<EnqueueList>.Fail("La file de musiques ne peut pas être vide.");
        return Result<EnqueueList>.Ok(new EnqueueList(songs));
    }
}
