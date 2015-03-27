using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class LevelConfigs
{
    public static int levelsCompleted = 10;
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
        enemies1.Add(new Enemy(
            CreateCoords(5, 0, 5, 0, 5, 0),
            CreateRotations("down", "down", "down")));

        List<Friendly> friendlies1 = new List<Friendly>();

        TileType[,] level1 = new TileType[11, 6];
        level1[5, 5] = TileType.Door;
        level1[0, 3] = TileType.Wall; level1[1, 3] = TileType.Wall;
        level1[4, 0] = TileType.Wall; level1[4, 1] = TileType.Wall;
        CreateConfig(11, 6, level1, enemies1, friendlies1, 10);

        //Level2
        List<Enemy> enemies2 = new List<Enemy>();
        enemies2.Add(new Enemy(
            CreateCoords(10, 5, 10, 5, 10, 5),
            CreateRotations("left", "left", "left")));

        List<Friendly> friendlies2 = new List<Friendly>();

        friendlies2.Add(new Friendly(
            CreateCoords(9, 0, 9, 0, 9, 0),
            CreateRotations("down", "down_right", "left")));

        friendlies2.Add(new Friendly(
            CreateCoords(1, 1, 1, 1, 1, 1),
            CreateRotations("down", "down", "down")));

        int[,] level2 = new int[11, 6]
               {{0,0,0,0,0,0},
                {0,0,1,0,1,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,0,0,0,0,0},
                {0,1,0,0,3,2},
                {0,0,0,0,0,1},
                {0,0,0,0,0,0},
                {0,0,0,0,1,0},
                {0,1,0,0,1,0},
                {0,0,0,1,3,0}};
        CreateConfig(11, 6, ToTileType(level2, 11, 6), enemies2, friendlies2, 15);

        //Level3
        List<Enemy> enemies3 = new List<Enemy>();
        enemies3.Add(new Enemy(
            CreateCoords(5, 5, 5, 5, 5, 5),
            CreateRotations("down", "down", "down")));

        enemies3.Add(new Enemy(
            CreateCoords(5, 2, 5, 2, 5, 2),
            CreateRotations("left", "left", "left")));

        List<Friendly> friendlies3 = new List<Friendly>();

        friendlies3.Add(new Friendly(
            CreateCoords(6, 7, 6, 7, 6, 7),
            CreateRotations("down", "down_right", "left")));

        int[,] level3 = new int[8, 8]
          {{0,0,0,0,0,0,0,0},
           {0,0,0,0,1,1,1,0},
           {0,1,0,1,1,0,0,2},
           {0,1,0,0,0,0,1,0},
           {0,1,0,1,1,1,1,0},
           {0,1,0,0,0,0,0,0},
           {0,1,0,1,1,1,1,0},
           {0,0,0,0,0,0,0,0}};
        CreateConfig(8, 8, ToTileType(level3, 8, 8), enemies3, friendlies3, 20);

        //Level4
        List<Enemy> enemies4 = new List<Enemy>();

        enemies4.Add(new Enemy(
            CreateCoords(0, 2, 3, 0, 0, 2), 
            CreateRotations("right", "down", "down")));

        enemies4.Add(new Enemy(
            CreateCoords(3, 0, 4, 1, 2, 1),
            CreateRotations("down", "right", "right")));

        enemies4.Add(new Enemy(
            CreateCoords(3, 5, 6, 4, 6, 1),
            CreateRotations("left", "down_left", "down")));

        List<Friendly> friendlies4 = new List<Friendly>();

        friendlies4.Add(new Friendly(
            CreateCoords(0, 0, 4, 0, 3, 2),
            CreateRotations("down", "down_right", "left")));

        friendlies4.Add(new Friendly(
            CreateCoords(4, 5, 6, 5, 5, 1),
            CreateRotations("up", "up_right", "down")));

        int[,] level4 = new int[10, 6]
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
        CreateConfig(10, 6, ToTileType(level4, 10, 6), enemies4, friendlies4, 20);

        //Level4
        List<Enemy> enemies5 = new List<Enemy>();

        enemies5.Add(new Enemy(
            CreateCoords(0, 7, 0, 1, 1, 6),
            CreateRotations("down_right", "down", "down")));

        enemies5.Add(new Enemy(
            CreateCoords(4, 3, 1, 0, 2, 6),
            CreateRotations("down", "down", "right")));

        enemies5.Add(new Enemy(
            CreateCoords(6, 0, 4, 3, 4, 2),
            CreateRotations("down", "right", "down")));

        enemies5.Add(new Enemy(
            CreateCoords(8, 3, 6, 5, 6, 5),
            CreateRotations("down", "down", "up")));

        enemies5.Add(new Enemy(
            CreateCoords(11, 3, 8, 3, 6, 8),
            CreateRotations("down", "left", "down")));

        enemies5.Add(new Enemy(
            CreateCoords(12, 5, 11, 0, 8, 2),
            CreateRotations("left", "down", "down")));

        List<Friendly> friendlies5 = new List<Friendly>();

        friendlies5.Add(new Friendly(
            CreateCoords(0, 8, 0, 0, 0, 8),
            CreateRotations("up", "down_right", "up")));

        friendlies5.Add(new Friendly(
            CreateCoords(4, 0, 6, 0, 2, 5),
            CreateRotations("down", "down", "up")));

        friendlies5.Add(new Friendly(
            CreateCoords(8, 0, 6, 4, 5, 3),
            CreateRotations("down", "down", "left")));

        friendlies5.Add(new Friendly(
            CreateCoords(11, 0, 12, 0, 7, 3),
            CreateRotations("down", "down_left", "right")));

        int[,] level5 = new int[13, 12]
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
        CreateConfig(13, 12, ToTileType(level5, 13, 12), enemies5, friendlies5, 30);

        //Level 5
        List<Enemy> enemies5 = new List<Enemy>();

        enemies6.Add(new Enemy(
            CreateCoords(0, 2, 2, 0, 2, 0),
            CreateRotations("up_right", "down_left", "down")));

        enemies6.Add(new Enemy(
            CreateCoords(3, 0, 1, 2, 0, 6),
            CreateRotations("down_left", "up_right", "up_right")));

        enemies6.Add(new Enemy(
            CreateCoords(0, 7, 2, 3, 0, 9),
            CreateRotations("down", "up_right", "right")));

        enemies6.Add(new Enemy(
            CreateCoords(8, 7, 6, 7, 8, 7),
            CreateRotations("right", "right", "up_right")));

        enemies6.Add(new Enemy(
            CreateCoords(10, 5, 9, 5, 10, 5),
            CreateRotations("left", "down", "down")));

        enemies6.Add(new Enemy(
            CreateCoords(14, 2, 12, 7, 13, 3),
            CreateRotations("up", "left", "down")));

        enemies6.Add(new Enemy(
            CreateCoords(16, 1, 9, 8, 14, 0),
            CreateRotations("down_right", "up", "down")));

        enemies6.Add(new Enemy(
            CreateCoords(18, 9, 17, 5, 18, 4),
            CreateRotations("up", "down", "down")));

        List<Friendly> friendlies6 = new List<Friendly>();

        friendlies6.Add(new Friendly(
            CreateCoords(0, 0, 0, 0, 0, 1),
            CreateRotations("down_right", "down_right", "down")));

        friendlies6.Add(new Friendly(
            CreateCoords(0, 9, 4, 2, 1, 6),
            CreateRotations("up", "down_left", "up")));

        friendlies6.Add(new Friendly(
            CreateCoords(9, 6, 9, 6, 2, 9),
            CreateRotations("down", "up", "left")));

        friendlies6.Add(new Friendly(
            CreateCoords(14, 0, 17, 7, 9, 6),
            CreateRotations("down", "up", "down")));

        friendlies6.Add(new Friendly(
            CreateCoords(18, 0, 18, 0, 13, 2),
            CreateRotations("down_left", "down", "down_right")));

        int[,]level6 = new int[19, 10]
        {{0,0,0,1,0,0,0,0,0,0,},
         {0,0,0,1,0,0,0,0,1,0,},
         {0,0,0,0,0,0,1,0,1,0,},
         {0,0,0,0,0,0,1,0,1,0,},
         {1,1,0,0,0,0,1,0,1,0,},
         {1,1,1,1,0,0,1,0,1,0,},
         {1,1,1,1,1,0,1,0,1,0,},
         {1,1,1,1,1,0,1,0,1,0,},
         {1,1,1,1,1,0,0,0,1,0,},
         {1,1,1,1,1,0,0,0,0,2,},
         {1,1,1,1,1,0,0,0,1,0,},
         {1,1,1,1,1,0,1,0,1,0,},
         {1,1,1,1,0,0,1,0,1,0,},
         {1,1,0,0,0,0,1,0,1,0,},
         {0,0,0,0,0,0,1,0,1,0,},
         {0,1,1,1,0,0,1,0,1,0,},
         {0,0,1,1,0,0,1,0,1,0,},
         {0,0,0,1,0,0,0,0,1,0,},
         {0,0,0,0,0,0,0,0,0,0,}};
        CreateConfig(19, 10, ToTileType(level6, 19, 10), enemies6, friendlies6, 45);
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
