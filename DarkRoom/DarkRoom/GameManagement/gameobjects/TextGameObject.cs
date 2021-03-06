﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TextGameObject : GameObject
{
    //GameObject consisting only of text
    protected SpriteFont spriteFont;
    protected Color color;
    protected string text;
    protected float visibleTime;
    protected bool visibleTimer;

    public TextGameObject(string text, int layer = 0, bool visibleTimer = false, string id = "")
        : base(layer, id)
    {
        spriteFont = GameEnvironment.AssetManager.Content.Load<SpriteFont>("menuFont");
        this.text = text;
        color = Color.White;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(spriteFont, text, this.GlobalPosition, color);
    }

    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    public Vector2 Size
    {
        get
        {
            return spriteFont.MeasureString(text);
        }
    }
}

