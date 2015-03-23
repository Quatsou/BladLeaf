using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

partial class Level : GameObjectList
{

    public override void HandleInput(InputHelper inputHelper)
    {
        if (pauseButton.Pressed)
            GameEnvironment.GameStateManager.SwitchTo("pauseState");

        base.HandleInput(inputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        if (timer > 0)
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        else
            GameEnvironment.GameStateManager.SwitchTo("gameOverState");

        if (!completed)
            CheckCompletion();
        else
            CheckDoor();

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

    public void CheckCompletion()
    {
        int escapedFriendlyCount = 0;

        GameObjectList friendlyList = GameWorld.Find("friendlyList") as GameObjectList;
        foreach (Friendly f in friendlyList.Objects)
        {
            if (f.escaped)
                escapedFriendlyCount++;
            if (escapedFriendlyCount == friendlyList.Objects.Count)
                completed = true;
        }
    }

    public void CheckDoor()
    {
        TileField tiles = GameWorld.Find("tiles") as TileField;
        Player player = GameWorld.Find("player") as Player;
        if (player.BoundingBox.Intersects(door.BoundingBox))
            GameEnvironment.GameStateManager.SwitchTo("levelsState");
    }
}

