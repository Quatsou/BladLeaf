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
        Vector2[] lvl3Coordse1 = new Vector2[3]; float[] lvl3rotationse1 = new float[3];
        lvl3Coordse1[0] = new Vector2(0, 2); lvl3rotationse1[0] = 1.570f;
        lvl3Coordse1[1] = new Vector2(3, 0); lvl3rotationse1[1] = 3.141f;
        lvl3Coordse1[2] = new Vector2(0, 2); lvl3rotationse1[2] = 3.141f;
        enemies3.Add(new Enemy(lvl3Coordse1, lvl3rotationse1));

        Vector2[] lvl3Coordse2 = new Vector2[3]; float[] lvl3rotationse2 = new float[3];
        lvl3Coordse2[0] = new Vector2(3, 0); lvl3rotationse2[0] = 3.141f;
        lvl3Coordse2[1] = new Vector2(4, 1); lvl3rotationse2[1] = 1.570f;
        lvl3Coordse2[2] = new Vector2(2, 1); lvl3rotationse2[2] = 1.570f;
        enemies3.Add(new Enemy(lvl3Coordse2, lvl3rotationse2));

        Vector2[] lvl3Coordse3 = new Vector2[3]; float[] lvl3rotationse3 = new float[3];
        lvl3Coordse3[0] = new Vector2(3, 5); lvl3rotationse3[0] = 4.712f;
        lvl3Coordse3[1] = new Vector2(6, 4); lvl3rotationse3[1] = 3.927f;
        lvl3Coordse3[2] = new Vector2(6, 1); lvl3rotationse3[2] = 3.141f;
        enemies3.Add(new Enemy(lvl3Coordse3, lvl3rotationse3));

        List<Friendly> friendlies3 = new List<Friendly>();
        Vector2[] lvl3Coordsf1 = new Vector2[3]; float[] lvl3rotationsf1 = new float[3];
        lvl3Coordsf1[0] = new Vector2(0, 0); lvl3rotationsf1[0] = 3.141f;
        lvl3Coordsf1[1] = new Vector2(4, 0); lvl3rotationsf1[1] = 2.356f;
        lvl3Coordsf1[2] = new Vector2(3, 2); lvl3rotationsf1[2] = 4.712f;
        friendlies3.Add(new Friendly(lvl3Coordsf1, lvl3rotationsf1));

        Vector2[] lvl3Coordsf2 = new Vector2[3]; float[] lvl3rotationsf2 = new float[3];
        lvl3Coordsf2[0] = new Vector2(4, 5); lvl3rotationsf2[0] = 0f;
        lvl3Coordsf2[1] = new Vector2(6, 5); lvl3rotationsf2[1] = 0.785f;
        lvl3Coordsf2[2] = new Vector2(5, 1); lvl3rotationsf2[2] = 3.141f;
        friendlies3.Add(new Friendly(lvl3Coordsf2, lvl3rotationsf2));

        TileType[,] level3 = new TileType[10, 6];
        level3[0, 5] = TileType.Door;
        level3[4, 2] = TileType.Wall; level3[4, 3] = TileType.Wall;
        level3[5, 2] = TileType.Wall; level3[5, 3] = TileType.Wall;
        CreateConfig(10, 6, level3, enemies3, friendlies3, 60);
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
