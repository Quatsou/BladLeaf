using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Friendly : AnimatedGameObject
{
    public Vector2 Coords;

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

    public void Escape()
    {
        visible = false;
    }
}
