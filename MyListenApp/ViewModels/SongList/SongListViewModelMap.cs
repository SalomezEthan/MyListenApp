using MyArchitecture.PresenterLayer;
using MyListen.Common.DataTransfertObjects;

namespace MyListenApp.ViewModels.SongList
{
    internal sealed class SongListViewModelMap (SongListViewModelFactory songListViewModelFactory) 
    : ViewModelMap<SongListViewModel>
    {
        readonly SongListViewModelFactory factory = songListViewModelFactory;

        public SongListViewModel CreateSongListViewModel(SongListInfos infos)
        {
            var vm = factory.CreateSongList(infos);
            return SetNewViewModel(infos.Id, vm);
        }

        public SongListViewModel GetSafeWithSongListInfos(SongListInfos infos)
        {
            var vm = TryGetViewModel(infos.Id);
            return vm ?? CreateSongListViewModel(infos);
        }
    }
}
