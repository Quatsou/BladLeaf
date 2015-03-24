using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Slash : AnimatedGameObject
{
    GameObject killedObject;

    public Slash(Vector2 startPosition, GameObject killedObject)
        : base(3)
    {
        this.killedObject = killedObject;

        this.position = startPosition;

        this.LoadAnimation("Sprites/spr_slash@7", "slash", false, 0.025f);
        this.PlayAnimation("slash");

        origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

        GameEnvironment.AssetManager.PlaySound("Audio/snd_slash", 1);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (Current.SheetIndex == 6)
        {
            GameWorld.Remove(this);
            killedObject.Visible = false;
        }
    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }
}
