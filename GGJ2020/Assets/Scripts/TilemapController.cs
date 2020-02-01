using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{

    public Tilemap levelLayout;
    public List<TileInfo> ti = new List<TileInfo>();

    [System.Serializable]
    public struct SprToType
    {
        public string spr;
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
                    ti.Add(new TileInfo(j, i, t));
                }
            }
    }
    // Start is called before the first frame update
    void Awake()
    {
        for(int i=0; i < sprToType.Length; i++)
        {
            strType.Add(sprToType[i].spr, sprToType[i].type);
        }
    }
    private void Start()
    {
        GetAllTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
