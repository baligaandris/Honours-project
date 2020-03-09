using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTagFollow : MonoBehaviour
{
    private Vector3 relativePostition;
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        relativePostition = transform.position - character.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = character.transform.position + relativePostition;
    }
}
