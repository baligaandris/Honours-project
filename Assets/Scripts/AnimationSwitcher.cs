using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AnimationSwitcher : MonoBehaviour
{
    public string lasttrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [YarnCommand("SwitchAnimation")]
    public void SwitchToNewAnimation(string condition) {
        GetComponent<Animator>().SetTrigger(condition);
        lasttrigger = condition;
    }
}
