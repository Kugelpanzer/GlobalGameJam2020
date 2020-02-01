public class Level01 : Level
{
    public new void initLevel ()
    {
        base.initLevel();

        // init board
        board.AddTile(GameMode.Edit, new Tile(-4, 3, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(-3, 3, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(-2, 3, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(-1, 3, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(-0, 3, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(1, 3, TileType.Waste));

        board.AddTile(GameMode.Edit, new Tile(-4, 2, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(-3, 2, TileType.Nature));
        board.AddTile(GameMode.Edit, new Tile(-2, 2, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(-1, 2, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(0, 2, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(1, 2, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(2, 2, TileType.Mountain));

        board.AddTile(GameMode.Edit, new Tile(-3, 1, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(-2, 1, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(-1, 1, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(0, 1, TileType.Mountain));
        board.AddTile(GameMode.Edit, new Tile(1, 1, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(2, 1, TileType.Waste));

        board.AddTile(GameMode.Edit, new Tile(-3, 0, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(-2, 0, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(-1, 0, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(0, 0, TileType.Mountain));
        board.AddTile(GameMode.Edit, new Tile(1, 0, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(2, 0, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(3, 0, TileType.Waste));

        board.AddTile(GameMode.Edit, new Tile(-2, -1, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(-1, -1, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(0, -1, TileType.Mountain));
        board.AddTile(GameMode.Edit, new Tile(1, -1, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(2, -1, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(3, -1, TileType.Waste));

        board.AddTile(GameMode.Edit, new Tile(-2, -2, TileType.Mountain));
        board.AddTile(GameMode.Edit, new Tile(-1, -2, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(0, -2, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(1, -2, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(2, -2, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(3, -2, TileType.Goo));
        board.AddTile(GameMode.Edit, new Tile(4, -2, TileType.Waste));

        board.AddTile(GameMode.Edit, new Tile(-1, -3, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(0, -3, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(1, -3, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(2, -3, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(3, -3, TileType.Waste));
        board.AddTile(GameMode.Edit, new Tile(4, -3, TileType.Waste));
    }
}
