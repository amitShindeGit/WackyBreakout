using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();

    public static bool Initialized
    {
        get { return initialized; }
    }

    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.BallLost, Resources.Load<AudioClip>("BallLost"));
        audioClips.Add(AudioClipName.BlockHit, Resources.Load<AudioClip>("BlockHit"));
        audioClips.Add(AudioClipName.GameOverAudio, Resources.Load<AudioClip>("GameOverAudio"));
        audioClips.Add(AudioClipName.MenuClick, Resources.Load<AudioClip>("MenuClick"));
        audioClips.Add(AudioClipName.PaddleBorderHit, Resources.Load<AudioClip>("PaddleBorderHit"));
        audioClips.Add(AudioClipName.SpeedUp, Resources.Load<AudioClip>("SpeedUp"));
        audioClips.Add(AudioClipName.Freeze, Resources.Load<AudioClip>("Freeze"));
        audioClips.Add(AudioClipName.BallSpawn, Resources.Load<AudioClip>("BallSpawn"));
        audioClips.Add(AudioClipName.BonusBlock, Resources.Load<AudioClip>("BonusBlock"));
    }


    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
