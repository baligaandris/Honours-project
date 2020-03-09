using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using Yarn.Unity.Example;

public class TextFlash : MonoBehaviour
{
    public GameObject Player;
    public Text titleText;
    public Text anyText;
    public Color color;
    public Image backgroundTitle;
    bool textActive;
    bool m_fading;
    public bool first = true;
    public Text tutorialText1, tutorialText2;
    public Image tutorialImage1, tutorialImage2;


    // Start is called before the first frame update
    void Start()
    {
        textActive = true;
        Player.GetComponent<PlayerController>().enabled = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (first)
        {
            if (Input.anyKey)
            {
                FindObjectOfType<DialogueRunner>().StartDialogue(GetComponent<NPC>().talkToNode);
                titleText.enabled = false;
                anyText.enabled = false;
                first = false;
                tutorialText1.gameObject.SetActive(false);
                tutorialText2.gameObject.SetActive(false);
                tutorialImage1.gameObject.SetActive(false);
                tutorialImage2.gameObject.SetActive(false);
            }
        }


        if (textActive == true)
        {
            anyText.color = new Color(color.r, color.g, color.b, 0.1f + Mathf.PingPong(Time.time * 1.1f, 1f));
        }
    }
    [YarnCommand("FadeIn")]
    public void FadeIn()
    {
        Destroy(anyText);
        textActive = false;
        backgroundTitle.CrossFadeAlpha(0f, 2f, true);
        titleText.CrossFadeAlpha(0f, 2f, true);
        Player.GetComponent<PlayerController>().enabled = true;
    }
}
