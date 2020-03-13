using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class BackgroundAudioSwapper : MonoBehaviour
{
    public List<AudioClip> backgroundSounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [YarnCommand("SwapBackgroundAudio")]
    public void SwapBackgroundSound(string backgroundSoundIndex)
    {
        int index;
        int.TryParse(backgroundSoundIndex, out index);
        GetComponent<AudioSource>().clip = backgroundSounds[index];
        GetComponent<AudioSource>().Play();
    }
}
