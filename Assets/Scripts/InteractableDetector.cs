using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{

    public GameObject interactableInReach;
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
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "Interactable" || collision.gameObject.tag == "NPC" || collision.gameObject.tag == "Usable")
        {
            interactableInReach = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject==interactableInReach)
        {
            interactableInReach = null;
            GetComponentInParent<PlayerController>().dropdown.gameObject.SetActive(false);
        }
    }
}
