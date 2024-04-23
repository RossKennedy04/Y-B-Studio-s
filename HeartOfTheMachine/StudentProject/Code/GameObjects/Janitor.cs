using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProject.Code.GameObjects
{
    internal class Janitor : GameObject
    {
        private int _speed;

        public Janitor() 
        {
            SetSprite("JanitorTemp");
            _speed = 3;
        }

        public override void Update(float deltaTime)
        {
            JanitorMovement();
        }

        public float GetYLocation()
        {
            return GetY();
        }
        public float GetXLocation()
        {
            return GetX();
        }

        public void JanitorMovement()
        {
            if (GameInput.IsKeyHeld("W"))
            {
                SetPosition(GetX(), GetY() - _speed);
                CollisionDetection();
            }
            if (GameInput.IsKeyHeld("A"))
            {
                SetPosition(GetX() - _speed, GetY());
                CollisionDetection();
            }
            if (GameInput.IsKeyHeld("S"))
            {
                SetPosition(GetX(), GetY() + _speed);
                CollisionDetection();
            }
            if (GameInput.IsKeyHeld("D"))
            {
                SetPosition(GetX() + _speed, GetY());
                CollisionDetection();
            }
        }

        public void CollisionDetection()
        {
            if(IsAtScreenEdge() || !IsTouching<Level_1_Bounds>() || IsTouching<WallLength>() || IsTouching<WallCorner>()) 
            {
                RevertPosition();
            }
        }
    }
}
