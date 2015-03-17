using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class Level : GameObjectList
{
    public Level(int sizeX, int sizeY, TileType[,] levelConfig, List<Source> lightSources)
    {
        Player player = new Player(new Vector2(120, 100));
        this.Add(player);

        Lights lights = new Lights(new Flashlight(player));
        this.Add(lights);

        TileField tiles = new TileField(sizeY, sizeX, 0, "tiles");
        this.Add(tiles);

        float startX = Camera.camPos.X - ((sizeX * 64) / 2);
        float startY = Camera.camPos.Y - ((sizeY * 64) / 2);

        for (int x = 0; x < sizeX; x++)
            for (int y = 0; y < sizeY; y++)
            {
                Tile tile = new Tile(new Vector2(startX + (x * 64), startY + (y * 64)), levelConfig[x,y], x, y);
                tiles.Add(tile, x, y);
            }

        foreach (Source s in lightSources)
            lights.Add(s);
    }
}
