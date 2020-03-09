using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Usable : MonoBehaviour
    {
    public bool used;
    public bool reusable;
    PlayableDirector director;
    public AudioClip interactedSound;
    // Start is called before the first frame update
    void Start()
    {
        director = GetComponentInChildren<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseUsable()
    {
        if (reusable == true)
        {
            director.Play();
            GetComponent<AudioSource>().clip = interactedSound;
            GetComponent<AudioSource>().Play();
        }
        else if(reusable == false && used == false)
        {
            director.Play();
            GetComponent<AudioSource>().clip = interactedSound;
            GetComponent<AudioSource>().Play();
            used = true;
        }
        else
        {
            //Andras trigger dialogue or text as an error.
        }

    }
}
