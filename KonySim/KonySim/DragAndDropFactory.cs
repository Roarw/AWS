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
        int deltaX;
        int deltaY;
        int mouseX;
        int mouseY;
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
            mouseX = GameWorld.MouseX;
            mouseY = GameWorld.MouseY;

            if (isPressed)
            {
                transform.Position = new Vector2((float)mouseX  - (float)deltaX, (float)mouseY- (float)deltaY);
            }
        }

        public void MouseEnter()
        {
            
        }

        public void MouseExit()
        {
        }

        public void MousePressed()
        {
            deltaX =  mouseX - (int)transform.Position.X;
            deltaY = mouseY - (int)transform.Position.Y;
            
            isPressed = true;
        }

        public void MouseReleased()
        {
            isPressed = false;
        }

        
    }
}
