using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ShadowMap : GameObject
{
    float[,] shadowMap;
    int lightRange = 200;
    Level level;

    public ShadowMap(Level level)
        : base(4, "shadow")
    {
        this.level = level;
        SetInitialSM();
    }

    public void SetInitialSM()
    {
        //Hier wordt een shadowmap aangemaakt voor het level (lightsources veranderen niet van positie)
        shadowMap = new float[level.sizeX * Tile.TILESIZE / 8, level.sizeY * Tile.TILESIZE / 8];
        FilterTiles();
    }

    public void FilterTiles()
    {
        //Voor optimalisatie worden eerst alle tiles gezet die sowieso helemaal belicht zijn of helemaal donker.
        for (int x = 0; x < level.sizeX; x++)
            for (int y = 0; y < level.sizeX; y++)
            {
                if (level.levelLayout[x, y] != TileType.Wall)
                {
                    int minDistance = MinDistanceToLS(x, y);
                    int tileIns = (int)Math.Sqrt(Math.Pow(Tile.TILESIZE, 2) * 2) + 1;
                    if (minDistance + tileIns < lightRange)
                    {
                        SetTileSMTo(x, y, 1);
                    }
                    else if (minDistance - tileIns < lightRange)
                    {

                    }
                }
            }
    }

    private int MinDistanceToLS(int x, int y)
    {
        //Minimal distance to lightsource
        double minimal = double.MaxValue;

        foreach (LightSource ls in level.lightSources)
        {
            double distance = DistanceTo(new Point((int)ls.Position.X, (int)ls.Position.Y), new Point(x, y));
            if (distance < minimal)
            {
                minimal = distance;
            }
        }
        return (int)minimal;
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
        for (int xi = 0; xi < level.sizeX * 8; xi++)
            for (int yi = 0; yi < level.sizeX * 8; yi++)
            {
                DrawingHelper.DrawRectangle(new Rectangle(xi, yi, 8, 8), spriteBatch, Color.Black * (1 - shadowMap[xi, yi]));
            }
    }

    
}

