using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;
using UnityEngine.SceneManagement;

public class RPGPlayer : MonoBehaviour
{
    public int hp = 100, damage = 10, defense = 0, gold = 0, XP=0;
    public int evasion = 10;

    public int xpCostOf10Hp=1, xpCostOfDamage=1, xpCostOfDefense=1;

    public TMP_Text hpText, xpText, goldText, damageText, defenceText, evasionText;
    public bool dead=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FadeTextToWhite(hpText, 1);
        FadeTextToWhite(xpText, 1);
        FadeTextToWhite(goldText, 1);
        FadeTextToWhite(damageText, 1);
        FadeTextToWhite(defenceText, 1);
        FadeTextToWhite(evasionText, 1);

        if (hp<=0 && dead==false)
        {
            FindObjectOfType<DialogueRunner>().StartDialogue("Death");
            dead = true;
        }
    }
    [YarnCommand("Restart")]
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [YarnCommand("Quit")]
    public void Quit() {
        Application.Quit();
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
        if (hpText.text!= hp.ToString())
        {
            FlashInColor(hpText, hp);
            hpText.text = hp.ToString();
        }
        if (xpText.text != XP.ToString())
        {
            FlashInColor(xpText, XP);
            xpText.text = XP.ToString();
        }
        if (goldText.text != gold.ToString())
        {
            FlashInColor(goldText, gold);
            goldText.text = gold.ToString();
        }
        if (damageText.text != damage.ToString())
        {
            FlashInColor(damageText, damage);
            damageText.text = damage.ToString();
        }
        if (defenceText.text != defense.ToString())
        {
            FlashInColor(defenceText, defense);
            defenceText.text = defense.ToString();
        }
        if (evasionText.text != evasion.ToString())
        {
            FlashInColor(evasionText, evasion);
            evasionText.text = evasion.ToString();
        }

    }

    public void FlashInColor(TMP_Text text, int currentvalue)
    {
        int displayValue;
        int.TryParse(text.text, out displayValue);
        if (displayValue > currentvalue)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.green;
        }
    }
    public void FadeTextToWhite(TMP_Text text, int speed)
    {
        text.color = new Color(text.color.r + speed*Time.deltaTime, text.color.g + speed*Time.deltaTime, text.color.b + speed* Time.deltaTime);
    }
}
