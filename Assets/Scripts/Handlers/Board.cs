using System;
using System.Collections.Generic;
using Managers;
using Pieces;
using UnityEngine;

namespace Handlers
{
    [Serializable]
    public class Board : ICloneable
    {
        public List<CellHandler> cellsList;
        public Piece[,] Pieces;
        // public Transform piecesParent; // tranform des pieces
        // public Transform cellsParent; // transform des cells

        public void SetupDefaultBoard()
        {
            Pieces = new Piece[8, 8] // remplissage de la matrice début de partie
            {
                {
                    new Rook(Color.white), new Pawn(Color.white), null, null, null, null, new Pawn(Color.black),
                    new Rook(Color.black)
                },
                {
                    new Knight(Color.white), new Pawn(Color.white), null, null, null, null, new Pawn(Color.black),
                    new Knight(Color.black)
                },
                {
                    new Bishop(Color.white), new Pawn(Color.white), null, null, null, null, new Pawn(Color.black),
                    new Bishop(Color.black)
                },
                {
                    new Queen(Color.white), new Pawn(Color.white), null, null, null, null, new Pawn(Color.black),
                    new Queen(Color.black)
                },
                {
                    new King(Color.white), new Pawn(Color.white), null, null, null, null, new Pawn(Color.black),
                    new King(Color.black)
                },
                {
                    new Bishop(Color.white), new Pawn(Color.white), null, null, null, null, new Pawn(Color.black),
                    new Bishop(Color.black)
                },
                {
                    new Knight(Color.white), new Pawn(Color.white), null, null, null, null, new Pawn(Color.black),
                    new Knight(Color.black)
                },
                {
                    new Rook(Color.white), new Pawn(Color.white), null, null, null, null, new Pawn(Color.black),
                    new Rook(Color.black)
                },
            };
        }

        // Deplacement fictif de la piece sur la matrice après l'action du joueur.
        public void MovePiece(Piece piece, Vector2Int cell)
        {
            if (Pieces[piece.coordinate.x, piece.coordinate.y] != null)
                Pieces[piece.coordinate.x, piece.coordinate.y] = null;
            // déplacement vers les coordonnées de destination 
            Pieces[cell.x, cell.y] =
                Pieces[piece.coordinate.x, piece.coordinate.y]; 
            // reset des coordonnées de destination
            Pieces[piece.coordinate.x, piece.coordinate.y] = null; 
        }

        public int GetHeuristicValue(Color povColor)
        {
            //povColor est la couleur de l'IA
            // reset before calculating 
            int heuristicValue = 0; 
            // pour chaque pièce on prend le score, prendre uniquement celles de la bonne couleur 
            foreach (var piece in Pieces)
            {
                if (piece == null) continue;

                int pieceValue = piece.Score;
                int positionValue = GameManager.UsePieceSquareTables ? GetPositionValue(piece) : 0;

                if (piece.Color == povColor)
                {
                    heuristicValue += pieceValue + positionValue;
                }
                else
                {
                    heuristicValue -= pieceValue + positionValue;
                }
            }

            // Adjust the heuristic value based on the player's color
            return heuristicValue;
        }
        

        private int GetPositionValue(Piece piece)
        {
            int[,] pst = PieceSquareTable.GetTable(piece, piece.Color);
            int row = 7 - piece.coordinate.y; // Adjust row index based on your coordinate system
            int col = piece.coordinate.x;
            // Check if the coordinates are within the valid range
            if (row >= 0 && row < 8 && col >= 0 && col < 8)
            {
                return pst[row, col];
            }
            else
            {
                // Handle invalid coordinates (e.g., return a default value or log an error)
                return 0;
            }
        }


        public object Clone()
        {
            Board newBoard = new Board() // creation d'un nouveau plateau fictif
            {
                Pieces = new Piece[8, 8]
            };
            foreach (Piece piece in Pieces)
            {
                if (piece == null) continue;
                newBoard.Pieces[piece.coordinate.x, piece.coordinate.y] = (Piece)piece.Clone();
            }
            return newBoard;
        }
    }
}
