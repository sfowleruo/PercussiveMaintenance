using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatKeeper : MonoBehaviour
{
    public List<TowerSoundSource> TowerSources = new List<TowerSoundSource>();

    public float SongStart;

    public int TempoBPM = 60;
    public int MeasuresPerCycle = 4;
    public int FramesPerMeasure = 4;
    public float NoteTimePerBeat = .5f;

    public float LastNote = 0;

    public int CurrentMeasure = 0;
    public int CurrentFrame   = 0;

    public GameObject ConductorLine;

    bool IsPlaying = false;

    public void TogglePlayStop()
    {
        if (IsPlaying)
        {
            StopSong();
        }
        else
        {
            PlaySong();
        }
    }

    public void PlaySong()
    {
        SongStart = Time.time;
        LastNote = 0;
        IsPlaying = true;
    }

    public void StopSong()
    {
        IsPlaying = false;
    }

    public void Update()
    {
        if (!IsPlaying)
            return;
        if(Time.time - LastNote > NoteTimePerBeat)
        {
            PlayFrame(CurrentMeasure, CurrentFrame);
            IncrementFrame();
            LastNote = Time.time;
        }
    }

    public void PlayFrame(int measure, int frame)
    {
        foreach(var tower in TowerSources)
        {
            if(tower.MeasurePlayTimes[measure][frame] == '1')
            {
                tower.PlayClip();
            }
        }
    }
    
    public void IncrementFrame()
    {
        CurrentFrame += 1;
        if(CurrentFrame == FramesPerMeasure)
        {
            CurrentMeasure += 1;
            CurrentMeasure %= MeasuresPerCycle;
            CurrentFrame = 0;
        }
    }
}
