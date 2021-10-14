using EssentialMechanics;
using EssentialMechanics.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lighting2D : MonoBehaviour
{
    List<illuminatedTile> oldTiles = new List<illuminatedTile>();
 public void lightenAround(int range,Color lightColor, Tilemap tilemap, Vector2 center)
    {
        oldTiles = new List<illuminatedTile>();
        for (int x = -range; x <= range; x++)
        {
            bool obstructed = false;
            List<Node> obstructedNodes = new List<Node>();

            for (int y = 0; y <= range; y++)
            {
                Vector2 tilePosition = new Vector2((int)center.x + x, (int)center.y + y);

                if (Vector2.Distance(center, tilePosition) <= range && Tilemap2D.getTile(tilemap, tilePosition) != null)
                    render(range, lightColor, tilemap, center, tilePosition);
            }
            for (int y = 0; y >= -range; y--)
            {
                Vector2 tilePosition = new Vector2((int)center.x + x, (int)center.y + y);

                if (Vector2.Distance(center, tilePosition) <= range && Tilemap2D.getTile(tilemap, tilePosition) != null)
                    render(range, lightColor, tilemap, center, tilePosition);
            }
        }
    }

    private void render(int range,Color lightColor, Tilemap tilemap, Vector2 center, Vector2 tilePosition, bool obstructed = false)
    {
        Color color = EssentialMechanics.Tilemap2D.getTile(tilemap, tilePosition).color;

        float ctn = ((1 - Vector2.Distance(center, tilePosition) / range) * lightColor.a);

        if (obstructed)
            ctn = ((1 - 0.9f) * lightColor.a);

        color = new Color(
            color.r + Mathf.MoveTowards(color.r, lightColor.r, lightColor.r) * lightColor.r * ctn,
            color.g + Mathf.MoveTowards(color.g, lightColor.g, lightColor.g) * lightColor.g * ctn,
            color.b + Mathf.MoveTowards(color.b, lightColor.b, lightColor.b) * lightColor.b * ctn,
            color.a);

        illuminatedTile tile = new illuminatedTile();
        tile.position = tilePosition;
        tile.color = EssentialMechanics.Tilemap2D.getTile(tilemap, tilePosition).color;
        tile.tilemap = tilemap;

        oldTiles.Add(tile);
        EssentialMechanics.Tilemap2D.setTileColor(color, tilePosition, tilemap);
    }
    public void resetTileLighting()
    {
        foreach(illuminatedTile tile in oldTiles)
        {
            EssentialMechanics.Tilemap2D.setTileColor(tile.color, tile.position, tile.tilemap);
        }
    }
}
class illuminatedTile
{
    public Vector2 position;
    public Color color;
    public Tilemap tilemap;
}
