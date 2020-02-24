using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource[] gameplayAudioSources;
    [SerializeField] private AudioClip[] clips;

    public static EventHandler<AudioEvent> audioEventHandler;


    private float counter, audioTransitionTime;
    private bool transitioning;
    // Start is called before the first frame update
    void Start()
    {
        gameplayAudioSources[0].volume = 1f;
        gameplayAudioSources[1].volume = 0f;

        audioEventHandler += TransitionAudio;
    }

    private void OnDestroy()
    {
        var events = audioEventHandler.GetInvocationList();

        foreach (var del in events)
        {
            audioEventHandler -= (del as EventHandler<AudioEvent>);
        }
    }

    private void TransitionAudio(object sender, AudioEvent e)
    {
        if(e.TransitionMainAudio)
        {
            if(e.Audio == null)
            {
                TransitionAudio(e.TransitionAudioTime);
            }
            else
            {
                TransitionAudio(e.TransitionAudioTime, e.Audio);
            }
        }
        else
        {
            PlayClip(e.Audio, e.SinglePlayAudioDelay);
        }
    }

    public void TransitionAudio(float time)
    {
        audioTransitionTime = time;
        transitioning = true;
    }

    public void TransitionAudio(float time, AudioClip transitionTo)
    {
        TransitionAudio(time);
        gameplayAudioSources[1].clip = transitionTo;
    }

    public void PlayClip(AudioClip clip, ulong delay = 0)
    {
        gameplayAudioSources[2].clip = clip;
        gameplayAudioSources[2].Play(delay);
    }

    private void Update()
    {
        if(transitioning)
        {
            if (counter == 0f)
                gameplayAudioSources[1].Play();

            counter += Time.deltaTime;
            gameplayAudioSources[0].volume = 1 - (counter / audioTransitionTime);
            gameplayAudioSources[1].volume = (counter / audioTransitionTime);
            if(counter >= audioTransitionTime)
            {
                //Swap audio sources
                var temp = gameplayAudioSources[1];
                gameplayAudioSources[1] = gameplayAudioSources[0];
                gameplayAudioSources[0] = temp;

                //Lock volumes and disable transition;
                gameplayAudioSources[0].volume = 1f;
                gameplayAudioSources[1].volume = 0f;

                transitioning = false;
                counter = 0f;
            }
        }
    }
}

public struct AudioEvent
{
    public bool TransitionMainAudio { get; private set; }
    public float TransitionAudioTime { get; private set; }
    public AudioClip Audio { get; private set; }
    public ulong SinglePlayAudioDelay { get; private set; }

    public AudioEvent(AudioClip audioClip = null, bool audioTransition = false, float transitionTime = 0f, ulong delay = 1)
    {
        TransitionMainAudio = audioTransition;
        TransitionAudioTime = transitionTime;
        Audio = audioClip;
        SinglePlayAudioDelay = delay;
    }
}
