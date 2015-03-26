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
        Vector2[] lvl1Coordse = new Vector2[3]; float[] lvl1rotationse = new float[3];
        lvl1Coordse[0] = new Vector2(1, 1); lvl1rotationse[0] = 3.14f;
        lvl1Coordse[1] = new Vector2(2, 2); lvl1rotationse[1] = 4.20f;
        lvl1Coordse[2] = new Vector2(3, 3); lvl1rotationse[2] = 1.11f;
        enemies1.Add(new Enemy(lvl1Coordse, lvl1rotationse));

        List<Friendly> friendlies1 = new List<Friendly>();
        Vector2[] lvl1Coordsf = new Vector2[3]; float[] lvl1rotationsf = new float[3];
        lvl1Coordsf[0] = new Vector2(1, 0); lvl1rotationsf[0] = 3.14f;
        lvl1Coordsf[1] = new Vector2(2, 1); lvl1rotationsf[1] = 4.20f;
        lvl1Coordsf[2] = new Vector2(3, 2); lvl1rotationsf[2] = 1.11f;
        friendlies1.Add(new Friendly(lvl1Coordsf, lvl1rotationsf));

        TileType[,] level1 = new TileType[9, 6];
        level1[2, 5] = TileType.Door;
        CreateConfig(9, 6, level1, enemies1, friendlies1, 5);

        //Level2
        List<Enemy> enemies2 = new List<Enemy>();
        Vector2[] lvl2Coordse = new Vector2[3]; float[] lvl2rotationse = new float[3];
        lvl2Coordse[0] = new Vector2(0, 1); lvl2rotationse[0] = 3.14f;
        lvl2Coordse[1] = new Vector2(1, 2); lvl2rotationse[1] = 4.20f;
        lvl2Coordse[2] = new Vector2(2, 3); lvl2rotationse[2] = 1.11f;
        enemies2.Add(new Enemy(lvl2Coordse, lvl2rotationse));

        List<Friendly> friendlies2 = new List<Friendly>();
        Vector2[] lvl2Coordsf = new Vector2[3]; float[] lvl2rotationsf = new float[3];
        lvl2Coordsf[0] = new Vector2(0, 0); lvl2rotationsf[0] = 3.14f;
        lvl2Coordsf[1] = new Vector2(1, 1); lvl2rotationsf[1] = 4.20f;
        lvl2Coordsf[2] = new Vector2(2, 2); lvl2rotationsf[2] = 1.11f;
        friendlies2.Add(new Friendly(lvl2Coordsf, lvl2rotationsf));

        TileType[,] level2 = new TileType[13, 8];
        level2[6, 7] = TileType.Door;
        for (int x = 3; x < 5; x++)
            for (int y = 0; y < 3; y++)
                level2[x, y] = TileType.Wall;
        for (int x = 8; x < 10; x++)
            for (int y = 0; y < 3; y++)
                level2[x, y] = TileType.Wall;

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
