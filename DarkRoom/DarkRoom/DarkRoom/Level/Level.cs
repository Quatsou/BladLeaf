using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class Level : GameObjectList
{
    public float timer;
    public SpriteFont font;

    public Level(int sizeX, int sizeY, TileType[,] levelConfig, List<Source> lightSources, List<Enemy> enemies, List<Friendly> friendlies, float timer)
    {
        this.timer = timer;
        font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("levelFont");

        Player player = new Player(new Vector2(20, 100));
        this.Add(player);

        Lights lights = new Lights();
        this.Add(lights);

        TileField tiles = new TileField(sizeY, sizeX, 0, "tiles");
        this.Add(tiles);

        float startX = Camera.camPos.X - ((sizeX * 64) / 2);
        float startY = Camera.camPos.Y - ((sizeY * 64) / 2);
        tiles.fieldAnchor = new Vector2(startX, startY);
        tiles.fieldLength = new Vector2(sizeX * 64, sizeY * 64);

        for (int x = 0; x < sizeX; x++)
            for (int y = 0; y < sizeY; y++)
            {
                tiles.Add(new Tile(new Vector2(startX + (x * 64), startY + (y * 64))
                    , levelConfig[x, y]), x, y);

                if (levelConfig[x, y] == TileType.Door)
                   player.Position = new Vector2(startX + (x * 64) + 32, startY + (y * 64) + 28);
            }

        foreach (Source s in lightSources)
            lights.Add(s);

        GameObjectList enemyList = new GameObjectList(2, "enemyList") as GameObjectList;
        this.Add(enemyList);
        foreach (Enemy e in enemies)
        {
            e.Position = new Vector2(startX + 32 + (e.Position.X * 64), startY + 32 + (e.Position.Y * 64));
            enemyList.Add(e);
        }

        GameObjectList friendlyList = new GameObjectList(2, "friendlyList") as GameObjectList;
        this.Add(friendlyList);
        foreach (Friendly f in friendlies)
        {
            f.Position = new Vector2(startX + 32 + (f.Position.X * 64), startY + 32 + (f.Position.Y * 64));
            friendlyList.Add(f);
        }
    }
}
