using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    public Sprite[] Cut2To5;
    public Image Cut2;
    public int cutsceneScene = 1;
    private Animator Cut1To2Anim;

    [Header("Cut2")]
    public Image[] Cut2TextBox;
    public Text[] Cut2Text;

    [Header("Cut3")]
    public Image[] Cut3TextBox;
    public Text[] Cut3Text;

    // Start is called before the first frame update
    void Start()
    {
        Cut1To2Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            NextScene();
        }
    }

    void NextScene()
    {
        if(cutsceneScene == 1)
        {
            Cut1To2Anim.enabled = true;
            Cut2.gameObject.GetComponent<AudioSource>().Play();
        }
        else if(cutsceneScene >= 2 && cutsceneScene < Cut2To5.Length + 2)
        {
            Cut2.sprite = Cut2To5[cutsceneScene - 2];
        }
        else
        {
            SceneManager.LoadScene("Game");
        }

        if(cutsceneScene == 2)
        {
            Cut2.gameObject.GetComponent<AudioSource>().Play();
            StopAllCoroutines();
            StartCoroutine(FadeIn(1f, Cut3TextBox, Cut3Text)); // 1f is magic number! HAHA
            Color inviscolor = new Color(0, 0, 0, 0);
            foreach(Image image in Cut2TextBox)
            {
                image.color = inviscolor;
            }
        }

        if(cutsceneScene == 3)
        {
            StopAllCoroutines();
            Color inviscolor = new Color(0, 0, 0, 0);
            foreach (Image image in Cut3TextBox)
            {
                image.color = inviscolor;
            }
        }

        cutsceneScene++;
    }

    IEnumerator FadeIn(float second, Image[] imagefadein, Text[] textfadein)
    {
        Color color = new Color(1, 1, 1, imagefadein[0].color.a + (0.1f / second));
        foreach (Image image in imagefadein)
        {
            image.color = color;
        }

        foreach (Text text in textfadein)
        {
            text.color = color;
        }

        yield return new WaitForSeconds(0.1f);

        if (imagefadein[0].color.a != 1)
        {
            StartCoroutine(FadeIn(second, imagefadein, textfadein));
        }
    }

    public void startFadeInCut2()
    {
        Debug.Log("FadeIn2");
        StartCoroutine(FadeIn(1f, Cut2TextBox, Cut2Text)); // 1f is magic number!
    }
}
