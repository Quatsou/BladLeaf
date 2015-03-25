using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PlayingState : IGameLoopObject
{
    public Level currentLevel;
    public LevelConfigs levelConfigs;
    public int levelNum;

    public PlayingState(ContentManager Content)
    {
        levelConfigs = new LevelConfigs();
    }

    public void LoadLevel(int levelNum)
    {
        GameEnvironment.AssetManager.PlayMusic("Audio/Dark Room", true);
        currentLevel = new Level(levelConfigs.sizesX[levelNum - 1], levelConfigs.sizesY[levelNum - 1], levelConfigs.configurations[levelNum - 1], levelConfigs.enemiesConfig[levelNum - 1], levelConfigs.friendliesConfig[levelNum - 1],
            levelConfigs.timerConfig[levelNum - 1]);
        this.levelNum = levelNum;
    }

    public virtual void HandleInput(InputHelper inputHelper) //Triggers the pause state
    {
        currentLevel.HandleInput(inputHelper);
    }

    public virtual void Update(GameTime gameTime)
    {
        currentLevel.Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        currentLevel.Draw(gameTime, spriteBatch);
    }

    public virtual void Reset()
    {
        currentLevel.Reset();
    }
}
