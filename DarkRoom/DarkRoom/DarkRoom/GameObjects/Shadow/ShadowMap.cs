﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ShadowMap : GameObject
{
    double[,] shadowMapInitial, shadowMap;
    double lightRange = 150, innerRange = 100;
    const int lightTileSep = 8;
    int lightBlockSize = Tile.TILESIZE / lightTileSep;
    double tileIns = (double)Math.Sqrt(Math.Pow(Tile.TILESIZE, 2) * 2) + 1;
    int sizeX, sizeY;
    TileType[,] levelLayout;
    List<LightSource> lightSources;
    Vector2 levelOffset;
    Player player;
    double lightLevel;
    public static bool flashLightMode = false, lightsoff = false;

    public ShadowMap(int sizeX, int sizeY, TileType[,] levelLayout, List<LightSource> lightSources, Vector2 levelOffset, Player player)
        : base(4, "shadow")
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.levelLayout = levelLayout;
        this.lightSources = lightSources;
        this.levelOffset = levelOffset;
        this.player = player;
        SetInitialSM();
    }

    public void SetInitialSM()
    {
        //Hier wordt een shadowmap aangemaakt voor het level (lightsources veranderen niet van positie)
        shadowMap = new double[sizeX * lightTileSep, sizeY * lightTileSep];
        shadowMapInitial = new double[sizeX * lightTileSep, sizeY * lightTileSep];
        if (lightSources.Count > 0)
            CalculateLLLS();
        CalculateLLFL();
    }

    public void CalculateLLLS()
    {
        //Voor optimalisatie worden eerst alle tiles gezet die sowieso helemaal belicht zijn of helemaal donker.
        for (int x = 0; x < sizeX; x++)
            for (int y = 0; y < sizeY; y++)
            {
                //Muren zijn sowieso shaduw
                if (levelLayout[x, y] != TileType.Wall)
                {

                    List<double> dist = GetDistances((x + 0.5) * Tile.TILESIZE, (y + 0.5) * Tile.TILESIZE);
                    double minDistance = double.MaxValue;

                    //If there are lights
                    if (dist.Any())
                        minDistance = GetDistances((x + 0.5) * Tile.TILESIZE, (y + 0.5) * Tile.TILESIZE).Min();

                    double tileIns = Math.Sqrt(Math.Pow(Tile.TILESIZE, 2) * 2) + 1;
                    if (minDistance + tileIns < innerRange)
                    {
                        SetTileSMTo(x, y, 1, shadowMapInitial);
                    }
                    //Gedeeltelijk in range van een lightsource
                    else if (minDistance - tileIns < lightRange)
                    {
                        for (int xi = 0; xi < lightTileSep; xi++)
                            for (int yi = 0; yi < lightTileSep; yi++)
                            {
                                List<double> distances = GetDistances(x * Tile.TILESIZE + (xi + 0.5) * lightBlockSize, y * Tile.TILESIZE + (yi + 0.5) * lightBlockSize);
                                lightLevel = 0;
                                foreach (int d in distances)
                                {
                                    double temp = 1 - (d - innerRange) / (lightRange - innerRange);
                                    if (temp < 0)
                                        temp = 0;
                                    lightLevel += Math.Pow(temp, 2);
                                }
                                if (lightLevel > 1)
                                    lightLevel = 1;
                                shadowMapInitial[x * lightTileSep + xi, y * lightTileSep + yi] = Math.Sqrt(lightLevel);
                            }
                    }
                    
                }
            }
    }

    private List<double> GetDistances(double x, double y)
    {
        List<double> distances = new List<double>();
        foreach (LightSource ls in lightSources)
        {
            if (ls.On)
                distances.Add(DistanceTo((ls.Position.X + 0.5) * Tile.TILESIZE, (ls.Position.Y + 0.5) * Tile.TILESIZE, x, y));
        }
        return distances;
    }

    private double DistanceTo(double x1, double y1, double x2, double y2)
    {
        //Helper methode voor distance 2 punten.
        return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }

    private void SetTileSMTo(int x, int y, double value, double[,] map)
    {
        for (int xi = 0; xi < lightTileSep; xi++)
            for (int yi = 0; yi < lightTileSep; yi++)
            {
                map[x * lightTileSep + xi, y * lightTileSep + yi] = value;
            }
    }

    private void CalculateLLFL()
    {
        ////Lightlevels voor zaklamp
        for (int x = 0; x < sizeX; x++)
            for (int y = 0; y < sizeY; y++)
            {
                //Muren zijn sowieso shaduw
                if (levelLayout[x, y] != TileType.Wall)
                {
                    //Check of geheel in range van zaklamp
                    double distanceFL = DistanceTo((x + 0.5) * Tile.TILESIZE, (y + 0.5) * Tile.TILESIZE, player.GlobalPosition.X - levelOffset.X, player.GlobalPosition.Y - levelOffset.Y);
                    double angle = TileInRangeFL((x + 0.5) * Tile.TILESIZE, (y + 0.5) * Tile.TILESIZE, distanceFL);
                    if (distanceFL + tileIns < Player.flashLightInnerRange && distanceFL > Tile.TILESIZE && (angle < Player.flashLightFOV / 2 - 45 || angle > 360 - (Player.flashLightFOV / 2 - 45)))
                    {
                        SetTileSMTo(x, y, 1, shadowMap);
                    }
                    //Check gedeeltelijk in range zaklamp
                    else if (distanceFL < Tile.TILESIZE || (distanceFL - tileIns < Player.flashLightRange && (angle < Player.flashLightFOV / 2 + 45 || angle > 360 - (Player.flashLightFOV / 2 + 45))))
                    {
                        for (int xi = 0; xi < lightTileSep; xi++)
                            for (int yi = 0; yi < lightTileSep; yi++)
                            {
                                double distanceFLBlock = DistanceTo(x * Tile.TILESIZE + (xi + 0.5) * lightBlockSize, y * Tile.TILESIZE + (yi + 0.5) * lightBlockSize, player.GlobalPosition.X - levelOffset.X, player.GlobalPosition.Y - levelOffset.Y);
                                double angleSep = TileInRangeFL(x * Tile.TILESIZE + (xi + 0.5) * lightBlockSize, y * Tile.TILESIZE + (yi + 0.5) * lightBlockSize, distanceFLBlock);
                                double temp = 0;
                                if ((angleSep < Player.flashLightFOV / 2 - 15 || angleSep > 360 - (Player.flashLightFOV / 2 - 15)))
                                {
                                    temp = 1 - (distanceFLBlock - Player.flashLightInnerRange) / (Player.flashLightRange - Player.flashLightInnerRange);
                                }
                                else if (angleSep < Player.flashLightFOV / 2 + 15)
                                {
                                    temp = 1 - Math.Max((angleSep - Player.flashLightFOV / 2) / 30, (distanceFLBlock - Player.flashLightInnerRange) / (Player.flashLightRange - Player.flashLightInnerRange));
                                }
                                else if(angleSep > 360 - (Player.flashLightFOV / 2 + 15))
                                {
                                    temp = 1 - Math.Max((360 - (angleSep - Player.flashLightFOV / 2)) / 30, (distanceFLBlock - Player.flashLightInnerRange) / (Player.flashLightRange - Player.flashLightInnerRange));
                                }
                                if (temp < 0)
                                    temp = 0;
                                lightLevel = shadowMapInitial[x * lightTileSep + xi, y * lightTileSep + yi];
                                lightLevel = Math.Sqrt(Math.Pow(lightLevel, 2) + Math.Pow(temp, 2));
                                if (lightLevel > 1)
                                    lightLevel = 1;
                                shadowMap[x * lightTileSep + xi, y * lightTileSep + yi] = lightLevel;
                                
                            }
                    }
                }
            }
    }

    private double TileInRangeFL(double x, double y, double p13)
    {
        double p12 = 10; //DistanceTo(player.GlobalPosition.X - levelOffset.X, player.GlobalPosition.Y - levelOffset.Y,
           // player.GlobalPosition.X - levelOffset.X + player.direction.X * 10, player.GlobalPosition.Y - levelOffset.Y + player.direction.Y * 10);
        double p23 = DistanceTo(player.GlobalPosition.X - levelOffset.X + player.direction.X * 10, player.GlobalPosition.Y - levelOffset.Y + player.direction.Y * 10, x, y);

        //Console.WriteLine(p23);

        return CalculateAngle(p12, p13, p23);
    }
    private double CalculateAngle(double p12, double p13, double p23)
    {
        double result = Math.Acos((p12 * p12 + p13 * p13 - p23 * p23) / (2 * p12 * p13)) / (2 * Math.PI) * 360;
        return result;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        for (int x = 0; x < sizeX * lightTileSep; x++)
            for (int y = 0; y < sizeY * lightTileSep; y++)
                shadowMap[x,y] = shadowMapInitial[x,y];

        CalculateLLFL();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (lightsoff)
        {
            return;
        }
        if (flashLightMode)
        {
            for (int xi = 0; xi < sizeX * lightTileSep; xi++)
                for (int yi = 0; yi < sizeY * lightTileSep; yi++)
                {
                    DrawingHelper.DrawFillRectangle(new Rectangle((int)levelOffset.X + xi * lightBlockSize, (int)levelOffset.Y + yi * lightBlockSize, lightBlockSize, lightBlockSize), spriteBatch, Color.Black * (float)(1 - shadowMap[xi, yi]));
                }
        }
    }
}

