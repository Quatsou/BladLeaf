using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ShadowMap : GameObject
{
    float[,] shadowMap;
    float lightRange = 250, innerRange = 200;
    const int lightTileSep = 16;
    int lightBlockSize = Tile.TILESIZE / lightTileSep;
    int sizeX, sizeY;
    TileType[,] levelLayout;
    List<LightSource> lightSources;
    Vector2 levelOffset;

    public ShadowMap(int sizeX, int sizeY, TileType[,] levelLayout, List<LightSource> lightSources, Vector2 levelOffset)
        : base(4, "shadow")
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.levelLayout = levelLayout;
        this.lightSources = lightSources;
        this.levelOffset = levelOffset;
        SetInitialSM();
    }

    public void SetInitialSM()
    {
        //Hier wordt een shadowmap aangemaakt voor het level (lightsources veranderen niet van positie)
        shadowMap = new float[sizeX * lightTileSep, sizeY * lightTileSep];
        FilterTiles();
    }

    public void FilterTiles()
    {
        //Voor optimalisatie worden eerst alle tiles gezet die sowieso helemaal belicht zijn of helemaal donker.
        for (int x = 0; x < sizeX; x++)
            for (int y = 0; y < sizeY; y++)
            {
                if (levelLayout[x, y] != TileType.Wall)
                {
                    List<float> dist = GetDistances((x + 0.5f) * Tile.TILESIZE, (y + 0.5f) * Tile.TILESIZE);
                    float minDistance = float.MaxValue;

                    //If there are lights
                    if (dist.Any())
                        minDistance = GetDistances((x + 0.5f) * Tile.TILESIZE, (y + 0.5f) * Tile.TILESIZE).Min();

                    float tileIns = (float)Math.Sqrt(Math.Pow(Tile.TILESIZE, 2) * 2) + 1;
                    if (minDistance + tileIns < innerRange)
                    {
                        Console.WriteLine("x: " + x + ", y: " + y);
                        SetTileSMTo(x, y, 1);
                    }
                    else if (minDistance - tileIns < lightRange)
                    {
                        for (int xi = 0; xi < lightTileSep; xi++)
                            for (int yi = 0; yi < lightTileSep; yi++)
                            {
                                List<float> distances = GetDistances(x * Tile.TILESIZE + (xi + 0.5f) * lightBlockSize, y * Tile.TILESIZE + (yi + 0.5f) * lightBlockSize);
                                float lightLevel = 0;
                                foreach (int d in distances)
                                {
                                    float temp = 1 - (d - innerRange) / (lightRange - innerRange);
                                    if (temp < 0)
                                        temp = 0;
                                    lightLevel += (float)Math.Pow(temp, 2);
                                }
                                if (lightLevel > 1)
                                    lightLevel = 1;
                                shadowMap[x * lightTileSep + xi, y * lightTileSep + yi] = (float)Math.Sqrt(lightLevel);
                            }
                    }
                }
            }
    }

    private List<float> GetDistances(float x, float y)
    {
        List<float> distances = new List<float>();
        foreach (LightSource ls in lightSources)
        {
            if (ls.On)
                distances.Add(DistanceTo((ls.Position.X + 0.5f) * Tile.TILESIZE, (ls.Position.Y + 0.5f) * Tile.TILESIZE, x, y));
        }
        return distances;
    }

    private float DistanceTo(float x1, float y1, float x2, float y2)
    {
        //Helper methode voor distance 2 punten.
        return (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }

    private void SetTileSMTo(int x, int y, float value)
    {
        for (int xi = 0; xi < lightTileSep; xi++)
            for (int yi = 0; yi < lightTileSep; yi++)
            {
                shadowMap[x * lightTileSep + xi, y * lightTileSep + yi] = value;
            }
    }

    public void ToggleLights()
    {
        foreach (LightSource ls in lightSources)
            ls.LightOff(0.3f, this);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.MouseLeftButtonPressed())
            ToggleLights();

        base.HandleInput(inputHelper);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int xi = 0; xi < sizeX * lightTileSep; xi++)
            for (int yi = 0; yi < sizeY * lightTileSep; yi++)
            {
                DrawingHelper.DrawFillRectangle(new Rectangle((int)levelOffset.X + xi * lightBlockSize, (int)levelOffset.Y + yi * lightBlockSize, lightBlockSize, lightBlockSize), spriteBatch, Color.Black * (1 - shadowMap[xi, yi]));
            }
    }
}

