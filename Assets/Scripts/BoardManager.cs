using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionZ = -1;

    public List<GameObject> chessPiecesPrefabs;
    private List<GameObject> activeChessPieces;

    private Quaternion orientationWhite = Quaternion.Euler(270, 270, 0);
    private Quaternion orientationBlack = Quaternion.Euler(270, 90, 0);

    private void Start()
    {
        SpawnAllChessPieces();
    }

    private void Update()
    {
        UpdateSelection();
        DrawChessBoard();
    } 

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionZ = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionZ = -1;
        }
    }

    private void DrawChessBoard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heigthLine = Vector3.forward * 8;

        for (int i = 0; i <=8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * i;
                Debug.DrawLine(start, start + heigthLine);
            }
        }

        // Draw the selection
        if(selectionX >= 0 && selectionZ >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionZ + Vector3.right * selectionX,
                Vector3.forward * (selectionZ + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectionZ + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionZ + Vector3.right * (selectionX + 1));
        }
    }

    private Vector3 GetTileCenter(int x, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * z) + TILE_OFFSET;
        return origin;

    }

    private void SpawnWhiteChessPiece(int index, Vector3 position)
    {
        GameObject go = Instantiate(chessPiecesPrefabs[index], position, orientationWhite) as GameObject;
        go.transform.SetParent(transform);
        activeChessPieces.Add(go);
    }

    private void SpawnBlackChessPiece(int index, Vector3 position)
    {
        GameObject go = Instantiate(chessPiecesPrefabs[index], position, orientationBlack) as GameObject;
        go.transform.SetParent(transform);
        activeChessPieces.Add(go);
    }

    private void SpawnAllChessPieces()
    {
        activeChessPieces = new List<GameObject>();

        // Spawn white team
        SpawnWhiteChessPiece(0, GetTileCenter(3, 0));        // King
        SpawnWhiteChessPiece(1, GetTileCenter(4, 0));        // Queen
        SpawnWhiteChessPiece(2, GetTileCenter(0, 0));        // Rook 1
        SpawnWhiteChessPiece(2, GetTileCenter(7, 0));        // Rook 2
        SpawnWhiteChessPiece(3, GetTileCenter(2, 0));        // Bishop 1
        SpawnWhiteChessPiece(3, GetTileCenter(5, 0));        // Bishop 2
        SpawnWhiteChessPiece(4, GetTileCenter(1, 0));        // Knight 1
        SpawnWhiteChessPiece(4, GetTileCenter(6, 0));        // Knight 2
        for (int i = 0; i < 8; i ++)                         // Pawns
        {
            SpawnWhiteChessPiece(5, GetTileCenter(i, 1));
        }

        // Spawn black team
        SpawnBlackChessPiece(6, GetTileCenter(3, 7));        // King
        SpawnBlackChessPiece(7, GetTileCenter(4, 7));        // Queen
        SpawnBlackChessPiece(8, GetTileCenter(0, 7));        // Rook 1
        SpawnBlackChessPiece(8, GetTileCenter(7, 7));        // Rook 2
        SpawnBlackChessPiece(9, GetTileCenter(2, 7));        // Bishop 1
        SpawnBlackChessPiece(9, GetTileCenter(5, 7));        // Bishop 2
        SpawnBlackChessPiece(10, GetTileCenter(1, 7));        // Knight 1
        SpawnBlackChessPiece(10, GetTileCenter(6, 7));        // Knight 2
        for (int i = 0; i < 8; i++)                          // Pawns
        {
            SpawnBlackChessPiece(11, GetTileCenter(i, 6));
        }


    }
}
