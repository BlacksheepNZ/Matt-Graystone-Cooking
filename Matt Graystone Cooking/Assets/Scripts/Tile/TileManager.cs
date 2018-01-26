using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour {

    private List<Tile> Tile;
    public int Width;
    public int Height;
    public float AreaScale;
    public GameObject TilePrefab;
    public Transform Parent;

    public IEnumerator CreateChunk(Transform parent, TileType tileType)
    {
        bool Loaded = false;

        while (Loaded == false)
        {
            int i = 0;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    GameObject buffObject = Instantiate(TilePrefab);
                    buffObject.transform.SetParent(parent);
                    buffObject.transform.localPosition = new Vector3(x * AreaScale + (AreaScale / 2), y * AreaScale + (AreaScale / 2), 0);
                    buffObject.transform.localScale = Vector3.one;

                    Tile tile = buffObject.GetComponent<Tile>();
                    tile.ID = i++;
                    tile.X = x * AreaScale;
                    tile.Y = y * AreaScale;

                    RectTransform rt = tile.GetComponent(typeof(RectTransform)) as RectTransform;
                    rt.sizeDelta = new Vector2(AreaScale, AreaScale);

                    tile.TileType = tileType;
                    Tile.Add(tile);
                }
            }

            Loaded = true;
            yield return null;
        }
    }

    // Use this for initialization
    void Start () {
        Tile = new List<Tile>();

        StartCoroutine(CreateChunk(Parent, TileType.Floor));

        Tile[0].TileType = TileType.Chair;
        Tile[50].TileType = TileType.Chair;

        float[,] tilesmap = new float[Width, Height];
        Grid grid = new Grid(Width, Height, tilesmap);

        Point _from = new Point(0, 0);
        Point _to = new Point(10, 10);

        List<Point> path = Pathfinding.FindPath(grid, _from, _to);

        Debug.Log(path.Count);

        for (int i = 0; i < path.Count; i++)
        {
            Debug.Log(path[i].x + " " + path[i].y);
        }
    }
}
