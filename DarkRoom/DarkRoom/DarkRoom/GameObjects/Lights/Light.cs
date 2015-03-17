using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
class Source : GameObject
{
    public Source(Vector2 startPos)
    {
        this.position = startPos;
    }
}

class Flashlight : GameObject
{
    public Flashlight(Player player)
    {
        
    }

}