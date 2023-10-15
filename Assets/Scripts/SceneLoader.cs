using System;
using UnityEngine;

[Serializable]
public class Placement
{
    public char Value;
    public GameObject PrefabBlock;
    public Transform Parent;
}

public class SceneLoader : MonoBehaviour
{
    //Asset Load
    public TextAsset Levels;
    public Placement[] Placements;
    string[] Lines;

    //Data Load
    public bool[,] Walls = new bool[1, 1];

    private void Start()
    {
        Lines = Levels.text.Split("\n");

        for (int i = 0; i < Lines.Length; i++)
        {
            for (int j = 0; j < Lines[i].Length; j++)
            {
                foreach (var item in Placements)
                {
                    if (Lines[i][j] == item.Value && item.PrefabBlock != null)
                    {
                        Instantiate(item.PrefabBlock, new Vector3(j, -i), Quaternion.identity, item.Parent);
                    }
                }
            }
        }
    }
}
