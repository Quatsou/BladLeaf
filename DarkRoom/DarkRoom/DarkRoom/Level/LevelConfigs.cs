using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class LevelConfigs
{
    public static int levelsCompleted = 3;
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
        Vector2[] lvl1Coordse = new Vector2[1]; float[] lvl1rotationse = new float[1];
        lvl1Coordse[0] = new Vector2(5, 2); lvl1rotationse[0] = 0f;
        enemies1.Add(new Enemy(lvl1Coordse, lvl1rotationse));

        List<Friendly> friendlies1 = new List<Friendly>();

        TileType[,] level1 = new TileType[11, 6];
        level1[5, 5] = TileType.Door;
        CreateConfig(11, 6, level1, enemies1, friendlies1, 10);

        //Level2
        List<Enemy> enemies2 = new List<Enemy>();

        List<Friendly> friendlies2 = new List<Friendly>();
        Vector2[] lvl2Coordsf = new Vector2[1]; float[] lvl2rotationsf = new float[1];
        lvl2Coordsf[0] = new Vector2(5, 2); lvl2rotationsf[0] = 3.141f;
        friendlies2.Add(new Friendly(lvl2Coordsf, lvl2rotationsf));

        TileType[,] level2 = new TileType[11, 6];
        level2[5, 5] = TileType.Door;
        CreateConfig(11, 6, level2, enemies2, friendlies2, 10);

        //Level3
        List<Enemy> enemies3 = new List<Enemy>();

        enemies3.Add(new Enemy(
            CreateCoords(0, 2, 3, 0, 0, 2), 
            CreateRotations("right", "down", "down")));

        enemies3.Add(new Enemy(
            CreateCoords(3, 0, 4, 1, 2, 1),
            CreateRotations("down", "right", "right")));

        enemies3.Add(new Enemy(
            CreateCoords(3, 5, 6, 4, 6, 1),
            CreateRotations("left", "down_left", "down")));

        List<Friendly> friendlies3 = new List<Friendly>();

        friendlies3.Add(new Friendly(
            CreateCoords(0, 0, 4, 0, 3, 2),
            CreateRotations("down", "down_right", "left")));

        friendlies3.Add(new Friendly(
            CreateCoords(4, 5, 6, 5, 5, 1),
            CreateRotations("up", "up_right", "down")));

        int[,] level3 = new int[10, 6]
           {{0,0,0,0,0,2},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,1,1,0,0},
            {0,0,1,1,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0}};
        CreateConfig(10, 6, ToTileType(level3, 10, 6), enemies3, friendlies3, 60);

        //Level4
        List<Enemy> enemies4 = new List<Enemy>();

        enemies4.Add(new Enemy(
            CreateCoords(0, 7, 0, 1, 1, 6),
            CreateRotations("down_right", "down", "down")));

        enemies4.Add(new Enemy(
            CreateCoords(4, 3, 1, 0, 2, 6),
            CreateRotations("down", "down", "right")));

        enemies4.Add(new Enemy(
            CreateCoords(6, 0, 4, 3, 4, 2),
            CreateRotations("down", "right", "down")));

        enemies4.Add(new Enemy(
            CreateCoords(8, 3, 6, 5, 6, 5),
            CreateRotations("down", "down", "up")));

        enemies4.Add(new Enemy(
            CreateCoords(11, 3, 8, 3, 6, 8),
            CreateRotations("down", "left", "down")));

        enemies4.Add(new Enemy(
            CreateCoords(12, 5, 11, 0, 8, 2),
            CreateRotations("left", "down", "down")));

        List<Friendly> friendlies4 = new List<Friendly>();

        friendlies4.Add(new Friendly(
            CreateCoords(0, 8, 0, 0, 0, 8),
            CreateRotations("up", "down_right", "up")));

        friendlies4.Add(new Friendly(
            CreateCoords(4, 0, 6, 0, 2, 5),
            CreateRotations("down", "down", "up")));

        friendlies4.Add(new Friendly(
            CreateCoords(8, 0, 6, 4, 5, 3),
            CreateRotations("down", "down", "left")));

        friendlies4.Add(new Friendly(
            CreateCoords(11, 0, 12, 0, 7, 3),
            CreateRotations("down", "down_left", "right")));

        int[,] level4 = new int[13, 12]
           {{0,0,0,0,0,0,0,0,0,1,1,1},
            {0,0,0,0,0,0,0,0,0,1,1,1},
            {0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,1,1,1,0,0,0,1,1,1},
            {0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,0,1,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,2},
            {1,1,1,0,1,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,1,1,1,0,0,0,1,1,1},
            {0,0,0,0,0,0,0,0,0,1,1,1},
            {0,0,0,0,0,0,0,0,0,1,1,1},
            {0,0,0,0,0,0,0,0,0,1,1,1}};
        CreateConfig(13, 12, ToTileType(level4, 13, 12), enemies4, friendlies4, 60);

    }

    private Vector2[] CreateCoords(int x1, int y1, int x2, int y2, int x3, int y3)
    {
        Vector2[] result = new Vector2[3];
        result[0] = new Vector2(x1, y1);
        result[1] = new Vector2(x2, y2);
        result[2] = new Vector2(x3, y3);
        return result;
    }

    private float[] CreateRotations(string dir1, string dir2, string dir3)
    {
        float[] result = new float[3];
        result[0] = StringToRotation(dir1);
        result[1] = StringToRotation(dir2);
        result[2] = StringToRotation(dir3);
        return result;
    }

    private float StringToRotation(string s)
    {
        switch (s)
        {
            case "up": return 0f;
            case "up_right": return 0.785f;
            case "right": return 1.570f;
            case "down_right": return 2.356f;
            case "down": return 3.141f;
            case "down_left": return 3.927f;
            case "left": return 4.712f;
            case "up_left": return 5.498f;
        }
        return 0f;
    }

    private TileType[,] ToTileType(int[,] array, int x, int y)
    {
        TileType[,] result = new TileType[x, y];
        for (int xi = 0; xi < x; xi++)
            for (int yi = 0; yi < y; yi++)
            {
                result[xi, yi] = (TileType)array[xi, yi];
            }
        return result;
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
