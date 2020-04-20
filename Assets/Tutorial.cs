using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("EnableTutorial")]
    public void EnableTutorial()
    {
        tutorialCanvas.SetActive(true);
    }

    public void DisableTutorial()
    {
        tutorialCanvas.SetActive(false);
    }
}
