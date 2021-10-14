using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EssentialMechanics
{
    public class Tilemap2D
    {
        public static TileBase getTile(Tilemap tileMap, Vector3 pos)
        {
            Vector3Int tilePos = tileMap.WorldToCell(pos);
            TileBase tile = tileMap.GetTile(tilePos);

            return tile;
        }
        public static Tile getTile(Tilemap tileMap, Vector2 pos)
        {
            Vector3Int tilePos = tileMap.WorldToCell(new Vector3(pos.x,pos.y,0f));
            Tile tile = tileMap.GetTile<Tile>(tilePos);

            return tile;
        }
        public static void setTileColor(Color color, Vector2 pos, Tilemap tileMap)
        {
            Vector3Int tilePos = tileMap.WorldToCell(new Vector3(pos.x, pos.y, 0f));
            tileMap.SetTileFlags(tilePos, TileFlags.None);
            tileMap.SetColor(tilePos, color);
        }
    }
}
