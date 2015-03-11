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
    public Texture2D tile, tile_dark, door, door_dark;

    public Tile(Vector2 pos, TileType type)
        : base("Sprites/spr_tile", 1, "tile", 0)
    {
        this.position = pos;
        this.type = type;

        tile = GameEnvironment.AssetManager.GetSprite("Sprites/spr_tile");
        tile_dark = GameEnvironment.AssetManager.GetSprite("Sprites/spr_tile_dark");
        door = GameEnvironment.AssetManager.GetSprite("Sprites/spr_door");
        door_dark = GameEnvironment.AssetManager.GetSprite("Sprites/spr_door_dark");
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (type == TileType.Background)
            spriteBatch.Draw(tile_dark, position, Color.White);
        else if (type == TileType.Door)
            spriteBatch.Draw(door_dark, position, Color.White);
        else
            return;
    }
}