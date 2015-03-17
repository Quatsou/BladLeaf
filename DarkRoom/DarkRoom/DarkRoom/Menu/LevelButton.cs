using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class LevelButton : SpriteGameObject
{
    //A  button with 2 states, pressed or released
    protected bool pressed;
    protected bool selected;

    public LevelButton(int number)
        : base("Sprites/Menu/spr_button" + number.ToString(), 1, "")
    {
        pressed = false;
        selected = false;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        pressed = inputHelper.MouseLeftButtonPressed() &&
            BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);

        // if (pressed)
        // GameEnvironment.AssetManager.PlaySound("snd_menu_click", 1f);

        if (this.BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y))
        {
            //if (!selected)
            //GameEnvironment.AssetManager.PlaySound("snd_menu_select", 1);
            selected = true;
        }
        else
            selected = false;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //if (selected)
           // spriteBatch.Draw(selectedButton, this.position, null, Color.White);
       // else
            base.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        base.Reset();
        pressed = false;
    }

    public bool Pressed
    {
        get { return pressed; }
    }

    public bool Selected
    {
        get { return selected; }
    }
}