using System.Collections;
using System.Collections.Generic;
using RhythmTool;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SoundManager : MonoBehaviour
{
    public  List<string> songNames = new List<string>();
    private GameObject currentClipGameObject;
    private AudioSource currentClip;
    private RhythmData rhythmData;
    private RhythmEventProvider eventProvider;

    public RhythmData GetRhythmData()
    {
        return rhythmData;
    }

    public RhythmEventProvider GetEventProvider()
    {
        return eventProvider;
    }
    
    
    private void Start()
    {
        RhythmToolLib.CreateAnalyzer(true,true,withVolumeSampler:true);
        RhythmToolLib.SetOnsetDetectorParams(threshold: 0.7f);
        RhythmToolLib.SetVolumeSamplerParams(smoothing: 14);
    }

    public void StartMusicWithName(string musicName)
    {
        if(!currentClipGameObject)
        {
            currentClipGameObject = Instantiate(new GameObject(musicName),GameObject.FindWithTag("MainCamera").transform);
        }
        else
        {
            currentClipGameObject.name = musicName;
        }

        if (!currentClip)
        {
            currentClip = currentClipGameObject.AddComponent<AudioSource>();
        }
        
        currentClip.clip = Resources.Load("Music/"+musicName) as AudioClip;
        currentClip.Play();
        
    }
    
    public void StartRandomMusic()
    {
        int randomName = Random.Range(0, songNames.Count);
        StartMusicWithName(songNames[randomName]);
    }

    public void AnalyzeCurrentClip()
    {
        rhythmData = RhythmToolLib.AnalyzeSound(currentClip.clip);
    }
    
    public void CreateEventProviderOnCurrentSoundWithOffset(float offset = 0)
    {
        eventProvider = RhythmToolLib.CreateEventProvider(currentClip.gameObject, rhythmData, offset);
    }
}

