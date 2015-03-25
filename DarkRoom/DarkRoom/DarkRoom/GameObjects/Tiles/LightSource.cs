using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
 
class LightSource : SpriteGameObject
{
    public bool On = true;
    Timer lightOffTimer;

    public LightSource(Vector2 startPosition)
        : base("Sprites/spr_dot", 2, "light")
    {
        position = startPosition;
    }

    public void LightOff(float seconds, ShadowMap shadowMap)
    {
        if (On)
        {
            On = false;
            shadowMap.SetInitialSM();

            lightOffTimer = new Timer(seconds * 1000);
            lightOffTimer.Enabled = true;
            lightOffTimer.Elapsed += delegate { lightOffTimer.Enabled = false; LightBackOn(shadowMap); };
        }
    }

     void LightBackOn(ShadowMap shadowMap)
     {
         On = true;
         shadowMap.SetInitialSM();
     }

}