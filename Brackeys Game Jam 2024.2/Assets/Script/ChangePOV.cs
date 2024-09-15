    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePOV : MonoBehaviour
{
    public bool IsCamRec;
    public bool ChangeToCam;
    public GameObject Car;
    public GameObject Camera;

    private void OnMouseDown()
    {
        if (ChangeToCam)
        {
            Camera.SetActive(true);
            Car.SetActive(false);
            FindObjectOfType<Cam>().TurnRec(IsCamRec);
        }
        else
        {
            Camera.SetActive(false);
            Car.SetActive(true);
        }
    }
}
