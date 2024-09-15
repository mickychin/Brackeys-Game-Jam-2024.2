using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class DeadBlur : MonoBehaviour
{
    [Range(0, 1)] public float weight;
    public GameObject DeactiveAfterDead;

    private void Update()
    {
        if (GameObject.FindWithTag("FadeInAFterDead"))
        {
            GameObject.FindWithTag("FadeInAFterDead").GetComponent<Animator>().enabled = true;
            Destroy(gameObject);
        }
    }

    public void BlurCamera()
    {
        Camera.main.GetComponent<PostProcessVolume>().weight = weight;
        //volume.weight = 1f;
    }

    public void LoadMainMenu()
    {
        DeactiveAfterDead.SetActive(false);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
