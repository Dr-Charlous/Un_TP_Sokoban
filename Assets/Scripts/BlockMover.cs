using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Categories;

public class BlockMover : MonoBehaviour
{
    public enum Color
    {
        Blue,
        Red,
        Green
    }

    public Color color;

    public SceneLoader SceneLoader;
    public Categories[,] DataGrid;
    public Vector3Int BlockPos;
    public float Speed = 0.2f;
    public bool Snapping = false;

    private void Start()
    {
        if (SceneLoader == null)
            SceneLoader = GetComponentInParent<SceneLoader>();

        DataGrid = new Categories[SceneLoader.SizeX, SceneLoader.SizeY];
        DataGrid = SceneLoader.DataGrid;
        BlockPos = new Vector3Int((int)transform.position.x, -(int)transform.position.y);
    }

    private void Update()
    {
        if (BlockPos != new Vector3Int((int)transform.position.x, -(int)transform.position.y))
        {
            BlockPos = new Vector3Int((int)transform.position.x, -(int)transform.position.y);
        }

        Vector3Int GoPos = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow) && DataGrid[BlockPos.x, BlockPos.y - 1] != null && (DataGrid[BlockPos.x, BlockPos.y - 1].state != BlockState.Wall && DataGrid[BlockPos.x, BlockPos.y - 1].state != BlockState.MoveBlock))
        {
            if (DataGrid[BlockPos.x, BlockPos.y + 1] != null && DataGrid[BlockPos.x, BlockPos.y + 1].state == BlockState.Player)
            {
                BlockPos = new Vector3Int(BlockPos.x, -BlockPos.y + 1);
                GoPos = BlockPos;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && DataGrid[BlockPos.x, BlockPos.y + 1] != null && (DataGrid[BlockPos.x, BlockPos.y + 1].state != BlockState.Wall) && DataGrid[BlockPos.x, BlockPos.y + 1].state != BlockState.MoveBlock)
        {
            if (DataGrid[BlockPos.x, BlockPos.y - 1] != null && DataGrid[BlockPos.x, BlockPos.y - 1].state == BlockState.Player)
            {
                BlockPos = new Vector3Int(BlockPos.x, -BlockPos.y - 1);
                GoPos = BlockPos;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && DataGrid[BlockPos.x + 1, BlockPos.y] != null && (DataGrid[BlockPos.x + 1, BlockPos.y].state != BlockState.Wall) && DataGrid[BlockPos.x + 1, BlockPos.y].state != BlockState.MoveBlock)
        {
            if (DataGrid[BlockPos.x - 1, BlockPos.y] != null && DataGrid[BlockPos.x - 1, BlockPos.y].state == BlockState.Player)
            {
                BlockPos = new Vector3Int(BlockPos.x + 1, -BlockPos.y);
                GoPos = BlockPos;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && DataGrid[BlockPos.x - 1, BlockPos.y] != null && (DataGrid[BlockPos.x - 1, BlockPos.y].state != BlockState.Wall) && DataGrid[BlockPos.x - 1, BlockPos.y].state != BlockState.MoveBlock)
        {
            if (DataGrid[BlockPos.x + 1, BlockPos.y] != null && DataGrid[BlockPos.x + 1, BlockPos.y].state == BlockState.Player)
            {
                BlockPos = new Vector3Int(BlockPos.x - 1, -BlockPos.y);
                GoPos = BlockPos;
            }
        }

        if (GoPos != Vector3Int.zero)
        {
            if (DataGrid[GoPos.x, -GoPos.y].state == BlockState.Arrival && DataGrid[GoPos.x, -GoPos.y].check == gameObject.GetComponent<Categories>().check)
            {
                Debug.Log("Win");
            }

            DataGrid[(int)transform.position.x, -(int)transform.position.y].state = BlockState.Floor;
            DataGrid[GoPos.x, -GoPos.y].state = BlockState.MoveBlock;

            transform.DOComplete();
            transform.DOMove(GoPos, Speed, Snapping);
        }
    }
}
