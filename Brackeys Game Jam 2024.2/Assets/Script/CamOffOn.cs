using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOffOn : MonoBehaviour
{
    [Header("screen")]
    public GameObject batteryUI;
    public GameObject camUI;
    public GameObject camScreen;
    public GameObject blankScreen;
    public GameObject AmountOfPeeopleUI;
    public bool IsOn;
    public bool IsRanOutOfBattery = false;
    private Cam cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Cam>();
        // remove it if not start with cam off
        IsOn = false;
        blankScreen.SetActive(true);
        camScreen.SetActive(false);
        camUI.SetActive(false);
        batteryUI.SetActive(false);
        AmountOfPeeopleUI.SetActive(false);
        cam.TurnRec(false);
    }

    private void OnMouseDown()
    {
        if (IsRanOutOfBattery)
        {
            return;
        }

        if (!IsOn)
        {
            FindObjectOfType<Tornado>().IsCamOpen = true;
            IsOn = true;
            blankScreen.SetActive(false);
            camScreen.SetActive(true);
            camUI.SetActive(true);
            batteryUI.SetActive(true);
            AmountOfPeeopleUI.SetActive(true);
            //cam.TurnRec(true);
        }
        else
        {
            FindObjectOfType<Tornado>().IsCamOpen = false;
            IsOn = false;
            blankScreen.SetActive(true);
            camScreen.SetActive(false);
            camUI.SetActive(false);
            batteryUI.SetActive(false);
            AmountOfPeeopleUI.SetActive(false);
            cam.TurnRec(false);
        }
    }

    public void RanOutOfBattery()
    {
        IsRanOutOfBattery = true;
        FindObjectOfType<Tornado>().IsCamOpen = false;
        IsOn = false;
        blankScreen.SetActive(true);
        camScreen.SetActive(false);
        camUI.SetActive(false);
        batteryUI.SetActive(false);
        AmountOfPeeopleUI.SetActive(false);
        cam.TurnRec(false);
    }
}
