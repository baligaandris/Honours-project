using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Spawner : MonoBehaviour
{
    public GameObject ItemToSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("Spawn")]
    public void Spawn()
    {
        Debug.Log("Spawning");
        Instantiate(ItemToSpawn,transform.position,Quaternion.identity);
    }
}
