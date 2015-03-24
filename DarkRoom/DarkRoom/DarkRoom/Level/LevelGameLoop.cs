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
        if (clockTimer > 0)
            clockTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        else
        {
            clockTimer = 1;
            timer -= 1;
            GameEnvironment.AssetManager.PlaySound("Audio/snd_clocktick", 0.5f);
        }

        if(timer <= 0)
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
            {
                completed = true;

                Door door = GameWorld.Find("door") as Door;
                Vector2 doorPos = door.GlobalPosition + new Vector2(Tile.TILESIZE/2, -Tile.TILESIZE);
                GameWorld.Add(new DoorArrow(doorPos));
            }
        }
    }

    public void CheckDoor()
    {
        TileField tiles = GameWorld.Find("tiles") as TileField;
        Player player = GameWorld.Find("player") as Player;
        if (player.BoundingBox.Intersects(door.BoundingBox))
        {
            LevelConfigs.levelsCompleted++;
            GameEnvironment.GameStateManager.SwitchTo("levelsState");
        }
    }
}

