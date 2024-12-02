using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace NameThatTune
{
    public class Sample
    {
        private string sampleURL;
        private MediaFoundationReader audioFile;
        private WaveOutEvent outputDevice;

        public bool isPlaying()
        {
            return outputDevice?.PlaybackState == PlaybackState.Playing;
        }

        public bool isPaused()
        {
            return outputDevice?.PlaybackState == PlaybackState.Paused;
        }
        public bool isStopped()
        {
            return outputDevice?.PlaybackState == PlaybackState.Stopped;
        }


        public Sample(string sampleURL)
        {
            this.sampleURL = sampleURL;
            try
            {
                audioFile = new MediaFoundationReader(sampleURL);
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error initializing playback: " + e.Message);
            }
        }

        public async void Play()
        {
            if (outputDevice == null || audioFile == null) return;
            if (!isPlaying())
            {
                outputDevice.Play();
            }
        }

        public void Pause()
        {
            if (isPlaying() && !isStopped()) outputDevice.Pause();
        }

        public void Stop()
        {
            if (!isStopped()) outputDevice.Stop();
        }

        public void TogglePlayback()
        {
            if (isPlaying()) Pause();
            else if (isPaused()) Play();
        }

    }
}