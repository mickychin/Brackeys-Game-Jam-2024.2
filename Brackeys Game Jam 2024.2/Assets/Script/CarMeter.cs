using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMeter : MonoBehaviour
{
    [Header("Speed Meter")]
    public GameObject SpeedMeter;
    public float SpeedIncrease;
    public float MaxSpeed;
    public float Speed;
    public float friction;
    public int gear;
    const float MaxSpeedMeterRotation = 220f;

    [Header("Gear Meter")]
    public GameObject GearMeter;
    public int MaxGearRound;
    public int CurrentGearRound;
    const float MaxGearMeterRotation = 155f;

    [Header("Distance Travel")]
    public float distanceTravelled;
    public float distance2SpawnTornado;

    [Header("Sound effect")]
    public AudioClip IdleSound;
    public AudioClip MovingSound;
    private AudioSource audioSource;

    private GearStick gearstick;
    private Tornado tornado;
    private GasMeter gasMeter;
    public Animator BGanimator;

    // Start is called before the first frame update
    private void Awake()
    {
        BGanimator.speed = Speed / 10;
    }

    void Start()
    {
        //StartCoroutine(SpeedChanging()); // remove this later
        audioSource = GetComponent<AudioSource>();
        gasMeter = FindObjectOfType<GasMeter>();
        gearstick = FindObjectOfType<GearStick>();
        tornado = FindObjectOfType<Tornado>();
        CurrentGearRound = 0;
        //StartCoroutine(SpeedChanging());
    }

    public void ChangeGear(int GearChange)
    {
        if(gear != GearChange)
        {
            gear = GearChange;
            StopAllCoroutines();
            StartCoroutine(SpeedChanging());
        }
    }

    private IEnumerator SpeedChanging()
    {
        //Debug.Log("HI");
        yield return new WaitForSeconds(0.1f);

        //Gear P R D
        if (Speed < MaxSpeed && gear == 2)
        {
            //ITS ABOUT DRIVE ITS ABOUT POWER
            audioSource.clip = MovingSound;
            audioSource.pitch = Mathf.Clamp(audioSource.pitch + 0.05f, 0, Random.Range(2.9f,3f));
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if (Speed < 0)
            {
                Speed *= friction;
            }
            else
            {
                BGanimator.SetBool("Forward", true);
            }

            if(gasMeter.currentGas > 0)
            {
                Speed += SpeedIncrease / 10;
            }
            //SpeedMeter.transform.localEulerAngles = new Vector3(0, 0, -MaxSpeedMeterRotation * Speed / MaxSpeed);
        }
        else if (gear == 1 && -MaxSpeed / 2 < Speed) // this /2 is also magic number but normally going backward is slower than forward
        {
            //REVERSE
            audioSource.clip = MovingSound;
            audioSource.pitch = Mathf.Clamp(audioSource.pitch + 0.1f, 0, 2);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if (Speed > 0)
            {
                Speed *= friction;
            }
            else
            {
                BGanimator.SetBool("Forward", false);
            }
            Speed -= SpeedIncrease / 10;
        }
        else if(gear == 0)
        {
            //STOPU
            audioSource.pitch = Mathf.Clamp(audioSource.pitch - 0.1f, 1, 3);
            audioSource.clip = IdleSound;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            Speed *= friction;
            if((Speed < 0.1 && Speed > 0) || (Speed > -0.1 && Speed < 0))
            {
                Speed = 0;
                StopAllCoroutines();
            }
            //SpeedMeter.transform.localEulerAngles = new Vector3(0, 0, -MaxSpeedMeterRotation * Speed / MaxSpeed);
        }
        SpeedMeter.transform.localEulerAngles = new Vector3(0, 0, -MaxSpeedMeterRotation * Mathf.Abs(Speed) / MaxSpeed);
        BGanimator.speed = Mathf.Abs(Speed / 10); //this divide by 10 is magic number probably

        //Gear stuff, code to make gear animate;
        if(CurrentGearRound >= 1 && ((Mathf.Abs(Speed) / (MaxSpeed / MaxGearRound)) - CurrentGearRound) < 0.2f)
        {
            //move it down to reset the meter
            GearMeter.transform.localEulerAngles = new Vector3(0, 0, -MaxGearMeterRotation * 1 / ((Mathf.Abs(Speed) / ((MaxSpeed / MaxGearRound)) - CurrentGearRound) * 100));
        }
        else
        {
            //move meter normally
            GearMeter.transform.localEulerAngles = new Vector3(0, 0, -MaxGearMeterRotation * ((Mathf.Abs(Speed) / (MaxSpeed / MaxGearRound)) - CurrentGearRound));
        }

        //change gear :)
        if ((Mathf.Abs(Speed) / (MaxSpeed / MaxGearRound)) - CurrentGearRound >= 1)
        {
            //change gear
            CurrentGearRound++;
        }
        else if ((Mathf.Abs(Speed) / (MaxSpeed / MaxGearRound)) - CurrentGearRound < 0)
        {
            CurrentGearRound--;
        }

        //CHANGE DISTANCE
        if(tornado.IsTornadoSpawned == true)
        {
            tornado.Distance -= Speed;
        }

        if(distanceTravelled > distance2SpawnTornado)
        {
            tornado.SpawnTornado();
        }

        distanceTravelled += Speed / 10;

        StartCoroutine(SpeedChanging());
    }
}
