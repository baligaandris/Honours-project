using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunThugActivator : MonoBehaviour
{
    public List<NPCMovement> npcs;
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
        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].enabled = true;
        }
    }
}
