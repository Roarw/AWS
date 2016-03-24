using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    class UIBuilders
    {
        //Creating an image.
        public static GameObject CreateImage(string sprite, Vector2 position)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(position));
            go.AddComponent(new SpriteRender(sprite, 0.1f));
            return go;
        }

        //Creating an image with offset.
        public static GameObject CreateImageWOffset(string sprite, Vector2 position, float depth, int yTopOffset, int yBottomOffset)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(position));
            go.AddComponent(new SpriteRender(sprite, depth, yTopOffset, yBottomOffset));
            return go;
        }

        //Creating an image with offset.
        public static GameObject CreateImageWOffset(string sprite, Vector2 position, float depth, int yTopOffset, int yBottomOffset, int xRightOffset, int xLeftOffset)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(position));
            go.AddComponent(new SpriteRender(sprite, depth, yTopOffset, yBottomOffset, xRightOffset, xLeftOffset));
            return go;
        }
    }
}
