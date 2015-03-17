using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

class Player : AnimatedGameObject
{
    public float moveSpeed = 0;

    public Player(Vector2 startPosition)
        : base(2, "player")
    {
        this.LoadAnimation("Sprites/spr_player", "player", false, 1);
        this.PlayAnimation("player");

        position = startPosition;
        origin = new Vector2(sprite.Width/2, sprite.Height/2);
    }

    public override void Update(GameTime gameTime)
    {
        Vector2 direction = new Vector2((float)Math.Cos(sprite.Rotation - Math.PI / 2),
                                    (float)Math.Sin(sprite.Rotation - Math.PI / 2));
        direction.Normalize();
        Console.WriteLine(direction);

        velocity = direction * moveSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        TileField tiles = GameWorld.Find("tiles") as TileField;

        foreach (Tile tile in tiles.Objects)
        {
            if (tile.type == TileType.Wall && CollidesWith(tile))
            {
                if (Velocity.X > 0)
                    position = new Vector2(tile.BoundingBox.Left - Sprite.Width / 2, Position.Y);
                else if (Velocity.X < 0)
                    position = new Vector2(tile.BoundingBox.Right + Sprite.Width / 2, Position.Y);

                /*if (Velocity.Y > 0)
                    position = new Vector2(Position.X, tile.BoundingBox.Top - Sprite.Height / 2);
                else if (Velocity.Y < 0)
                    position = new Vector2(Position.X, tile.BoundingBox.Bottom + Sprite.Height / 2);*/

                break;
            }
        }

        base.Update(gameTime);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        moveSpeed = 0;

        if (inputHelper.IsKeyDown(Keys.W))
            moveSpeed = 10;
        if (inputHelper.IsKeyDown(Keys.S))
            moveSpeed = -4f;

        if (inputHelper.IsKeyDown(Keys.A))
            sprite.Rotation -= 0.05f;
        if (inputHelper.IsKeyDown(Keys.D))
            sprite.Rotation += 0.05f;

        base.HandleInput(inputHelper);
    }

}
