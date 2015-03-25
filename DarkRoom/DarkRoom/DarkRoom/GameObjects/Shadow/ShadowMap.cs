using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ShadowMap : GameObject
{
    float[,] shadowMapInitial, shadowMap;
    float lightRange = 300, innerRange = 150;
    const int lightTileSep = 16;
    int lightBlockSize = Tile.TILESIZE / lightTileSep;
    float tileIns = (float)Math.Sqrt(Math.Pow(Tile.TILESIZE, 2) * 2) + 1;
    int sizeX, sizeY;
    TileType[,] levelLayout;
    List<LightSource> lightSources;
    Vector2 levelOffset;
    Player player;
    float lightLevel;

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
        shadowMap = new float[sizeX * lightTileSep, sizeY * lightTileSep];
        shadowMapInitial = new float[sizeX * lightTileSep, sizeY * lightTileSep];
        CalculateLightLevels();
    }

    public void CalculateLightLevels()
    {
        //Voor optimalisatie worden eerst alle tiles gezet die sowieso helemaal belicht zijn of helemaal donker.
        for (int x = 0; x < sizeX; x++)
            for (int y = 0; y < sizeY; y++)
            {
                //Muren zijn sowieso shaduw
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
                        SetTileSMTo(x, y, 1, shadowMapInitial);
                    }
                    //Gedeeltelijk in range van een lightsource
                    else if (minDistance - tileIns < lightRange)
                    {
                        for (int xi = 0; xi < lightTileSep; xi++)
                            for (int yi = 0; yi < lightTileSep; yi++)
                            {
                                List<float> distances = GetDistances(x * Tile.TILESIZE + (xi + 0.5f) * lightBlockSize, y * Tile.TILESIZE + (yi + 0.5f) * lightBlockSize);
                                lightLevel = 0;
                                foreach (int d in distances)
                                {
                                    float temp = 1 - (d - innerRange) / (lightRange - innerRange);
                                    if (temp < 0)
                                        temp = 0;
                                    lightLevel += (float)Math.Pow(temp, 2);
                                }
                                if (lightLevel > 1)
                                    lightLevel = 1;
                                shadowMapInitial[x * lightTileSep + xi, y * lightTileSep + yi] = (float)Math.Sqrt(lightLevel);
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

    private void SetTileSMTo(int x, int y, float value, float[,] map)
    {
        for (int xi = 0; xi < lightTileSep; xi++)
            for (int yi = 0; yi < lightTileSep; yi++)
            {
                map[x * lightTileSep + xi, y * lightTileSep + yi] = value;
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

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        for (int x = 0; x < sizeX * lightTileSep; x++)
            for (int y = 0; y < sizeY * lightTileSep; y++)
                shadowMap[x,y] = shadowMapInitial[x,y];

        ////Lightlevels voor zaklamp
        for (int x = 0; x < sizeX; x++)
            for (int y = 0; y < sizeY; y++)
            {
                //Muren zijn sowieso shaduw
                if (levelLayout[x, y] != TileType.Wall)
                {
                    //Check of geheel in range van zaklamp
                    float distanceFL = DistanceTo((x + 0.5f) * Tile.TILESIZE, (y + 0.5f) * Tile.TILESIZE, player.GlobalPosition.X - levelOffset.X, player.GlobalPosition.Y - levelOffset.Y);
                    if (distanceFL + tileIns < Player.flashLightInnerRange)
                    {
                        SetTileSMTo(x, y, 1, shadowMap);
                    }
                    //Check gedeeltelijk in range zaklamp
                    else if (distanceFL - tileIns < Player.flashLightRange)
                    {
                        for (int xi = 0; xi < lightTileSep; xi++)
                            for (int yi = 0; yi < lightTileSep; yi++)
                            {
                                float distanceFLBlock = DistanceTo(x * Tile.TILESIZE + (xi + 0.5f) * lightBlockSize, y * Tile.TILESIZE + (yi + 0.5f) * lightBlockSize, player.GlobalPosition.X - levelOffset.X, player.GlobalPosition.Y - levelOffset.Y);
                                float temp = 1 - (distanceFLBlock - Player.flashLightInnerRange) / (Player.flashLightRange - Player.flashLightInnerRange);
                                if (temp < 0)
                                    temp = 0;
                                lightLevel = shadowMapInitial[x * lightTileSep + xi, y * lightTileSep + yi];
                                lightLevel = (float)Math.Sqrt(Math.Pow(lightLevel, 2) + Math.Pow(temp, 2));
                                if (lightLevel > 1)
                                    lightLevel = 1;
                                shadowMap[x * lightTileSep + xi, y * lightTileSep + yi] = lightLevel;
                            }
                    }
                }
            }
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

