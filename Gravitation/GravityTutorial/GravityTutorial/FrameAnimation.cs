using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GravityTutorial
{
    class FrameAnimation : ICloneable
    {

        // der erste Frame der Animation
        private Rectangle rectInitialFrame;


        // Anzahl der Frames in der Animation
        private int iFrameCount = 1;


        // Welcher frame gerade angezeigt wird (geht von 0 bis iFrameCount-1)
        private int iCurrentFrame = 0;


        // Zeit die ein frame angezeigt wird
        private float fFrameLength = 0.2f;


        // Zeit die vergangen ist seit der letzten animation
        private float fFrameTimer = 0.0f;


        // anzahl wie oft die Animation abgespielt wurde
        private int iPlayCount = 0;


        // die animation die als nächstes abgespielt werden soll
        private string sNextAnimation = null;

 
        // Anzahl der Animationsframes
        public int FrameCount
        {
            get { return iFrameCount; }
            set { iFrameCount = value; }
        }

       
        // Zeit die ein Frame angezeigt wird (Sekunden)
        public float FrameLength
        {
            get { return fFrameLength; }
            set { fFrameLength = value; }
        }

       
        // der derzeit gezeigte Frame
        public int CurrentFrame
        {
            get { return iCurrentFrame; }
            set { iCurrentFrame = (int)MathHelper.Clamp(value, 0, iFrameCount - 1); }
        }

        public int FrameWidth
        {
            get { return rectInitialFrame.Width; }
        }

        public int FrameHeight
        {
            get { return rectInitialFrame.Height; }
        }

 
        // Rechteck des angezeigten animations Frame
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(
                    rectInitialFrame.X + (rectInitialFrame.Width * iCurrentFrame),
                    rectInitialFrame.Y, rectInitialFrame.Width, rectInitialFrame.Height);
            }
        }

        public int PlayCount
        {
            get { return iPlayCount; }
            set { iPlayCount = value; }
        }

        public string NextAnimation
        {
            get { return sNextAnimation; }
            set { sNextAnimation = value; }
        }

        public FrameAnimation(Rectangle FirstFrame, int Frames)
        {
            rectInitialFrame = FirstFrame;
            iFrameCount = Frames;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int Frames)
        {
            rectInitialFrame = new Rectangle(X, Y, Width, Height);
            iFrameCount = Frames;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int Frames, float FrameLength)
        {
            rectInitialFrame = new Rectangle(X, Y, Width, Height);
            iFrameCount = Frames;
            fFrameLength = FrameLength;
        }

        public FrameAnimation(int X, int Y,
            int Width, int Height, int Frames,
            float FrameLength, string strNextAnimation)
        {
            rectInitialFrame = new Rectangle(X, Y, Width, Height);
            iFrameCount = Frames;
            fFrameLength = FrameLength;
            sNextAnimation = strNextAnimation;
        }

        public void Update(GameTime gameTime)
        {
            fFrameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (fFrameTimer > fFrameLength)
            {
                fFrameTimer = 0.0f;
                iCurrentFrame = (iCurrentFrame + 1) % iFrameCount;
                if (iCurrentFrame == 0)
                    iPlayCount = (int)MathHelper.Min(iPlayCount + 1, int.MaxValue);
            }
        }

        object ICloneable.Clone()
        {
            return new FrameAnimation(this.rectInitialFrame.X, this.rectInitialFrame.Y,
                                      this.rectInitialFrame.Width, this.rectInitialFrame.Height,
                                      this.iFrameCount, this.fFrameLength, sNextAnimation);
        }
    }
}
