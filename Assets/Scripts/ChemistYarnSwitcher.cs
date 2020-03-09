using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistYarnSwitcher : MonoBehaviour
{
    public GameObject chemist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        chemist.GetComponent<YarnSwitcher>().Switch(null);
    }
}
