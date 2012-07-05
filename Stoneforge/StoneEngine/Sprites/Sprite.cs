using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace StoneEngine
{
    public class Sprite
    {
        protected Texture2D texture;
        int width = 32;
        int height = 32;
        float radius = 8.0f;
        float speed = 2.0f;

        public Vector2 Position = Vector2.Zero;
        public Vector2 OriginOffset = Vector2.Zero;

        public Vector2 Origin
        {
            get { return Position + OriginOffset; }
        }
        public Vector2 Center
        {
            get { return Position + new Vector2(Width / 2, Height / 2); }
        }
        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Width, Height); }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = (float)Math.Max(value, 0.0f); }
        }
        public float CollisionRadius
        {
            get { return radius; }
            set { radius = (float)Math.Max(value, 0); }
        }
        public int Width
        {
            get { return width; }
            set { width = (int)Math.Max(0, value); }
        }
        public int Height
        {
            get { return height; }
            set { height = (int)Math.Max(0, value); }
        }

        public static bool AreColliding(Sprite a, Sprite b)
        {
            Vector2 d = b.Origin - a.Origin;
            return d.Length() < (b.CollisionRadius + a.CollisionRadius);
        }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public void ClampToArea(int width, int height)
        {
            if (Position.X < 0)
                Position.X = 0;
            if (Position.Y < 0)
                Position.Y = 0;
            if (Position.X > width - Bounds.Width)
                Position.X = width - Bounds.Width;
            if (Position.Y > height - Bounds.Height)
                Position.Y = height - Bounds.Height;
        }

        //I think I need this so that the descended class can use it for the animation.
        public virtual void Update(GameTime gameTime)
        {
            return;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White);
        }
    }
}
