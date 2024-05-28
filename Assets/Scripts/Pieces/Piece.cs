using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pieces
{
    [Serializable]
    public abstract class Piece : ICloneable {
    
        public Color Color { get; }
        //location actuelle
        public Vector2Int coordinate;
        public bool hasPlayed;
    
        public abstract int Score { get; }
        
        protected Piece(Color color) {
            Color = color;
        }
    
        public abstract List<Vector2Int> PossibleMovement(Piece[,] matrix);
    
        private bool IsWithinChessboardBounds(int x, int y)
        {
            return x >= 0 && x <= 7 && y >= 0 && y <= 7;
        }

        public object Clone()
        {

            return MemberwiseClone();
        }
    
    }
}