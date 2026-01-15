using MyArchitecture.PresenterLayer;
using MyListen.Common.DataTransfertObjects;

namespace MyListenApp.ViewModels.Song
{
    internal sealed class SongViewModelMap(SongViewModelFactory songViewModelFactory) : ViewModelMap<SongViewModel>
    {
        readonly SongViewModelFactory factory = songViewModelFactory;

        public SongViewModel CreateSongViewModel(SongInfos songInfo)
        {
            var songViewModel = factory.CreateViewModel(songInfo);
            return SetNewViewModel(songInfo.Id, songViewModel);
        }

        public SongViewModel GetSafeWithSongInfos(SongInfos infos)
        {
            var vm = TryGetViewModel(infos.Id);
            return vm ?? CreateSongViewModel(infos);
        }
    }
}
