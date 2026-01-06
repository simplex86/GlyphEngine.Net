using System;
using NAudio.Wave;
using NAudio.Extras;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CAudioSource
    {
        private WaveOutEvent device = new WaveOutEvent();
        private float baseVolume = 1.0f;
        private float coefVolume = 1.0f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="volume"></param>
        /// <param name="loop"></param>
        internal CAudioSource(string filepath, float volume, bool loop)
        {
            baseVolume = Math.Clamp(volume, 0.0f, 1.0f);

            var reader = new AudioFileReader(filepath);
            if (loop)
            {
                var stream = new LoopStream(reader);
                device.Init(stream);
            }
            else
            {
                device.Init(reader);
            }
            device.PlaybackStopped += OnPlaybackStoppedHandler;

            Loop = loop;
        }

        /// <summary>
        /// 音量
        /// </summary>
        public float Volume
        {
            set
            {
                coefVolume = Math.Clamp(value, 0.0f, 1.0f);
                if (device != null)
                {
                    device.Volume = baseVolume * coefVolume;
                }
            }
            get { return coefVolume; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Loop { get; }

        /// <summary>
        /// 播放
        /// </summary>
        public void Play()
        {
            device?.Play();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            device?.Stop();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            device?.Pause();
        }

        /// <summary>
        /// 继续
        /// </summary>
        public void Resume()
        {
            device?.Play();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        private void Destroy()
        {
            device?.Stop();
            device?.Dispose();
            device = null;
        }

        /// <summary>
        /// 播放完后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPlaybackStoppedHandler(object? sender, StoppedEventArgs e)
        {
            Destroy();
        }
    }
}
