using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitboardScript : MonoBehaviour
{
    long p1Board = 0;
    long p2Board = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetP1State(int row, int col)
    {
        return GetCellState(p1Board, row, col);
    }

    public void SetP1State(int row, int col)
    {
        p1Board = SetCellState(p1Board, row, col);
    }

    public bool GetP2State(int row, int col)
    {
        return GetCellState(p2Board, row, col);
    }

    public void SetP2State(int row, int col)
    {
        p2Board = SetCellState(p2Board, row, col);
    }

    public long SetCellState(long bitboard, int row, int col)
    {
        long newBit = 1L << (row * 8 + col);
        if (GetCellState(bitboard, row, col))
            return (bitboard & ~newBit);
        return (bitboard |= newBit);
    }

    public bool GetCellState(long bitboard, int row, int col)
    {
        long mask = 1L << (row * 8 + col);
        return ((bitboard & mask) != 0);
    }

    public void PrintBoard()
    {
      /*  Debug.Log("P1 Board:");
        for(int i = 0; i < 8; ++i)
        {
            string row = "";
            for(int j = 0; j < 8; ++j)
            {
                row += p1Board >> (i * 8 + j);
            }
            Debug.Log(row);
        }*/
    }
}
