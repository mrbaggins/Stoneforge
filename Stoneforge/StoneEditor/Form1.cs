using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using StoneEngine;

namespace StoneEditor
{
    using Image = System.Drawing.Image;


    public partial class Form1: Form
    {
        string[] imgExtensions = new string[] { ".jpg", ".png", ".tga" };

        SpriteBatch batch;
        Texture2D selectedTexture;

        Camera camera = new Camera();

        Map map = new Map();
        MapLayer currentLayer;

        int fillCounter = 1500;
        int cellX, cellY;

        Dictionary<string, Texture2D> textureDict = new Dictionary<string,Texture2D>(); //Should this be moved to the layers so each one has it's own textures?
        Dictionary<string,MapLayer> layerDict = new Dictionary<string,MapLayer>();

        public GraphicsDevice GraphicsDevice
        {
            get { return mapDisplay.GraphicsDevice; }
        }

        public Form1()
        {
            InitializeComponent();

            mapDisplay.OnInitialize += new EventHandler(mapDisplay_OnInitialize);
            mapDisplay.OnDraw += new EventHandler(mapDisplay_OnDraw);

            Application.Idle += delegate { mapDisplay.Invalidate(); };

            opendialog.Filter = "Layer file|*.layer";
            saveDialog.Filter = "Layer file|*.layer";

            Mouse.WindowHandle = mapDisplay.Handle;

        }

        void mapDisplay_OnInitialize(object sender, EventArgs e)
        {
            batch = new SpriteBatch(GraphicsDevice);

            using (StreamReader reader = new StreamReader("Content/Selection.png"))
            {
                selectedTexture = Texture2D.FromStream(GraphicsDevice, reader.BaseStream);
            }
        }
        void mapDisplay_OnDraw(object sender, EventArgs e)
        {
            Logic();
            Render();
        }

        private void Logic()
        {
            if (currentLayer != null)
            {
                int mx = Mouse.GetState().X;
                int my = Mouse.GetState().Y;

                if (mx >= 0 && mx < mapDisplay.Width && my >= 0 && my < mapDisplay.Height)
                {
                    cellX = mx / Map.TileWidth;
                    cellY = my / Map.TileHeight;

                    //Offset cellX/Y based on position of camera here

                    cellX = (int)MathHelper.Clamp(cellX, 0, map.Width - 1);
                    cellY = (int)MathHelper.Clamp(cellY, 0, map.Height - 1);
                }
                else
                    cellX = cellY = -1;
            }
        }

        private void Render()
        {
            GraphicsDevice.Clear(Color.SlateGray);

            Point min = Map.ConvertPositionToCell(camera.position);
            Point max = Map.ConvertPositionToCell(camera.position + new Vector2(
                    mapDisplay.GraphicsDevice.Viewport.Width + Map.TileWidth - 1,
                    mapDisplay.GraphicsDevice.Viewport.Height + Map.TileHeight - 1));


            foreach (MapLayer layer in map.Layers)
            {
                layer.Draw(batch, camera, min, max);

                batch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
                for (int y = 0; y < layer.Height; y++)
                {
                    for (int x = 0; x < layer.Width; x++)
                    {
                        if (layer.GetCellIndex(x, y) == -1 && showEmptyCheckbox.Checked == true)
                        {
                            batch.Draw(
                            selectedTexture,
                            new Rectangle(
                                x * Map.TileWidth - (int)camera.position.X,
                                y * Map.TileHeight - (int)camera.position.Y,
                                Map.TileWidth,
                                Map.TileHeight),
                            Color.White
                            );
                        }
                    }
                }
                batch.End();
            }
            if (currentLayer != null)
            {
                if (cellX != -1 && cellY != -1)
                {
                    batch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
                    batch.Draw(
                            selectedTexture,
                            new Rectangle(
                                cellX * Map.TileWidth - (int)camera.position.X,
                                cellY * Map.TileHeight - (int)camera.position.Y,
                                Map.TileWidth,
                                Map.TileHeight),
                            Color.Red
                            );
                    batch.End();
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            opendialog.Filter = "Layer File|*.layer"; //To be changed to opening a map, not layers. Most of this will be done by taking this to Map class.

            if (opendialog.ShowDialog() == DialogResult.OK)
            {
                string filename = opendialog.FileName;

                string[] textureNames;

                MapLayer layer = MapLayer.FromFile(filename, out textureNames);

                layerDict.Add(Path.GetFileName(filename), layer);
                map.Layers.Add(layer);
                layerListbox.Items.Add(Path.GetFileName(filename));

                foreach (string tName in textureNames)
                {
                    if (textureDict.ContainsKey(tName))
                    {
                        layer.AddTexture(textureDict[tName]);
                        continue;
                    }

                    string fullPath = contentPathTextbox.Text + "/" + tName;

                    foreach (string ext in imgExtensions)
                    {
                        if (File.Exists(fullPath + ext))
                        {
                            fullPath += ext;
                            break;
                        }
                    }

                    Texture2D tex;
                    using (StreamReader reader = new StreamReader(fullPath))
                    {
                        tex = Texture2D.FromStream(GraphicsDevice, reader.BaseStream);
                    }
                    textureDict.Add(tName, tex);
                    layer.AddTexture(tex);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check or make a map file.
            //For each layer in the map
                //Work out texture usage (Nicks)
                //Save it (MapLayer.save)
                
            
                    
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            return;
        }

        private void layerListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (layerListbox.SelectedItem != null)
            {
                currentLayer = layerDict[layerListbox.SelectedItem as string];
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            return;
        }

        
    }
}
