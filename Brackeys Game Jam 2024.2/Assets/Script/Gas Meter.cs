using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMeter : MonoBehaviour
{
    public GameObject carGasMeter;
    const float MaxMeterRotation = -210;
    public float currentGas;
    public float maxGas;
    public float gasLoseMultiplier;
    public int gear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(-MaxMeterRotation * Mathf.Abs(currentGas / maxGas));
        carGasMeter.transform.localEulerAngles = new Vector3(0, 0, MaxMeterRotation * Mathf.Abs(currentGas / maxGas));

        if(currentGas <= 0)
        {
            currentGas = 0;
            FindObjectOfType<CarMeter>().gear = 0;
            FindObjectOfType<Tornado>().dirGoing = -2;
        }
    }

    private void FixedUpdate()
    {
        if(gear == 1 || gear == 2)
        {
            currentGas -= gasLoseMultiplier;
        }
    }
}
