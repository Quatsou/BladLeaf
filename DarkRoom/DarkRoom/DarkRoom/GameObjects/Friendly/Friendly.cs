using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Friendly : AnimatedGameObject
{
    public Vector2 Coords;
    public bool escaped;
    float escapeRotateValue = 0.15f;

    public Friendly(Vector2 startPosition, float rotation)
        : base(2, "friendly")
    {
        this.LoadAnimation("Sprites/spr_friendly", "friendly", false, 1);
        this.PlayAnimation("friendly");

        Coords = startPosition;
        origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

        sprite.Rotation = rotation;
    }

    public override Rectangle BoundingBox
    {
        get { return new Rectangle((int)position.X - 20, (int)position.Y - 20, 40, 40); }
    }

    public override void Update(GameTime gameTime)
    {
        if (escaped)
        {
            if (sprite.Alpha < 0)
                visible = false;
            else
            {
                sprite.Alpha -= escapeRotateValue / 12f;
                escapeRotateValue += (0.7f - escapeRotateValue) / 10f;
                sprite.Rotation += escapeRotateValue;
            }
        }

        base.Update(gameTime);
    }

    public override void Reset()
    {
        visible = true;
        escaped = false;
        sprite.Alpha = 1f;
        sprite.Rotation = 0f;
    }

    public void Escape()
    {
        GameEnvironment.AssetManager.PlaySound("Audio/snd_friendlyreleased",1);
        escaped = true;
    }
}
