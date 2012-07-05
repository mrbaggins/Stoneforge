using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace StoneEngine
{
    public class Camera
    {
        public Vector2 position = Vector2.Zero;

        public Matrix TransformMatrix
        {
            get { return Matrix.CreateTranslation(new Vector3(-position, 0f)); }
        }

        /**********
         * Change this to lock to the selection cursor, as the camera will not be tracking 
         * a sprite, but instead the current viewport of the dwarfs
         * 
         *  public void LockToTarget(AnimatedSprite sprite, int screenWidth, int screenHeight)
         *  {
         *       position.X = sprite.Position.X + (sprite.CurrentAnimation.CurrectRect.Width / 2) - (screenWidth / 2);
         *       position.Y = sprite.Position.Y + (sprite.CurrentAnimation.CurrectRect.Height / 2) - (screenHeight / 2);
         *   }
         ***************/

        public void ClampToArea(int width, int height)
        {
            if (position.X > width)
                position.X = width;
            if (position.Y > height)
                position.Y = height;

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
        }
    }
}
