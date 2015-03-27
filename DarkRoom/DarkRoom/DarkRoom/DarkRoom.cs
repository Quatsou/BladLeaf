using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

public class DarkRoom : GameEnvironment
{
    static void Main()
    {
        DarkRoom game = new DarkRoom();
        game.Run();
    }

    public DarkRoom()
    {
        Content.RootDirectory = "Content";
        this.IsMouseVisible = true;
    }

    public void setScaling(int scaleX, int scaleY)
    {
        samplerState = SamplerState.PointClamp; //Change to deal with scaling. Necessary for pixel art sprites to not blur.
        graphics.PreferredBackBufferWidth = (scaleX);
        graphics.PreferredBackBufferHeight = (scaleY);
        graphics.ApplyChanges();
        inputHelper.Scale = new Vector2((float)GraphicsDevice.Viewport.Width / screen.X,
                                        (float)GraphicsDevice.Viewport.Height / screen.Y);
    }

    public void SetFullScreen(bool fullscreen = true)
    {
        float scalex = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / (float)screen.X;
        float scaley = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / (float)screen.Y;
        float finalscale = 1f;

        if (!fullscreen)
            finalscale = 2;
        else
        {
            finalscale = scalex;
            if (Math.Abs(1 - scaley) < Math.Abs(1 - scalex))
                finalscale = scaley;
        }

        graphics.PreferredBackBufferWidth = (int)(finalscale * screen.X);
        graphics.PreferredBackBufferHeight = (int)(finalscale * screen.Y);
        graphics.IsFullScreen = fullscreen;
        graphics.ApplyChanges();
        inputHelper.Scale = new Vector2((float)GraphicsDevice.Viewport.Width / screen.X,
                                        (float)GraphicsDevice.Viewport.Height / screen.Y);
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        screen = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        setScaling(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        Camera.camPos = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height) / 2;
        gameStateManager.AddGameState("playingState", new PlayingState(Content));
        gameStateManager.AddGameState("titleState", new TitleState(this));
        gameStateManager.AddGameState("levelsState", new LevelsState());
        gameStateManager.AddGameState("gameOverState", new GameOverState());
        gameStateManager.AddGameState("pauseState", new PauseState());
        /*gameStateManager.AddGameState("OptionsState", new OptionsState(this));
        gameStateManager.AddGameState("helpState", new HelpState());
        gameStateManager.AddGameState("controlsState", new ControlsState());
        gameStateManager.AddGameState("levelFinishedState", new LevelFinishedState());
        gameStateManager.AddGameState("gameWinState", new GameWinState());*/
        GameEnvironment.gameStateManager.SwitchTo("titleState");
    }
}
