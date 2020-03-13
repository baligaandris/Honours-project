using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class NPCImageSwapper : MonoBehaviour
{

    public List<Sprite> npcs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [YarnCommand("SwapNPC")]
    public void SwapNPC(string NPCIndex)
    {
            int index;
            int.TryParse(NPCIndex, out index);
            GetComponent<Image>().sprite = npcs[index];
    }
}
