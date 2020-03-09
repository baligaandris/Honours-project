using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Target : MonoBehaviour
{
    public int revengeValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [YarnCommand("Kill")]
    public void Die()
    {
        FindObjectOfType<RevengeMeter>().increaseRevenege(revengeValue);
        Destroy(gameObject);

    }
}
