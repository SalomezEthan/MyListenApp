using MyListen.Common.DataTransfertObjects;
using MyListen.SongList;
using MyListenApp.ViewModels.Song;
using System;
namespace MyListenApp.ViewModels.SongList
{
    internal sealed class SongListViewModelFactory(SongListComposantFactory composantFactory, SongViewModelMap songViewModelMap)
    {
        readonly SongListComposantFactory composantFactory = composantFactory;
        readonly SongViewModelMap songViewModelMap = songViewModelMap;

        public SongListViewModel CreateSongList(PlaylistInfos infos)
        {
            return new SongListViewModel(
                infos,
                composantFactory.CreateCollectSongs(),
                composantFactory.CreatePlaySongList(),
                composantFactory.CreateRenamePlaylist(),
                songViewModelMap
            );
        }
    }
}
