using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class Level : GameObjectList
{
    public Level(int sizeX, int sizeY, TileType[,] levelConfig)
    {
        Player player = new Player(new Vector2(120, 100));
        this.Add(player);

        TileField tiles = new TileField(sizeY, sizeX, 0, "tiles");
        this.Add(tiles);

        for (int x = 0; x < sizeX; x++)
            for (int y = 0; y < sizeY; y++)
            {
                tiles.Add(new Tile(new Vector2(((15 - sizeX / 2) * 64) + (x * 64), ((8 - sizeY / 2) * 64) + (y * 64))
                    , levelConfig[x, y]), x, y);
            }
    }
}
