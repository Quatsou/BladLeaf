using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class LevelButton : SpriteGameObject
{
    //A  button with 2 states, pressed or released
    protected bool pressed = false;
    protected bool selected = false;
    protected int number;
    SpriteFont font;

    public LevelButton(int number)
        : base("Sprites/Menu/spr_levelbutton", 1, "")
    {
        font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("levelFont");
        this.number = number;
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
        if (LevelConfigs.levelsCompleted < number - 1)
            return;
        else
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, number.ToString(), new Vector2(position.X + 7, position.Y), Color.Black);
        }
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