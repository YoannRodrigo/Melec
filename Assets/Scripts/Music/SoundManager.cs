using System;
using System.Collections.Generic;
using RhythmTool;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public  List<string> songNames = new List<string>();
    private GameObject currentClipGameObject;
    private AudioSource currentClip;
    private RhythmData rhythmData;
    private RhythmEventProvider eventProvider;
    private int sceneId = 0;
    
    private static float _masterVolume = 1;
    private static float _musicVolume = 0.4f;
    private static float _soundEffectVolume = 1;

    public void SetMasterVolume(Slider slider)
    {
        _masterVolume = slider.value;
        UpdateVolume();
    }

    public void SetMusicVolume(Slider slider)
    {
        _musicVolume = slider.value;
        UpdateVolume();
    }
    
    public void SetSoundEffectVolume(Slider slider)
    {
        _soundEffectVolume = slider.value;
    }

    private void UpdateVolume()
    {
        if (currentClip)
        {
            currentClip.volume = _masterVolume * _musicVolume;
        }
    }
    
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

    public void InitAnalyzer()
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
        currentClip.volume = _masterVolume * _musicVolume;
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

