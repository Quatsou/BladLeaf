using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class LevelsState : GameObjectList
{
    protected LevelButton[] buttons;
    protected Button quitbutton;
    protected PlayingState p;

    public LevelsState()
    {
        p = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;

        SpriteGameObject title = new SpriteGameObject("Sprites/Menu/spr_title");
        title.Position = new Vector2(GameEnvironment.Screen.X / 2 - title.Sprite.Width / 2, 100);
        this.Add(title);

        quitbutton = new Button("Back");
        quitbutton.Position = new Vector2(100, 590);
        this.Add(quitbutton);

        buttons = new LevelButton[5];

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = new LevelButton(i + 1);
            buttons[i].Position = new Vector2(15+(i + 1) * 100, 430);
            this.Add(buttons[i]);
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        for (int i = 0; i < buttons.Length; i++)
            if (buttons[i].Pressed)
            {
                p.LoadLevel(i + 1);
                GameEnvironment.GameStateManager.SwitchTo("playingState");
            }

        if (quitbutton.Pressed)
            GameEnvironment.GameStateManager.SwitchTo("titleState");
    }
}
