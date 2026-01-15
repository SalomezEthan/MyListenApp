using MyArchitecture.PresenterLayer;
using MyListen.Common.DataTransfertObjects;
using MyListen.SongList.UseCases;
using MyListenApp.ViewModels.Song;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyListenApp.ViewModels.SongList
{
    internal sealed partial class SongListViewModel : BaseViewModel
    {
        public Guid Id { get; }

        string _name;
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        int _count;
        public int Count
        {
            get => _count;
            set => SetValue(ref _count, value);
        }

        ObservableCollection<SongViewModel> _songs = [];
        public ObservableCollection<SongViewModel> Songs
        {
            get => _songs;
            set => SetValue(ref _songs, value);
        }

        readonly CollectSongs collectSongs;
        readonly PlaySongList playSongList;
        readonly RenameSongList renameSongList;

        internal SongListViewModel(SongListInfos infos, CollectSongs collectSongs, PlaySongList playSongList, RenameSongList renameSongList, SongViewModelMap songViewModelMap)
        {
            this.Id = infos.Id;
            this._name = infos.Name;
            this._count = infos.Count;

            this.collectSongs = collectSongs;
            this.collectSongs.ResultSended += (s, e) =>
            {
                Songs = [.. e.CollectedSongs.Select(songViewModelMap.GetSafeWithSongInfos)];
            };

            this.playSongList = playSongList;

            this.renameSongList = renameSongList;
            this.renameSongList.ResultSended += (s, e) =>
            {
                if (e.IsSuccess)
                {
                    Name = e.GetValue();
                }
            };
        }

        public void CollectSongs()
        {
            var request = new CollectSongsRequest(Id);
            collectSongs.Execute(request);
        }

        public void PlaySongList(Guid? songId = null)
        {
            var request = new PlaySongListRequest(Id, songId);
            playSongList.Execute(request);
        }

        public void Rename(string newName)
        {
            var request = new RenameSongListRequest(Id, newName);
            renameSongList.Execute(request);
        }
    }
}
