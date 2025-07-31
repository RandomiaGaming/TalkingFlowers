using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkingFlowers
{
    public static class PhysicsEngine
    {
        public static List<PhysicsObject> PhysicsObjects = new List<PhysicsObject>();
        private static PhysicsObject HeldObject = null;
        private static bool HoldingObject = false;
        public static void Update()
        {
            for (int i = 0; i < PhysicsObjects.Count; i++)
            {
                Update(PhysicsObjects[i]);
            }
        }
        public static void Grab(int mouseX, int mouseY)
        {

        }
        public static void Drop()
        {

        }
        public sealed class PhysicsObject
        {
            public bool Held;
            public int PositionX;
            public int PositionY;
            public int Width;
            public int Height;
            public float VelocityX;
            public float VelocityY;
            public float SubPixelX;
            public float SubPixelY;
        }
    }
}
