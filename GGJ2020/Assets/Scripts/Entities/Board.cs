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

    public bool tileAllowed (GameMode gameMode, Tile tile)
    {
        if (gameMode == GameMode.Edit) return true;
        if (!tiles.Contains(tile)) return true;
        if (getTileTypeOnPosition(tile.x, tile.y) == TileType.Waste) return true;
        return false;
    }

    private TileType getTileTypeOnPosition (int x, int y)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.x == x && tile.y == y) return tile.type;
        }
        return TileType.Mountain;
    }
}
