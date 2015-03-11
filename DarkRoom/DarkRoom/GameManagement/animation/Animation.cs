using System;
using Microsoft.Xna.Framework;

public class Animation : SpriteSheet
{
    //Keep track of spritesheet properties with sheetindex and looping etc.
    protected float frameTime;
    protected bool isLooping;
    protected float time;

    public Animation(string assetname, bool isLooping, float frametime = 0.1f) : base(assetname)
    {
        this.frameTime = frametime;
        this.isLooping = isLooping;
    }

    public void Play()
    {
        this.sheetIndex = 0;
        this.time = 0.0f;
    }

    public void Update(GameTime gameTime)
    {
        time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        while (time > frameTime)
        {
            time -= frameTime;
            if (isLooping)
                sheetIndex = (sheetIndex + 1) % this.NumberSheetElements;
            else
                sheetIndex = Math.Min(sheetIndex + 1, this.NumberSheetElements - 1);
        }
    }

    public float FrameTime
    {
        get { return frameTime; }
        set { frameTime = value; }
    }

    public float Time
    {
        get { return time; }
    }

    public bool IsLooping
    {
        get { return isLooping; }
        set { isLooping = value; }
    }

    public int CountFrames
    {
        get { return this.NumberSheetElements; }
    }

    public bool AnimationEnded
    {
        get { return !this.isLooping && sheetIndex >= NumberSheetElements - 1; }
    }
}

