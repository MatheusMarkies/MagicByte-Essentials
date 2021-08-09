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

        List<List<int>> getAudioToneChannels(int samplesCount)
        {
            List<List<int>> returnList = new List<List<int>>();

            List<int> SubBass = new List<int>();//List Index = 0
            List<int> Bass = new List<int>();//List Index = 1
            List<int> LowMiddle = new List<int>();//List Index = 2
            List<int> Middle = new List<int>();//List Index = 3
            List<int> HighMiddle = new List<int>();//List Index = 4
            List<int> High = new List<int>();//List Index = 5

            int hertzPerBin = (int)getMaxFrequency() / (int)samplesCount;

            for (int i = 0; i < (int)samplesCount / hertzPerBin; i++)
            {
                if ((i * hertzPerBin) < 63)
                    SubBass.Add(i);
                if ((i * hertzPerBin) > 63 && (i * hertzPerBin) < 250)
                    Bass.Add(i);
                if ((i * hertzPerBin) > 250 && (i * hertzPerBin) < 640)
                    LowMiddle.Add(i);
                if ((i * hertzPerBin) > 640 && (i * hertzPerBin) < 2500)
                    Middle.Add(i);
                if ((i * hertzPerBin) > 2500 && (i * hertzPerBin) < 5000)
                    HighMiddle.Add(i);
                if ((i * hertzPerBin) > 5000)
                    High.Add(i);
            }

            returnList.Add(SubBass);
            returnList.Add(Bass);
            returnList.Add(LowMiddle);
            returnList.Add(Middle);
            returnList.Add(HighMiddle);
            returnList.Add(High);

            return returnList;
        }
        List<float> createAudioHistory(int samplesCount, float[] samples)
        {
            List<float> History = new List<float>();
            List<List<int>> AudioToneChannels = getAudioToneChannels(samplesCount);

            for(int i = 0; i < AudioToneChannels.Count; i++)
            {
                int g = 0;
                float average = 0;
                int index = AudioToneChannels[i][g];
                for(int e = 0; e < samples.Length; e++)
                {
                    if(e == index)
                    {
                        average += samples[e];
                        g++;
                        index = AudioToneChannels[i][g];
                    }
                }
                average /= g;
                History.Add(average);
            }

            return History;
        }
        bool fullBeatDetector(float[] history, float[] oldhistory, float tolerance = 0)
        {
            float cteOldHistory = 0;
            float cteHistory = 0;
            foreach (float i in oldhistory)
                cteOldHistory += i;
            foreach (float i in history)
                cteHistory += i;

            if ((cteHistory / history.Length - cteOldHistory / oldhistory.Length) > tolerance)
                return true;
            else
                return false;
        }

        bool beatDetector(float[] history, float[] oldhistory, int index, float tolerance = 0)
        {
            float cteOldHistory = oldhistory[index];
            float cteHistory = history[index];

            if ((cteHistory - cteOldHistory) > tolerance)
                return true;
            else
                return false;
        }
        Note getAudioNote(float frequency)
        {
            Note nearNote = createNote("", 0f);

            List<Note> Notes = getStandardNotes();

            for (int i = 0; i < Notes.Count; i++)
                for (int j = 1; j < 8; j++)
                {
                    if (Mathf.Abs(frequency - nearNote.frequency) > Mathf.Abs(frequency - Notes[i].frequency * j) || nearNote.frequency == 0)
                    {
                        nearNote = createNote(Notes[i].name, Notes[i].frequency * j, j);
                    }
                }

            if (Mathf.Abs(frequency - nearNote.frequency) < 2)
                return nearNote;
            else
                return createNote("", 0f);
        }
    }
}