﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteSheet
{
    //Draws the correct animation frames
    protected Texture2D sprite;
    protected int sheetIndex;
    protected int sheetColumns;
    protected int sheetRows;
    protected bool mirror;
    protected float rotation = 0;
    protected Color color = Color.White;
    protected float alpha = 1f;

    public SpriteSheet(string assetname, int sheetIndex = 0)
    {
        sprite = GameEnvironment.AssetManager.GetSprite(assetname);
        this.sheetIndex = sheetIndex;
        this.sheetColumns = 1;
        this.sheetRows = 1;

        // see if we can extract the number of sheet elements from the assetname
        string[] assetSplit = assetname.Split('@');
        if (assetSplit.Length <= 1)
            return;

        string sheetNrData = assetSplit[assetSplit.Length - 1];
        string[] colrow = sheetNrData.Split('x');
        this.sheetColumns = int.Parse(colrow[0]);
        if (colrow.Length == 2)
            this.sheetRows = int.Parse(colrow[1]);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale, Vector2 origin)
    {
        int columnIndex = sheetIndex % sheetColumns;
        int rowIndex = sheetIndex / sheetColumns % sheetRows;
        Rectangle spritePart = new Rectangle(columnIndex * this.Width, rowIndex * this.Height, this.Width, this.Height);
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (mirror)
            spriteEffects = SpriteEffects.FlipHorizontally;
        spriteBatch.Draw(sprite, new Vector2(position.X * scale, position.Y * scale), spritePart, color * alpha,
            rotation, origin, scale, spriteEffects, 0.0f);
    }

    public Color GetPixelColor(int x, int y)
    {
        int column_index = sheetIndex % sheetColumns;
        int row_index = sheetIndex / sheetColumns % sheetRows;
        Rectangle sourceRectangle = new Rectangle(column_index * this.Width + x, row_index * this.Height + y, 1, 1);
        Color[] retrievedColor = new Color[1];
        sprite.GetData<Color>(0, sourceRectangle, retrievedColor, 0, 1);
        return retrievedColor[0];
    }

    public Texture2D Sprite
    {
        get { return sprite; }
    }

    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    public int Width
    {
        get
        {
            return sprite.Width / sheetColumns;
        }
    }

    public int Height
    {
        get
        {
            return sprite.Height / sheetRows;
        }
    }

    public bool Mirror
    {
        get { return mirror; }
        set { mirror = value; }
    }

    public float Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    public float Alpha
    {
        get { return alpha; }
        set { alpha = value; }
    }

    public int SheetIndex
    {
        get
        {
            return this.sheetIndex;
        }
        set
        {
            if (value < this.sheetColumns * this.sheetRows && value >= 0)
                this.sheetIndex = value;
        }
    }

    public int NumberSheetElements
    {
        get { return this.sheetColumns * this.sheetRows; }
    }
}