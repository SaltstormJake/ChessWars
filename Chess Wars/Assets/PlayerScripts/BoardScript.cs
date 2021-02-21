using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public GameObject[] pieces = new GameObject[64];
    [SerializeField] public BitboardScript bits = null;
    [SerializeField] GameObject[] piecePrefabs = null;

    [SerializeField] GameObject space = null;
    [SerializeField] Material black = null;

    bool[] p1Board = new bool[64];
    bool[] p2Board = new bool[64];

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        bool checker = false;
        for(int i = 0; i < 8; ++i)
        {
            checker = !checker;
            for(int j = 0; j < 8; ++j)
            {
                GameObject thisSpace = Instantiate(space, new Vector3(j, i, 100), Quaternion.identity);
                if(checker)
                    thisSpace.GetComponent<Renderer>().material = black;
                checker = !checker;
            }
        }
        SetP1State(0, 0);
        SetP1State(7, 0);
        SetP2State(0, 7);
        SetP2State(7, 7);
    }

    public bool GetP1State(int row, int col)
    {
        return p1Board[row * 8 + col];
    }

    public bool GetP2State(int row, int col)
    {
        return p2Board[row * 8 + col];
    }

    public void SetP1State(int row, int col)
    {
        p1Board[row * 8 + col] = !p1Board[row * 8 + col];
    }

    public void SetP2State(int row, int col)
    {
        p2Board[row * 8 + col] = !p2Board[row * 8 + col];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintBoard()
    {
        Debug.Log("P1 board:");
        for(int i = 0; i < 8; ++i)
        {
            string row = "";
            for(int j = 0; j < 8; ++j)
            {
                if (p1Board[i * 8 + j] == true)
                    row += "1";
                else
                    row += "0";
            }
            Debug.Log(row);
        }
    }

}
