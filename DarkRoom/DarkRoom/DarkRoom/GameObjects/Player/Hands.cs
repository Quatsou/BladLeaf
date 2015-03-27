using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Hands : AnimatedGameObject
{
    Player p;

    public Hands(Player p)
        : base (p.Layer)
    {
        this.p = p;
        this.LoadAnimation("Sprites/spr_player_hands", "hands", false);
        this.PlayAnimation("hands");
        origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
    }

    public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
    {
        position = p.Position;
        sprite.Rotation = p.Sprite.Rotation;

        base.Update(gameTime);
    }
}
