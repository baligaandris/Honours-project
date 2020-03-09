using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GunSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [YarnCommand("GunSound")]
    public void PlayGunSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
