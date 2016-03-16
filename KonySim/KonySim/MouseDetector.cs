using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace AWS
{
    class MouseDetector : Component, ILoadContent, IUpdate
    {
        Transform transform;
        SpriteRender spriteRender;

        int mouseX;
        int mouseY;
        ButtonState leftButtonState;
        bool mouseHover;
        bool mousePressed;

        private Rectangle CollisionBox
        {
            get
            {
                return
                    new Rectangle((int)transform.Position.X, (int)transform.Position.Y,
                    spriteRender.Rectangle.Width, spriteRender.Rectangle.Height);
            }
        }

        public MouseDetector(GameObject gameObject) : base(gameObject)
        {
            mouseHover = false;
            mousePressed = false;
        }

        public void LoadContent(ContentManager content)
        {
            this.transform = (Transform)gameObject.GetComponent("Transform");
            this.spriteRender = (SpriteRender)gameObject.GetComponent("SpriteRender");
        }

        public void Update(float deltaTime)
        {
            mouseX = GameWorld.MouseX;
            mouseY = GameWorld.MouseY;
            leftButtonState = Mouse.GetState().LeftButton;
            
            CheckMouseCollision();
        }

        private void CheckMouseCollision()
        {
            if (CollidingWithMouse())
            {
                if (!mouseHover)
                {
                    mouseHover = true;
                    gameObject.MouseEnter();
                }

                if (leftButtonState == ButtonState.Pressed && !mousePressed)
                {
                    mousePressed = true;
                    gameObject.MousePressed();
                }
                
            }
            else if (mouseHover)
            {
                mouseHover = false;
                gameObject.MouseExit();
            }

            if (leftButtonState == ButtonState.Released && mousePressed)
            {
                mousePressed = false;
                gameObject.MouseReleased();
            }
        }

        private bool CollidingWithMouse()
        {
            return (CollisionBox.X < mouseX && CollisionBox.X + CollisionBox.Width > mouseX &&
                CollisionBox.Y < mouseY && CollisionBox.Y + CollisionBox.Height > mouseY);
        }
    }
}
