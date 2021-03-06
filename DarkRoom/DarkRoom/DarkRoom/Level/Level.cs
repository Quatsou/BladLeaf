﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class Level : GameObjectList
{
    float clockTimer;
    public float timer, randomTimer, endTimer;
    public float[] rotations;
    public SpriteFont font;
    public bool completed, finishedRandomizing;
    public Door door;
    public List<LightSource> lightSources = new List<LightSource>();
    public int sizeX, sizeY, randomizeCounter;
    PauseButton pauseButton;
    public TileType[,] levelLayout;
    public static bool failed, killedEnemies;

    public Level(int sizeX, int sizeY, TileType[,] levelConfig, List<Enemy> enemies, List<Friendly> friendlies, float timer)
    {
        ShadowMap.flashLightMode = false;

        rotations = new float[8];
        rotations[0] = 0f;
        rotations[1] = 0.785f;
        rotations[2] = 1.570f;
        rotations[3] = 2.356f;
        rotations[4] = 3.141f;
        rotations[5] = 3.927f;
        rotations[6] = 4.712f;
        rotations[7] = 5.498f;

        completed = false;
        failed = false;
        killedEnemies = false;
        finishedRandomizing = false;
        randomTimer = 1.5f;
        randomizeCounter = 5;
        endTimer = 2f;
        this.timer = timer;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        levelLayout = levelConfig;
        font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("levelFont");

        pauseButton = new PauseButton();
        pauseButton.Position = new Vector2(1806, 50);
        this.Add(pauseButton);

        Player player = new Player(new Vector2(20, 100));
        this.Add(player);
        this.Add(new Hands(player));

        TileField tiles = new TileField(sizeY, sizeX, 0, "tiles");
        this.Add(tiles);
                
        float startX = Camera.camPos.X - ((sizeX * Tile.TILESIZE) / 2);
        float startY = Camera.camPos.Y - ((sizeY * Tile.TILESIZE) / 2);
        tiles.fieldAnchor = new Vector2(startX, startY);
        tiles.fieldLength = new Vector2(sizeX * Tile.TILESIZE, sizeY * Tile.TILESIZE);

        GameWorld.Add(new BlackBar(new Vector2(startX - 192, startY), "Sprites/spr_black_side"));
        GameWorld.Add(new BlackBar(new Vector2(startX + tiles.fieldLength.X, startY), "Sprites/spr_black_side"));
        GameWorld.Add(new BlackBar(new Vector2(startX, startY - 192), "Sprites/spr_black_top"));
        GameWorld.Add(new BlackBar(new Vector2(startX, startY + tiles.fieldLength.Y), "Sprites/spr_black_top"));

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
                    lightSources.Add(new LightSource(new Vector2(x, y)));
                    LightSource temp = new LightSource(new Vector2(startX + 32 + (x * Tile.TILESIZE), startY + 32 + (y * Tile.TILESIZE)));
                    this.Add(temp);
                }
            }

        GameObjectList enemyList = new GameObjectList(2, "enemyList") as GameObjectList;
        this.Add(enemyList);
        foreach (Enemy e in enemies)
        {
            e.Reset();
            Cone c = new Cone("spr_cone_up", e);
            GameWorld.Add(c);
            e.cone = c;
            enemyList.Add(e);
        }

        GameObjectList friendlyList = new GameObjectList(2, "friendlyList") as GameObjectList;
        this.Add(friendlyList);
        foreach (Friendly f in friendlies)
        {
            f.Reset();
            friendlyList.Add(f);
        }

        Randomize();

        //Remove previous door arrow
        DoorArrow doorArrow = GameWorld.Find("doorarrow") as DoorArrow;
        if(doorArrow != null)
            GameWorld.Remove(doorArrow);

        ShadowMap shadowMap = new ShadowMap(sizeX, sizeY, levelConfig, lightSources, new Vector2(startX, startY), player);
        this.Add(shadowMap);
        this.Add(new BlackScreen());
    }

    public void Randomize()
    {
        GameObjectList enemyList = GameWorld.Find("enemyList") as GameObjectList;
        GameObjectList friendlyList = GameWorld.Find("friendlyList") as GameObjectList;

        foreach (Enemy e in enemyList.Objects)
        {
            e.Position = CalculateRandomPos();
            e.Sprite.Rotation = rotations[GameEnvironment.Random.Next(0, rotations.Length - 1)];
            e.cone.AdjustCone();
        }
        foreach (Friendly f in friendlyList.Objects)
        {
            f.Position = CalculateRandomPos();
            float rnd = (float)GameEnvironment.Random.Next(0, 64) / 10;
            f.Sprite.Rotation = ((float)GameEnvironment.Random.NextDouble() * rnd);
        }
        randomizeCounter--;
    }

    public void FinishRandomizing()
    {
        TileField tiles = GameWorld.Find("tiles") as TileField;
        GameObjectList enemyList = GameWorld.Find("enemyList") as GameObjectList;
        GameObjectList friendlyList = GameWorld.Find("friendlyList") as GameObjectList;

        int config = 0;

        if (enemyList.Objects.Count != 0)
        {
            Enemy subject = enemyList.Objects[0] as Enemy;
            config = GameEnvironment.Random.Next(0, subject.Coords.Length);
        }
        else
        {
            Friendly subject = friendlyList.Objects[0] as Friendly;
            config = GameEnvironment.Random.Next(0, subject.Coords.Length);
        }

        foreach (Enemy e in enemyList.Objects)
        {
            e.Position = new Vector2(tiles.fieldAnchor.X + 32 + (e.Coords[config].X * Tile.TILESIZE), tiles.fieldAnchor.Y + 32 + (e.Coords[config].Y * Tile.TILESIZE));
            e.Sprite.Rotation = e.startRotation[config];
            e.cone.AdjustCone();
        }
        foreach (Friendly f in friendlyList.Objects)
        {
            f.Position = new Vector2(tiles.fieldAnchor.X + 32 + (f.Coords[config].X * Tile.TILESIZE), tiles.fieldAnchor.Y + 32 + (f.Coords[config].Y * Tile.TILESIZE));
            f.Sprite.Rotation = f.startRotation[config];
        }
    }

    public Vector2 CalculateRandomPos()
    {

        TileField tiles = GameWorld.Find("tiles") as TileField;
        Vector2 fieldDimensions = new Vector2(tiles.fieldLength.X / 64, tiles.fieldLength.Y / 64);

        int x = GameEnvironment.Random.Next(0, (int)fieldDimensions.X + 1);
        int y = GameEnvironment.Random.Next(0, (int)fieldDimensions.Y + 1);

        if (tiles.GetTileType(x, y) == TileType.Wall || tiles.GetTileType(x, y) == TileType.Door)
            return CalculateRandomPos();
        else
        {
            GameObjectList enemyList = GameWorld.Find("enemyList") as GameObjectList;
            GameObjectList friendlyList = GameWorld.Find("friendlyList") as GameObjectList;
            foreach (Enemy e in enemyList.Objects)
            {
                if (e.Position == new Vector2(tiles.fieldAnchor.X + 32 + (x * 64), tiles.fieldAnchor.Y + 32 + (y * 64)))
                    return CalculateRandomPos();
            }
            foreach (Friendly f in friendlyList.Objects)
            {
                if (f.Position == new Vector2(tiles.fieldAnchor.X + 32 + (x * 64), tiles.fieldAnchor.Y + 32 + (y * 64)))
                    return CalculateRandomPos();
            }
            return new Vector2(tiles.fieldAnchor.X + 32 + (x * 64), tiles.fieldAnchor.Y + 32 + (y * 64));
        }
    }
}
