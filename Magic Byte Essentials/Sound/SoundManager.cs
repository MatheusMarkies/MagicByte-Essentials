using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics.Sound
{
    public class SoundManager : MonoBehaviour
{
        public void playSound(AudioSource source, AudioClip clip, float volume)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.volume = Mathf.Clamp01(volume);
                source.Play();
            }
        }
        public void volumeUpdate(AudioSource source, float volume) { source.volume = Mathf.Clamp01(volume); }
        public void stopPlaying(AudioSource source)
        {
            source.Stop();
        }
    }
}
