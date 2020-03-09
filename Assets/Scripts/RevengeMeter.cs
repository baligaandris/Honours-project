using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevengeMeter : MonoBehaviour
{
    public int revenge = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseRevenege(int amount)
    {
        revenge += amount;
        GetComponent<Slider>().value = revenge;
    }
}
