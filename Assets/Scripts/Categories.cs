using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Categories : MonoBehaviour
{
    public enum BlockState
    {
        Floor,
        Wall,
        MoveBlock,
        Arrival,
        Player
    }

    public BlockState state;

    public enum ColorCheck
    {
        None,
        Blue,
        Red,
        Green
    }

    public ColorCheck check;
}
