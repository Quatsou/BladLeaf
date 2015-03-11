class TileField : GameObjectGrid
{
    public TileField(int rows, int columns, int layer = 0, string id = "")
        : base(rows, columns, layer, id)
    {
    }

    public TileType GetTileType(int x, int y)
    {
        if (x < 0 || x >= Columns)
            return TileType.Wall;
        if (y < 0 || y >= Rows)
            return TileType.Wall;
        Tile current = this.Objects[x, y] as Tile;
        return current.type;
    }
}
