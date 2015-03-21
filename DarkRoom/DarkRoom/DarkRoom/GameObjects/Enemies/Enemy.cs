using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Enemy : AnimatedGameObject
{
    public Enemy(Vector2 startPosition, float rotation)
        : base(2, "enemy")
    {
        this.LoadAnimation("Sprites/spr_enemy", "enemy", false, 1);
        this.PlayAnimation("enemy");

        position = startPosition;
        origin = new Vector2(sprite.Width/2, sprite.Height/2);

        sprite.Rotation = rotation;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Player player = GameWorld.Find("player") as Player;
        if (this.CollidesWith(player))
            Environment.Exit(0);
    }

    public void Die()
    {
        visible = false;
    }
}