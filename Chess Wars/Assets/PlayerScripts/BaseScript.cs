using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool CheckBounds(float x, float y)
    {
        return (x >= 0 && x <= 8 && y >= 0 && y <= 8);
    }

    protected bool CanMove(float x, float y)
    {
        return false;
    }
}
