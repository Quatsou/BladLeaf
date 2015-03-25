using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

class Player : AnimatedGameObject
{
    public Vector2 hitPoint;
    float smoothRotationValue = 0f;
    public const float flashLightFOV = 2f;
    public const float flashLightRange = 200f;
    public const float flashLightInnerRange = 150f;
    const float ROTATIONSMOOTHNESS = 3f; // Higher is slower and smoother
    public bool CanMove;

    public Player(Vector2 startPosition)
        : base(3, "player")
    {
        this.LoadAnimation("Sprites/spr_player", "player", false, 1);
        this.PlayAnimation("player");

        position = startPosition;
        origin = new Vector2(sprite.Width/2, sprite.Height/2);
        CanMove = false;
    }

    public override void Update(GameTime gameTime)
    {
        hitPoint = new Vector2(position.X, position.Y - 48);
        base.Update(gameTime);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (!CanMove)
            return;

        Movement(inputHelper);
        StayInBounds();

        GameObjectList enemyList = GameWorld.Find("enemyList") as GameObjectList;

        foreach (Enemy e in enemyList.Objects)
        {
            if (e.BoundingBox.Contains(new Point((int)RotateVector2(hitPoint, sprite.Rotation, position).X, (int)RotateVector2(hitPoint, sprite.Rotation, position).Y)))
                e.selected = true;
            else
                e.selected = false;
        }

        GameObjectList friendlyList = GameWorld.Find("friendlyList") as GameObjectList;

        foreach (Friendly f in friendlyList.Objects)
        {
            if (f.BoundingBox.Contains(new Point((int)RotateVector2(hitPoint, sprite.Rotation, position).X, (int)RotateVector2(hitPoint, sprite.Rotation, position).Y)))
                f.selected = true;
            else
                f.selected = false;
        }

        if (inputHelper.MouseLeftButtonPressed() || inputHelper.KeyPressed(Keys.Space))
        {
            foreach (Enemy e in enemyList.Objects)
            {
                if (e.dead)
                    continue;
                if (e.BoundingBox.Contains(new Point((int)RotateVector2(hitPoint, sprite.Rotation, position).X, (int)RotateVector2(hitPoint, sprite.Rotation, position).Y)))
                {
                    e.Die();
                    return;
                }
            }

            foreach (Friendly f in friendlyList.Objects)
            {
                if (f.escaped)
                    continue;
                if (f.BoundingBox.Contains(new Point((int)RotateVector2(hitPoint, sprite.Rotation, position).X, (int)RotateVector2(hitPoint, sprite.Rotation, position).Y)))
                {
                    f.Die();
                    return;
                }
            }
        }

        base.HandleInput(inputHelper);
    }

    public void Movement(InputHelper inputHelper)
    {
        float moveSpeed = 0;

        if (inputHelper.IsKeyDown(Keys.W))
            moveSpeed = 4;
        if (inputHelper.IsKeyDown(Keys.S))
            moveSpeed = -2f;

        if (inputHelper.IsKeyDown(Keys.A))
            smoothRotationValue += (-0.1f - smoothRotationValue) / ROTATIONSMOOTHNESS;
        if (inputHelper.IsKeyDown(Keys.D))
            smoothRotationValue += (0.1f - smoothRotationValue) / ROTATIONSMOOTHNESS;

        if(!inputHelper.IsKeyDown(Keys.A) && !inputHelper.IsKeyDown(Keys.D))
            smoothRotationValue += (0 - smoothRotationValue) / (ROTATIONSMOOTHNESS/2);

        sprite.Rotation += smoothRotationValue;

        Vector2 direction = new Vector2((float)Math.Cos(sprite.Rotation - Math.PI / 2),
                                    (float)Math.Sin(sprite.Rotation - Math.PI / 2));
        direction.Normalize();
        position += direction * moveSpeed;
    }

    public void StayInBounds()
    {
        Rectangle BoundingBox = new Rectangle((int)position.X - sprite.Width / 2, 
            (int)position.Y - sprite.Height / 2, sprite.Width, sprite.Height);

        TileField tiles = GameWorld.Find("tiles") as TileField;
        if (BoundingBox.Left < tiles.fieldAnchor.X)
            position.X = tiles.fieldAnchor.X + BoundingBox.Width / 2;
        else if (BoundingBox.Right > tiles.fieldAnchor.X + tiles.fieldLength.X)
            position.X = tiles.fieldAnchor.X + tiles.fieldLength.X - BoundingBox.Width / 2;
        if (BoundingBox.Top < tiles.fieldAnchor.Y)
            position.Y = tiles.fieldAnchor.Y + BoundingBox.Height / 2;
        else if (BoundingBox.Bottom > tiles.fieldAnchor.Y + tiles.fieldLength.Y)
            position.Y = tiles.fieldAnchor.Y + tiles.fieldLength.Y - BoundingBox.Height / 2;

        Tile currentTile = tiles.Get((int)(position.X - tiles.fieldAnchor.X) / 64,
            (int)(position.Y - tiles.fieldAnchor.Y) / 64) as Tile;
        foreach (Tile t in tiles.Objects)
        {
            if (this.BoundingBox.Intersects(t.BoundingBox) && t.type == TileType.Wall)
            {
                Vector2 depth = Collision.CalculateIntersectionDepth(BoundingBox, t.BoundingBox);
                if (Math.Abs(depth.X) < Math.Abs(depth.Y))
                    position.X += depth.X;
                else if (Math.Abs(depth.Y) < Math.Abs(depth.X))
                    position.Y += depth.Y;
            }
        }

    }

    public static Vector2 RotateVector2(Vector2 point, float radians, Vector2 pivot)
    {
        float cosRadians = (float)Math.Cos(radians);
        float sinRadians = (float)Math.Sin(radians);

        Vector2 translatedPoint = new Vector2();
        translatedPoint.X = point.X - pivot.X;
        translatedPoint.Y = point.Y - pivot.Y;

        Vector2 rotatedPoint = new Vector2();
        rotatedPoint.X = translatedPoint.X * cosRadians - translatedPoint.Y * sinRadians + pivot.X;
        rotatedPoint.Y = translatedPoint.X * sinRadians + translatedPoint.Y * cosRadians + pivot.Y;

        return rotatedPoint;
    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        Texture2D pt = GameEnvironment.AssetManager.GetSprite("Sprites/spr_dot");
        Texture2D dotGlow = GameEnvironment.AssetManager.GetSprite("Sprites/spr_dot_glow");
        Vector2 dotPos = RotateVector2(hitPoint, sprite.Rotation, position);
        spriteBatch.Draw(pt, new Vector2(dotPos.X - pt.Width / 2, dotPos.Y - pt.Height / 2), Color.White * 0.4f);
        spriteBatch.Draw(dotGlow, new Vector2(dotPos.X - dotGlow.Width / 2, dotPos.Y - dotGlow.Height / 2), Color.White * 0.4f);
    }
}
