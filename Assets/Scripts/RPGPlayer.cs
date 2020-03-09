using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class RPGPlayer : MonoBehaviour
{
    public int hp = 100, damage = 10, defense = 0, gold = 0, XP=0;
    public int evasion = 10;


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
        hp -= incomingDamage - defense;
    }

    public void Attack(Enemy enemy)
    {
        int attackRoll = Random.Range(0, 100);
        if (attackRoll>enemy.evasion)
        {
            enemy.TakeDamage(damage);
        }
    }

    public void BeAttackedBy(Enemy enemy)
    {
        int attackRoll = Random.Range(0, 100);
        if (attackRoll>evasion)
        {
            TakeDamage(enemy.damage);
        }
    }

    public void SpendXPOn(int attribute)
    {
        XP -= 1;
        attribute += 1;
    }

    [YarnCommand("SpendMouney")]
    public void SpendMoney(string cost, string hpIncrease, string damageIncrease, string evasionIncrease, string defenseIncrease)
    {
        int costInt, hpIncreaseInt, damageIncreaseInt, evasionIncreaseInt, defenseIncreaseInt;


        int.TryParse(cost, out costInt);
        int.TryParse(hpIncrease, out hpIncreaseInt);
        int.TryParse(damageIncrease, out damageIncreaseInt);
        int.TryParse(evasionIncrease, out evasionIncreaseInt);
        int.TryParse(defenseIncrease, out defenseIncreaseInt);

        if (costInt<gold)
        {
            gold -= costInt;
            hp += hpIncreaseInt;
            damage += damageIncreaseInt;
            evasion += evasionIncreaseInt;
            defense += defenseIncreaseInt;
        }
    }
    
}
