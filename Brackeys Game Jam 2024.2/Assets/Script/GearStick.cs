using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearStick : MonoBehaviour
{
    private AudioSource audioSource;
    public int gear; // Gear 0 = P, 1 = R, 2 = D
    private float startingAngle;
    private float startingRotation;
    private bool ChangingGear;
    private Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangingGear = false;
        //FindObjectOfType<CarMeter>().ChangeGear(gear);
    }

    private void FixedUpdate()
    {
        if (ChangingGear)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            //Debug.Log(angle);
            transform.eulerAngles = new Vector3(0f, 0f, startingRotation + angle - startingAngle);
        }
    }

    private void OnMouseDown()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //Debug.Log(angle);
        startingAngle = angle;
        startingRotation = transform.localRotation.eulerAngles.z;
        //Debug.Log(startingRotation);
        ChangingGear = true;    
    }

    private void OnMouseUp()
    {
        audioSource.Play();
        ChangingGear = false;
        //Debug.Log(transform.localRotation.eulerAngles.z);

        if (transform.localRotation.eulerAngles.z >= 345f && transform.localRotation.eulerAngles.z < 355f)
        {
            // R ( REVERSE )
            gear = 1;
            transform.eulerAngles = new Vector3 (0f, 0f, -10f);
        }
        else if (transform.localRotation.eulerAngles.z >= 170f && transform.localRotation.eulerAngles.z < 345f)
        {
            // D ( DRIVE )
            gear = 2;
            transform.eulerAngles = new Vector3(0f, 0f, -20f);
        }
        else
        {
            // P ( PARK )
            gear = 0;
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        FindObjectOfType<CarMeter>().ChangeGear(gear);
        FindObjectOfType<GasMeter>().gear = gear;
    }
}
