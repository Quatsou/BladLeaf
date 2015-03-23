using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

enum TileType
{
    Background,
    Wall,
    Door
}

class Tile : SpriteGameObject
{
    public TileType type;

    public Tile(Vector2 pos, TileType type)
        : base("Sprites/spr_tile", 1, "tile", 0)
    {
        this.position = pos;
        this.type = type;

        if (type == TileType.Wall)
            visible = false;
    }
}