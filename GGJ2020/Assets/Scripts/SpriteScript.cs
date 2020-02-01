using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScript : MonoBehaviour
{

    [System.Serializable]
    public struct TypeToSpr
    {
        public Sprite spr;
        public TileType type;
    }
    public TypeToSpr[] typeToSpr;
    public Dictionary<TileType,Sprite> typeSpr = new Dictionary< TileType,Sprite>();

    [System.Serializable]
    public struct WallTypeToSpr
    {
        public Sprite spr;
        public WallType type;
    }
    public WallTypeToSpr[] wallTypeToSpr;
    public Dictionary<WallType, Sprite> wallTypeSpr = new Dictionary<WallType, Sprite>();
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < typeToSpr.Length; i++)
        {
            typeSpr.Add( typeToSpr[i].type,typeToSpr[i].spr);
        }
        for (int i = 0; i < wallTypeToSpr.Length; i++)
        {
            wallTypeSpr.Add(wallTypeToSpr[i].type, wallTypeToSpr[i].spr);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
