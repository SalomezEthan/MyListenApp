using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;

namespace MyListenApp.Services
{
    public sealed class BackgroundService
    {
        MediaPlayer? backgroundPlayer;

        public void SetPlayer(MediaPlayer player)
        {
            this.backgroundPlayer = player;
        }

        public void SetPlayState(bool isPlaying)
        {
            if (isPlaying) backgroundPlayer?.Play();
            else backgroundPlayer?.Pause();
        }
    }
}
