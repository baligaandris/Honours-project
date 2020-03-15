using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 100, damage = 1, defense = 0;
    public int evasion = 10;
    public string EnemyName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int incomingDamage)
    {
        if (incomingDamage>defense)
        {
            hp -= incomingDamage - defense;
        }
        else
        {
            hp -= 1;
        }
        
    }
}
