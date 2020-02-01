using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    List<Tile> tiles = new List<Tile>();
    List<Wall> walls = new List<Wall>();

    List<Tile> surroundedGooTiles = new List<Tile>();
    List<Tile> notSurroundedGooTiles = new List<Tile>();

    public Board ()
    {
        tiles.Clear();
        walls.Clear();
    }

    public void AddWall (Wall wall)
    {
        walls.Add(wall);
        RecalculateSurroundedTiles();
    }

    public void ChangeTileType (int x, int y, TileType tileType)
    {
        Tile tile = tiles.Find(existingTile => existingTile.x == x && existingTile.y == y);
        if (tile == null) return;
        tile.type = tileType;
        if (tileType == TileType.Goo) RecalculateSurroundedTiles();
    }

    private void RemoveTile(int x, int y)
    {
        Tile tileToRemove = GetTile(x, y);
        tiles.RemoveAll(tile => tile.x == x && tile.y == y);
        Destroy(tileToRemove.gameObject);
    }

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

    private void RecalculateSurroundedTiles ()
    {
        surroundedGooTiles.Clear();
        notSurroundedGooTiles.Clear();
        foreach (Tile tile in tiles)
        {
            TileIsGooThatIsSurrounded(tile.x, tile.y);
        }
    }

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
            if (!TileHasLeftWall(x, y))
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x - 1, y, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!TileHasTopLeftWall(x, y))
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x - 1, y + 1, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!TileHasTopRightWall(x, y))
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x, y + 1, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!TileHasRightWall(x, y))
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x + 1, y, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!TileHasBottomRightWall(x, y))
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x + 1, y - 1, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!TileHasBottomLeftWall(x, y))
            {
                checkedTiles.Add(tile);
                if (!TileIsGooThatIsSurrounded(x, y - 1, checkedTiles, ref surroundedTiles)) return false;
            }
            if (!surroundedTiles.Find(surroundedTile => surroundedTile.x == x && surroundedTile.y == y)) surroundedTiles.Add(tile);
        }
        return true;
    }

    public bool IsNatureTilePlayable (int x, int y)
    {
        return (tiles.Find(tile => tile.x == x && tile.y == y)).type == TileType.Waste;
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
        if (wallType == WallType.Backslash)
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
        if (wallType == WallType.Backslash)
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
}
