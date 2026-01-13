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
        private CAudioClip clip;
        private WaveOutEvent device = new WaveOutEvent();
        private float volume = 1.0f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clip"></param>
        internal CAudioSource(CAudioClip clip)
        {
            this.clip = clip;

            var reader = new AudioFileReader(clip.Path);
            if (clip.Loop)
            {
                var stream = new LoopStream(reader);
                device.Init(stream);
            }
            else
            {
                device.Init(reader);
            }
            device.PlaybackStopped += OnPlaybackStoppedHandler;
        }

        /// <summary>
        /// 音量
        /// </summary>
        public float Volume
        {
            set
            {
                volume = Math.Clamp(value, 0.0f, 1.0f);
                if (device != null)
                {
                    device.Volume = clip.Volume * volume;
                }
            }
            get { return volume; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Loop => clip.Loop;

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
