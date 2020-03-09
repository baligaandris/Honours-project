using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPCMovement : MonoBehaviour
{
    public List<GameObject> nodes;
    public List<float> waitTimes;
    public List<bool> stopAtNode;
    Rigidbody2D rb;
    int currentNodeIndex=0;
    public float speed=1;
    float counter = 0;
    public GameObject dialogueContainer;

    
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //rb.velocity = (nodes[currentNodeIndex].transform.position - transform.position).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueContainer.activeSelf==false)
        {
            if (nodes.Count > 0)
            {
                if (counter <= 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, nodes[currentNodeIndex].transform.position, speed * Time.deltaTime);
                    if (transform.position!=nodes[currentNodeIndex].transform.position && GetComponent<AnimationSwitcher>().lasttrigger !="Walk")
                    {
                        GetComponent<AnimationSwitcher>().SwitchToNewAnimation("Walk");
                        
                    }
                    if (transform.position == nodes[currentNodeIndex].transform.position)
                    {
                        if (stopAtNode[currentNodeIndex]==false)
                        {
                            counter = waitTimes[currentNodeIndex];
                            if (currentNodeIndex < nodes.Count - 1)
                            {

                                currentNodeIndex++;
                                //rb.velocity = (nodes[currentNodeIndex].transform.position - transform.position).normalized * speed;
                            }
                            else
                            {
                                currentNodeIndex = 0;
                                //rb.velocity = (nodes[currentNodeIndex].transform.position - transform.position).normalized * speed;
                            }
                            if (nodes[currentNodeIndex].transform.position.x - transform.position.x!=0)
                            {
                                transform.localScale = new Vector3(Mathf.Sign(nodes[currentNodeIndex].transform.position.x - transform.position.x), 1);
                            }
                            
                        }
                        else
                        {
                            if (GetComponent<AnimationSwitcher>().lasttrigger != "Idle")
                            {
                                GetComponent<AnimationSwitcher>().SwitchToNewAnimation("Idle");
                            }
                            
                        }
                        
                    }
                }
                else
                {
                    counter -= Time.deltaTime;
                    if (GetComponent<AnimationSwitcher>().lasttrigger != "Idle")
                    {
                        GetComponent<AnimationSwitcher>().SwitchToNewAnimation("Idle");
                    }
                }
            }
            
        }

        
    }
    [YarnCommand("Go")]
    public void MoveOnFromNode(string nodeNumber)
    {
        stopAtNode[int.Parse(nodeNumber)] = false;

    }

    private void OnBecameVisible()
    {
        speed = 3;
    }

    private void OnBecameInvisible()
    {
        speed = 30;
    }

}
