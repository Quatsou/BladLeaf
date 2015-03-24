using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Button : SpriteGameObject
{
    //A  button with 2 states, pressed or released
    protected bool pressed = false;
    protected bool selected = false;
    protected string text;
    Texture2D selectedButton;
    SpriteFont font;

    public Button(string text, int layer = 1, string id = "")
        : base("Sprites/Menu/spr_button", layer, id)
    {
        this.text = text;
        font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("menuFont");
        selectedButton = GameEnvironment.AssetManager.GetSprite("Sprites/Menu/spr_button_selected");
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

        spriteBatch.DrawString(font, text, new Vector2(position.X + 20, position.Y + 20), Color.White);
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