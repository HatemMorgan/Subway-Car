using UnityEngine;
using UnityEditor;

public class Position
{
    // column
    private int column;
    // row
    private int row;

    public Position(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public int getRow()
    {
        return row;
    }

    public int getColumn()
    {
        return column;
    }
}