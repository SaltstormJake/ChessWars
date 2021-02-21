using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public enum Ranks
    {
        PAWN,
        BISHOP,
        ROOK,
        QUEEN,
        KNIGHT
    }

    bool selected = false;

    [SerializeField] public bool playerOne = true;

    Ranks rank;

    [SerializeField] Sprite[] pieceSprites = null;

    [SerializeField] Sprite[] highlightedPieces = null;

    SpriteRenderer thisSprite = null;

    int player = 0;

    void Awake()
    {
        thisSprite = gameObject.GetComponent<SpriteRenderer>();
        if (playerOne)
            player = 1;
        else
            player = 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        rank = Ranks.PAWN;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Promote()
    {
        switch (rank)
        {
            case Ranks.PAWN:
                rank = Ranks.BISHOP;
                thisSprite.sprite = pieceSprites[1];
                break;
            case Ranks.BISHOP:
                rank = Ranks.ROOK;
                thisSprite.sprite = pieceSprites[2];
                break;
            case Ranks.ROOK:
                rank = Ranks.QUEEN;
                thisSprite.sprite = pieceSprites[3];
                break;
            case Ranks.QUEEN:
                rank = Ranks.KNIGHT;
                thisSprite.sprite = pieceSprites[4];
                break;
            case Ranks.KNIGHT:
                Debug.Log("Player wins");
                break;
            default:
                break;
        }
    }

    public bool CanMove(float x, float y)
    {
        int destX = Mathf.FloorToInt(x);
        int destY = Mathf.FloorToInt(y);
        int currX = Mathf.FloorToInt(transform.position.x);
        int currY = Mathf.FloorToInt(transform.position.y);
        switch (rank)
        {
            case Ranks.PAWN:
                return CanMovePawn(destX, destY, currX, currY);
            case Ranks.BISHOP:
                return CanMoveBishop(destX, destY, currX, currY);
            case Ranks.ROOK:
                return CanMoveRook(destX, destY, currX, currY);
            case Ranks.QUEEN:
                return CanMoveQueen(destX, destY, currX, currY);
            case Ranks.KNIGHT:
                return CanMoveKnight(destX, destY, currX, currY);
            default:
                return false;
        }
    }

    bool CanMovePawn(int destX, int destY, int currX, int currY)
    {
        return (Mathf.Abs(destX - currX) + Mathf.Abs(destY - currY) == 1);
    }

    bool CanMoveBishop(int destX, int destY, int currX, int currY)
    {
        return (Mathf.Abs(destX - currX) == Mathf.Abs(destY - currY) && (Mathf.Abs(destX - currX) != 0));
    }

    bool CanMoveRook(int destX, int destY, int currX, int currY)
    {
        return ((Mathf.Abs(destX - currX) > 0) ^ (Mathf.Abs(destY - currY) > 0));
    }

    bool CanMoveQueen(int destX, int destY, int currX, int currY)
    {
        return (CanMoveRook(destX, destY, currX, currY) || CanMoveBishop(destX, destY, currX, currY));
    }

    bool CanMoveKnight(int destX, int destY, int currX, int currY)
    {
        int x = Mathf.Abs(destX - currX);
        int y = Mathf.Abs(destY - currY);
        return ((x == 2 && y == 1) || (x == 1 && y == 2));
    }

    public void ChoosePiece()
    {
        switch (rank)
        {
            case Ranks.PAWN:
                thisSprite.sprite = highlightedPieces[0];
                break;
            case Ranks.BISHOP:
                thisSprite.sprite = highlightedPieces[1];
                break;
            case Ranks.ROOK:
                thisSprite.sprite = highlightedPieces[2];
                break;
            case Ranks.QUEEN:
                thisSprite.sprite = highlightedPieces[3];
                break;
            case Ranks.KNIGHT:
                thisSprite.sprite = highlightedPieces[4];
                break;
            default:
                break;
        }
    }


}
