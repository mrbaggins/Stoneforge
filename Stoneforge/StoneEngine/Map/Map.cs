using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StoneEngine
{
    public class Map
    {
        public const int TileWidth = 32;
        public const int TileHeight = 32;
        
        public static Point ConvertPositionToCell(Vector2 position)
        {
            return new Point((int)(position.X / (float)TileWidth), (int)(position.Y / (float)TileHeight));
        }

        public List<MapLayer> Layers = new List<MapLayer>();

        public int WidthInPixels
        {
            get { return Layers[0].Width * TileWidth; }
        }
        public int HeightInPixels
        {
            get { return Layers[0].Height * TileHeight; }
        }
        public int Width
        {
            get { return Layers[0].Width; }
        }
        public int Height
        {
            get { return Layers[0].Height; }
        }

        public static Map LoadMap(string filename)
        {
            Map map = new Map();

            return map;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            Point min = ConvertPositionToCell(camera.position);
            Point max = ConvertPositionToCell(camera.position + new Vector2(
                    spriteBatch.GraphicsDevice.Viewport.Width + TileWidth - 1,
                    spriteBatch.GraphicsDevice.Viewport.Height + TileHeight - 1));

            foreach (MapLayer layer in Layers)
                layer.Draw(spriteBatch, camera, min, max);

        }
    }
}
