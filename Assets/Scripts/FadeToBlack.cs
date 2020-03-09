using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class FadeToBlack : MonoBehaviour
{
    bool startFading = false;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startFading)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, GetComponent<Image>().color.a + Time.deltaTime/6);
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime / 6);
        }
    }
    [YarnCommand("Fade")]
    public void Fade()
    {
        startFading = true;
    }
}
