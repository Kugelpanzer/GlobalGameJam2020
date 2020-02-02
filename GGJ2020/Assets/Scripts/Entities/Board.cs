﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    List<Tile> tiles = new List<Tile>();
    List<Wall> walls = new List<Wall>();

//    List<Tile> surroundedGooTiles = new List<Tile>();
//    List<Tile> notSurroundedGooTiles = new List<Tile>();


    public void CreateTile(Tile t)
    {
        tiles.Add(t);
    }

    public Board ()
    {
        tiles.Clear();
        walls.Clear();
    }

    public void AddWall (Wall wall)
    {
      // if (wall.type == WallType.Vertical) wall.x--;
      // if (wall.type == WallType.Backslash) wall.y--;
        walls.Add(wall);
//        RecalculateSurroundedTiles();
    }

   /* public void AddWall (Tile tile, WallType wallType)
    {
        Wall wall = new Wall();
        if (wallType == WallType.Vertical)
        {
            wall.x = tile.x - 1;
            wall.y = tile.y;
            wall.type = wallType;
        }
        if (wallType == WallType.Backslash)
        {
            wall.x = tile.x;
            wall.y = tile.y - 1;
            wall.type = wallType;
        }
        if (wallType == WallType.Slash)
        {
            wall.x = tile.x;
            wall.y = tile.y;
            wall.type = wallType;
        }
        AddWall(wall);
    }*/

    public void ChangeTileType (int x, int y, TileType tileType)
    {
        Tile tile = tiles.Find(existingTile => existingTile.x == x && existingTile.y == y);
        if (tile == null) return;
        tile.type = tileType;
        tile.UpdateSprite();
//        if (tileType == TileType.Goo) RecalculateSurroundedTiles();
    }

   /* private void RemoveTile(int x, int y)
    {
        Tile tileToRemove = GetTile(x, y);
        tiles.RemoveAll(tile => tile.x == x && tile.y == y);
        Destroy(tileToRemove.gameObject);
    }*/

    public Tile GetTile (int x, int y)
    {
        return tiles.Find(tile => tile.x == x && tile.y == y);
    }
    public bool HasTile (int x, int y)
    {
        return GetTile(x, y) != null;
    }

    public bool HasWall (int x, int y, WallType wallType)
    {
        return walls.Find(wall => wall.x == x && wall.y == y && wall.type == wallType) != null;
    }

    private TileType GetTileTypeOnPosition (int x, int y)
    {
        Tile tile = GetTile(x, y);
        if (tile != null) return tile.type;
        return TileType.Mountain;
    }

    public bool TileHasRightTile (int x, int y)
    {
        return HasTile(x + 1, y);
    }
    public bool TileHasTopRightTile(int x, int y)
    {
        return HasTile(x, y + 1);
    }
    public bool TileHasBottomRightTile(int x, int y)
    {
        return HasTile(x + 1, y - 1);
    }

    private bool TileHasRightWall (int x, int y)
    {
        return HasWall(x, y, WallType.Vertical);
    }
    private bool TileHasLeftWall (int x, int y)
    {
        return TileHasRightWall(x - 1, y);
    }

    private bool TileHasTopRightWall (int x, int y)
    {
        return HasWall(x, y, WallType.Backslash);
    }
    private bool TileHasBottomLeftWall (int x, int y)
    {
        return TileHasTopRightWall(x, y - 1);
    }

    private bool TileHasBottomRightWall (int x, int y)
    {
        return HasWall(x, y, WallType.Slash);
    }
    private bool TileHasTopLeftWall (int x, int y)
    {
        return TileHasBottomRightWall(x - 1, y + 1);
    }

/*
    private void RecalculateSurroundedTiles ()
    {
        surroundedGooTiles.Clear();
        notSurroundedGooTiles.Clear();
        foreach (Tile tile in tiles)
        {
            TileIsGooThatIsSurrounded(tile.x, tile.y);
        }
    }
*/

/*
    public bool TileIsGooThatIsSurrounded (int x, int y)
    {
        Tile tile = GetTile(x, y);
        if (tile == null) return false;
        if (tile.type != TileType.Goo) return false;
        List<Tile> checkedTiles = new List<Tile>();
        List<Tile> returnedTiles = new List<Tile>();
        bool surrounded = TileIsGooThatIsSurrounded(x, y, checkedTiles, ref returnedTiles);
        if (surrounded)
        {
            foreach (Tile returnedTile in returnedTiles)
            {
                if (!surroundedGooTiles.Find(surroundedTile => surroundedTile.x == returnedTile.x && surroundedTile.y == returnedTile.y)) surroundedGooTiles.Add(returnedTile);
            }
        }
        else
        {
            if (returnedTiles.Count == 0) returnedTiles.Add(tile);
            foreach (Tile returnedTile in returnedTiles)
            {
                if (!notSurroundedGooTiles.Find(notSurroundedTile => notSurroundedTile.x == returnedTile.x && notSurroundedTile.y == returnedTile.y)) notSurroundedGooTiles.Add(returnedTile);
            }
        }
        return surrounded;
    }

    private bool TileIsGooThatIsSurrounded (int x, int y, List<Tile> checkedTiles, ref List<Tile> surroundedTiles)
    {
        // if tile is already checked goo tile
        if (checkedTiles.Find(checkedTile => checkedTile.x == x && checkedTile.y == y) != null) return true;
        // if tile is already surely surrounded tile
        if (surroundedGooTiles.Find(surroundedTile => surroundedTile.x == x && surroundedTile.y == y)) return true;
        // if tile is already surely not a surrounded tile
        if (notSurroundedGooTiles.Find(notSurroundedTile => notSurroundedTile.x == x && notSurroundedTile.y == y)) return false;

        Tile tile = GetTile(x, y);

        // if tile is waste return false
        if (tile != null && tile.type == TileType.Waste) return false;

        // if tile is goo check around
        if (tile != null && tile.type == TileType.Goo)
        {
            if (!TileHasLeftWall(x, y) && HasTile(x - 1, y) && GetTileTypeOnPosition(x - 1, y) == TileType.Waste)
            {
                surroundedTiles = checkedTiles;
                return false;
            }
            if (!TileHasLeftWall(x, y) && HasTile(x - 1, y) && GetTileTypeOnPosition(x - 1, y) == TileType.Goo)
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x - 1, y, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!TileHasTopLeftWall(x, y) && HasTile(x - 1, y + 1) && GetTileTypeOnPosition(x - 1, y + 1) == TileType.Waste)
            {
                surroundedTiles = checkedTiles;
                return false;
            }
            if (!TileHasTopLeftWall(x, y) && HasTile(x - 1, y + 1) && GetTileTypeOnPosition(x - 1, y + 1) == TileType.Goo)
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x - 1, y + 1, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!TileHasTopRightWall(x, y) && HasTile(x, y + 1) && GetTileTypeOnPosition(x, y + 1) == TileType.Waste)
            {
                surroundedTiles = checkedTiles;
                return false;
            }
            if (!TileHasTopRightWall(x, y) && HasTile(x, y + 1) && GetTileTypeOnPosition(x, y + 1) == TileType.Goo)
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x, y + 1, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!TileHasRightWall(x, y) && HasTile(x + 1, y) && GetTileTypeOnPosition(x + 1, y) == TileType.Waste)
            {
                surroundedTiles = checkedTiles;
                return false;
            }
            if (!TileHasRightWall(x, y) && HasTile(x + 1, y) && GetTileTypeOnPosition(x + 1, y) == TileType.Goo)
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x + 1, y, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!TileHasBottomRightWall(x, y) && HasTile(x + 1, y - 1) && GetTileTypeOnPosition(x + 1, y - 1) == TileType.Waste)
            {
                surroundedTiles = checkedTiles;
                return false;
            }
            if (!TileHasBottomRightWall(x, y) && HasTile(x + 1, y - 1) && GetTileTypeOnPosition(x + 1, y - 1) == TileType.Goo)
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x + 1, y - 1, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!TileHasBottomLeftWall(x, y) && HasTile(x, y - 1) && GetTileTypeOnPosition(x, y - 1) == TileType.Waste)
            {
                surroundedTiles = checkedTiles;
                return false;
            }
            if (!TileHasBottomLeftWall(x, y) && HasTile(x, y - 1) && GetTileTypeOnPosition(x, y - 1) == TileType.Goo)
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x, y - 1, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!surroundedTiles.Find(surroundedTile => surroundedTile.x == x && surroundedTile.y == y)) surroundedTiles.Add(tile);
        }
        return true;
    }
*/

    public bool IsNatureTilePlayable (int x, int y)
    {
        return (tiles.Find(tile => tile.x == x && tile.y == y)).type == TileType.Waste;
    }

    public bool IsNatureWallPlayable (Tile tile, WallType wallType)
    {
        return IsNatureWallPlayable(tile.x, tile.y, wallType);
    }

    public bool IsNatureWallPlayable (int x, int y, WallType wallType)
    {
        // wall already exists -> not playable
        if (HasWall(x, y, wallType)) return false;

        // \ walls
        if (wallType == WallType.Backslash)
        {
            // edge of screen -> not playable
            if (!HasTile(x, y) || !HasTile(x, y + 1)) return false;

            // adjacent tiles both mountains or goo -> not playable
            {
                if (GetTileTypeOnPosition(x, y) == TileType.Mountain && GetTileTypeOnPosition(x, y + 1) == TileType.Mountain) return false;
                if (GetTileTypeOnPosition(x, y) == TileType.Goo && GetTileTypeOnPosition(x, y + 1) == TileType.Goo) return false;
            }

            // adjacent tiles are nature -> playable
            {
                // adjacent tiles
                if (GetTileTypeOnPosition(x, y) == TileType.Nature) return true;
                if (GetTileTypeOnPosition(x, y + 1) == TileType.Nature) return true;
                // tiles adjacent to end of wall
                if (HasTile(x - 1, y + 1) && GetTileTypeOnPosition(x - 1, y + 1) == TileType.Nature) return true;
                if (HasTile(x + 1, y) && GetTileTypeOnPosition(x + 1, y) == TileType.Nature) return true;
            }
            
            // one of 4 adjacent walls existing -> playable
            {
                if (HasWall(x - 1, y + 1, WallType.Vertical)) return true;
                if (HasWall(x - 1, y + 1, WallType.Slash)) return true;
                if (HasWall(x, y + 1, WallType.Slash)) return true;
                if (HasWall(x, y, WallType.Vertical)) return true;
            }
        }

        // | walls
        if (wallType == WallType.Vertical)
        {
            // edge of screen -> not playable
            if (!HasTile(x, y) || !HasTile(x + 1, y)) return false;

            // adjacent tiles both mountains or goo -> not playable
            {
                if (GetTileTypeOnPosition(x, y) == TileType.Mountain && GetTileTypeOnPosition(x + 1, y) == TileType.Mountain) return false;
                if (GetTileTypeOnPosition(x, y) == TileType.Goo && GetTileTypeOnPosition(x + 1, y) == TileType.Goo) return false;
            }

            // adjacent tiles are nature -> playable
            {
                // adjacent tiles
                if (GetTileTypeOnPosition(x, y) == TileType.Nature) return true;
                if (GetTileTypeOnPosition(x + 1, y) == TileType.Nature) return true;
                // tiles adjacent to end of wall
                if (HasTile(x, y + 1) && GetTileTypeOnPosition(x, y + 1) == TileType.Nature) return true;
                if (HasTile(x + 1, y - 1) && GetTileTypeOnPosition(x + 1, y - 1) == TileType.Nature) return true;
            }

            // one of 4 adjacent walls existing -> playable
            {
                if (HasWall(x, y, WallType.Backslash)) return true;
                if (HasWall(x, y + 1, WallType.Slash)) return true;
                if (HasWall(x, y, WallType.Slash)) return true;
                if (HasWall(x + 1, y - 1, WallType.Backslash)) return true;
            }
        }

        // / walls
        if (wallType == WallType.Slash)
        {
            // edge of screen -> not playable
            if (!HasTile(x, y) || !HasTile(x + 1, y - 1)) return false;

            // adjacent tiles both mountains or goo -> not playable
            {
                if (GetTileTypeOnPosition(x, y) == TileType.Mountain && GetTileTypeOnPosition(x + 1, y - 1) == TileType.Mountain) return false;
                if (GetTileTypeOnPosition(x, y) == TileType.Goo && GetTileTypeOnPosition(x + 1, y - 1) == TileType.Goo) return false;
            }

            // adjacent tiles are nature -> playable
            {
                // adjacent tiles
                if (GetTileTypeOnPosition(x, y) == TileType.Nature) return true;
                if (GetTileTypeOnPosition(x + 1, y - 1) == TileType.Nature) return true;
                // tiles adjacent to end of wall
                if (HasTile(x + 1, y) && GetTileTypeOnPosition(x + 1, y) == TileType.Nature) return true;
                if (HasTile(x, y - 1) && GetTileTypeOnPosition(x, y - 1) == TileType.Nature) return true;
            }

            // one of 4 adjacent walls existing -> playable
            {
                if (HasWall(x, y, WallType.Vertical)) return true;
                if (HasWall(x, y - 1, WallType.Backslash)) return true;
                if (HasWall(x + 1, y - 1, WallType.Backslash)) return true;
                if (HasWall(x, y - 1, WallType.Vertical)) return true;
            }
        }

        // any other case -> not playable
        return false;
    }

    public bool ExecuteGooMove()
    {
        System.Random random = new System.Random();
        if (random.Next(6) == 0)
        {
            bool jumped = JumpGoo();
            if (jumped) return true;
            bool expanded = ExpandGoo();
            if (expanded) return true;
        }
        else
        {
            bool expanded = ExpandGoo();
            if (expanded) return true;
            bool jumped = JumpGoo();
            if (jumped) return true;
        }
        return false;
    }
    private bool ExpandGoo()
    {
        List<Tile> gooTiles = GooTilesThatCanExpand();
        if (gooTiles.Count == 0) return false;
        System.Random random = new System.Random();
        int index = random.Next(gooTiles.Count);
        Tile tile = gooTiles[index];
        List<Tile> possibleTiles = TilesGooCanSpread(tile.x, tile.y);
        if ((possibleTiles.Count) <= 2) ChangeTileType(possibleTiles[0].x, possibleTiles[0].y, TileType.Goo);
        if ((possibleTiles.Count) == 2) ChangeTileType(possibleTiles[1].x, possibleTiles[1].y, TileType.Goo);
        int index1 = random.Next(possibleTiles.Count);
        int index2 = -1;
        do
        {
            index2 = random.Next(possibleTiles.Count);
        }
        while (index1 == index2);
        ChangeTileType(possibleTiles[index1].x, possibleTiles[index1].y, TileType.Goo);
        ChangeTileType(possibleTiles[index2].x, possibleTiles[index2].y, TileType.Goo);
        return true;
    }

    private bool JumpGoo()
    {
        List<Tile> possibleTiles = TilesThatGooCanJumpTo();
        if (possibleTiles.Count == 0) return false;
        System.Random random = new System.Random();
        int index = random.Next(possibleTiles.Count);
        ChangeTileType(possibleTiles[index].x, possibleTiles[index].y, TileType.Goo);
        return true;
    }

    private List<Tile> GooTilesThatCanExpand ()
    {
        List<Tile> returnValue = new List<Tile>();
        foreach (Tile tile in tiles)
        {
            if (tile.type != TileType.Goo) continue;
            if (NumberOfTilesGooCanSpread(tile.x, tile.y) >= 2) returnValue.Add(tile);
        }
        if (returnValue.Count > 0) return returnValue;
        foreach (Tile tile in tiles)
        {
            if (tile.type != TileType.Goo) continue;
            if (NumberOfTilesGooCanSpread(tile.x, tile.y) >= 1) returnValue.Add(tile);
        }
        return returnValue;
    }

    private List<Tile> TilesThatGooCanJumpTo ()
    {
        List<Tile> returnValue = new List<Tile>();
        foreach (Tile gooTile in tiles)
        { 
            List<Tile> canJumpToTiles = TilesGooCanJump(gooTile.x, gooTile.y);
            foreach (Tile canJumpToTile in canJumpToTiles)
            if (!returnValue.Find(tile => tile.x == canJumpToTile.x && tile.y == canJumpToTile.y)) returnValue.Add(canJumpToTile);
        }
        return returnValue;
    }

    private int NumberOfTilesGooCanSpread(int x, int y)
    {
        return TilesGooCanSpread(x, y).Count;
    }

    private List<Tile> TilesGooCanSpread (int x, int y)
    {
        Tile gooTile = tiles.Find(tile => tile.x == x && tile.y == y);
        if (gooTile == null) return null;
        List<Tile> returnValue = new List<Tile>();
        {
            if (!TileHasLeftWall(x, y) && HasTile(x - 1, y) && GetTileTypeOnPosition(x - 1, y) == TileType.Waste) returnValue.Add(GetTile(x - 1, y));
            if (!TileHasTopLeftWall(x, y) && HasTile(x - 1, y + 1) && GetTileTypeOnPosition(x - 1, y + 1) == TileType.Waste) returnValue.Add(GetTile(x - 1, y + 1));
            if (!TileHasTopRightWall(x, y) && HasTile(x, y + 1) && GetTileTypeOnPosition(x, y + 1) == TileType.Waste) returnValue.Add(GetTile(x, y + 1));
            if (!TileHasRightWall(x, y) && HasTile(x + 1, y) && GetTileTypeOnPosition(x + 1, y) == TileType.Waste) returnValue.Add(GetTile(x + 1, y));
            if (!TileHasBottomRightWall(x, y) && HasTile(x + 1, y - 1) && GetTileTypeOnPosition(x + 1, y - 1) == TileType.Waste) returnValue.Add(GetTile(x + 1, y - 1));
            if (!TileHasBottomLeftWall(x, y) && HasTile(x, y - 1) && GetTileTypeOnPosition(x, y - 1) == TileType.Waste) returnValue.Add(GetTile(x, y - 1));
        }
        return returnValue;
    }

    private List<Tile> TilesGooCanJump (int x, int y)
    {
        Tile gooTile = tiles.Find(tile => tile.x == x && tile.y == y);
        if (gooTile == null) return null;
        List<Tile> returnValue = new List<Tile>();
        {
            if (HasTile(x - 3, y) && GetTileTypeOnPosition(x - 3, y) == TileType.Waste) returnValue.Add(GetTile(x - 3, y));
            if (HasTile(x - 3, y + 3) && GetTileTypeOnPosition(x - 3, y + 3) == TileType.Waste) returnValue.Add(GetTile(x - 3, y + 3));
            if (HasTile(x, y + 3) && GetTileTypeOnPosition(x, y + 3) == TileType.Waste) returnValue.Add(GetTile(x, y + 3));
            if (HasTile(x + 3, y) && GetTileTypeOnPosition(x + 3, y) == TileType.Waste) returnValue.Add(GetTile(x + 3, y));
            if (HasTile(x + 3, y - 3) && GetTileTypeOnPosition(x + 3, y - 3) == TileType.Waste) returnValue.Add(GetTile(x + 3, y - 3));
            if (HasTile(x, y - 3) && GetTileTypeOnPosition(x, y - 3) == TileType.Waste) returnValue.Add(GetTile(x, y - 3));
        }
        return returnValue;
    }
}
