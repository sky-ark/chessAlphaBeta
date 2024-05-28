using System;
using Handlers;
using Unity.VisualScripting;
using UnityEngine;

public class Minimax 
{
   
   // l'IA doit jouer, pour celà elle doit calculer le meilleur coup 
   // création de Node pour chaque coup possible
   // recupération de l'heuristique à la profondeur voulue
   // refaire monter l'heuristique jusqu'au tour actuel
   // faire jouer le meilleur coup en fonction du résultat
   // reset nodes et heuristic avant d'passer le tour au joueur ( blancs )


   public int minimax(Node node, int depth, bool maximizingPlayer)
   {
      if (depth == 0 || node.IsTerminal)
      {
         int heuristic = node.GetHeuristic();
         Debug.Log($" {node.TurnColor.r}{node.MoveFromParent.Piece} 's to {node.MoveFromParent.DestinationCell} 's destination heuristic is {heuristic}");

         return heuristic;
      }
      if (maximizingPlayer)
      {
         int maxEval = int.MinValue;
         foreach (Node child in node.GetChilds())
         {
            int eval = minimax(child, depth - 1,false);
            maxEval = Mathf.Max(maxEval, eval);
         }

         return maxEval;
      }
      else
      {
         int minEval = int.MaxValue;
         foreach (Node child in node.GetChilds())
         {
            int eval = minimax(child, depth - 1,true);
            minEval = Mathf.Min(minEval, eval);
         }

         return minEval;
      }
   }

   public Move FindBestMove(Node node, int depth)
   {
      int bestEval = int.MinValue;
      Move bestMove = null;

      foreach (Node child in node.GetChilds())
      {
         int eval = minimax(child, depth - 1, false);
         if (eval > bestEval)
         {
            bestEval = eval;
            bestMove = child.MoveFromParent;
         }

      }

      if (bestMove == null)
      {
         throw new Exception("Got some error happening here");
      }
      Debug.Log($"The best move for {bestMove.Piece.Color.r} {bestMove.Piece} at {bestMove.Piece.coordinate} is {bestMove.DestinationCell}");

      return bestMove;

   }
}


// construction node
// recuperation heuristic
// comparaison heuristic des nodes de même profondeur
// maximiser minimiser en fonction de qui joue 
