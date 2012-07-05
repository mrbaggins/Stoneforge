using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace StoneEngine
{
    public class AnimatedSprite : Sprite
    {
        public Dictionary<string, FrameAnimation> Animations = new Dictionary<string, FrameAnimation>();

        string currentAnimation = null;
        bool animating = true;

        public bool IsAnimating
        {
            get { return animating; }
            set { animating = value; }
        }
        public FrameAnimation CurrentAnimation
        {
            get
            {
                if (!string.IsNullOrEmpty(currentAnimation))
                    return Animations[currentAnimation];
                else
                    return null;
            }
        }
        public string CurrentAnimationName
        {
            get { return currentAnimation; }
            set
            {
                if (Animations.ContainsKey(value))
                    currentAnimation = value;
            }
        }


        public AnimatedSprite(Texture2D texture) : base(texture)
        {
            //Nicks file has "currentAnimation = "Down" here to avoid a null pointer exception.
        }

        /**
         *  I think this is purely to set the NPCS in nicks to all be walking. Seems largely useless.
         * 
         *  If not animating, return
         *  
         *  make a temp animation set to the current animation
         *  
         *  If there's an animation in teh dictionary
         *      get an array of all the animation names
         *      set currentanimationstring to the first one
         *      set animation = the actual animation of the string.
         *
         *  else, return
         *      
         */
        public override void Update(GameTime gameTime)
        {
            if (!IsAnimating)
            {
                if (CurrentAnimation != null && CurrentAnimation.CurrentFrame !=1)
                {
                    CurrentAnimation.CurrentFrame = 1;
                    CurrentAnimation.Update(gameTime);
                }
                return;
            }

            FrameAnimation animation = CurrentAnimation;
            
            if (animation == null)
            {
                if (Animations.Count > 0)
                {
                    string[] keys = new String[Animations.Count];
                    Animations.Keys.CopyTo(keys, 0);

                    currentAnimation = keys[0];

                    animation = CurrentAnimation;
                }
                else
                    return;
            }

            animation.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            FrameAnimation animation = CurrentAnimation;

            if (animation != null)
                spriteBatch.Draw(
                    texture,
                    Position,
                    animation.CurrentRect,
                    Color.White);
        }
    }
}
