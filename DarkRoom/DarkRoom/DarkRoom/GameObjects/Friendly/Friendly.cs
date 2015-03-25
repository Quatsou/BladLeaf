using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Friendly : AnimatedGameObject
{
    public Vector2[] Coords;
    public bool escaped = false;
    float escapeRotateValue = 0.15f;
    public bool selected = false;
    public bool dead = false;
    public float[] startRotation;
    double sinValue = 0f;
    float offset = 0;

    public Friendly(Vector2[] coords, float[] rotation)
        : base(2, "friendly")
    {
        this.LoadAnimation("Sprites/spr_friendly", "friendly", false, 1);
        this.PlayAnimation("friendly");

        this.Coords = coords;
        origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

        startRotation = rotation;
    }

    public override Rectangle BoundingBox
    {
        get { return new Rectangle((int)position.X - 20, (int)position.Y - 20, 40, 40); }
    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        if (visible && selected)
        {
            SpriteSheet selectedSprite = new SpriteSheet("Sprites/spr_friendlyselect");

            sinValue += gameTime.ElapsedGameTime.TotalSeconds * Math.PI * 6;
            offset = ((float)Math.Sin(sinValue) + 1);
            float scale = offset / 27 + 0.925f;
            float alpha = offset - 0.6f;
            
            Rectangle spritePart = new Rectangle(0, 0, selectedSprite.Width, selectedSprite.Height);
            spriteBatch.Draw(selectedSprite.Sprite, new Vector2(position.X, position.Y), spritePart, Color.DarkGreen * alpha,
            sprite.Rotation, new Vector2(selectedSprite.Width / 2, selectedSprite.Height / 2), scale, SpriteEffects.None, 0.0f);
        }
        base.Draw(gameTime, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        //Check collision with player
        Player player = GameWorld.Find("player") as Player;

        if (!escaped)
        {
            if(CollidesWith(player))
                Escape();
        }
        else
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
        dead = false;
        escaped = false;
        sprite.Alpha = 1f;
        sprite.Rotation = 0f;
    }

    public void Escape()
    {
        if (!dead)
        {
            GameEnvironment.AssetManager.PlaySound("Audio/snd_friendlyreleased", 1);
            escaped = true;
        }
    }

    public void Die()
    {
        dead = true;
        Slash slash = new Slash(position, this as GameObject);
        slash.Sprite.Color = Color.Red;
        GameWorld.Add(slash);
    }
}