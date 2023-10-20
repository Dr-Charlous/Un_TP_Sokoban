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
    //Cam
    public Camera Cam;
    public float OffSetX = 0;
    public float OffSetY = 0;

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

        for (int k = 0; k < Lines.Length; k++)
        {
            if (Lines[k].Length > SizeX)
            {
                SizeX = Lines[k].Length;
            }
        }
        SizeY = Lines.Length;

        DataGrid = new Categories[SizeX, SizeY];
        Cam.transform.position = new Vector3(SizeX/2 - OffSetX, -SizeY/2 + OffSetY, Cam.transform.position.z);
        Cam.orthographicSize = SizeX / 3.15f;

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
