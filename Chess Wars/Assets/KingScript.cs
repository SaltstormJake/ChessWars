using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingScript : MonoBehaviour
{
    [SerializeField] Sprite p1Turn = null;
    [SerializeField] Sprite p2Turn = null;
    [SerializeField] PlayerScript player = null;
    SpriteRenderer thisSprite = null;

    void Awake()
    {
        thisSprite = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.p1Turn)
            thisSprite.sprite = p1Turn;
        else
            thisSprite.sprite = p2Turn;
    }
}
