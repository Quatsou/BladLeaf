using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class DoorArrow : SpriteGameObject
{
    float center;
    float offset = 0;
    double sinValue = 0;
    
    public DoorArrow(Vector2 position) : base("Sprites/spr_doorarrow", 2, "doorarrow")
    {
        this.position = position;
        this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        center = position.Y;
    }

    public override void Update(GameTime gameTime)
    {
        sinValue += gameTime.ElapsedGameTime.TotalSeconds * Math.PI * 6;
        offset = (float)Math.Sin(sinValue);
        position.Y = center + offset;

        base.Update(gameTime);
    }
}
