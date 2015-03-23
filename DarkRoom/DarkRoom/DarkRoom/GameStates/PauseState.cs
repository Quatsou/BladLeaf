using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PauseState : GameObjectList
{
    Button quitbutton, resumebutton;
    PlayingState playingstate;  

    public PauseState()
    {
        playingstate = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;

        TextGameObject gameover = new TextGameObject("Paused");
        gameover.Position = new Vector2(GameEnvironment.Screen.X / 2 - 150, 100);
        this.Add(gameover);

        quitbutton = new Button("Quit");
        quitbutton.Position = new Vector2(GameEnvironment.Screen.X / 2 - quitbutton.Width / 2, 900);
        this.Add(quitbutton);

        resumebutton = new Button("Resume");
        resumebutton.Position = new Vector2(GameEnvironment.Screen.X / 2 - resumebutton.Width / 2, 400);
        this.Add(resumebutton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (quitbutton.Pressed)
            GameEnvironment.GameStateManager.SwitchTo("levelsState");

        if (resumebutton.Pressed || inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
        {
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
