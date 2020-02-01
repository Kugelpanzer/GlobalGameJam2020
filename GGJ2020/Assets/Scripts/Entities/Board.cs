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
        // \ walls
        if (wallType == WallType.Backslash)
        {
            // edge of screen is not playable
            if (!hasTile(x, y) || !hasTile(x, y + 1)) return false;
            // adjacent tiles
            if (getTileTypeOnPosition(x, y) == TileType.Nature) return true;
            if (getTileTypeOnPosition(x, y + 1) == TileType.Nature) return true;
            // tiles adjacent to end of wall
            if (hasTile(x - 1, y + 1) && getTileTypeOnPosition(x - 1, y + 1) == TileType.Nature) return true;
            if (hasTile(x + 1, y) && getTileTypeOnPosition(x + 1, y) == TileType.Nature) return true;
        }
        // | walls
        if (wallType == WallType.Backslash)
        {
            // edge of screen is not playable
            if (!hasTile(x, y) || !hasTile(x + 1, y)) return false;
            // adjacent tiles
            if (getTileTypeOnPosition(x, y) == TileType.Nature) return true;
            if (getTileTypeOnPosition(x + 1, y) == TileType.Nature) return true;
            // tiles adjacent to end of wall
            if (hasTile(x, y + 1) && getTileTypeOnPosition(x, y + 1) == TileType.Nature) return true;
            if (hasTile(x + 1, y - 1) && getTileTypeOnPosition(x + 1, y - 1) == TileType.Nature) return true;
        }
        // / walls
        if (wallType == WallType.Backslash)
        {
            // edge of screen is not playable
            if (!hasTile(x, y) || !hasTile(x + 1, y - 1)) return false;
            // adjacent tiles
            if (getTileTypeOnPosition(x, y) == TileType.Nature) return true;
            if (getTileTypeOnPosition(x + 1, y - 1) == TileType.Nature) return true;
            // tiles adjacent to end of wall
            if (hasTile(x + 1, y) && getTileTypeOnPosition(x + 1, y) == TileType.Nature) return true;
            if (hasTile(x, y - 1) && getTileTypeOnPosition(x, y - 1) == TileType.Nature) return true;
        }
        // TODO
        return false;
    }
}
