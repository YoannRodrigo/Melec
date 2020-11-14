using System.Collections.Generic;
using RhythmTool;
using UnityEngine;

/// <summary>
///     Class linked to Rhythm Tool add-on
///     http://hellomeow.net/rhythmtool/documentation/html/53f2927b-71fd-4719-aae5-34b7ff45a9ad.htm
///     Can be used to :
///     - Create Sound Analyzer
///     - Set parameters for analyzer
///     - Analyze sound into rhythm data from an Audio Source
///     - Create Event Provider from an Audio Source and a Rhythm Data
/// </summary>
public static class RhythmToolLib
{
    private static GameObject _analyzer;

    /// <summary>
    ///     Create sound analyzer in scene
    /// </summary>
    /// <param name="withBeatDetector"> Is beat detector needed ? </param>
    /// <param name="withOnsetDetector"> Is onset detector needed ? </pvaram>
    /// <param name="withSegmenter"> Is segmenter needed ? </param>
    /// <param name="withChromagram"> Is Chromagram needed ? </param>
    /// <param name="withVolumeSampler"> Is volume sampler needed ? </param>
    public static void CreateAnalyzer(bool withBeatDetector = true, bool withOnsetDetector = false, bool withSegmenter = false, bool withChromagram = false, bool withVolumeSampler = false)
    {
        if (_analyzer != null)
        {
            Object.DestroyImmediate(_analyzer);
        }

        _analyzer = new GameObject {name = "Analyzer"};
        _analyzer.AddComponent<RhythmAnalyzer>();
        if (withBeatDetector)
        {
            _analyzer.AddComponent<BeatTracker>();
        }

        if (withOnsetDetector)
        {
            _analyzer.AddComponent<OnsetDetector>();
        }

        if (withSegmenter)
        {
            _analyzer.AddComponent<Segmenter>();
        }

        if (withChromagram)
        {
            _analyzer.AddComponent<Chromagram>();
        }

        if (withVolumeSampler)
        {
            _analyzer.AddComponent<VolumeSampler>();
        }
    }

    public static void RemoveAnalyzer()
    {
        Object.DestroyImmediate(_analyzer);
    }

    /// <summary>
    ///     If onset detector exist, update params
    /// </summary>
    /// <param name="normalization">
    ///     Normalize the song. A higher value helps find onsets for quiet songs, but can increase
    ///     false positives.
    /// </param>
    /// <param name="threshold">
    ///     Threshold for finding onsets. A lower value will make the onset detection more sensitive, but
    ///     can increase false positives.
    /// </param>
    /// <param name="bufferSize">
    ///     The size of the buffer determines the minimum time between detected onsets and how much of the
    ///     surrounding data is used for calculating the threshold.
    /// </param>
    public static void SetOnsetDetectorParams(float normalization = 0.2f, float threshold = 0.3f, int bufferSize = 12)
    {
        OnsetDetector onsetDetector = _analyzer.GetComponent<OnsetDetector>();
        if (onsetDetector != null)
        {
            onsetDetector.normalization = Mathf.Clamp(normalization, 0, 1);
            onsetDetector.threshold = Mathf.Clamp(threshold, 0, 1);
            onsetDetector.bufferSize = Mathf.Clamp(bufferSize, 2, 32);
        }
    }

    /// <summary>
    ///     If segmenter exist, update params
    /// </summary>
    /// <param name="threshold">he threshold for detecting large differences in volume.</param>
    /// <param name="smoothing">How much smoothing is applied to the audio signal.</param>
    public static void SetSegmenterParams(float threshold = 22f, int smoothing = 8)
    {
        Segmenter segmenter = _analyzer.GetComponent<Segmenter>();
        if (segmenter != null)
        {
            segmenter.threshold = Mathf.Clamp(threshold, 0, 64);
            segmenter.smoothing = Mathf.Clamp(smoothing, 1, 16);
        }
    }

    /// <summary>
    ///     If volume sampler exist, update params
    /// </summary>
    /// <param name="interval">How often to sample volume.</param>
    /// <param name="smoothing">How much smoothing is applied.</param>
    public static void SetVolumeSamplerParams(int interval = 4, int smoothing = 8)
    {
        VolumeSampler volumeSampler = _analyzer.GetComponent<VolumeSampler>();
        if (volumeSampler != null)
        {
            volumeSampler.interval = Mathf.Clamp(interval, 1, 64);
            volumeSampler.smoothing = Mathf.Clamp(smoothing, 0, 16);
        }
    }

    /// <summary>
    ///     Create event provider and add Rhythm player on audio source
    /// </summary>
    /// <param name="audioSource"> GameObject with the audio source </param>
    /// <param name="rhythmData"> Rhythm data from the audio source </param>
    /// <param name="offset"> Time in seconds so events can be triggered ahead of time</param>
    /// <returns> The event provider </returns>
    public static RhythmEventProvider CreateEventProvider(GameObject audioSource, RhythmData rhythmData, float offset = 0)
    {
        RhythmEventProvider eventProvider = ScriptableObject.CreateInstance<RhythmEventProvider>();
        eventProvider.offset = offset;
        RhythmPlayer rhythmPlayer;
        if (audioSource.GetComponent<RhythmPlayer>() == null)
        {
            rhythmPlayer = audioSource.AddComponent<RhythmPlayer>();
            rhythmPlayer.rhythmData = rhythmData;
            rhythmPlayer.targets = new List<RhythmTarget> {eventProvider};
        }
        else
        {
            rhythmPlayer = audioSource.GetComponent<RhythmPlayer>();
            rhythmPlayer.targets.Add(eventProvider);
        }

        return eventProvider;
    }

    /// <summary>
    ///     Analyze a sound
    /// </summary>
    /// <param name="audioClip"> Sound to analyze </param>
    /// <returns> The rhythm data from the audio clip </returns>
    public static RhythmData AnalyzeSound(AudioClip audioClip)
    {
        return _analyzer.GetComponent<RhythmAnalyzer>().Analyze(audioClip);
    }
}
