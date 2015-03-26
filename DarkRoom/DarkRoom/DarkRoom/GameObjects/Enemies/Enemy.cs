using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Enemy : AnimatedGameObject
{
    public Vector2[] Coords;
    public bool dead;
    public bool selected = false;
    public float[] startRotation;

    double sinValue = 0;
    float offset = 0;

    public Enemy(Vector2[] coords, float[] rotation)
        : base(2, "enemy")
    {
        this.LoadAnimation("Sprites/spr_enemy", "enemy", false, 1);
        this.PlayAnimation("enemy");

        this.Coords = coords;
        origin = new Vector2(sprite.Width/2, sprite.Height/2);

        startRotation = rotation;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Player player = GameWorld.Find("player") as Player;
        if (!dead && this.CollidesWith(player))
            GameEnvironment.GameStateManager.SwitchTo("gameOverState");
    }

    public override Rectangle BoundingBox
    {
        get { return new Rectangle((int)position.X - 20, (int)position.Y - 20, 40, 40); }
    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        if (visible && selected)
        {
            SpriteSheet selectedSprite = new SpriteSheet("Sprites/spr_frenemy_selected");

            sinValue += gameTime.ElapsedGameTime.TotalSeconds * Math.PI * 6;
            offset = ((float)Math.Sin(sinValue) + 1);
            float scale = offset / 27 + 0.925f;
            float alpha = offset - 0.6f;

            Rectangle spritePart = new Rectangle(0, 0, selectedSprite.Width, selectedSprite.Height);
            spriteBatch.Draw(selectedSprite.Sprite, new Vector2(position.X, position.Y), spritePart, Color.White * alpha,
            sprite.Rotation, new Vector2(selectedSprite.Width/2,selectedSprite.Height/2), scale, SpriteEffects.None, 0.0f);
        }

        base.Draw(gameTime, spriteBatch);

        if (visible && !dead && ShadowMap.flashLightMode)
        {
            sprite.Alpha = 0;

            SpriteSheet darksprite = new SpriteSheet("Sprites/spr_darkfrenemy");
            spriteBatch.Draw(darksprite.Sprite, new Vector2(position.X, position.Y), null, Color.White,
            sprite.Rotation, new Vector2(darksprite.Sprite.Width / 2, darksprite.Sprite.Height / 2), 1, SpriteEffects.None, 0.0f);
        }
        else
        {
            sprite.Alpha = 1;
        }
    }

    public override void Reset()
    {
        selected = false;
        visible = true;
        dead = false;
    }

    public void Die()
    {
        dead = true;
        Slash slash = new Slash(position, this as GameObject);
        slash.Sprite.Color = Color.LightGreen;
        GameWorld.Add(slash);
    }
}