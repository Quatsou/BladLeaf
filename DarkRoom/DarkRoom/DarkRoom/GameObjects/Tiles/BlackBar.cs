using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BlackBar : SpriteGameObject
{
    public BlackBar(Vector2 startPos, string imageAsset)
        : base(imageAsset, 10)
    {
        this.position = startPos;
    }
}
