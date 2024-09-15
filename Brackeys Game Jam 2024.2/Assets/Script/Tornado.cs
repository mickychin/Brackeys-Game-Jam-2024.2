using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Tornado : MonoBehaviour
{
    [Header("Distance")]
    public float Distance;
    public float MaxDistance;
    public float MinDistance;

    [Header("Size")]
    public float MaxSize;
    public float MinSize;

    [Header("Speed")]
    public float MaxSpeed;
    public float SpeedIncrease;
    public float Speed;
    public float secondsBe4ChangeDir;
    public float secondPasses;
    public int dirGoing;

    [Header("Radio")]
    public Sprite[] textSprites;
    public SpriteRenderer PopUpText;
    public SpriteRenderer PopUpTextBox;
    //public Transform PopUpTransform;
    public float PopUpSecond;
    public float FadeOutSecond;

    [Header("Opacity")]
    public float OpacityMultiplier;
    public float MinOpacity;
    public float maxOpacity;

    [Header("Screen Shakes")]
    public float MaxScreenShakeX;
    public float MaxScreenShakeY;
    public float ScreenShakeDelay;

    [Header("Sky")]
    public float skyOpacityDecRate;
    public float greyskyMultiplier;
    public GameObject skyclear;
    public GameObject skygray;

    [Header("Adrenaline Effects")]
    public GameObject adrenalineEffect;
    public float MinDis2ShowAdre;
    public float MaxDis2ShowAdre;

    [Header("Kill")]
    public Animator backgroundsAnimator;
    public float Distance2Kill;

    [Header("Scores")]
    public Text ScoreDisplay;
    public float score;
    public float scoreMultiplier;
    public bool IsCamRecording;

    [Header("Cam Battery")]
    public bool IsCamOpen;
    public float BatteryDrain;
    public float CurrentBattery;
    public float MaxBattery;
    public GameObject[] BatteryBars;

    [Header("EscapeEnding")]
    public float SecondBe4N; // seconds before the game end after getting too far from tornado
    public float Seconds2FadeInEscape; // seconds take to load the ending in
    public GameObject escapeParent;
    public Text ViewCount;
    public Image escapeBG;
    public Image MainmenuEscape;
    public Image viewsImage;
    public Image YouEscapeImage;
    private float SecondPassesEscape = 0;


    [Header("Values")]
    public bool IsTornadoSpawned;
    public bool GameEnding;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        IsTornadoSpawned = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //spriteRenderer.enabled = false;
        //animator.enabled = false;
        StartCoroutine(ScreenShakes());
    }

    private void FixedUpdate()
    {
        if (IsTornadoSpawned)
        {
            Color c = new Color(skyclear.GetComponent<SpriteRenderer>().color.r, skyclear.GetComponent<SpriteRenderer>().color.g, skyclear.GetComponent<SpriteRenderer>().color.b, skyclear.GetComponent<SpriteRenderer>().color.a * skyOpacityDecRate);
            skyclear.GetComponent<SpriteRenderer>().color = c;
        }
        //size according to distance
        float size = Mathf.Clamp(MaxSize * (MaxDistance - Distance) / MaxDistance, MinSize, MaxSize);
        transform.localScale = new Vector3(size, size, 1f);

        //Position according to distance
        //opacity according to distance
        Color color = new Color(1f, 1f, 1f, Mathf.Clamp((MaxDistance - (Distance * OpacityMultiplier)) / MaxDistance, MinOpacity, maxOpacity));
        spriteRenderer.color = color;
        //order in layer according to distance
        if (Distance / MaxDistance > 99f / 100f)
        {
            spriteRenderer.sortingOrder = -14;
        }
        else if (Distance / MaxDistance > 14f / 15f)
        {
            spriteRenderer.sortingOrder = -9;
        }
        else if ( Distance / MaxDistance > 1.5f / 5f)
        {
            //spriteRenderer.sortingOrder = 10;
        }
        else if (Distance / MaxDistance > 3f / 5f)
        {
            //spriteRenderer.sortingOrder = -2;
        }

        skygray.GetComponent<SpriteRenderer>().color = new Color(1,1,1, Mathf.Clamp((Distance - (MaxDistance * greyskyMultiplier)) / (MaxDistance * greyskyMultiplier), 0, 1));
    }

    private void Update()
    {
        if(Distance < Distance2Kill)
        {
            //dead
            backgroundsAnimator.enabled = true;
            FindObjectOfType<CarMeter>().gear = 0;
            if (FindObjectOfType<GearStick>())
            {
                FindObjectOfType<GearStick>().gear = 0;
            }
        }

        if (IsCamOpen)
        {
            //drain battery
            CurrentBattery -= BatteryDrain / 10; //magic number :O
            //Debug.Log(CurrentBattery / MaxBattery);
            if (CurrentBattery / MaxBattery < 2f / 3f)
            {
                //Debug.Log(CurrentBattery / MaxBattery);
                BatteryBars[2].SetActive(false);
            }
            if (CurrentBattery / MaxBattery < 1f / 3f)
            {
                BatteryBars[1].SetActive(false);
            }
            if (CurrentBattery / MaxBattery < 0f / 3f && FindObjectOfType<CamOffOn>())
            {
                BatteryBars[0].SetActive(false);
                FindObjectOfType<CamOffOn>().RanOutOfBattery();
            }
        }

        if(spriteRenderer.color.a == 0 && !GameEnding)
        {
            // escape ending
            GameEnding = true;
            StartCoroutine(EscapeEnd());
        }

        if(ViewCount.color.a == 1)
        {
            Distance = MaxDistance;
        }

        // adrenaline effects
        SpriteRenderer adreRenderer = adrenalineEffect.GetComponent<SpriteRenderer>();
        Color color = new Color(adreRenderer.color.r, adreRenderer.color.g, adreRenderer.color.b, (MaxDis2ShowAdre - Mathf.Clamp(Distance, MinDis2ShowAdre, MaxDis2ShowAdre)) / (MaxDis2ShowAdre - MinDis2ShowAdre));
        adreRenderer.color = color;
    }

    IEnumerator EscapeEnd()
    {
        // if first coroutine then wait
        if(escapeParent.activeSelf == false)
        {
            float DisplayedScore = Mathf.Round(score * 10) / 10;
            ViewCount.text = DisplayedScore.ToString() + "k";
            yield return new WaitForSeconds(SecondBe4N);
            escapeParent.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(0.1f); //fade in with the fps of 10
            SecondPassesEscape += 0.1f;
            Color color = new Color(1,1,1, SecondPassesEscape / Seconds2FadeInEscape);
            ViewCount.color = color;
            escapeBG.color = color;
            MainmenuEscape.color = color;
            viewsImage.color = color;
            YouEscapeImage.color = color;
        }

        if(ViewCount.color.a != 1)
        {
            StartCoroutine(EscapeEnd());
        }
    }

    IEnumerator ScreenShakes()
    {
        yield return new WaitForSeconds(ScreenShakeDelay);

        float ShakeX = Random.Range(-MaxScreenShakeX * (MaxDistance - Distance) / MaxDistance, MaxScreenShakeX * (MaxDistance - Distance) / MaxDistance);
        float ShakeY = Random.Range(-MaxScreenShakeY * (MaxDistance - Distance) / MaxDistance, MaxScreenShakeY * (MaxDistance - Distance) / MaxDistance);
        Camera.main.transform.position = new Vector3(ShakeX, ShakeY, -10);
        
        StartCoroutine(ScreenShakes());
    }

    IEnumerator UpdateTornado()
    {
        //Debug.Log("Update");
        yield return new WaitForSeconds(0.1f);

        if(dirGoing == 0)
        {
            Speed *= 0.9f;
        }
        else
        {
            Speed = Mathf.Clamp(Speed + (dirGoing * SpeedIncrease / 10), -MaxSpeed, MaxSpeed);
        }

        if(secondPasses > secondsBe4ChangeDir)
        {
            secondPasses = 0; //reset timer
            //make random dir change;
            dirGoing = Random.Range(-1,2);
            StartCoroutine(PopUpRadioTextBox(dirGoing));
        }
        else
        {
            secondPasses += 0.1f;
        }

        Distance += Speed;

        if (IsCamRecording)
        {
            score += scoreMultiplier * (MaxDistance - Distance) / MaxDistance;
            float DisplayedScore = Mathf.Round(score * 10) / 10;
            ScoreDisplay.text = DisplayedScore.ToString() + "k";
        }

        //size according to distance
        //transform.localScale = new Vector3(MaxDistance - Distance, MaxDistance - Distance, 1f);
        //Position according to distance
        //opacity according to distance
        //order in layer according to distance
        StartCoroutine(UpdateTornado());
    }

    public void SpawnTornado()
    {
        spriteRenderer.enabled = true;
        animator.enabled = true;
    }

    public void TornadoSpawned()
    {
        IsTornadoSpawned = true;
        StartCoroutine(UpdateTornado());
    }

    IEnumerator PopUpRadioTextBox(int direction)
    {
        PopUpTextBox.gameObject.GetComponent<AudioSource>().Play();
        Debug.Log(direction + 1);
        PopUpText.sprite = textSprites[direction + 1];
        //PopUpTextBox.gameObject.SetActive(true);

        if(PopUpTextBox.GetComponent<Animator>().enabled == false)
        {
            PopUpTextBox.GetComponent<Animator>().enabled = true;
        }
        else
        {
            PopUpTextBox.GetComponent<Animator>().SetBool("Fade", false);
        }

        yield return new WaitForSeconds(PopUpSecond);

        PopUpTextBox.GetComponent<Animator>().SetBool("Fade", true);
    }

}
