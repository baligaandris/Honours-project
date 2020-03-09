using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnSwitcher : MonoBehaviour
{
    public GameObject NPC;
    public string newNode;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        
    }
    [YarnCommand("Switch")]
    public void Switch(string nNode)
    {
        if (nNode ==null)
        {
            NPC.GetComponent<Yarn.Unity.Example.NPC>().talkToNode = newNode;
        }
        else
        {
            NPC.GetComponent<Yarn.Unity.Example.NPC>().talkToNode = nNode;
        }
        
        Debug.Log("switched");
    }
}
