using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ShadowMap : GameObject
{
    float[,] shadowMap;
    int lightRange = 250, innerRange = 200;
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
        shadowMap = new float[sizeX * Tile.TILESIZE / 8, sizeY * Tile.TILESIZE / 8];
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
                    int minDistance = MinDistanceToLS(x, y);
                    int tileIns = (int)Math.Sqrt(Math.Pow(Tile.TILESIZE, 2) * 2) + 1;
                    if (minDistance + tileIns < innerRange)
                    {
                        Console.WriteLine("x: " + x + ", y: " + y);
                        SetTileSMTo(x, y, 1);
                    }
                    else if (minDistance - tileIns < lightRange)
                    {
                        //List<int> distances = GetDistances(x, y);
                        //int lightLevel = 0;
                        //foreach (int d in distances)
                        //{
                        //    lightLevel += (d - innerRange) / (lightRange - innerRange);
                        //}
                    }
                }
            }
    }

    private int MinDistanceToLS(int x, int y)
    {
        //Minimal distance to lightsource
        double minimal = double.MaxValue;

        foreach (LightSource ls in lightSources)
        {
            double distance = DistanceTo(new Point((int)ls.Position.X, (int)ls.Position.Y), new Point(x, y));
            if (distance < minimal)
            {
                minimal = distance;
            }
        }
        return (int)minimal * Tile.TILESIZE;
    }

    private double DistanceTo(Point p1, Point p2)
    {
        //Helper methode voor distance 2 punten.
        return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
    }

    private void SetTileSMTo(int x, int y, float value)
    {
        for (int xi = 0; xi < 8; xi++)
            for (int yi = 0; yi < 8; yi++)
            {
                shadowMap[x * 8 + xi, y * 8 + yi] = value;
            }
    }

    public void ToggleLights()
    {

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int xi = 0; xi < sizeX * 8; xi++)
            for (int yi = 0; yi < sizeY * 8; yi++)
            {
                DrawingHelper.DrawFillRectangle(new Rectangle((int)levelOffset.X + xi * 8, (int)levelOffset.Y + yi * 8, 8, 8), spriteBatch, Color.Red * (1 - shadowMap[xi, yi]) * 0.5f);
            }
    }
}

