using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace AWS
{
    class DragAndDropFactory : ButtonFactory, ILoadContent, IUpdate
    {
        GameObject go;
        int mouseX;
        int mouseY;
        int mouse1X;
        int mouse1Y;
        bool isPressed;
        SpriteRender spriteRender;
        Transform transform;

        public DragAndDropFactory(GameObject go)
        {
            this.go = go;
        }

        public void LoadContent(ContentManager content)
        {
            this.spriteRender = (SpriteRender)go.GetComponent("SpriteRender");
            this.transform = (Transform)go.GetComponent("Transform");
            isPressed = false;
        }

        public void Update(float deltaTime)
        {
            mouseX = Mouse.GetState().Position.X;
            mouseY = Mouse.GetState().Position.Y;

            if (isPressed)
            {
                transform.Position = new Vector2(mouseX - mouse1X/1.3f, mouseY - mouse1Y/1.3f);
            }
        }

        public void MouseEnter()
        {
            
        }

        public void MouseExit()
        {
            isPressed = false;
        }

        public void MousePressed()
        {
            mouse1X = Mouse.GetState().Position.X;
            mouse1Y = Mouse.GetState().Position.Y;
            System.Diagnostics.Debug.WriteLine(mouse1X + " " + mouse1Y);
            isPressed = true;
        }

        public void MouseReleased()
        {
            isPressed = false;
        }

        
    }
}
