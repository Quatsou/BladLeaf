using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Source : GameObject
{
    private GraphicsDevice graphicsDevice;

    public RenderTarget2D RenderTarget { get; private set; }
    public Vector2 LightPosition { get; set; }
    public Vector2 LightAreaSize { get; set; }
    RenderTarget2D screenShadows;

    public Source(GraphicsDevice graphicsDevice, ShadowmapSize size)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

    }
}

