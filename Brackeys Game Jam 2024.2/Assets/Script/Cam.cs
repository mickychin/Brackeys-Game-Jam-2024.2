using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [Header("Red Live Dot")]
    public GameObject LiveDot;
    public float TimeBetweenBlink;

    [Header("Screen")]
    public bool IsRecording;

    public ChangePOV changePOV;

    private Tornado tornado;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(blinking());
        IsRecording = false;
        tornado = FindObjectOfType<Tornado>();
    }

    private void Awake()
    {
        tornado = FindObjectOfType<Tornado>();
    }

    IEnumerator blinking()
    {
        yield return new WaitForSeconds(TimeBetweenBlink);
        if (LiveDot.activeInHierarchy == true)
        {
            LiveDot.SetActive(false);
        }
        else
        {
            LiveDot.SetActive(true);
        }

        StartCoroutine(blinking());
    }

    private void OnMouseDown()
    {
        if(!FindObjectOfType<CamOffOn>().IsOn)
        {
            return;
        }

        //Debug.Log(!IsRecording);
        TurnRec(!IsRecording);

        /*
        if (IsRecording)
        {
            LiveDot.SetActive(false);
            IsRecording = false;
            StopAllCoroutines();
        }
        else
        {
            LiveDot.SetActive(true);
            IsRecording = true;
            StartCoroutine(blinking());
        }
        */
    }

    public void TurnRec(bool Rec)
    {
        if (!Rec)
        {
            tornado.IsCamRecording = false;
            changePOV.IsCamRec = false;
            LiveDot.SetActive(false);
            IsRecording = false;
            StopAllCoroutines();
        }
        else
        {
            tornado.IsCamRecording = true;
            changePOV.IsCamRec = true;
            LiveDot.SetActive(true);
            IsRecording = true;
            StartCoroutine(blinking());
        }
    }
}
