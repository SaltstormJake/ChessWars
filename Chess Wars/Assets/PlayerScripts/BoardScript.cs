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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateBoard()
    {

    }

}
