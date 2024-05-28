using System.Collections.Generic;
using Managers;
using Pieces;
using UnityEngine;

namespace Handlers
{
    public class PieceHandler : MonoBehaviour {
    
        public Piece Piece;
    
        private Vector3 _initialPosition;
        private static bool _pieceSelected;
        public Color originalColor;

        public void Setup(Piece current) {
            Piece = current;
            Piece.coordinate = new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.z);
        }
    
        private void OnMouseOver() {
            if (GameManager.Instance.WhiteTurn)
            {
                if (!this.gameObject.CompareTag("White"))
                {
                    return;
                }
            }
            else if (!GameManager.Instance.WhiteTurn)
            {
                    if (!this.gameObject.CompareTag("Black"))
                    {
                        return;
                    } 
            }
            // faire un return si c'est le tour des 
            
            GetComponent<MeshRenderer>().material.color = Color.green;
        
            if (Input.GetButtonDown("Fire1") && GameManager.Instance.canSelectPiece) {  // selection et déselection pas pratique
                GameManager.Instance.canSelectPiece = false;
                //Debug.Log("Selected : " + gameObject);
                List<Vector2Int> movements = Piece.PossibleMovement(GameManager.Instance.BoardMatrix.Pieces);

                GameManager.Instance.EnableCells(movements);
            
                GameManager.Instance.SelectedPiece = this;
            }

            if (Input.GetButtonDown("Fire2"))    // ne fonctionne que si l'on clique sur l'une des pieces, pourquoi pas le sortir du mouseOver ? 
            {
                GetComponent<MeshRenderer>().material.color = originalColor;
                GameManager.Instance.SelectedPiece = null;
                GameManager.Instance.canSelectPiece = true;
            }
        }

        private void OnMouseExit()
        {
            GetComponent<MeshRenderer>().material.color = originalColor;
        }

        private void OnDestroy()
        {
            Destroy(this.gameObject);
        }
    
    }
}