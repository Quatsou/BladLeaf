using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class LevelConfigs
{
    public List<TileType[,]> configurations = new List<TileType[,]>();
    public List<int> sizesX = new List<int>();
    public List<int> sizesY = new List<int>();
    public List<List<Source>> lightSourcesConfig = new List<List<Source>>();
    public List<List<Enemy>> enemiesConfig = new List<List<Enemy>>();
    public List<List<Friendly>> friendliesConfig = new List<List<Friendly>>();
    public List<float> timerConfig = new List<float>();

    public LevelConfigs()
    {
        //Level1
        List<Source> ls1 = new List<Source>();

        List<Enemy> enemies1 = new List<Enemy>();
        enemies1.Add(new Enemy(new Vector2(4, 2), 0f));

        List<Friendly> friendlies1 = new List<Friendly>();
        friendlies1.Add(new Friendly(new Vector2(4, 0), 3.14f));

        TileType[,] level1 = new TileType[9, 6];
        level1[4, 5] = TileType.Door;
        CreateConfig(9, 6, level1, ls1, enemies1, friendlies1, 5);

        //Level2
        List<Source> ls2 = new List<Source>();

        List<Enemy> enemies2 = new List<Enemy>();
        enemies2.Add(new Enemy(new Vector2(6, 2), 0f));

        List<Friendly> friendlies2 = new List<Friendly>();
        friendlies2.Add(new Friendly(new Vector2(6, 0), 3.14f));

        TileType[,] level2 = new TileType[13, 8];
        level2[6, 7] = TileType.Door;
        for (int x = 3; x < 5; x++)
            for (int y = 0; y < 3; y++)
                level2[x, y] = TileType.Wall;
        for (int x = 8; x < 10; x++)
            for (int y = 0; y < 3; y++)
                level2[x, y] = TileType.Wall;
        CreateConfig(13, 8, level2, ls2, enemies2, friendlies2, 60);
    }

    public void CreateConfig(int sizeX, int sizeY, TileType[,] config, List<Source> lightSources, List<Enemy> enemies, List<Friendly> friendlies, float timer)
    {
        sizesX.Add(sizeX);
        sizesY.Add(sizeY);
        configurations.Add(config);
        lightSourcesConfig.Add(lightSources);
        enemiesConfig.Add(enemies);
        friendliesConfig.Add(friendlies);
        timerConfig.Add(timer);
    }
}
