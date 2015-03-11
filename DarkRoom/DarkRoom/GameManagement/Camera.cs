using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Camera
{
    //Camera class with the properties, the actual bounding boxes can be found within player physics "Handle Camera"
    public static Vector2 pos;
    public static Vector2 levelSize;
    public static bool inBossfight;
    public static float scale;

    public Camera() 
    {
        levelSize = new Vector2(256 + 160, 224);
        pos = new Vector2(0,0);
        inBossfight = false;
        scale = 1;
    }
}