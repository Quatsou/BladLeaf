using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BlackScreen : SpriteGameObject
{
    public BlackScreen()
        : base("", 10)
    {

    }

    public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        if (ShadowMap.lightsoff)
            DrawingHelper.DrawFillRectangle(new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y), spriteBatch, Color.Black);
    }
}
