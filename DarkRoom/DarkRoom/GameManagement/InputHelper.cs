using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class InputHelper
{
    //Fundamental class for input through peripherals
    protected MouseState currentMouseState, previousMouseState;
    protected KeyboardState currentKeyboardState, previousKeyboardState;
    protected GamePadState[] padState = new GamePadState[5];
    protected Vector2 scale;

    public InputHelper()
    {
        scale = Vector2.One;
    }

    public void Update()
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        padState[3] = padState[1]; //padState[playerNr + 2] == previousPadState
        padState[4] = padState[2];
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();
        padState[2] = GamePad.GetState(PlayerIndex.One); //padState[playerNr] == currentPadState
        padState[1] = GamePad.GetState(PlayerIndex.Two);
    }

    public Vector2 Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    public Vector2 MousePosition
    {
        get { return new Vector2(currentMouseState.X, currentMouseState.Y) / scale; }
    }

    public bool MouseLeftButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
    }

    public bool MouseLeftButtonDown()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed;
    }

    public bool KeyPressed(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
    }

    public bool KeyReleased(Keys k)//Checks if key was just released
    {
        return previousKeyboardState.IsKeyDown(k) && currentKeyboardState.IsKeyUp(k);
    }

    public bool ButtonPressed(Buttons b, int playerNr)
    {
        if (padState[playerNr].IsButtonDown(b) && padState[playerNr + 2].IsButtonUp(b))
            return true;
        return false;
    }

    public bool ButtonReleased(Buttons b, int playerNr)//Checks if button was just released
    {
        if (padState[playerNr].IsButtonUp(b) && padState[playerNr + 2].IsButtonDown(b))
            return true;
        return false;
    }

    public bool IsButtonDown(Buttons b, int playerNr)
    {
        if (padState[playerNr].IsButtonDown(b) && padState[playerNr + 2].IsButtonDown(b))
            return true;
        return false;
    }

    public bool LeftTriggerDown(int playerNr)
    {
        if (padState[playerNr].Triggers.Left == 1)
            return true;
        return false;
    }

    public bool LeftTriggerUp(int playerNr)
    {
        if (padState[playerNr].Triggers.Left != 1 && padState[playerNr + 2].Triggers.Left == 1)
            return true;
        return false;
    }

    public bool RightTriggerPressed(int playerNr)
    {
        if (padState[playerNr].Triggers.Right == 1 && padState[playerNr + 2].Triggers.Right != 1)
            return true;
        return false;
    }

    public bool ThumbSticks(string dir, int playerNr)
    {
        if (dir == "left" && padState[playerNr].ThumbSticks.Left.X < 0)
            return true;
        else if (dir == "right" && padState[playerNr].ThumbSticks.Left.X > 0)
            return true;
        else if (dir == "up" && padState[playerNr].ThumbSticks.Left.Y > 0.5)
            return true;
        else if (dir == "down" && padState[playerNr].ThumbSticks.Left.Y < -0.5)
            return true;
        else
            return false;
    }

    public bool IsKeyDown(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k);
    }

    public bool AnyKeyPressed
    {
        get { return currentKeyboardState.GetPressedKeys().Length > 0 && previousKeyboardState.GetPressedKeys().Length == 0; }
    }
}