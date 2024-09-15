using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private float startingAngle;
    private float startingRotation;
    private Vector2 mousePos;
    private bool ChangingVolume;

    // Start is called before the first frame update
    void Start()
    {
        ChangingVolume = false;
        audioSource = FindObjectOfType<Radio>().GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (ChangingVolume)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            //Debug.Log(transform.localRotation.eulerAngles.z);
            transform.eulerAngles = new Vector3(0f, 0f, startingRotation + angle - startingAngle);
            //Debug.Log(transform.localRotation.eulerAngles.z / 360);
            audioSource.volume =  transform.localRotation.eulerAngles.z / 360;
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
        ChangingVolume = true;
    }

    private void OnMouseUp()
    {
        ChangingVolume = false;
    }
}
