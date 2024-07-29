using System;
using System.Media;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 音效
    /// </summary>
    public class CAudio
    {
        private SoundPlayer player;

        public async Task Load(string location)
        {
            try
            {
                player = new SoundPlayer(location);
                player.LoadAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public void Play()
        {
            try
            {
                player?.Play();
            }
            catch (Exception ex)
            {

            }
        }

        public void Stop()
        {
            try
            {
                player?.Stop();
            }
            catch (Exception ex)
            {

            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CAudioAttribute : Attribute
    {
        public string Location { get; }

        public CAudioAttribute(string location)
        {
            Location = location;
        }
    }
}
