using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public GameObject[] pieces = new GameObject[64];
    [SerializeField] BitboardScript p1Bits = null;
    [SerializeField] BitboardScript p2Bits = null;
    [SerializeField] GameObject[] piecePrefabs = null;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateBoard()
    {

    }

}
