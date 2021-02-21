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

    [SerializeField] private BoardScript board = null;

    int iterator = 0;

    void Awake()
    {
        thisSprite = gameObject.GetComponent<SpriteRenderer>();
        if (!playerOne)
            iterator += 5;
    }
    // Start is called before the first frame update
    void Start()
    {
        rank = Ranks.PAWN;
        thisSprite.sprite = pieceSprites[0 + iterator];
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
                thisSprite.sprite = pieceSprites[1 + iterator];
                break;
            case Ranks.BISHOP:
                rank = Ranks.ROOK;
                thisSprite.sprite = pieceSprites[2 + iterator];
                break;
            case Ranks.ROOK:
                rank = Ranks.QUEEN;
                thisSprite.sprite = pieceSprites[3 + iterator];
                break;
            case Ranks.QUEEN:
                rank = Ranks.KNIGHT;
                thisSprite.sprite = pieceSprites[4 + iterator];
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
        Debug.Log("DestX:" + destX + " CurrX:" + currX + "\nDestY:" + destY + " CurrY:" + currY);
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
        if (selected)
        {
            switch (rank)
            {
                case Ranks.PAWN:
                    thisSprite.sprite = pieceSprites[0 + iterator];
                    break;
                case Ranks.BISHOP:
                    thisSprite.sprite = pieceSprites[1 + iterator];
                    break;
                case Ranks.ROOK:
                    thisSprite.sprite = pieceSprites[2 + iterator];
                    break;
                case Ranks.QUEEN:
                    thisSprite.sprite = pieceSprites[3 + iterator];
                    break;
                case Ranks.KNIGHT:
                    thisSprite.sprite = pieceSprites[4 + iterator];
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (rank)
            {
                case Ranks.PAWN:
                    thisSprite.sprite = highlightedPieces[0 + iterator];
                    break;
                case Ranks.BISHOP:
                    thisSprite.sprite = highlightedPieces[1 + iterator];
                    break;
                case Ranks.ROOK:
                    thisSprite.sprite = highlightedPieces[2 + iterator];
                    break;
                case Ranks.QUEEN:
                    thisSprite.sprite = highlightedPieces[3 + iterator];
                    break;
                case Ranks.KNIGHT:
                    thisSprite.sprite = highlightedPieces[4 + iterator];
                    break;
                default:
                    break;
            }
        }
        selected = !selected;
    }

    public void MovePiece(int row, int col)
    {
        if (playerOne)
        {
            board.bits.SetP1State((int)transform.position.y, (int)transform.position.x);
        }
        else
        {
            board.bits.SetP2State((int)transform.position.y, (int)transform.position.x);
        }
        Vector3 pos = transform.position;
        pos.x = col;
        pos.y = row;
        transform.position = pos;
        ChoosePiece();
    }

}
