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

    public LevelConfigs()
    {
        //Level1
        List<Source> ls1 = new List<Source>();
        TileType[,] level1 = new TileType[9, 6];
        level1[4, 5] = TileType.Door;
        CreateConfig(9, 6, level1, ls1);

        //Level2
        List<Source> ls2 = new List<Source>();
        TileType[,] level2 = new TileType[13, 8];
        level2[6, 7] = TileType.Door;
        for (int x = 3; x < 5; x++)
            for (int y = 0; y < 3; y++)
                level2[x, y] = TileType.Wall;
        CreateConfig(13, 8, level2, ls2);
    }

    public void CreateConfig(int sizeX, int sizeY, TileType[,] config, List<Source> lightSources)
    {
        sizesX.Add(sizeX);
        sizesY.Add(sizeY);
        configurations.Add(config);
        lightSourcesConfig.Add(lightSources);
    }
}
