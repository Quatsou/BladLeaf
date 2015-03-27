using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Cone : SpriteGameObject
{
    Enemy e;

    public Cone(string imageAsset, Enemy e)
        : base ("Sprites/" + imageAsset, 4)
    {
        this.e = e;
    }

    public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
    {
        base.Update(gameTime);

        Player player = GameWorld.Find("player") as Player;
        if (player.CollidesWith(this) && player.CanMove)
        {
            player.CanMove = false;
            ShadowMap.flashLightMode = false;
            this.visible = true;
            Level.failed = true;
        }
    }

    public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!ShadowMap.flashLightMode)
            base.Draw(gameTime, spriteBatch);
    }

    public void AdjustCone()
    {
        if (e.Sprite.Rotation == 0f)
            this.ChangeSprite("Sprites/spr_cone_up", false);
        else if (e.Sprite.Rotation == 0.785f)
            this.ChangeSprite("Sprites/spr_cone_up_right", false);
        else if (e.Sprite.Rotation == 1.570f)
            this.ChangeSprite("Sprites/spr_cone_right", false);
        else if (e.Sprite.Rotation == 2.356f)
            this.ChangeSprite("Sprites/spr_cone_down_right", false);
        else if (e.Sprite.Rotation == 3.141f)
            this.ChangeSprite("Sprites/spr_cone_down", false);
        else if (e.Sprite.Rotation == 3.927f)
            this.ChangeSprite("Sprites/spr_cone_down_left", false);
        else if (e.Sprite.Rotation == 4.712f)
            this.ChangeSprite("Sprites/spr_cone_left", false);
        else if (e.Sprite.Rotation == 5.498f)
            this.ChangeSprite("Sprites/spr_cone_up_left", false);

        this.Position = new Vector2(e.Position.X - 320 / 2, e.Position.Y - 320 / 2);
    }
}