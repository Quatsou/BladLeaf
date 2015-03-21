using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

partial class Level : GameObjectList
{

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        if (timer > 0)
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        else
            GameEnvironment.GameStateManager.SwitchTo("gameOverState");
        base.Update(gameTime);
    }

    public override void Reset()
    {
        base.Reset();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.DrawString(font, timer.ToString("0:00"), new Vector2(50, 50), Color.White);
    }
}

