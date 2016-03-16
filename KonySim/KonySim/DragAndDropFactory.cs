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
        }

        public void Update(float deltaTime)
        {
            mouseX = Mouse.GetState().Position.X;
            mouseY = Mouse.GetState().Position.Y;
        }

        public void MouseEnter()
        {
            
        }

        public void MouseExit()
        {
            
        }

        public void MousePressed()
        {
            
        }

        public void MouseReleased()
        {
            transform.Position = new Vector2(mouseX, mouseY);
        }

        
    }
}
