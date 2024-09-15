using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float Speed;
    public bool GoingLeft;
    public float X2KillPos;
    public float X2SpawnPos;
    public float MinOpacity;
    public float MaxOpacity;
    public float MinY;
    public float MaxY;
    public float MaxBlack;
    private SpriteRenderer spriteRenderer;
    private Tornado tornado;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tornado = FindObjectOfType<Tornado>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tornado.skyclear.GetComponent<SpriteRenderer>().color.a > 0.1)
        {
            Color color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1 - tornado.skyclear.GetComponent<SpriteRenderer>().color.a);
            //Debug.Log(color.a);
            spriteRenderer.color = color;
            //Debug.Log(spriteRenderer.color.a);
        }

        if (transform.position.x < X2KillPos && GoingLeft)
        {
            resetTornado();
        }
        else if(transform.position.x > X2KillPos && !GoingLeft)
        {
            resetTornado();
        }

        float c = MaxBlack * tornado.Distance / tornado.MaxDistance;
        spriteRenderer.color = new Color(c, c, c, spriteRenderer.color.a);
    }

    private void resetTornado()
    {
        //Debug.Log("Reset");
        if(tornado.skyclear.GetComponent<SpriteRenderer>().color.a > 0.1)
        {
            Color c = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1 - tornado.skyclear.GetComponent<SpriteRenderer>().color.a);
            spriteRenderer.color = c;
        }
        else
        {
            transform.position = new Vector2(X2SpawnPos, Random.Range(MinY, MaxY));
            Color color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Random.Range(MinOpacity, MaxOpacity));
            spriteRenderer.color = color;
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(Speed, 0, 0);
    }
}
