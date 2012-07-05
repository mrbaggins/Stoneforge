using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StoneEngine
{
    public class MapLayer
    {
        List<Texture2D> textures = new List<Texture2D>();
        int[,] map;

        public int Width
        {
            get { return map.GetLength(1); }
        }
        public int Height
        {
            get { return map.GetLength(0); }
        }

        public MapLayer(int width, int height)
        {
            map = new int[height, width];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[y, x] = -1;
                }
            }
        }
        public MapLayer(int[,] existingMap)
        {
            map = (int[,])existingMap.Clone();
        }

        public void SetCellIndex(int x, int y, int newIndex)
        {
            map[y, x] = newIndex;
        }
        public void SetCellIndex(Point p, int newIndex)
        {
            map[p.Y, p.X] = newIndex;
        }
        public int GetCellIndex(int x, int y)
        {
            return map[y, x];
        }
        public int GetCellIndex(Point p)
        {
            return map[p.Y, p.X];
        }

        public int IsUsingTexture(Texture2D t)
        {
            if (textures.Contains(t))
                return textures.IndexOf(t);
            return -1;
        }
        public void AddTexture(Texture2D t)
        {
            textures.Add(t);
        }
        public void RemoveTexture(Texture2D t)
        {
            RemoveIndex(textures.IndexOf(t));
            textures.Remove(t);
        }
        private void RemoveIndex(int index)
        {
            if (index == -1)
                return;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (map[y, x] == index)
                        map[y, x] = -1;
                    else if (map[y, x] > index)
                        map[y, x]--;
                }
            }
        }
        private void ReplaceIndex(int oldIndex, int newIndex)
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    if (map[y, x] == oldIndex)
                        map[y, x] = newIndex;
        }

        public void Save(string filename, string[] textureNames)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("[Textures]");
                foreach (string t in textureNames)
                    writer.WriteLine(t);

                writer.WriteLine();

                writer.WriteLine("[Properties]");
                //writer.WriteLine("Alpha = " + Alpha.ToString());

                writer.WriteLine();

                writer.WriteLine("[Layout]");

                for (int y = 0; y < Height; y++)
                {
                    string line = string.Empty;

                    for (int x = 0; x < Width; x++)
                    {
                        line += map[y, x].ToString() + " ";
                    }

                    writer.WriteLine(line);
                }
            }
        }
        public static MapLayer FromFile(ContentManager content, string filename)
        {
            MapLayer MapLayer;

            List<string> textureNames = new List<string>();

            MapLayer = ProcessFile(filename, textureNames);

            MapLayer.LoadTextures(content, textureNames.ToArray());

            return MapLayer;
        }
        public static MapLayer FromFile(string filename, out string[] textureNameArray)
        {
            MapLayer layer;
            List<string> textureNames = new List<string>();

            layer = ProcessFile(filename, textureNames);

            textureNameArray = textureNames.ToArray();

            return layer;
        }
        private static MapLayer ProcessFile(string filename, List<string> textureNames)
        {
            MapLayer MapLayer;
            List<List<int>> tempLayout = new List<List<int>>();
            Dictionary<string, string> properties = new Dictionary<string, string>();

            using (StreamReader reader = new StreamReader(filename))
            {
                bool readingTextures = false;
                bool readingLayout = false;
                bool readingProperties = false;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();

                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.Contains("[Textures]"))
                    {
                        readingTextures = true;
                        readingLayout = false;
                        readingProperties = false;
                    }
                    else if (line.Contains("[Layout]"))
                    {
                        readingTextures = false;
                        readingLayout = true;
                        readingProperties = false;
                    }
                    else if (line.Contains("[Properties]"))
                    {
                        readingTextures = false;
                        readingLayout = false;
                        readingProperties = true;
                    }

                    else if (readingTextures)
                    {
                        textureNames.Add(line);
                    }
                    else if (readingLayout)
                    {
                        List<int> row = new List<int>();

                        string[] cells = line.Split(' ');

                        foreach (string c in cells)
                        {
                            if (!string.IsNullOrEmpty(c))
                                row.Add(int.Parse(c));
                        }

                        tempLayout.Add(row);
                    }
                    else if (readingProperties)
                    {
                        string[] pair = line.Split('=');
                        string key = pair[0].Trim();
                        string value = pair[1].Trim();

                        properties.Add(key, value);
                    }
                }
            }

            int width = tempLayout[0].Count;
            int height = tempLayout.Count;

            MapLayer = new MapLayer(width, height);

            foreach (KeyValuePair<string, string> property in properties)
            {
                switch (property.Key)
                {
                    case "Alpha":
                        throw new Exception("You stopped being able to use alpha in layers you fool!");
                        //MapLayer.Alpha = float.Parse(property.Value);
                        //break;
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    MapLayer.SetCellIndex(x, y, tempLayout[y][x]);
                }
            }
            return MapLayer;
        }
        public void LoadTextures(ContentManager content, params string[] textureNames)
        {
            Texture2D texture;

            foreach (string tName in textureNames)
            {
                texture = content.Load<Texture2D>(tName);
                textures.Add(texture);
            }
        }
        public void fringeDraw(SpriteBatch batch, int x, int y, int sX, int sY)
        {
            Texture2D texture = textures[0];

            batch.Draw(
                texture,
                new Rectangle(
                    x * Map.TileWidth,
                    y * Map.TileHeight,
                    Map.TileWidth,
                    Map.TileHeight),
                new Rectangle(sX, sY, 16, 16),
                Color.White
                );
        }
        public void Draw(SpriteBatch batch, Camera camera, Point min, Point max)
        {
            batch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.TransformMatrix);

            min.X = (int)Math.Max(min.X, 0);
            min.Y = (int)Math.Max(min.Y, 0);
            max.X = (int)Math.Min(max.X, Width);
            max.Y = (int)Math.Min(max.Y, Width);

            for (int x = min.X; x < max.X; x++)
            {
                for (int y = min.Y; y < max.Y; y++)
                {
                    int textureIndex = map[y, x];
                    Texture2D texture = textures[0];

                    Rectangle source = new Rectangle(0,0,16,16);

                    if (textureIndex == -1) // It's empty. Is there anything interesting near it?
                    {
                        //Directions named from where the tile is coming.
                        bool up = false, down = false, left =false, right = false;
                        bool dl = false, dr = false, ul = false, ur = false;

                        //Work out where there IS a tile on this layer
                        if (y > 0 && GetCellIndex(x, y - 1) == 0)
                            down = true;
                        if (y < Height && GetCellIndex(x, y + 1) == 0)
                            up = true;
                        if (x > 0 && GetCellIndex(x - 1, y) == 0)
                            left = true;
                        if (x < Width && GetCellIndex(x + 1, y) == 0)
                            right = true;

                        if (x > 0 && y > 0 && GetCellIndex(x - 1, y - 1) == 0)
                            ul = true;
                        if (x > 0 && y < Height && GetCellIndex(x - 1, y + 1) == 0)
                            dl = true;
                        if (x < Width && y > 0 && GetCellIndex(x + 1, y - 1) == 0)
                            ur = true;
                        if (x < Width && y < Width && GetCellIndex(x + 1, y + 1) == 0)
                            dr = true;

                        //If no tiles need to be drawn around this one, then skip testing for drawing.
                        if (!down && !up && !left && !right && !ul && !dl && !ur && !dr)
                            continue;

                        //Corners
                        if (dl && !up && !left)
                            fringeDraw(batch, x, y, 80, 0);
                        if (dr && !up && !right)
                            fringeDraw(batch, x, y, 48, 0);
                        if (ur && !down && !right)
                            fringeDraw(batch, x, y, 48, 32);
                        if (ul && !down && !left)
                            fringeDraw(batch, x, y, 80, 32);

                        //One side
                        if (down && !left && !up && !right)
                            fringeDraw(batch, x, y, 64, 32);
                        else if (!down && left && !up && !right)
                            fringeDraw(batch, x, y, 80, 16);
                        else if (!down && !left && up && !right)
                            fringeDraw(batch, x, y, 64, 0);
                        else if (!down && !left && !up && right)
                            fringeDraw(batch, x, y, 48, 16);

                        //Two sides (corner)
                        else if (down && left && !up && !right)
                            fringeDraw(batch, x, y, 0, 16);
                        else if (down && !left && !up && right)
                            fringeDraw(batch, x, y, 16, 16);
                        else if (!down && left && up && !right)
                            fringeDraw(batch, x, y, 0, 32);
                        else if (!down && !left && up && right)
                            fringeDraw(batch, x, y, 16, 32);

                        // Two sides (opposite)
                        else if (down && !left && up && !right)
                        {
                            fringeDraw(batch, x, y, 64, 0);
                            fringeDraw(batch, x, y, 64, 32);
                        }
                        else if (!down && left && !up && right)
                        {
                            fringeDraw(batch, x, y, 48, 16);
                            fringeDraw(batch, x, y, 80, 16);
                        }

                        //Three sides
                        else if (down && left && right && !up)
                            fringeDraw(batch, x, y, 32, 16);
                        else if (!down && left && right && up)
                            fringeDraw(batch, x, y, 32, 32);
                        else if (down && left && !right && up)
                            fringeDraw(batch, x, y, 16, 0);
                        else if (down && !left && right && up)
                            fringeDraw(batch, x, y, 32, 0);

                        //Four sides
                        else if (down && left && right && up)
                            fringeDraw(batch, x, y, 64, 16);
                    }
                    else //It's a 0 and hence is actually a tile we need to draw fully
                    batch.Draw(
                        texture,
                        new Rectangle(
                            x * Map.TileWidth,
                            y * Map.TileHeight,
                            Map.TileWidth,
                            Map.TileHeight),
                        source,
                        Color.White
                        );
                }
            }
            batch.End();
        }
    }
}
