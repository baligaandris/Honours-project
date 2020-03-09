using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity.Example;
using Yarn.Unity;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    string lastPressedDirection;
    public float vInput;
    public float hInput;
    public float speed = 1;
    Rigidbody2D rb;
    public GameObject interactableDetector;
    private InventoryScript inventory;
    public GameObject interactable;
    public TMP_Dropdown dropdown;

    public GameObject noItemsText;

    public List<AudioClip> footStepSounds;
    public float waitBetweenSteps =0.1f;
    float stepCD;

    public GameObject dialogueContainer;
    public GameObject dialogueHandler;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<InventoryScript>();
        stepCD = waitBetweenSteps;
       
    }


    // Update is called once per frame
    void Update()
    {
        vInput = 0;
        hInput = 0;
        if (dialogueContainer.activeSelf == false && GetComponent<AnimationSwitcher>().lasttrigger!="Die")
        {
            vInput = Input.GetAxisRaw("Vertical");
            hInput = Input.GetAxisRaw("Horizontal");
        }
            if (Input.GetButtonDown("Horizontal"))
            {
                lastPressedDirection = "Horizontal";
 
            }
            if (Input.GetButtonDown("Vertical"))
            {
                lastPressedDirection = "Vertical";
            }

            if (lastPressedDirection == "Vertical" && vInput != 0)
            {
                rb.velocity = new Vector2(0, vInput * speed);
                //interactableDetector.transform.localPosition = new Vector2(0, vInput*1f);
                GetComponent<Animator>().SetBool("walking", true);
            }
            else
            if (lastPressedDirection == "Horizontal" && hInput != 0)
            {
                rb.velocity = new Vector2(hInput * speed, 0);
                //interactableDetector.transform.localPosition = new Vector2(Mathf.Abs(hInput*0.5f), 0);
                if (hInput >0)
                {
                    GetComponent<Animator>().SetBool("walking", true);
                    transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x), transform.localScale.y);
                }
                else
                {
                    GetComponent<Animator>().SetBool("walking", true);
                    transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x)*-1, transform.localScale.y);
                }
            
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                GetComponent<Animator>().SetBool("walking", false);

            }
            //playing walking audio
            if (GetComponent<Animator>().GetBool("walking"))
            {

                if (GetComponent<AudioSource>().isPlaying==false)
                {


                    if (stepCD <=0)
                    {
                        int i = Random.Range(0, footStepSounds.Count - 1);
                        GetComponent<AudioSource>().clip = footStepSounds[i];
                        GetComponent<AudioSource>().Play();
                        stepCD = waitBetweenSteps;
                    }
                    else
                    {
                        stepCD -= Time.deltaTime;
                    }


                }

            }
        if (dialogueContainer.activeSelf == false && GetComponent<AnimationSwitcher>().lasttrigger != "Die")
        {

            if (dropdown.gameObject.activeInHierarchy == false && dialogueContainer.activeInHierarchy == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                    if (hit.collider != null)
                    {
                        interactable = hit.collider.gameObject;
                        Debug.Log(Vector3.Distance(transform.position, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, transform.position.z)));
                        if (Vector3.Distance(transform.position, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y,transform.position.z))<2)
                        {   
                            if (interactable.tag == "Item")
                            {
                                inventory.AddNewItem(interactable.GetComponent<Item>().item);
                                Destroy(interactable);
                                interactable = null;
                            }
                            else if (interactable.tag == "NPC")
                            {
                                FindObjectOfType<DialogueRunner>().StartDialogue(interactable.GetComponent<NPC>().talkToNode);

                            }
                            else if (interactable.tag == "Interactable")
                            {
                                if (inventory.inventory.Count > 0)
                                {
                                    dropdown.gameObject.SetActive(true);
                                    dropdown.options.Clear();
                                    List<string> options = new List<string>();
                                    options.Add("");
                                    foreach (ItemSO item in GetComponent<InventoryScript>().inventory)
                                    {
                                        options.Add(item.itemName);
                                    }
                                    dropdown.AddOptions(options);
                                }
                                else
                                {
                                    noItemsText.SetActive(true);
                                }

                            }
                            else if (interactable.tag == "Usable")
                            {
                                interactable.GetComponent<Usable>().UseUsable();
                            }

                    }
                }
                

                }
            }
            
        }
        //else
        //{
        //    if (Input.GetButton("Jump"))
        //    {
        //        dialogueHandler.GetComponent<DialogueUIBehaviour>().
        //    }
        //}
       
    }
    public void PickItem()
    {
        if (dropdown.options.Count > 1 && dropdown.value!=0)
        {
            interactable.GetComponent<Interactable>().Interact(inventory.inventory[dropdown.value - 1]);
            dropdown.value = 0;
            interactable = null;
        }
        dropdown.gameObject.SetActive(false);
    }

    
}
