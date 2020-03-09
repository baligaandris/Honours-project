using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class Interactable : MonoBehaviour
{
    
    public List<ItemSO> usableItems;
    public List<PlayableAsset> itemResult;
    public List<PlayableAsset> otherItemResult;
    public PlayableDirector playableDirector;
    GameObject inventory;
    public bool used;
    public bool reusable;
    public bool otherstate;
    //sounds
    public AudioClip successSound;
    public AudioClip failSound;

    public GameObject wrongText;

    public Dictionary<ItemSO, PlayableAsset> DictionaryConsequences;
    public Dictionary<ItemSO, PlayableAsset> DictionaryOtherConsequences;
    // Start is called before the first frame update
    void Start()
    {
        //wrongText = GameObject.Find("Wrong item text");
        inventory = GameObject.FindGameObjectWithTag("Player");
        DictionaryConsequences = new Dictionary<ItemSO, PlayableAsset>();

        if (itemResult.Count != 0)
        for (int i = 0; i < usableItems.Count; i++)
        {
            DictionaryConsequences.Add(usableItems[i], itemResult[i]);
        }

        DictionaryOtherConsequences = new Dictionary<ItemSO, PlayableAsset>();

        if (otherItemResult.Count != 0)
        for (int i = 0; i < usableItems.Count; i++)
        {
            DictionaryOtherConsequences.Add(usableItems[i], otherItemResult[i]);
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(ItemSO playersInput)
    {
        if (otherstate == false)
        {
            if (DictionaryConsequences.TryGetValue(playersInput, out PlayableAsset action) && used == false && reusable == false)
            {
                playableDirector = gameObject.transform.Find(playersInput.itemName).GetComponentInChildren<PlayableDirector>();
                playableDirector.Play(action);
                used = true;
                GetComponent<AudioSource>().PlayOneShot(successSound);
                this.tag = "Untagged";

                if (playersInput.oneUse == true)
                {
                    inventory.GetComponent<InventoryScript>().inventory.Remove(playersInput);

                }

            }
            else if (DictionaryConsequences.TryGetValue(playersInput, out PlayableAsset act) && reusable == true)
            {
                playableDirector = gameObject.transform.Find(playersInput.itemName).GetComponentInChildren<PlayableDirector>();
                playableDirector.Play(action);
                GetComponent<AudioSource>().PlayOneShot(successSound);
                otherstate = true;

                if (playersInput.oneUse == true)
                {
                    inventory.GetComponent<InventoryScript>().inventory.Remove(playersInput);

                }
            }
            else if (DictionaryConsequences.TryGetValue(playersInput, out PlayableAsset unaction) == false && used == false)
            {
                GetComponent<AudioSource>().PlayOneShot(failSound);
                wrongText.SetActive(true);

            }
        }
        else
        {
            if (DictionaryOtherConsequences.TryGetValue(playersInput, out PlayableAsset action) && used == false && reusable == false)
            {
                playableDirector = gameObject.transform.Find(playersInput.itemName + "Other").GetComponentInChildren<PlayableDirector>();
                playableDirector.Play(action);
                used = true;
                GetComponent<AudioSource>().PlayOneShot(successSound);
                this.tag = "Untagged";

                if (playersInput.oneUse == true)
                {
                    inventory.GetComponent<InventoryScript>().inventory.Remove(playersInput);

                }

            }
            else if (DictionaryOtherConsequences.TryGetValue(playersInput, out PlayableAsset act) && reusable == true)
            {
                playableDirector = gameObject.transform.Find(playersInput.itemName + "Other").GetComponentInChildren<PlayableDirector>();
                playableDirector.Play(act);
                GetComponent<AudioSource>().PlayOneShot(successSound);
                otherstate = false;
                if (playersInput.oneUse == true)
                {
                    inventory.GetComponent<InventoryScript>().inventory.Remove(playersInput);

                }
            }
            else if (DictionaryOtherConsequences.TryGetValue(playersInput, out PlayableAsset unaction) == false && used == false)
            {
                GetComponent<AudioSource>().PlayOneShot(failSound);
                wrongText.SetActive(true);
            }
        }

    }
}
 