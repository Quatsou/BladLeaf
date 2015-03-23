using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Enemy : AnimatedGameObject
{
    public Vector2 Coords;
    public bool dead;

    public Enemy(Vector2 startPosition, float rotation)
        : base(2, "enemy")
    {
        this.LoadAnimation("Sprites/spr_enemy", "enemy", false, 1);
        this.PlayAnimation("enemy");

        Coords = startPosition;
        origin = new Vector2(sprite.Width/2, sprite.Height/2);

        sprite.Rotation = rotation;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Player player = GameWorld.Find("player") as Player;
        if (this.CollidesWith(player))
            GameEnvironment.GameStateManager.SwitchTo("gameOverState");
        Console.WriteLine(currentAnimation);
    }

    public override Rectangle BoundingBox
    {
        get { return new Rectangle((int)position.X - 20, (int)position.Y - 20, 40, 40); }
    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        visible = true;
        dead = false;
    }

    public void Die()
    {
        GameWorld.Add(new Slash(position));
        visible = false;
        dead = true;
    }
}