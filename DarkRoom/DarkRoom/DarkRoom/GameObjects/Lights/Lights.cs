using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Krypton;
using Krypton.Lights;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
class Lights : GameObjectList
{    
    public Lights(Flashlight flashlight)
    {
        this.Add(flashlight);
    }

    //protected override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    //{
    //}
}

