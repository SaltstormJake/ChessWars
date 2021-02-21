using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    private BoardScript board = null;

    public PlayerScript player = null;

    [SerializeField] PlayerTextScript textScript = null;

    int iterator = 0;

    AudioSource sound = null;

    [SerializeField] AudioClip select = null;
    [SerializeField] AudioClip attack = null;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
        thisSprite = gameObject.GetComponent<SpriteRenderer>();
        if (!playerOne)
            iterator += 5;
        board = GameObject.Find("Board").GetComponent<BoardScript>();
        player = GameObject.Find("PlayerManager").GetComponent<PlayerScript>();
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
        sound.clip = attack;
        sound.Play();
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
                textScript.PlayerWins(playerOne);
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
        if (playerOne)
        {
            if (board.GetP1State(destY, destX))
                return false;
        }
        else
        {
            if (board.GetP2State(destX, destY))
                return false;
        }
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
        //return (Mathf.Abs(destX - currX) + Mathf.Abs(destY - currY) == 1);
        return (Mathf.Abs(destX - currX) <= 1 && Mathf.Abs(destY - currY) <= 1
            && Mathf.Abs(destX - currX) + Mathf.Abs(destY - currY) > 0);
    }

    bool CanMoveBishop(int destX, int destY, int currX, int currY)
    {
        int startind = (currY * 8) + currX; //check with him to see this right
        int endind = (destY * 8) + destX;
        int absvaluediff = Math.Abs(endind - startind);
        bool p1turn = player.p1Turn;
        bool[] array1 = board.p1Board;
        bool[] array2 = board.p2Board;
        int add;
        if (Mathf.Abs(destX - currX) == Mathf.Abs(destY - currY) && (Mathf.Abs(destX - currX) != 0))
        {
            if (absvaluediff == 63)
                add = (endind - startind) / 7;
            else if (absvaluediff % 9 == 0)
                add = (endind - startind > 0 ? 9 : -9);
            else if (absvaluediff % 7 == 0)
                add = (endind - startind > 0 ? 7 : -7);
            else
                return (false);
            Debug.Log("Add:" + add);

            startind += add;
            while (startind != endind)
            {
                if (array1[startind] || array2[startind])
                {
                    return (false);
                }
                startind += add;
            }
            if (p1turn)
            {
                if (array1[endind])
                    return (false);
                else
                    return (true);
            }
            else
            {
                if (array2[endind])
                    return (false);
                else
                    return (true);
            }
        }
        else
            return (false);
    }

    bool CanMoveRook(int destX, int destY, int currX, int currY)
    {
        bool[] array1 = board.p1Board;
        bool[] array2 = board.p2Board;
        bool p1turn = player.p1Turn;
        if ((Mathf.Abs(destX - currX) > 0) ^ (Mathf.Abs(destY - currY) > 0))
        {
            int startind = (currY * 8) + currX; //check with him to see this right
            int endind = (destY * 8) + destX;
            int add;
            if (startind - (startind % 8) == endind - (endind % 8))
                add = (startind < endind ? 1 : -1);
            else
                add = (startind < endind ? 8 : -8);
            startind += add;
            while (startind != endind)
            {
                if (array1[startind] || array2[startind]) //all zeros where no pieces, right?
                    return (false);
                startind += add;
            }
            if (p1turn)
            {
                if (!array1[startind])
                    return (true);
                else
                    return (false);
            }
            else
            {
                if (!array2[startind])
                    return (true);
                else
                    return (false);
            }

        }
        else
            return (false);
    }

    bool CanMoveQueen(int destX, int destY, int currX, int currY)
    {
        return (CanMoveRook(destX, destY, currX, currY) || CanMoveBishop(destX, destY, currX, currY));
    }

    bool CanMoveKnight(int destX, int destY, int currX, int currY)
    {
        bool p1turn = player.p1Turn;
        int endind = (destY * 8) + destX;
        int x = Mathf.Abs(destX - currX);
        int y = Mathf.Abs(destY - currY);
        bool[] array1 = board.p1Board;
        bool[] array2 = board.p2Board;
        if ((x == 2 && y == 1) || (x == 1 && y == 2))
        {
            if ((!array1[endind] && p1turn) || (!array2[endind] && !p1turn))
                return (true);
        }
        return (false);
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
            sound.clip = select;
            sound.Play();
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
            board.SetP1State((int)transform.position.y, (int)transform.position.x);
        }
        else
        {
            board.SetP2State((int)transform.position.y, (int)transform.position.x);
        }
        bool CanCapture = false;
        if (playerOne)
        {
            CanCapture = board.GetP2State(row, col);
        }
        else
        {
            CanCapture = board.GetP1State(row, col);
        }
        if (CanCapture)
        {
            Collider[] colliders = Physics.OverlapSphere(new Vector3(col, row, 0), 0.1f);
            GameObject capturedPiece = colliders[0].gameObject;
            capturedPiece.GetComponent<PieceScript>().Death();
            Promote();
        }

        Vector3 pos = transform.position;
        pos.x = col;
        pos.y = row;
        transform.position = pos;

        if (playerOne)
        {
            board.SetP1State((int)transform.position.y, (int)transform.position.x);
        }
        else
        {
            board.SetP2State((int)transform.position.y, (int)transform.position.x);
        }

        ChoosePiece();
       // board.PrintBoard();
    }

    public void Death()
    {
        if (playerOne)
        {
            board.SetP1State((int)transform.position.y, (int)transform.position.x);
        }
        else
        {
            board.SetP2State((int)transform.position.y, (int)transform.position.x);
        }
        int newSpotR = UnityEngine.Random.Range(0, 8);
        int newSpotC = UnityEngine.Random.Range(0, 8);
        while(board.GetP1State(newSpotR, newSpotC) || board.GetP2State(newSpotR, newSpotC))
        {
            newSpotR = UnityEngine.Random.Range(0, 8);
            newSpotC = UnityEngine.Random.Range(0, 8);
        }
        Vector3 pos = transform.position;
        pos.y = newSpotR;
        pos.x = newSpotC;
        transform.position = pos;
        if (playerOne)
        {
            board.SetP1State(newSpotR, newSpotC);
        }
        else
        {
            board.SetP2State(newSpotR, newSpotC);
        }
    }

}
