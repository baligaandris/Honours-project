using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject inventoryDisplay;
    public Text inventoryDisplayText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryDisplay.gameObject.activeSelf)
            {
                inventoryDisplay.gameObject.SetActive(false);
            }
            else
            {
                inventoryDisplay.gameObject.SetActive(true);
                inventoryDisplayText.text = "";
                for (int i = 0; i < GetComponent<InventoryScript>().inventory.Count; i++)
                {
                    inventoryDisplayText.text += (GetComponent<InventoryScript>().inventory[i].itemName + ", ");
                }
                
            }
        }
    }


}
