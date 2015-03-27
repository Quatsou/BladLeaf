using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TitleState : GameObjectList
{
    protected Button playbutton, quitbutton;
    protected DarkRoom darkroom;

    public TitleState(DarkRoom game)
    {
        darkroom = game;

        SpriteGameObject title = new SpriteGameObject("Sprites/Menu/spr_title");
        title.Position = new Vector2(GameEnvironment.Screen.X / 2 - title.Sprite.Width / 2, 100);
        this.Add(title);

        playbutton = new Button("Play");
        playbutton.Position = new Vector2(100, 430);
        this.Add(playbutton);

        quitbutton = new Button("Quit");
        quitbutton.Position = new Vector2(100, 590);
        this.Add(quitbutton);

        GameEnvironment.AssetManager.PlayMusic("Audio/MenuLoop", true);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (playbutton.Pressed || inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            GameEnvironment.GameStateManager.SwitchTo("levelsState");

        if (quitbutton.Pressed)
            darkroom.Exit();

    }
}
