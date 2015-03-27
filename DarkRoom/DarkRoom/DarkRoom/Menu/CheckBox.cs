using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class CheckBox : SpriteGameObject
{
    //Used for toggling fullscreen mode
    protected Texture2D on, off, selected;
    bool ticked, pressed, select;

    public CheckBox(string assetname = "Sprites/Menu/spr_checkbox", int layer = 1, string id = "", int sheetIndex = 0)
        : base(assetname, layer, id, sheetIndex)
    {
        pressed = false;
        ticked = false;
        select = false;
        off = GameEnvironment.AssetManager.GetSprite("Sprites/Menu/spr_checkbox");
        on = GameEnvironment.AssetManager.GetSprite("Sprites/Menu/spr_checkbox_ticked");
        selected = GameEnvironment.AssetManager.GetSprite("Sprites/Menu/spr_checkbox_selected");
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y))
            select = true;
        else
            select = false;

        if (inputHelper.MouseLeftButtonPressed() &&
            BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y))
        {
            ticked = !ticked;
            pressed = true;
        }
        else
            pressed = false;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!ticked)
            spriteBatch.Draw(off, position, Color.White);
        else
            spriteBatch.Draw(on, position, Color.White);

            spriteBatch.Draw(selected, position, Color.White);

    }

    public bool Pressed
    { get { return pressed; } }

    public bool Ticked
    { get { return ticked; } }

    public bool Selected
    { get { return select; } }
}
