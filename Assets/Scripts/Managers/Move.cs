using Pieces;
using UnityEngine;

public class Move
{
    public Piece Piece { get; private set; }
    public Vector2Int OriginCell { get; private set; }
    public Vector2Int DestinationCell { get; private set; }

    public Move(Piece piece,Vector2Int originCell, Vector2Int destinationCell)
    {
        Piece = piece;
        OriginCell = originCell;
        DestinationCell = destinationCell;
    }
}