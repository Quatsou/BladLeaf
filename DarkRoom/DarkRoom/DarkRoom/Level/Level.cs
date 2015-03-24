using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class Level : GameObjectList
{
    float clockTimer;
    public float timer;
    public SpriteFont font;
    public bool completed;
    public Door door;
    public List<LightSource> lightSources;
    public int sizeX, sizeY;
    PauseButton pauseButton;

    public Level(int sizeX, int sizeY, TileType[,] levelConfig, List<Enemy> enemies, List<Friendly> friendlies, float timer)
    {
        completed = false;
        this.timer = timer;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("levelFont");

        pauseButton = new PauseButton();
        pauseButton.Position = new Vector2(1806, 50);
        this.Add(pauseButton);

        Player player = new Player(new Vector2(20, 100));
        this.Add(player);

        TileField tiles = new TileField(sizeY, sizeX, 0, "tiles");
        this.Add(tiles);

        float startX = Camera.camPos.X - ((sizeX * Tile.TILESIZE) / 2);
        float startY = Camera.camPos.Y - ((sizeY * Tile.TILESIZE) / 2);
        tiles.fieldAnchor = new Vector2(startX, startY);
        tiles.fieldLength = new Vector2(sizeX * Tile.TILESIZE, sizeY * Tile.TILESIZE);

        for (int x = 0; x < sizeX; x++)
            for (int y = 0; y < sizeY; y++)
            {
                tiles.Add(new Tile(new Vector2(startX + (x * Tile.TILESIZE), startY + (y * Tile.TILESIZE))
                    , levelConfig[x, y]), x, y);

                if (levelConfig[x, y] == TileType.Door)
                { 
                    player.Position = new Vector2(startX + (x * Tile.TILESIZE) + 32, startY + (y * Tile.TILESIZE) + 28);
                    door = new Door(new Vector2(startX + (x * Tile.TILESIZE), startY + 60 + (y * Tile.TILESIZE)));
                    this.Add(door);
                }
                else if (levelConfig[x, y] == TileType.LightSource)
                {
                    LightSource temp = new LightSource(new Vector2(startX + (x * Tile.TILESIZE), startY + 60 + (y * Tile.TILESIZE)));
                    lightSources.Add(temp);
                    this.Add(temp);
                }
            }

        GameObjectList enemyList = new GameObjectList(2, "enemyList") as GameObjectList;
        this.Add(enemyList);
        foreach (Enemy e in enemies)
        {
            e.Reset();
            e.Position = new Vector2(startX + 32 + (e.Coords.X * Tile.TILESIZE), startY + 32 + (e.Coords.Y * Tile.TILESIZE));
            enemyList.Add(e);
            Console.WriteLine(e.Position);
        }

        GameObjectList friendlyList = new GameObjectList(2, "friendlyList") as GameObjectList;
        this.Add(friendlyList);
        foreach (Friendly f in friendlies)
        {
            f.Reset();
            f.Position = new Vector2(startX + 32 + (f.Coords.X * Tile.TILESIZE), startY + 32 + (f.Coords.Y * Tile.TILESIZE));
            friendlyList.Add(f);
            Console.WriteLine("Added friendly");
        }

        //Remove previous door arrow
        DoorArrow doorArrow = GameWorld.Find("doorarrow") as DoorArrow;
        if(doorArrow != null)
            GameWorld.Remove(doorArrow);
    }
}
