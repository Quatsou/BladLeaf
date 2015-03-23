using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class PauseButton : SpriteGameObject
{
    //A  button with 2 states, pressed or released
    protected bool pressed;
    protected bool selected;
    Texture2D selectedButton;

    public PauseButton(int layer = 1, string id = "")
        : base("Sprites/Menu/spr_pause", layer, id)
    {
        pressed = false;
        selected = false;
        selectedButton = GameEnvironment.AssetManager.GetSprite("Sprites/Menu/spr_pause_selected");
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
        if (selected)
            spriteBatch.Draw(selectedButton, this.position, null, Color.White);
        else
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