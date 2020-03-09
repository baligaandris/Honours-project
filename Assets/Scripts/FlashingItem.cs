using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingItem : MonoBehaviour
{
    private SpriteRenderer sr;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Sign(speed)==-1 && (sr.color.a - Time.deltaTime * speed)>=1)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            speed = -speed;
        }
        else if (Mathf.Sign(speed) == 1 && (sr.color.a - Time.deltaTime * speed) <= 0)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            speed = -speed;
        }

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime * speed);
        Mathf.PingPong(sr.color.a, 1);

        //if (sr.color.a <= 0 || sr.color.a >= 1)
        //{
        //    speed = -speed;
        //}

    }
}
