using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{
    class MobileSprite
    {
  
        // Spriteanimations objekt welches die grafischen und animations Daten enthält
        SpriteAnimation asSprite;


        // 2 integers zur kollisionserkennung mithilfe der "bounding-box"
        int iCollisionBufferX = 0;
        int iCollisionBufferY = 0;


        // Status des Sprite feststellen.
        bool bActive = true;


        // Wenn true wird der Sprite auch gezeichnet
        bool bVisible = true;

        public SpriteAnimation Sprite
        {
            get { return asSprite; }
        }

        public Vector2 Position
        {
            get { return asSprite.Position; }
            set { asSprite.Position = value; }
        }


        public int HorizontalCollisionBuffer
        {
            get { return iCollisionBufferX; }
            set { iCollisionBufferX = value; }
        }

        public int VerticalCollisionBuffer
        {
            get { return iCollisionBufferY; }
            set { iCollisionBufferY = value; }
        }


        public bool IsVisible
        {
            get { return bVisible; }
            set { bVisible = value; }
        }

        public bool IsActive
        {
            get { return bActive; }
            set { bActive = value; }
        }


        public Rectangle BoundingBox
        {
            get { return asSprite.BoundingBox; }
        }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    asSprite.BoundingBox.X + iCollisionBufferX,
                    asSprite.BoundingBox.Y + iCollisionBufferY,
                    asSprite.Width - (2 * iCollisionBufferX),
                    asSprite.Height - (2 * iCollisionBufferY));
            }
        }

        public MobileSprite(Texture2D texture)
        {
            asSprite = new SpriteAnimation(texture);
        }

        public void Update(GameTime gameTime)
        {
           
            if (bActive)
                asSprite.Update(gameTime);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        { 
            if (bVisible)
            {
                asSprite.Draw(spriteBatch, 0, 0);
            }
        }
    }
}
