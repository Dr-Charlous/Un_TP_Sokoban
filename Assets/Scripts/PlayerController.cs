using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Categories;

public class PlayerController : MonoBehaviour
{
    public SceneLoader SceneLoader;
    public Categories[,] DataGrid;
    public Vector3Int PlayerPos;
    public float Speed = 0.2f;
    public bool Snapping = false;

    private void Start()
    {
        if (SceneLoader == null)
            SceneLoader = GetComponentInParent<SceneLoader>();

        DataGrid = new Categories[SceneLoader.SizeX, SceneLoader.SizeY];
        DataGrid = SceneLoader.DataGrid;
        PlayerPos = new Vector3Int((int)transform.position.x, -(int)transform.position.y);
    }

    private void Update()
    {
        if (PlayerPos != new Vector3Int((int)transform.position.x, -(int)transform.position.y))
        {
            PlayerPos = new Vector3Int((int)transform.position.x, -(int)transform.position.y);
        }

        Vector3Int GoPos = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (DataGrid[PlayerPos.x, PlayerPos.y - 1] == null || DataGrid[PlayerPos.x, PlayerPos.y - 1].state != BlockState.Wall && DataGrid[PlayerPos.x, PlayerPos.y - 1].state != BlockState.MoveBlock)
            {
                PlayerPos = new Vector3Int(PlayerPos.x, -PlayerPos.y + 1);
                GoPos = PlayerPos;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (DataGrid[PlayerPos.x, PlayerPos.y + 1] == null || DataGrid[PlayerPos.x, PlayerPos.y + 1].state != BlockState.Wall && DataGrid[PlayerPos.x, PlayerPos.y + 1].state != BlockState.MoveBlock)
            {
                PlayerPos = new Vector3Int(PlayerPos.x, -PlayerPos.y - 1);
                GoPos = PlayerPos;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (DataGrid[PlayerPos.x + 1, PlayerPos.y] == null || DataGrid[PlayerPos.x + 1, PlayerPos.y].state != BlockState.Wall && DataGrid[PlayerPos.x + 1, PlayerPos.y].state != BlockState.MoveBlock)
            {
                PlayerPos = new Vector3Int(PlayerPos.x + 1, -PlayerPos.y);
                GoPos = PlayerPos;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (DataGrid[PlayerPos.x - 1, PlayerPos.y] == null || DataGrid[PlayerPos.x - 1, PlayerPos.y].state != BlockState.Wall && DataGrid[PlayerPos.x - 1, PlayerPos.y].state != BlockState.MoveBlock)
            {
                PlayerPos = new Vector3Int(PlayerPos.x - 1, -PlayerPos.y);
                GoPos = PlayerPos;
            }
        }

        if (GoPos != Vector3Int.zero)
        {
            DataGrid[(int)transform.position.x, -(int)transform.position.y].state = BlockState.Floor;
            DataGrid[GoPos.x, -GoPos.y].state = BlockState.Player;

            transform.DOComplete();
            transform.DOMove(GoPos, Speed, Snapping);
        }
    }
}
