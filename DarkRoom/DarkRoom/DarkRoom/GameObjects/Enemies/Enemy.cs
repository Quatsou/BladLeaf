using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Enemy : AnimatedGameObject
{
    public Enemy(Vector2 startPosition)
        : base(2, "player")
    {
        this.LoadAnimation("Sprites/spr_player", "player", false, 1);
        this.PlayAnimation("player");

        position = startPosition;
        origin = new Vector2(sprite.Width/2, sprite.Height/2);
    }
}