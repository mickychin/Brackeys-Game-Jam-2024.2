using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEff : MonoBehaviour
{
    Tornado tornado;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tornado = FindObjectOfType<Tornado>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, tornado.GetComponent<SpriteRenderer>().color.a);
        if (tornado.GetComponent<SpriteRenderer>().enabled == true)
        {
            spriteRenderer.enabled = true;
        }
    }
}
