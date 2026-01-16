using MyListen.Common.DataTransfertObjects;
using MyListen.Song;

namespace MyListenApp.ViewModels.Song
{
    internal sealed class SongViewModelFactory(SongServiceFactory composantFactory)
    {
        readonly SongServiceFactory serviceFactory = composantFactory;

        public SongViewModel CreateViewModel(SongInfos infos)
        {
            return new SongViewModel(
                infos,
                serviceFactory.CreateSongService()
            );
        }
    }
}
