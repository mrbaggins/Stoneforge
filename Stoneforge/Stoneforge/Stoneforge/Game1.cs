using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using StoneEngine;

namespace Stoneforge
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map map = new Map();
        Camera camera = new Camera();

        AnimatedSprite testSprite;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            FrameAnimation down = new FrameAnimation(3, 32, 32, 0, 0);
            down.FramesPerSecond = 8;
            testSprite.Animations.Add("Down", down);
            FrameAnimation left = new FrameAnimation(3, 32, 32, 0, 32);
            left.FramesPerSecond = 8;
            testSprite.Animations.Add("Left", left); 
            FrameAnimation up = new FrameAnimation(3, 32, 32, 0, 96);
            up.FramesPerSecond = 8;
            testSprite.Animations.Add("Up", up); 
            FrameAnimation right = new FrameAnimation(3, 32, 32, 0, 64);
            right.FramesPerSecond = 8;
            testSprite.Animations.Add("Right", right);


        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Add the layer file to the map
            map.Layers.Add(MapLayer.FromFile(Content, "Content/Map/Base.layer"));
            map.Layers.Add(MapLayer.FromFile(Content, "Content/Map/Grass.layer"));

            //Add the ddwarf sheet to a folder called sprites in content.
            testSprite = new AnimatedSprite(Content.Load<Texture2D>("Sprites/Dwarf"));
            testSprite.OriginOffset = new Vector2(16, 31);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Vector2 motion = Vector2.Zero;

            /**
             * To use XBOX, uncomment these two lines, and comment out the keyboardstate, keystate and normalize lines
             */

            //GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            //motion = new Vector2(gamePadState.ThumbSticks.Left.X, -gamePadState.ThumbSticks.Left.Y);

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up))
                motion.Y--;
            if (keyState.IsKeyDown(Keys.Down))
                motion.Y++;
            if (keyState.IsKeyDown(Keys.Left))
                motion.X--;
            if (keyState.IsKeyDown(Keys.Right))
                motion.X++;

            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                //motion = CheckCollisionForMotion(motion, testSprite); // This one is used to slow down/speed up sprites based on the tile they're on.
                testSprite.Position += motion *= testSprite.Speed;
                updateSpriteAnimation(motion);
                testSprite.IsAnimating = true;
                //CheckForUnwalkableTiles(testSprite); // This one is used to stop sprites walking through walls.
            }
            else
                testSprite.IsAnimating = false;

            testSprite.ClampToArea(map.WidthInPixels, map.HeightInPixels);
            testSprite.Update(gameTime);

            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;

            camera.ClampToArea(map.WidthInPixels - screenWidth, map.HeightInPixels - screenHeight);

            base.Update(gameTime);
        }

        private void updateSpriteAnimation(Vector2 motion)
        {
            float motionAngle = (float)Math.Atan2(motion.Y, motion.X);
            
            if (motionAngle > -MathHelper.PiOver4 && motionAngle <MathHelper.PiOver4)
                testSprite.CurrentAnimationName = "Right";
            else if (motionAngle >= MathHelper.PiOver4 && motionAngle <= 3f* MathHelper.PiOver4)
                testSprite.CurrentAnimationName = "Down";
            else if (motionAngle <= -MathHelper.PiOver4 && motionAngle >= -3f * MathHelper.PiOver4)
                testSprite.CurrentAnimationName = "Up";
            else
                testSprite.CurrentAnimationName = "Left";
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SteelBlue);

            map.Draw(spriteBatch, camera);
            
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.TransformMatrix);
            testSprite.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
