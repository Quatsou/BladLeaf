using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        base.Update(gameTime);

        if (!finishedRandomizing)
        {
            if (randomTimer > 0)
                randomTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                if (randomizeCounter == -1)
                {
                    finishedRandomizing = true;
                    Player player = GameWorld.Find("player") as Player;
                    player.CanMove = true;
                    ShadowMap.lightsoff = false;
                    ShadowMap.flashLightMode = true;
                    randomTimer = 1f;
                    return;
                }
                if (ShadowMap.lightsoff)
                {
                    if (randomizeCounter == 0)
                    {
                        FinishRandomizing();
                        randomizeCounter--;
                    }
                    else
                        Randomize();

                    ShadowMap.lightsoff = false;
                }
                else
                    ShadowMap.lightsoff = true;

                randomTimer = 1f;
            }
            return;
        }

        if (Level.failed || Level.killedEnemies)
        {
            if (endTimer > 0)
            {
                endTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (endTimer < 1 && Level.killedEnemies)
                {
                    GameObjectList friendlyList = GameWorld.Find("friendlyList") as GameObjectList;
                    foreach (Friendly f in friendlyList.Objects)
                        f.Escape();
                }
                return;
            }
            else
            {
                if (Level.failed)
                    GameEnvironment.GameStateManager.SwitchTo("gameOverState");
                if (Level.killedEnemies)
                    GameEnvironment.GameStateManager.SwitchTo("levelsState");
                return;
            }
        }

        if (clockTimer > 0)
            clockTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        else
        {
            clockTimer = 1;
            timer -= 1;
            GameEnvironment.AssetManager.PlaySound("Audio/snd_clocktick", 1f);
        }

        if(timer <= 0)
            GameEnvironment.GameStateManager.SwitchTo("gameOverState");

        if (!completed)
            CheckCompletion();
        else
            CheckDoor();
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
        int deadEnemyCount = 0;

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

        GameObjectList enemyList = GameWorld.Find("enemyList") as GameObjectList;
        foreach (Enemy e in enemyList.Objects)
        {
            if (e.dead)
                deadEnemyCount++;
            if (deadEnemyCount == enemyList.Objects.Count)
            {
                Level.killedEnemies = true;
                Player player = GameWorld.Find("player") as Player;
                player.CanMove = false;
                ShadowMap.flashLightMode = false;
                PlayingState p = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;
                if (LevelConfigs.levelsCompleted == p.levelNum - 1)
                    LevelConfigs.levelsCompleted++;
            }
        }
    }

    public void CheckDoor()
    {
        TileField tiles = GameWorld.Find("tiles") as TileField;
        Player player = GameWorld.Find("player") as Player;
        if (player.BoundingBox.Intersects(door.BoundingBox))
        {
            PlayingState p = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;
            if (LevelConfigs.levelsCompleted == p.levelNum - 1)
                LevelConfigs.levelsCompleted++;
            GameEnvironment.GameStateManager.SwitchTo("levelsState");
        }
    }
}

