using System.Collections.Generic;
using Handlers;
using UnityEngine;

public class Node
{
    public Board CurrentBoard;
    public Color TurnColor; // couleur du joueur du tour simulé actuel
    public Color OwnerColor;

    public Node(Board board, Color turnColor, Color ownerColor, Move moveFromParent) // constructeur 
    {
        CurrentBoard = board;
        TurnColor = turnColor;
        OwnerColor = ownerColor;
        MoveFromParent = moveFromParent;
    }
    // search for pieces with right color

    // node = une board avec tous les coups possibles
    // ses enfants feront de même
    // un node = un move ? besoin de le return à l'IA pour qu'ils sachent quoi jouer et où.

    public Move MoveFromParent { get; private set; } // permet de faire le lien entre node et coup joué
    public bool IsTerminal => GetChilds().Count == 0;

    public List<Node> GetChilds()
    {
        var nodes = new List<Node>();
        
        foreach (var piece in CurrentBoard.Pieces)
        {
            if (piece == null) continue;
            //Si la couleur de la piece est celle de celui qui joue
            if (piece.Color == TurnColor)
            {
                //Prepare changement de nextTurnColor 
                Color nextTurnColor = (TurnColor == Color.white) ? Color.black : Color.white;
                // creation d'une board pour chaque move possible
                foreach (var possibleMove in piece.PossibleMovement(CurrentBoard.Pieces))
                {
                    // recuperation du clone de board casté 
                    var simulatedBoard = (Board)CurrentBoard.Clone();
                    // déplace la pièce sur ce clone de board
                    simulatedBoard.MovePiece(piece, possibleMove);
                    var move = new Move(piece, piece.coordinate, possibleMove);
                    Debug.Log(
                        $"calculating {piece.Color.r} piece's {piece} move from {piece.coordinate} to : {possibleMove}");
                    // creation d'une nouvelle node pour le tour ennemi suivant après avoir joué ce coup 
                    nodes.Add(new Node(simulatedBoard, nextTurnColor, OwnerColor, move));
                    Debug.Log($"new node created {simulatedBoard}, TurnColor:{nextTurnColor.r}, the AI is playing {OwnerColor.r} ");
                }
            }
        }

        return nodes;
    }

    public int GetHeuristic()
    {
        return CurrentBoard.GetHeuristicValue(OwnerColor);
    }
}