using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] BoardScript board = null;
    [SerializeField] PlayerTextScript textScript = null;
    PieceScript selectedPiece = null;
    bool p1Turn = true;
    bool pieceChosen = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                int row = (int)hit.collider.gameObject.transform.position.y;
                int col = (int)hit.collider.gameObject.transform.position.x;
                if(row < 10 && col < 10)
                {
                    if (pieceChosen)
                    {
                        MakeMove(row, col);
                    }
                    else
                        SelectPiece(row, col, p1Turn, hit.collider.gameObject);
                }
            }
        }
    }

    void SelectPiece(int row, int col, bool player, GameObject piece)
    {
        if (piece == null)
            return;
        PieceScript script = piece.GetComponent<PieceScript>();
        if (script == null)
            return;
        if (script.playerOne != p1Turn)
            return;

        script.ChoosePiece();
        pieceChosen = true;
        selectedPiece = script;
    }

    void MakeMove(int row, int col)
    {
        if (!selectedPiece.CanMove(col, row))
            return;
        selectedPiece.MovePiece(row, col);
        selectedPiece = null;
        pieceChosen = false;
        p1Turn = !p1Turn;
        textScript.SwitchPlayer();
    }
}
