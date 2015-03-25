using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
class LightSource : SpriteGameObject
{
    public LightSource(Vector2 startPosition)
        : base("Sprites/spr_dot", 2, "light")
    {
        position = startPosition;
    }

}