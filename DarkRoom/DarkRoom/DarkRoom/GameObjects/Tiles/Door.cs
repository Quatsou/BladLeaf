using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Door : SpriteGameObject
{
    public Door(Vector2 startPosition)
        : base("Sprites/spr_doorwhite", 2)
    {
        position = startPosition;
    }
}
