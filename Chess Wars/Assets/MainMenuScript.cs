using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] Button playButton = null;
    [SerializeField] Button instructionsButton = null;
    [SerializeField] Button quitButton = null;
    [SerializeField] Button creditsButton = null;
    [SerializeField] Button backButton = null;
    [SerializeField] Button creditsBackButton = null;
    [SerializeField] GameObject topLevel = null;
    [SerializeField] GameObject bottomLevel = null;
    [SerializeField] GameObject creditsLevel = null;

    [SerializeField] AudioClip clickSound = null;
    [SerializeField] AudioClip musicClip = null;
    AudioSource sound = null;
    void Awake()
    {
        sound = GetComponent<AudioSource>();
        sound.loop = true;
        playButton.onClick.AddListener(OnClickPlay);
        instructionsButton.onClick.AddListener(OnClickInstructions);
        backButton.onClick.AddListener(OnClickBack);
        quitButton.onClick.AddListener(OnClickQuit);
        creditsButton.onClick.AddListener(OnClickCredits);
        creditsBackButton.onClick.AddListener(OnClickCreditsBack);

    }

    // Start is called before the first frame update
    void Start()
    {
        bottomLevel.SetActive(false);
        creditsLevel.SetActive(false);
        sound.clip = musicClip;
        sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClickPlay()
    {
        sound.PlayOneShot(clickSound);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    void OnClickInstructions()
    {
        sound.PlayOneShot(clickSound);
        topLevel.SetActive(false);
        bottomLevel.SetActive(true);
    }

    void OnClickBack()
    {
        sound.PlayOneShot(clickSound);
        bottomLevel.SetActive(false);
        topLevel.SetActive(true);
    }

    void OnClickCredits()
    {
        sound.PlayOneShot(clickSound);
        topLevel.SetActive(false);
        creditsLevel.SetActive(true);
    }

    void OnClickCreditsBack()
    {
        sound.PlayOneShot(clickSound);
        creditsLevel.SetActive(false);
        topLevel.SetActive(true);
    }

    void OnClickQuit()
    {
        sound.PlayOneShot(clickSound);
        Application.Quit();
    }
}
