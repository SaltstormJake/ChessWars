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

    public long SetP1State(int row, int col)
    {
        return SetCellState(p1Board, row, col);
    }

    public bool GetP2State(int row, int col)
    {
        return GetCellState(p2Board, row, col);
    }

    public long SetP2State(int row, int col)
    {
        return SetCellState(p2Board, row, col);
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
}
