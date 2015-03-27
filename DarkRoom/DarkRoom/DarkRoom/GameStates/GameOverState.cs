using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class GameOverState : GameObjectList
{
    Button quitbutton, retrybutton;
    PlayingState playingstate;

    public GameOverState()
    {
        playingstate = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;

        TextGameObject gameover = new TextGameObject("Game Over");
        gameover.Position = new Vector2(GameEnvironment.Screen.X / 2 - 150, 100);
        this.Add(gameover);

        quitbutton = new Button("Quit");
        quitbutton.Position = new Vector2(GameEnvironment.Screen.X / 2 - quitbutton.Width / 2, 900);
        this.Add(quitbutton);

        retrybutton = new Button("Retry");
        retrybutton.Position = new Vector2(GameEnvironment.Screen.X / 2 - retrybutton.Width / 2, 400);
        this.Add(retrybutton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (quitbutton.Pressed)
        {
            GameEnvironment.AssetManager.PlayMusic("Audio/MenuLoop", true);
            GameEnvironment.GameStateManager.SwitchTo("levelsState");
        }

        if (retrybutton.Pressed || inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
        {
            playingstate.LoadLevel(playingstate.levelNum);
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }

        base.HandleInput(inputHelper);
    }

    public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(playingstate.currentLevel.font, playingstate.currentLevel.timer.ToString("0:00"), new Vector2(50, 50), Color.White);
        base.Draw(gameTime, spriteBatch);
    }
}
