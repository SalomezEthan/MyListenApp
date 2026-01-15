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

        ObservableCollection<SongViewModel> _musics = [];
        public ObservableCollection<SongViewModel> Musics
        {
            get => _musics;
            set => SetValue(ref _musics, value);
        }

        readonly CollectMusics collectMusics;
        readonly PlaySongList playSongList;
        readonly RenamePlaylist renamePlaylist;

        internal SongListViewModel(PlaylistInfos infos, CollectMusics collectMusics, PlaySongList playSongList, RenamePlaylist renamePlaylist, SongViewModelMap songViewModelMap)
        {
            this.Id = infos.Id;
            this._name = infos.Name;
            this._count = infos.Count;

            this.collectMusics = collectMusics;
            this.collectMusics.ResultSended += (s, e) =>
            {
                Musics = [.. e.CollectedMusics.Select(songViewModelMap.GetSafeWithSongInfos)];
            };

            this.playSongList = playSongList;

            this.renamePlaylist = renamePlaylist;
            this.renamePlaylist.ResultSended += (s, e) =>
            {
                if (e.IsSuccess)
                {
                    Name = e.GetValue();
                }
            };
        }

        public void CollectMusics()
        {
            var request = new CollectMusicsRequest(Id);
            collectMusics.Execute(request);
        }

        public void PlaySongList(Guid? musicId = null)
        {
            var request = new PlaySongListRequest(Id, musicId);
            playSongList.Execute(request);
        }

        public void Rename(string newName)
        {
            var request = new RenamePlaylistRequest(Id, newName);
            renamePlaylist.Execute(request);
        }
    }
}
