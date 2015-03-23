using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Source : GameObject
{
    private GraphicsDevice graphicsDevice;

    public RenderTarget2D RenderTarget { get; private set; }
    public Vector2 LightPosition { get; set; }
    public Vector2 LightAreaSize { get; set; }
    RenderTarget2D screenShadows;

    public Source(GraphicsDevice graphicsDevice, ShadowmapSize size)
    {
        int baseSize = 2 << (int)size;
        LightAreaSize = new Vector2(baseSize);
        RenderTarget = new RenderTarget2D(graphicsDevice, baseSize, baseSize);
        this.graphicsDevice = graphicsDevice;
        screenShadows = new RenderTarget2D(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
    }

    public Vector2 ToRelativePosition(Vector2 worldPosition)
    {
        return worldPosition - (LightPosition - LightAreaSize * 0.5f);
    }

    public void BeginDrawingShadowCasters()
    {
        graphicsDevice.SetRenderTarget(RenderTarget);
        graphicsDevice.Clear(Color.Transparent);
    }

    public void EndDrawingShadowCasters()
    {
        graphicsDevice.SetRenderTarget(null);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        graphicsDevice.Clear(Color.Black);

        ////first light area
        //lightArea1.LightPosition = lightPosition;
        //lightArea1.BeginDrawingShadowCasters();
        //DrawCasters(lightArea1);
        //lightArea1.EndDrawingShadowCasters();
        //shadowmapResolver.ResolveShadows(lightArea1.RenderTarget, lightArea1.RenderTarget, lightPosition);

        ////second light area
        //lightArea2.LightPosition = lightPosition2;
        //lightArea2.BeginDrawingShadowCasters();
        //DrawCasters(lightArea2);
        //lightArea2.EndDrawingShadowCasters();
        //shadowmapResolver.ResolveShadows(lightArea2.RenderTarget, lightArea2.RenderTarget, lightPosition2);


        graphicsDevice.SetRenderTarget(screenShadows);
        graphicsDevice.Clear(Color.Black);
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
        //spriteBatch.Draw(lightArea1.RenderTarget, lightArea1.LightPosition - lightArea1.LightAreaSize * 0.5f, Color.Red);
        //spriteBatch.Draw(lightArea2.RenderTarget, lightArea2.LightPosition - lightArea2.LightAreaSize * 0.5f, Color.Blue);
        spriteBatch.End();

        graphicsDevice.SetRenderTarget(null);


        graphicsDevice.Clear(Color.Black);

        //DrawGround();

        BlendState blendState = new BlendState();
        blendState.ColorSourceBlend = Blend.DestinationColor;
        blendState.ColorDestinationBlend = Blend.SourceColor;

        spriteBatch.Begin(SpriteSortMode.Immediate, blendState);
        spriteBatch.Draw(screenShadows, Vector2.Zero, Color.White);
        spriteBatch.End();

        //DrawScene();

        base.Draw(gameTime, spriteBatch);
    }

    //private void DrawCasters(LightArea lightArea, SpriteBatch spriteBatch)
    //{
    //    spriteBatch.Begin();
    //    spriteBatch.Draw(testTexture, lightArea.ToRelativePosition(Vector2.Zero), Color.Black);
    //    //cat.Draw(spriteBatch, lightArea.ToRelativePosition(cat.Position), Color.Black);
    //    spriteBatch.End();
    //}
}

