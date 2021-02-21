using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerTextScript : MonoBehaviour
{
    bool player1 = true;
    Text text = null;
    bool won = false;
    [SerializeField] Text winningText = null;
    [SerializeField] PlayerScript player;
    [SerializeField] AudioSource musicSource = null;
    private AudioSource sound = null;
    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        text = this.GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //text.text = "White's Turn";
        text.text = "";
        winningText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPlayer()
    {
        if (!won)
        {
            //if (player1)
            //    text.text = "Black's Turn";
            //else
            //    text.text = "White's Turn";
            player1 = !player1;
        }
    }

    public void PlayerWins(bool playerOne)
    {
        winningText.gameObject.SetActive(true);
        if (playerOne)
        {
            winningText.text = "White Wins";
        }
        else
        {
            winningText.text = "Black Wins";
        }
        won = true;
        Destroy(player);
        sound.Play();
        musicSource.Stop();
        Invoke("ReturnToTitleScreen", 3.0f);
    }

    void ReturnToTitleScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("titleScreen");
    }
}
