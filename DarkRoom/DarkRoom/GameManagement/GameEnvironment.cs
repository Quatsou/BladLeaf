using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameEnvironment : Game
{
    // Making the other fundamental classes available outside the namespace
    protected GraphicsDeviceManager graphics;
    protected SpriteBatch spriteBatch;
    protected InputHelper inputHelper;
    protected Matrix spriteScale;
    protected SamplerState samplerState;
    
    
    protected static Point screen;
    protected static GameStateManager gameStateManager;
    protected static Random random;
    protected static AssetManager assetManager;
    protected static GameSettingsManager gameSettingsManager;

    public GameEnvironment()
    {

        graphics = new GraphicsDeviceManager(this);

        inputHelper = new InputHelper();
        gameStateManager = new GameStateManager();
        spriteScale = Matrix.CreateScale(1, 1, 1);
        samplerState = null;
        random = new Random();
        assetManager = new AssetManager(Content);
        gameSettingsManager = new GameSettingsManager();
    }

    public static Point Screen
    {
        get { return GameEnvironment.screen; }
        set { screen = value; }
    }

    public static Random Random
    {
        get { return random; }
    }

    public static AssetManager AssetManager
    {
        get { return assetManager; }
    }

    public static GameStateManager GameStateManager
    {
        get { return gameStateManager; }
    }

    public static GameSettingsManager GameSettingsManager
    {
        get { return gameSettingsManager; }
    }

    protected override void LoadContent()
    {
        DrawingHelper.Initialize(this.GraphicsDevice);
        spriteBatch = new SpriteBatch(GraphicsDevice);

    }

    protected void HandleInput()
    {
        inputHelper.Update();
        if (inputHelper.KeyPressed(Keys.Escape))
            Environment.Exit(0);
        gameStateManager.HandleInput(inputHelper);
    }

    protected override void Update(GameTime gameTime)
    {
        HandleInput();
        gameStateManager.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin(SpriteSortMode.Deferred, null, samplerState, null, null, null, spriteScale);
        gameStateManager.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }
}