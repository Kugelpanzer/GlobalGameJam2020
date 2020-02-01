using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    public GameObject mapLayout;
    Tilemap levelLayout;
    List<TileInfo> ti = new List<TileInfo>();
    public GameObject tileAsset; //list that contains one of every tile asset
    public GameObject wallAsset;

    [System.Serializable]
    public struct SprToType
    {
        public Sprite spr;
        public TileType type;
    }
    public SprToType[] sprToType;
    private Dictionary<string, TileType> strType = new Dictionary<string, TileType>();

    public class TileInfo
    {
        public TileInfo(int x,int y,TileType type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
        public int x, y;
        public TileType type;
    }

    private void GetAllTiles()
    {
        BoundsInt bound = levelLayout.cellBounds;
        for(int i=bound.yMin;i<bound.yMax+1;i++)
            for(int j=bound.xMin;j<bound.xMax; j++)
            {
                if (levelLayout.HasTile(new Vector3Int(j,i,0)))
                {
                    TileType t = strType[levelLayout.GetSprite(new Vector3Int(j, i, 0)).name];

                   /* Debug.Log(levelLayout.GetSprite(new Vector3Int(j, i, 0)).name);
                    Debug.Log(j);
                    Debug.Log(i);*/
                    ti.Add(new TileInfo(j, i, t));
                }
            }
    }

    private void GenerateMap()
    {
        for(int i=0;i<ti.Count; i++)
        {
            GameObject gj = Instantiate(tileAsset);
            Tile b = gj.GetComponent<Tile>();
            b.x = ti[i].x;
            b.y = ti[i].y;
            b.type = ti[i].type;
            GetComponent<Board>().CreateTile(b);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        levelLayout = mapLayout.GetComponentInChildren<Tilemap>();
        for(int i=0; i < sprToType.Length; i++)
        {
            strType.Add(sprToType[i].spr.name, sprToType[i].type);
        }
    }
    private void Start()
    {
        GetAllTiles();
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
