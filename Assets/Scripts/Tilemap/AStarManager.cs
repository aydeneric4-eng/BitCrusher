using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(TilemapTileData))]
public class AStarManager : MonoBehaviour
{
    private TilemapTileData selfTileData;
    private Tilemap selfTilemap;
    [SerializeField] int maxSteps = 10000; // 10,000

    public static AStarManager instance;
    private void Awake()
    {
        instance = this;
        selfTileData = GetComponent<TilemapTileData>();
        selfTilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        GameManager.pathfindingInstance = this;
    }

    private float GetDistanceScore(TileAStarData tile, TileAStarData target, float offset = 0)
    {
        return (selfTilemap.CellToWorld(tile.selfPos) - selfTilemap.CellToWorld(target.selfPos)).magnitude + offset;
    }

    private void SetTileCost(TileAStarData tile, TileAStarData start, TileAStarData target, float offset = 0)
    {
        tile.gScore = GetDistanceScore(tile, start, offset);
        tile.hScore = GetDistanceScore(tile, target);
    }

    public List<Vector3> GetPath(Vector3 startPos, Vector3 targetPos)
    {
        //Debug.Log(selfTilemap);
        //Debug.Log(selfTilemap.WorldToCell(targetPos));
        //return null;
        TileAStarData startTile = selfTileData.GetTileData(selfTilemap.WorldToCell(startPos));
        TileAStarData targetTile = selfTileData.GetTileData(selfTilemap.WorldToCell(targetPos));
        //return null;
        if (startTile == null || targetTile == null)
        {
            //Debug.LogWarning("Failed to get start/target tile");
            //Debug.Log(startPos);
            //Debug.Log(startTile);
            //Debug.Log(targetPos);
            //Debug.Log(targetTile);
            //Debug.Log("#####");
            return null;
        }
        
        List<TileAStarData> openTiles = new List<TileAStarData>();
        List<TileAStarData> closedTiles = new List<TileAStarData>();

        List<Vector3> finalPath = new List<Vector3>();

        bool foundPath = false;
        int currentStep = 0;
        TileAStarData currentTile;

        SetTileCost(startTile, startTile, targetTile);
        openTiles.Add(startTile);
        while (!foundPath && currentStep < maxSteps)
        {
            //Debug.Log("Step:");
            //Debug.Log(currentStep);
            //Debug.Log("closedTiles.Count");
            //Debug.Log(closedTiles.Count);
            //Debug.Log("openTiles.Count");
            //Debug.Log(openTiles.Count);
            //Debug.Log(openTiles[0].selfPos);
            openTiles = openTiles.OrderBy(t => t.FScore()).ToList();
            currentTile = openTiles[0];
            openTiles.RemoveAt(0);
            closedTiles.Add(currentTile);

            if (currentTile == targetTile)
            {
                foundPath = true;
            }
            //Debug.Log("Checking neighbors");
            for (int x = currentTile.selfPos.x - 1; x <= currentTile.selfPos.x + 1; x++)
            {
                for (int y = currentTile.selfPos.y - 1; y <= currentTile.selfPos.y + 1; y++)
                {
                    //Debug.Log(new Vector2(x, y));
                    if (x == currentTile.selfPos.x && y == currentTile.selfPos.y)
                        continue;
                    TileAStarData neighborTile = selfTileData.GetTileData(new Vector3Int(x, y, 0));
                    if (neighborTile == null || closedTiles.Contains(neighborTile))
                        continue;

                    if (!openTiles.Contains(neighborTile) || neighborTile.gScore > GetDistanceScore(neighborTile, startTile, currentTile.gScore))
                    {
                        SetTileCost(neighborTile, startTile, targetTile, currentTile.gScore);
                        neighborTile.parentTile = currentTile;
                        if (!openTiles.Contains(neighborTile))
                        {
                            openTiles.Add(neighborTile);
                            //Debug.Log("add neighbor");
                        }
                    }
                }
            }
            currentStep++;
        }

        if (currentStep >= maxSteps)
        {
            //Debug.LogWarning("FAILED TO FIND PATH");
            //Debug.Log(currentStep);
            return null;
        }

        currentStep = 0;
        bool madePath = false;
        TileAStarData current = targetTile;
        while (currentStep < maxSteps)
        {
            finalPath.Add(selfTilemap.CellToWorld(current.selfPos) + selfTilemap.cellSize / 2);
            if (current == startTile)
            {
                madePath = true;
                break;
            }
               
            if (current.parentTile == null)
            {
                //Debug.LogWarning("ERROR in generating path");
                //Debug.Log(current.selfPos);
                //Debug.Log(startTile.selfPos);
                //Debug.Log(targetTile.selfPos);
                return null;
            }
            current = current.parentTile;
            currentStep++;
        }
        if (!madePath)
        {
            //Debug.Log("Couldnt make path in time");
            return null;
        }

        finalPath.Reverse();
        return finalPath;
    }

}
