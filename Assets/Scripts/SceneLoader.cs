using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Categories;

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
    [SerializeField] TileBase Floor;
    [SerializeField] Tilemap FloorTileMap;

    //Data Load
    public Categories[,] DataGrid;
    public int SizeX = 0;
    public int SizeY = 0;

    private void Start()
    {
        Lines = Levels.text.Split("\n");
        DataGrid = new Categories[SizeX, SizeY];

        for (int i = 0; i < Lines.Length; i++)
        {
            for (int j = 0; j < Lines[i].Length; j++)
            {
                foreach (var item in Placements)
                {
                    if (Lines[i][j] == item.Value)
                    {
                        if (item.PrefabBlock != null)
                        {
                            GameObject Object = Instantiate(item.PrefabBlock, new Vector3(j, -i), Quaternion.identity, item.Parent);
                            DataGrid[j, i] = Object.GetComponent<Categories>();
                        }

                        FloorTileMap.SetTile(new Vector3Int(j, -i), Floor);
                    }
                }
            }
        }
    }
}
