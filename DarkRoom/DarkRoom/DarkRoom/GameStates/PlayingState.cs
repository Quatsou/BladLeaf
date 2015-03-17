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
    public TileType[,] levelConfig = new TileType[28, 15];
    public LevelConfigs levelConfigs;

    public PlayingState(ContentManager Content)
    {
        levelConfigs = new LevelConfigs();
        LoadLevel(2);
    }

    public void LoadLevel(int levelNum)
    {
        currentLevel = new Level(levelConfigs.sizesX[levelNum - 1], levelConfigs.sizesY[levelNum - 1], levelConfigs.configurations[levelNum - 1], levelConfigs.lightSourcesConfig[levelNum - 1]);
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
