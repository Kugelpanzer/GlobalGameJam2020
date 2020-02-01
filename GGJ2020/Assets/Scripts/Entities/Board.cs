using System.Collections;
using System.Collections.Generic;

public class Board
{
    List<Tile> tiles = new List<Tile>();
    List<Wall> walls = new List<Wall>();

    public Board ()
    {
        tiles.Clear();
        walls.Clear();
    }

    public void AddTile (GameMode gameMode, Tile tile)
    {
        if (!tileAllowed(gameMode, tile)) return;
        RemoveTile(tile.x, tile.y);
        tiles.Add(tile);
    }

    private void RemoveTile(int x, int y)
    {
        tiles.RemoveAll(tile => tile.x == x && tile.y == y);
    }

    public Tile GetTile (int x, int y)
    {
        return tiles.Find(tile => tile.x == x && tile.y == y);
    }
    public bool hasTile (int x, int y)
    {
        return GetTile(x, y) != null;
    }

    public bool hasWall (int x, int y, WallType wallType)
    {
        return walls.Find(wall => wall.x == x && wall.y == y && wall.type == wallType) != null;
    }

    public bool tileAllowed (GameMode gameMode, Tile tile)
    {
        if (gameMode == GameMode.Edit) return true;
        if (!tiles.Contains(tile)) return true;
        if (getTileTypeOnPosition(tile.x, tile.y) == TileType.Waste) return true;
        return false;
    }

    private TileType getTileTypeOnPosition (int x, int y)
    {
        Tile tile = GetTile(x, y);
        if (tile != null) return tile.type;
        return TileType.Mountain;
    }

    public bool isNatureTilePlayable (int x, int y)
    {
        return (tiles.Find(tile => tile.x == x && tile.y == y)).type == TileType.Waste;
    }

    public bool isNatureWallPlayable (int x, int y, WallType wallType)
    {
        // wall already exists -> not playable
        if (hasWall(x, y, wallType)) return false;

        // \ walls
        if (wallType == WallType.Backslash)
        {
            // edge of screen -> not playable
            if (!hasTile(x, y) || !hasTile(x, y + 1)) return false;

            // adjacent tiles both mountains or goo -> not playable
            {
                if (getTileTypeOnPosition(x, y) == TileType.Mountain && getTileTypeOnPosition(x, y + 1) == TileType.Mountain) return false;
                if (getTileTypeOnPosition(x, y) == TileType.Goo && getTileTypeOnPosition(x, y + 1) == TileType.Goo) return false;
            }

            // adjacent tiles are nature -> playable
            {
                // adjacent tiles
                if (getTileTypeOnPosition(x, y) == TileType.Nature) return true;
                if (getTileTypeOnPosition(x, y + 1) == TileType.Nature) return true;
                // tiles adjacent to end of wall
                if (hasTile(x - 1, y + 1) && getTileTypeOnPosition(x - 1, y + 1) == TileType.Nature) return true;
                if (hasTile(x + 1, y) && getTileTypeOnPosition(x + 1, y) == TileType.Nature) return true;
            }
            
            // one of 4 adjacent walls existing -> playable
            {
                if (hasWall(x - 1, y + 1, WallType.Vertical)) return true;
                if (hasWall(x - 1, y + 1, WallType.Slash)) return true;
                if (hasWall(x, y + 1, WallType.Slash)) return true;
                if (hasWall(x, y, WallType.Vertical)) return true;
            }
        }

        // | walls
        if (wallType == WallType.Backslash)
        {
            // edge of screen -> not playable
            if (!hasTile(x, y) || !hasTile(x + 1, y)) return false;

            // adjacent tiles both mountains or goo -> not playable
            {
                if (getTileTypeOnPosition(x, y) == TileType.Mountain && getTileTypeOnPosition(x + 1, y) == TileType.Mountain) return false;
                if (getTileTypeOnPosition(x, y) == TileType.Goo && getTileTypeOnPosition(x + 1, y) == TileType.Goo) return false;
            }

            // adjacent tiles are nature -> playable
            {
                // adjacent tiles
                if (getTileTypeOnPosition(x, y) == TileType.Nature) return true;
                if (getTileTypeOnPosition(x + 1, y) == TileType.Nature) return true;
                // tiles adjacent to end of wall
                if (hasTile(x, y + 1) && getTileTypeOnPosition(x, y + 1) == TileType.Nature) return true;
                if (hasTile(x + 1, y - 1) && getTileTypeOnPosition(x + 1, y - 1) == TileType.Nature) return true;
            }

            // one of 4 adjacent walls existing -> playable
            {
                if (hasWall(x, y, WallType.Backslash)) return true;
                if (hasWall(x, y + 1, WallType.Slash)) return true;
                if (hasWall(x, y, WallType.Slash)) return true;
                if (hasWall(x + 1, y - 1, WallType.Backslash)) return true;
            }
        }

        // / walls
        if (wallType == WallType.Backslash)
        {
            // edge of screen -> not playable
            if (!hasTile(x, y) || !hasTile(x + 1, y - 1)) return false;

            // adjacent tiles both mountains or goo -> not playable
            {
                if (getTileTypeOnPosition(x, y) == TileType.Mountain && getTileTypeOnPosition(x + 1, y - 1) == TileType.Mountain) return false;
                if (getTileTypeOnPosition(x, y) == TileType.Goo && getTileTypeOnPosition(x + 1, y - 1) == TileType.Goo) return false;
            }

            // adjacent tiles are nature -> playable
            {
                // adjacent tiles
                if (getTileTypeOnPosition(x, y) == TileType.Nature) return true;
                if (getTileTypeOnPosition(x + 1, y - 1) == TileType.Nature) return true;
                // tiles adjacent to end of wall
                if (hasTile(x + 1, y) && getTileTypeOnPosition(x + 1, y) == TileType.Nature) return true;
                if (hasTile(x, y - 1) && getTileTypeOnPosition(x, y - 1) == TileType.Nature) return true;
            }

            // one of 4 adjacent walls existing -> playable
            {
                if (hasWall(x, y, WallType.Vertical)) return true;
                if (hasWall(x, y - 1, WallType.Backslash)) return true;
                if (hasWall(x + 1, y - 1, WallType.Backslash)) return true;
                if (hasWall(x, y - 1, WallType.Vertical)) return true;
            }
        }

        // any other case -> not playable
        return false;
    }
}
