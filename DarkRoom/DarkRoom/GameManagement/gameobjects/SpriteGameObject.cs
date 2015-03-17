using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteGameObject : GameObject
{
    //GameObject whose animation consists of a single sprite with a single frame
    protected SpriteSheet sprite;
    protected Vector2 origin;
    protected string assetname;

    public SpriteGameObject(string assetname, int layer = 0, string id = "", int sheetIndex = 0)
        : base(layer, id)
    {
        if (assetname != "")
        {
            sprite = new SpriteSheet(assetname, sheetIndex);
            this.assetname = assetname;
        }
        else
            sprite = null;
    }    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null)
            return;
        sprite.Draw(spriteBatch, new Vector2(this.GlobalPosition.X, this.GlobalPosition.Y), 1, origin);
    }

    public void ChangeSprite(string assetname, bool mirror, int sheetIndex = 0)
    {
        sprite = new SpriteSheet(assetname, sheetIndex);
        this.assetname = assetname;
        this.Mirror = mirror;
    }

    public SpriteSheet Sprite
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
            return sprite.Width;
        }
    }

    public int Height
    {
        get
        {
            return sprite.Height;
        }
    }

    public bool Mirror
    {
        get { return sprite.Mirror; }
        set {sprite.Mirror = value; }
    }

    public Vector2 Origin
    {
        get { return this.origin; }
        set { this.origin = value; }
    }
    
    public string AssetName
    {
        get { return assetname; }
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - origin.X);
            int top = (int)(GlobalPosition.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    public bool CollidesWith(SpriteGameObject obj)
    {
        if (!this.Visible || !obj.Visible || !BoundingBox.Intersects(obj.BoundingBox))
            return false;
        Rectangle b = Collision.Intersection(BoundingBox, obj.BoundingBox);
        for (int x = 0; x < b.Width; x++)
            for (int y = 0; y < b.Height; y++)
            {
                int thisx = b.X - (int)(GlobalPosition.X - origin.X) + x;
                int thisy = b.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                int objx = b.X - (int)(obj.GlobalPosition.X - obj.origin.X) + x;
                int objy = b.Y - (int)(obj.GlobalPosition.Y - obj.origin.Y) + y;
                if (sprite.GetPixelColor(thisx, thisy).A != 0
                    && obj.sprite.GetPixelColor(objx, objy).A != 0)
                    return true;
            }
        return false;
    }
}

