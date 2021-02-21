using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerTextScript : MonoBehaviour
{
    bool player1 = true;
    Text text = null;
    bool won = false;
    [SerializeField] PlayerScript player;
    private void Awake()
    {
        text = this.GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Player 1's Turn";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPlayer()
    {
        if (!won)
        {
            if (player1)
                text.text = "Player 2's Turn";
            else
                text.text = "Player 1's Turn";
            player1 = !player1;
        }
    }

    public void PlayerWins(bool playerOne)
    {
        if (playerOne)
        {
            text.text = "Player One Wins";
        }
        else
        {
            text.text = "Player Two Wins";
        }
        won = true;
        Destroy(player);
    }
}
