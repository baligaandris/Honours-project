using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;

public class RPGPlayer : MonoBehaviour
{
    public int hp = 100, damage = 10, defense = 0, gold = 0, XP=0;
    public int evasion = 10;

    public int xpCostOf10Hp=1, xpCostOfDamage=1, xpCostOfDefense=1;

    public TMP_Text hpText, xpText, goldText, damageText, defenceText, evasionText;

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
        if (incomingDamage > defense)
        {
            hp -= incomingDamage - defense;
        }
        else
        {
            hp -= 1;
        }
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
        RefreshUI();
    }

    [YarnCommand("SpendMoney")]
    public void SpendMoney(string cost, string hpIncrease, string damageIncrease, string evasionIncrease, string defenseIncrease)
    {
        int costInt, hpIncreaseInt, damageIncreaseInt, evasionIncreaseInt, defenseIncreaseInt;


        int.TryParse(cost, out costInt);
        int.TryParse(hpIncrease, out hpIncreaseInt);
        int.TryParse(damageIncrease, out damageIncreaseInt);
        int.TryParse(evasionIncrease, out evasionIncreaseInt);
        int.TryParse(defenseIncrease, out defenseIncreaseInt);

        if (costInt<=gold)
        {
            gold -= costInt;
            hp += hpIncreaseInt;
            damage += damageIncreaseInt;
            evasion += evasionIncreaseInt;
            defense += defenseIncreaseInt;
        }
        RefreshUI();
    }

    public void IncreaseDamage()
    {
        if (XP>=xpCostOfDamage)
        {
            damage++;
            XP -= xpCostOfDamage;
            RefreshUI();
        }
    }

    public void IncreaseHP()
    {
        if (XP>=xpCostOf10Hp)
        {
            hp += 10;
            XP -= xpCostOf10Hp;
            RefreshUI();
        }
    }

    public void IncreaseDefense()
    {
        if (XP>=xpCostOfDefense)
        {
            defense += 1;
            XP -= xpCostOfDefense;
            RefreshUI();
        }
    }

    public void RefreshUI()
    {
        hpText.text = hp.ToString();
        xpText.text = XP.ToString();
        goldText.text = gold.ToString();
        damageText.text = damage.ToString();
        defenceText.text = defense.ToString();
        evasionText.text = evasion.ToString();
    }
}
