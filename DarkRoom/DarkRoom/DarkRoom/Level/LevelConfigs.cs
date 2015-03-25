using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class LevelConfigs
{
    public static int levelsCompleted = 5;
    public List<TileType[,]> configurations = new List<TileType[,]>();
    public List<int> sizesX = new List<int>();
    public List<int> sizesY = new List<int>();
    public List<List<Enemy>> enemiesConfig = new List<List<Enemy>>();
    public List<List<Friendly>> friendliesConfig = new List<List<Friendly>>();
    public List<float> timerConfig = new List<float>();

    public LevelConfigs()
    {   
        //Level1
        List<Enemy> enemies1 = new List<Enemy>();
        enemies1.Add(new Enemy(new Vector2(4, 2), 0f));

        List<Friendly> friendlies1 = new List<Friendly>();
        friendlies1.Add(new Friendly(new Vector2(4, 0), 3.14f));

        TileType[,] level1 = new TileType[9, 6];
        level1[2, 5] = TileType.Door;
        level1[2, 4] = TileType.LightSource;
        CreateConfig(9, 6, level1, enemies1, friendlies1, 5);

        //Level2
        List<Enemy> enemies2 = new List<Enemy>();
        enemies2.Add(new Enemy(new Vector2(6, 2), 0f));

        List<Friendly> friendlies2 = new List<Friendly>();
        friendlies2.Add(new Friendly(new Vector2(6, 0), 3.14f));

        TileType[,] level2 = new TileType[13, 8];
        level2[6, 7] = TileType.Door;
        level1[2, 4] = TileType.LightSource;
        level1[7, 1] = TileType.LightSource;
        for (int x = 3; x < 5; x++)
            for (int y = 0; y < 3; y++)
                level2[x, y] = TileType.Wall;
        for (int x = 8; x < 10; x++)
            for (int y = 0; y < 3; y++)
                level2[x, y] = TileType.Wall;

        level2[7, 1] = TileType.LightSource;
        CreateConfig(13, 8, level2, enemies2, friendlies2, 60);
    }

    public void CreateConfig(int sizeX, int sizeY, TileType[,] config, List<Enemy> enemies, List<Friendly> friendlies, float timer)
    {
        sizesX.Add(sizeX);
        sizesY.Add(sizeY);
        configurations.Add(config);
        enemiesConfig.Add(enemies);
        friendliesConfig.Add(friendlies);
        timerConfig.Add(timer);
    }
}
