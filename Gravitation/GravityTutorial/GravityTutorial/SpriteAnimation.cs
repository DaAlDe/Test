using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GravityTutorial
{
    class SpriteAnimation
    {
        // die Textur die, die Sprites enthält
        Texture2D t2dTexture;

        // Werden animationen abgespielt --> true
        bool bAnimating = true;


        Color colorTint = Color.White;

        // Position der Sprites
        Vector2 v2Position = new Vector2(0, 0);
        Vector2 v2LastPosition = new Vector2(0, 0);

        // "Dictionary" welches alle Frameanimationen des Sprites enthält
        Dictionary<string, FrameAnimation> faAnimations = new Dictionary<string, FrameAnimation>();

        // Welche Frameanimation rennt gerade?
        string sCurrentAnimation = null;

        // keine Rotation des Sprites
        float fRotation = 0f;

        // Berechnung der Mitte des Sprites
        Vector2 v2Center;

        // Berechnung Breite und Höhe des Sprites
        int iWidth;
        int iHeight;



        // Position des Sprites (linke obere ecke)
        public Vector2 Position
        {
            get { return v2Position; }
            set
            {
                v2LastPosition = v2Position;
                v2Position = value;
            }
        }

        // X - Position des Sprites (linke obere ecke)
        public int X
        {
            get { return (int)v2Position.X; }
            set
            {
                v2LastPosition.X = v2Position.X;
                v2Position.X = value;
            }
        }

        // Y - Position des Sprites (linke obere ecke)
        public int Y
        {
            get { return (int)v2Position.Y; }
            set
            {
                v2LastPosition.Y = v2Position.Y;
                v2Position.Y = value;
            }
        }


        // Breite der animations frames (in Pixel)
        public int Width
        {
            get { return iWidth; }
        }

        // Höhe der animations frames (in Pixel)
        public int Height
        {
            get { return iHeight; }
        }


        // rotation des Frames wenn er gezeichnet wird --> 0
        public float Rotation
        {
            get { return fRotation; }
            set { fRotation = value; }
        }

        // Bildschirm Koordinaten der Box die den Sprite umgibt
        public Rectangle BoundingBox
        {
            get { return new Rectangle(X, Y, iWidth, iHeight); }
        }

 
        // Die Textur die zum sprite gehört
        public Texture2D Texture
        {
            get { return t2dTexture; }
        }


        // Falls der sprite beim Zeichnen gefärbt werden soll
        public Color Tint
        {
            get { return colorTint; }
            set { colorTint = value; }
        }


        // True wenn Animationen abgespielt werden. Sollte es false sein wird der Sprite nicht gezeichnet
        // Der sprite benötigt min. eine Frameanimation
        public bool IsAnimating
        {
            get { return bAnimating; }
            set { bAnimating = value; }
        }


        // Objekt der gerade abgespielten Animation
        public FrameAnimation CurrentFrameAnimation
        {
            get
            {
                if (!string.IsNullOrEmpty(sCurrentAnimation))
                    return faAnimations[sCurrentAnimation];
                else
                    return null;
            }
        }

        ///
        /// The string name of the currently playing animaton.  Setting the animation
        /// resets the CurrentFrame and PlayCount properties to zero.
        /// String name der aktuellen animation
        public string CurrentAnimation
        {
            get { return sCurrentAnimation; }
            set
            {
                if (faAnimations.ContainsKey(value))
                {
                    sCurrentAnimation = value;
                    faAnimations[sCurrentAnimation].CurrentFrame = 0;
                    faAnimations[sCurrentAnimation].PlayCount = 0;
                }
            }
        }

        public SpriteAnimation(Texture2D Texture)
        {
            t2dTexture = Texture;
        }

        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength)
        {
            faAnimations.Add(Name, new FrameAnimation(X, Y, Width, Height, Frames, FrameLength));
            iWidth = Width;
            iHeight = Height;
            v2Center = new Vector2(iWidth / 2, iHeight / 2);
        }

        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames,
           float FrameLength, string NextAnimation)
        {
            faAnimations.Add(Name, new FrameAnimation(X, Y, Width, Height, Frames, FrameLength, NextAnimation));
            iWidth = Width;
            iHeight = Height;
            v2Center = new Vector2(iWidth / 2, iHeight / 2);
        }

        public FrameAnimation GetAnimationByName(string Name)
        {
            if (faAnimations.ContainsKey(Name))
            {
                return faAnimations[Name];
            }
            else
            {
                return null;
            }
        }

        public void MoveBy(int x, int y)
        {
            v2LastPosition = v2Position;
            v2Position.X += x;
            v2Position.Y += y;
        }

        public void Update(GameTime gameTime)
        {
            // falls keine animation --> nichts tun
            if (bAnimating)
            {
                // falls keine momentan aktive animation da ist
                if (CurrentFrameAnimation == null)
                {
                    // Sichergehen dass dem sprite eine animation zugeordnet ist
                    if (faAnimations.Count > 0)
                    {
                        // aktive animation auf die erste animation setzen die dem sprite zugeordnet ist
                        string[] sKeys = new string[faAnimations.Count];
                        faAnimations.Keys.CopyTo(sKeys, 0);
                        CurrentAnimation = sKeys[0];
                    }
                    else
                    {
                        return;
                    }
                }

                // update Methode ausführen
                CurrentFrameAnimation.Update(gameTime);

                // überprüfen ob es eine "nachfolger" animation für die aktuelle animation gibt
                if (!String.IsNullOrEmpty(CurrentFrameAnimation.NextAnimation))
                {
                   
                    // falls ja --> prüfen ob die animation einmal komplett durchgelaufen ist
                    if (CurrentFrameAnimation.PlayCount > 0)
                    {
                        // falls ja --> nächste animation starten
                        CurrentAnimation = CurrentFrameAnimation.NextAnimation;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, int XOffset, int YOffset)
        {
            if (bAnimating)
                spriteBatch.Draw(t2dTexture, (v2Position + new Vector2(XOffset, YOffset) + v2Center),
                                CurrentFrameAnimation.FrameRectangle, colorTint,
                                fRotation, v2Center, 1f, SpriteEffects.None, 0);
        }
    }
}
