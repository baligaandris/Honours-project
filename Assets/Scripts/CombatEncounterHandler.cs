using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;

public class CombatEncounterHandler : MonoBehaviour
{
    public RPGPlayer player;
    
    public Encounter[] encounters;
    public int encounterIndex = 0;

    public Enemy activeEnemy;
    public bool inCombat = false;

    public TMP_Text hpText, enemyHpText, enemyNameText ,xpText, goldText, 
        damageText, defenceText, evasionText;
    

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<RPGPlayer>().RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    [YarnCommand("CreateCombat")]
    public void CreateCombatEncounter(string combatIndex)
    {
        int.TryParse(combatIndex, out encounterIndex);
        activeEnemy = Instantiate(encounters[encounterIndex].enemy);
        inCombat = true;
        player.GetComponent<RPGPlayer>().RefreshUI();
    }

    public void TakeEnemyTurn() {
        player.BeAttackedBy(activeEnemy);
    }

    [YarnCommand("ResolveCombat")]
    public void ResolveCombat()
    {
        if (inCombat)
        {
            while (player.hp>0 && activeEnemy.hp>0)
            {
                CombatRound();
            }
        }
        player.GetComponent<RPGPlayer>().RefreshUI();
    }

    public void CombatRound() {
        player.Attack(activeEnemy);
        TakeEnemyTurn();
        if (activeEnemy.hp <= 0)
        {
            player.gold += encounters[encounterIndex].rewardGold;
            player.XP += encounters[encounterIndex].rewardXP;
            inCombat = false;
        }
    }
    [YarnCommand("SkipCombat")]
    public void SkipEncounter(string skipXP, string skipGold)
    {
        int skipXPInt, skipGoldInt;
        int.TryParse(skipXP, out skipXPInt);
        int.TryParse(skipGold, out skipGoldInt);
        player.XP += skipXPInt;
        player.gold += skipGoldInt;
        player.GetComponent<RPGPlayer>().RefreshUI();
    }

    //public void RefreshUI()
    //{
    //    if (inCombat)
    //    {
    //        enemyHpText.text = activeEnemy.hp.ToString();
    //        enemyNameText.text = activeEnemy.EnemyName.ToString();
    //    }
    //    else
    //    {
    //        enemyHpText.text = "";
    //        enemyNameText.text = "";
    //    }
    //    hpText.text = player.hp.ToString();
    //    xpText.text = player.XP.ToString();
    //    goldText.text = player.gold.ToString();
    //    damageText.text = player.damage.ToString();
    //    defenceText.text = player.defense.ToString();
    //    evasionText.text = player.evasion.ToString();
    //}

}
[System.Serializable]
public class Encounter
{
    public Enemy enemy;
    public int rewardGold;
    public int rewardXP;
}
