using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Categories;

public class PlayerController : MonoBehaviour
{
    public SceneLoader SceneLoader;
    public Categories[,] DataGrid;
    public Vector3Int PlayerPos;

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
            PlayerPos = new Vector3Int((int)transform.position.x, -(int)transform.position.y);

        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (DataGrid[PlayerPos.x, PlayerPos.y - 1].state != BlockState.Wall)
            {
                PlayerPos = new Vector3Int(PlayerPos.x, -PlayerPos.y + 1);
                transform.position = PlayerPos;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (DataGrid[PlayerPos.x, PlayerPos.y + 1].state != BlockState.Wall)
            {
                PlayerPos = new Vector3Int(PlayerPos.x, -PlayerPos.y - 1);
                transform.position = PlayerPos;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (DataGrid[PlayerPos.x + 1, PlayerPos.y].state != BlockState.Wall)
            {
                PlayerPos = new Vector3Int(PlayerPos.x + 1, -PlayerPos.y);
                transform.position = PlayerPos;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (DataGrid[PlayerPos.x - 1, PlayerPos.y].state != BlockState.Wall)
            {
                PlayerPos = new Vector3Int(PlayerPos.x - 1, -PlayerPos.y);
                transform.position = PlayerPos;
            }
        }
    }
}
