using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace StoneEngine
{
    public class FrameAnimation : ICloneable
    {
        Rectangle[] frames;
        int currentFrame = 0;

        float frameLength = 0.15f;
        float timer = 0;

        public int FramesPerSecond
        {
            get { return (int)(1f / frameLength); }
            set { frameLength = (float)Math.Max(1f / (float)value, 0.001f); }
        }
        public Rectangle CurrentRect
        {
            get { return frames[currentFrame]; }
        }
        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = (int)MathHelper.Clamp(value, 0, frames.Length - 1); }
        }

        public FrameAnimation()
        {
            //Purely for the Clone() method to use.
        }
        public FrameAnimation(int numberOfFrames, int width, int height, int xOffset, int yOffset)
        {
            frames = new Rectangle[numberOfFrames];

            for (int i = 0; i < numberOfFrames; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = width;
                rect.Height = height;
                rect.X = xOffset + (i * width);     //All animations are done on a single line in the spritesheet.
                rect.Y = yOffset;                   //Separate lines are for separate styles/pieces of animation.

                frames[i] = rect;
            }
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= frameLength)
            {
                timer = 0.0f;

                currentFrame++;
                if (currentFrame >= frames.Length)
                    currentFrame = 0;
            }
        }

        public object Clone()
        {
            FrameAnimation anim = new FrameAnimation();

            anim.frameLength = frameLength;
            anim.frames = frames;

            return anim;
        }
    }
}
