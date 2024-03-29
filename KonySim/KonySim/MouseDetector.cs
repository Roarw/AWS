﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace KonySim
{
    internal class MouseDetector : Component, ILoadContent, IUpdate
    {
        private Transform transform;
        private SpriteRender spriteRender;

        private int mouseX;
        private int mouseY;
        private ButtonState leftButtonState;
        private bool mouseHover;
        private bool mousePressed;
        private bool mousePressedOnMe;

        private Rectangle CollisionBox
        {
            get
            {
                return
                    new Rectangle((int)transform.Position.X, (int)transform.Position.Y,
                    spriteRender.Rectangle.Width, spriteRender.Rectangle.Height);
            }
        }

        public MouseDetector() : base()
        {
            mouseHover = false;
            mousePressed = false;
        }

        public void LoadContent(ContentManager content)
        {
            this.transform = GameObject.GetComponent<Transform>();
            this.spriteRender = GameObject.GetComponent<SpriteRender>();
        }

        public void Update(float deltaTime)
        {
            mouseX = GameWorld.Instance.MouseX;
            mouseY = GameWorld.Instance.MouseY;
            leftButtonState = Mouse.GetState().LeftButton;

            CheckMouseCollision();
        }

        private void CheckMouseCollision()
        {
            if (!mousePressed)
            {
                if (leftButtonState == ButtonState.Pressed)
                {
                    mousePressed = true;

                    if (CollidingWithMouse())
                    {
                        mousePressedOnMe = true;
                        GameObject.MousePressed();
                    }
                }
            }
            else
            {
                if (leftButtonState == ButtonState.Released)
                {
                    mousePressed = false;

                    if (mousePressedOnMe)
                    {
                        mousePressedOnMe = false;
                        GameObject.MouseReleased();
                    }
                }
            }

            if (CollidingWithMouse())
            {
                if (!mouseHover)
                {
                    mouseHover = true;
                    GameObject.MouseEnter();
                }
            }
            else
            {
                if (mouseHover)
                {
                    mouseHover = false;
                    GameObject.MouseExit();
                }
            }
        }

        private bool CollidingWithMouse()
        {
            return (CollisionBox.X < mouseX && CollisionBox.X + CollisionBox.Width > mouseX &&
                CollisionBox.Y < mouseY && CollisionBox.Y + CollisionBox.Height > mouseY);
        }
    }
}
