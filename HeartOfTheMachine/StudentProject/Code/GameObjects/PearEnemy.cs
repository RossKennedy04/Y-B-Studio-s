using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProject.Code.GameObjects
{
    internal class PearEnemy : GameObject
    {

        private bool _targetFound;
        private GameObject _target;
        private int _direction;
        public PearEnemy() 
        {
            SetSprite("PearEnemy");
            _targetFound = false;
            _target = null;
            _direction = 1;
        }


        public override void Update(float deltaTime)
        {
            Movement();
        }

        public void Movement()
        {
            switch (_direction)
            {
                case 1: // RIGHT
                    SetPosition(GetX() + 2, GetY());
                    CollisionDetection();
                    break;
                case 2: // UP 
                    SetPosition(GetX(), GetY() - 2);
                    CollisionDetection();
                    break;
                case 3: // LEFT
                    SetPosition(GetX() - 2, GetY());
                    CollisionDetection();
                    break;
                case 4: // DOWN
                    SetPosition(GetX(), GetY() + 2);
                    CollisionDetection();
                    break;
            }
        }

        public void ChangeDirection()
        {
            if (_direction < 4)
            {
                _direction++;
            }
            else
            {
                _direction = 1;
            }
        }

        public void CollisionDetection()
        {
            if (IsAtScreenEdge() || !IsTouching<Level_1_Bounds>() || IsTouching<WallLength>() || IsTouching<WallCorner>())
            {
                RevertPosition();
                ChangeDirection();
            }
        }
    }
}
