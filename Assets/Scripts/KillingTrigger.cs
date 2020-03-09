using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingTrigger : MonoBehaviour
{
    public Spawner purpleKeySpawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Head of Security")
        {
            collision.gameObject.GetComponent<AnimationSwitcher>().SwitchToNewAnimation("Die");
            collision.gameObject.GetComponent<NPCMovement>().stopAtNode[5] = true;
            collision.gameObject.GetComponent<YarnSwitcher>().Switch(null);
            purpleKeySpawner.Spawn();
        }
    }
}
