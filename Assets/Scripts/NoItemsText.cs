using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoItemsText : MonoBehaviour
{
    public float showTime;
    public float fadeTime;
    private float showcounter;
    private float fadecounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        showcounter = showTime;
        fadecounter = fadeTime;
        GetComponent<TextMeshProUGUI>().alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        showcounter -= Time.deltaTime;
        if (showcounter<0)
        {
            GetComponent<TextMeshProUGUI>().alpha -= Time.deltaTime/fadeTime;
            fadecounter -= Time.deltaTime;
            if (fadecounter < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
