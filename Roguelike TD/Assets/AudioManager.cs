using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public List<AudioSource> audioSources;
    [SerializeField] public List<AudioClip> audioClips;
    [SerializeField] public WaveManager waveManager;
    [SerializeField] public float backgroundMusicVolume = 0.05f;

    int waveCount = -1;
    int curTrack = 0;


    private void Awake()
    {
        if (waveManager == null)
            waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        audioSources = new List<AudioSource>();

        for (int i = 0; i < audioClips.Count; i++)
        {
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.playOnAwake = false;
            audioSources.Add(a);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (AudioClip a in audioClips)
        {
            if (i < audioSources.Count)
            {
                audioSources[i].clip = a;
                audioSources[i].volume = 0f;
                audioSources[i].loop = true;
                audioSources[i].Play();
            }
            i++;
        }

        audioSources[0].volume = backgroundMusicVolume;
        audioSources[0].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveManager.GetWaveCount() != waveCount)
        {
            waveCount = waveManager.GetWaveCount();
            //UpdateBackgroundMusic(waveCount);
        }
    }

    private void UpdateBackgroundMusic(int waveCount)
    {
        if (waveCount == 0)
        {
            audioSources[1].volume = backgroundMusicVolume;
        }
        else if (waveCount == 4)
        {
            audioSources[2].volume = backgroundMusicVolume;
        }
        else if (waveCount == 7)
        {
            audioSources[3].volume = backgroundMusicVolume;
        }
        else if (waveCount == 10)
        {
            audioSources[4].volume = backgroundMusicVolume;
        }
        else if (waveCount > 15)
        {
            int i = 0; 
           foreach (AudioSource a in audioSources)
            {
                if (i == audioSources.Count - 1)
                    a.volume = backgroundMusicVolume;
                else
                    a.volume = 0f;

                i++;
            }
        }        
    }

    public void ToggleBackgroundMusicMuted(bool isMuted)
    {
        if (isMuted)
        {
            foreach (AudioSource a in audioSources)
            {
                a.volume = 0;
            }
            //audioSources[curTrack].volume = 0;
        }
        else
        {
            foreach (AudioSource a in audioSources)
            {
                a.volume = 0;
            }
            audioSources[curTrack].volume = backgroundMusicVolume;
        }
    }

    public void ToggleBackgroundMusicSwitch(bool isSwitched)
    {
        if (isSwitched)
            curTrack = 6;
        else
            curTrack = 0;

    }
}
