﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TitleState : GameObjectList
{
    protected Button playbutton, quitbutton;
    protected Game darkroom;

    public TitleState(Game game)
    {
        darkroom = game;

        SpriteGameObject title = new SpriteGameObject("Sprites/Menu/spr_title");
        title.Position = new Vector2(GameEnvironment.Screen.X / 2 - title.Sprite.Width / 2, 100);
        this.Add(title);

        playbutton = new Button("Sprites/Menu/spr_playbutton");
        playbutton.Position = new Vector2(50, 400);
        this.Add(playbutton);

        quitbutton = new Button("Sprites/Menu/spr_quitbutton");
        quitbutton.Position = new Vector2(50, 900);
        this.Add(quitbutton);
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
