using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class LevelConfigs
{
    public List<TileType[,]> configurations = new List<TileType[,]>();
    public List<int> sizesX = new List<int>();
    public List<int> sizesY = new List<int>();

    public LevelConfigs()
    {
        //Level1
        TileType[,] level1 = new TileType[9, 6];
        level1[4, 5] = TileType.Door;
        CreateConfig(9, 6, level1);

        //Level2
        TileType[,] level2 = new TileType[13, 8];
        level2[6, 7] = TileType.Door;
        for (int x = 3; x < 5; x++)
            for (int y = 0; y < 3; y++)
                level2[x, y] = TileType.Wall;

        CreateConfig(13, 8, level2);
    }

    public void CreateConfig(int sizeX, int sizeY, TileType[,] config)
    {
        sizesX.Add(sizeX);
        sizesY.Add(sizeY);
        configurations.Add(config);
    }
}
