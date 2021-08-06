using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics.Sound
{
    public class BeatMapping
    {
        public enum samplesCountPresets { _256 = 256, _512 = 512, _1024 = 1024, _2048 = 2048 };
        public struct Note
        {
            public string name;
            public float frequency;
            public int multiplier;
        }
        Note createNote(string name, float frequency, int multiplier = 0)
        {
            Note note = new Note();
            note.name = name;
            note.frequency = frequency;
            note.multiplier = multiplier;
            return note;
        }
        float[] getSamples(AudioSource audioSource, int samplesCount)
        {
            return audioSource.GetOutputData(samplesCount,0);
        }
        float getMaxFrequency()
        {
            return (float)AudioSettings.outputSampleRate / 2f;
        }
        float getCurrentFrequency(float[] Samples, int samplesCount, float maxFrequency)
        {
            float maxAmplitude = 0;
            int index = 0;
            for(int i = 0; i< samplesCount; i++)
            {
                if (Samples[i] > maxAmplitude)
                {
                    maxAmplitude = Samples[i];
                    index = i;
                }
            }
            float frequencyIndex = index;
            if (maxAmplitude > 0 && maxAmplitude < (int)samplesCount - 1)
            {
                float dL = Samples[index - 1] / Samples[index];
                float dR = Samples[index + 1] / Samples[index];
                frequencyIndex += 0.5f * (dR * dR - dL * dL);
            }
            return frequencyIndex * (maxFrequency / 2) / (int)samplesCount;
        }
        List<Note> getStandardNotes()
        {
            Note C = createNote("C", 16.35f);
            Note Cs = createNote("C#", 17.32f); ;
            Note D = createNote("D", 18.35f);
            Note Eb = createNote("Eb", 19.45f);
            Note E = createNote("E", 20.6f);
            Note F = createNote("F", 21.83f);
            Note Fs = createNote("F#", 23.12f);
            Note G = createNote("G", 24.5f);
            Note Gs = createNote("G#", 25.96f);
            Note A = createNote("A", 27.5f);
            Note Bb = createNote("Bb", 29.14f);
            Note B = createNote("B", 30.87f);

            List<Note> Notes = new List<Note>();
            Notes.Add(C); Notes.Add(Cs); Notes.Add(D); Notes.Add(Eb); Notes.Add(E); Notes.Add(F);
            Notes.Add(Fs); Notes.Add(G); Notes.Add(Gs); Notes.Add(A); Notes.Add(Bb); Notes.Add(B);
            return Notes;
        }
    }
}