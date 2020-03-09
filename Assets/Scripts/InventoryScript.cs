using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryScript : MonoBehaviour
{
    public List<ItemSO> inventory;
    public AudioClip pickUpSound;

    // Start is called before the first frame update
    void Start()
    { 
        inventory = new List<ItemSO>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewItem(ItemSO CollectedItem)
    {
        inventory.Add(CollectedItem);
        GetComponent<AudioSource>().PlayOneShot(pickUpSound);
        PrintList();
    }

    public void RemoveItem(ItemSO RemovedItem)
    {
        inventory.Remove(RemovedItem);
        PrintList();
    }

    public void PrintList()
    {
        foreach (ItemSO item in inventory)
        {
            print(item.name);
        }
    }
}
