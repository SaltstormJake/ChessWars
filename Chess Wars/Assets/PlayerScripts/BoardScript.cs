using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    GameObject[] pieces = new GameObject[64];
    BitboardScript bits = null;

    [SerializeField] GameObject[] piecePrefabs = null;

    void Awake()
    {
        bits = gameObject.GetComponent<BitboardScript>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
