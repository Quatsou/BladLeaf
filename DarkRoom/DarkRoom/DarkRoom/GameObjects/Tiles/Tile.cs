using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

enum TileType
{
    Background = 0,
    Wall = 1,
    Door = 2,
    LightSource = 3
}

class Tile : SpriteGameObject
{
    public TileType type;
    public const int TILESIZE = 64;

    public Tile(Vector2 pos, TileType type)
        : base("Sprites/spr_tile", 1, "tile", 0)
    {
        this.position = pos;
        this.type = type;

        if (type == TileType.Wall)
            visible = false;
    }
}