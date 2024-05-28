using System;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBeta
{
    private int _nodesVisited; // sert a afficher le nombre de noeuds visités

    public int alphaBeta(Node node, int depth, int alpha, int beta, bool maximizingPlayer)
    { 
            _nodesVisited ++; 
        if (depth == 0 || node.IsTerminal)
        {
            int heuristic = node.GetHeuristic();
            Debug.Log(
                $" {node.TurnColor.r}{node.MoveFromParent.Piece} 's to {node.MoveFromParent.DestinationCell} 's destination heuristic is {heuristic}");
            return heuristic;
        }

        if (maximizingPlayer)
        {
            int maxEval = int.MinValue;
            foreach (Node child in node.GetChilds())
            {
                int eval = alphaBeta(child, depth - 1, alpha, beta, false);
                maxEval = Mathf.Max(maxEval, eval);
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha)
                    break;
            }
            Debug.Log($"Max eval is  { maxEval } for  maximizingPlayer");
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (Node child in node.GetChilds())
            {
                int eval = alphaBeta(child, depth - 1, alpha, beta, true);
                minEval = Mathf.Min(minEval, eval);
                beta = Mathf.Min(beta, eval);
                if (beta <= alpha)
                    break;
            }
            Debug.Log($"Min eval is  { minEval } for  minimizingPlayer");
            return minEval;
        }
    }

    public Move FindBestMove(Node node, int depth)
    {
        int bestEval = int.MinValue;
        List<Move> bestMoves = new List<Move>();
        int alpha = int.MinValue;
        int beta = int.MaxValue;

        foreach (Node child in node.GetChilds())
        {
            int eval = alphaBeta(child, depth - 1, alpha, beta, false);
            if (eval > bestEval)
            {
                bestEval = eval;
                bestMoves.Clear();
                bestMoves.Add(child.MoveFromParent);
            }
            else if (eval == bestEval)
            {
                bestMoves.Add(child.MoveFromParent);
            }
        }

        if (bestMoves.Count == 0)
        {
            throw new Exception("Got some error happening here");
        }

        int bestMoveIndex = UnityEngine.Random.Range(0, bestMoves.Count);
        Move bestMove = bestMoves[bestMoveIndex];

        Debug.Log(
            $"The best move for {bestMove.Piece.Color.r} {bestMove.Piece} at {bestMove.Piece.coordinate} is {bestMove.DestinationCell} heuristic is {bestEval} Move selected randomly from {bestMoves.Count} equal moves");

        return bestMove;
    }
}