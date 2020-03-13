using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class BackgroundSwapper : MonoBehaviour
{
    public List<Sprite> backgrounds;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [YarnCommand("SwapBackground")]
    public void SwapBackground(string backgroundIndex)
    {
        int index;
        int.TryParse(backgroundIndex, out index);
        GetComponent<Image>().sprite = backgrounds[index];
    }
}
