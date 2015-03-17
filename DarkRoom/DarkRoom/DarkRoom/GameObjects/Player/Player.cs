using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Player : AnimatedGameObject
{
    public Player(Vector2 startPosition)
        : base(2, "player")
    {
        this.LoadAnimation("Sprites/spr_player", "player", false, 1);
        this.PlayAnimation("player");

        position = startPosition;
    }
}
