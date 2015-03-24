using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ShadowMap : GameObject
{
    float[,] shadowMap;

    public ShadowMap(Level level)
        : base(4, "shadow")
    {
        SetInitialSM(level);
    }

    public void SetInitialSM(Level level)
    {
        shadowMap = new float[level.sizeX * Tile.TILESIZE, level.sizeY * Tile.TILESIZE];

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

    }

    
}

