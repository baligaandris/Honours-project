using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class StopAudioLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [YarnCommand("StopLooping")]
    public void StopLoopingAudio()
    {
        GetComponent<AudioSource>().loop = false;
    }

}
