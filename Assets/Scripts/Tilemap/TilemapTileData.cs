using NUnit.Framework;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TilemapTileData : MonoBehaviour
{
    [System.NonSerialized] public Dictionary<Vector3Int, TileAStarData> tilemapData = new Dictionary<Vector3Int, TileAStarData>();
    private Tilemap selfTilemap;

    private void Awake()
    {
        selfTilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        GenerateNewTileDataset();
    }

    public void GenerateNewTileDataset()
    {
        tilemapData = new Dictionary<Vector3Int, TileAStarData>();

        BoundsInt bounds = selfTilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (selfTilemap.GetTile(pos) == null)
                {
                    continue;
                }
                TileAStarData tileData = new TileAStarData();
                tileData.selfPos = pos;
                tilemapData.Add(pos, tileData);
            }
        }
    }

    public TileAStarData GetTileData(Vector3Int tilePosition)
    {
        if (tilemapData.ContainsKey(tilePosition))
            return tilemapData[tilePosition];
        else
            return null;
    }

    public void SetTileData(TileAStarData data, Vector3Int key)
    {
        if (tilemapData.ContainsKey(key))
            tilemapData[key] = data;
        else
            tilemapData.Add(key, data);
    }
}
