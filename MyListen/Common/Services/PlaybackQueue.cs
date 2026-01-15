using MyArchitecture;

namespace MyListen.Common.Services;

public sealed class PlaybackQueue 
{
    EnqueueList? musicCollection;

    readonly Queue<Guid> playbackQueue = []; 
    readonly Stack<Guid> history = [];
    readonly Random random = new();
    bool isLooped = false;


    Guid _currentMusicId;
    public Guid currentMusicId
    {
        get => _currentMusicId;
        set
        {
            _currentMusicId = value;
            OnCurrentMusicIdChanged(value);
        }
    }

    public void SetPlaybackQueue(EnqueueList musicCollection)
    {
        this.musicCollection = musicCollection;
        FillPlaybackQueue(this.musicCollection.Songs);
        history.Clear();
    }

    void FillPlaybackQueue(IReadOnlyList<Guid> musicCollection)
    {
        FillFromSongList(musicCollection);
        currentMusicId = playbackQueue.First();
    }

    void FillFromSongList(IReadOnlyList<Guid> musicCollection)
    {
        playbackQueue.Clear();
        foreach (Guid id in musicCollection)
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
        
        currentMusicId = playbackQueue.Dequeue();
        return Result<Guid>.Ok(currentMusicId);
    }

    public Result SetShuffledPlaybackQueue(EnqueueList musicCollection, Guid StartedSong)
    {
        if (!musicCollection.Songs.Contains(StartedSong)) return Result.Fail("L'identifiant de la musique de départ est introuvable dans la collection.");
        this.musicCollection = musicCollection;
        history.Clear();
        return ShufflePlayBackQueue(StartedSong);
    }


    public Result ShufflePlayBackQueue(Guid? startedMusic = null)
    {
        if (musicCollection is null) return Result.Fail("La collection est vide.");

        List<Guid> musicIds = [.. musicCollection.Songs];
        List<Guid> shuffledList = [];

        if (startedMusic is Guid realId)
        {
            musicIds.Remove(realId);
            shuffledList.Add(realId);
        }

        while (musicIds.Count > 0)
        {
            int randomIndex = random.Next(musicIds.Count);
            shuffledList.Add(musicIds.ElementAt(randomIndex));
            musicIds.RemoveAt(randomIndex);
        }

        FillPlaybackQueue(shuffledList);
        OnIsShuffled(true);
        return Result.Ok();
    }

    public Result OrderPlaybackQueue()
    {
        if (musicCollection is null) return Result.Fail("La collection est vide.");
        FillPlaybackQueue(musicCollection.Songs);
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
        history.Push(currentMusicId);
        currentMusicId = playbackQueue.Dequeue();
        playbackQueue.Enqueue(currentMusicId);
        return currentMusicId;
    }

    public Guid OrderNext()
    {
        history.Push(currentMusicId);
        currentMusicId = playbackQueue.Dequeue();
        return currentMusicId;
    }

    public Result<Guid> PreviousSong()
    {
        if (!history.TryPop(out var id)) return Result<Guid>.Fail("Il n'y a pas de musique précédente dans l'historique.");
        currentMusicId = id;
        return Result<Guid>.Ok(currentMusicId);
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

    public event EventHandler<Guid>? CurrentMusicIdChanged;
    void OnCurrentMusicIdChanged(Guid newId)
    {
        CurrentMusicIdChanged?.Invoke(this, newId);
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
