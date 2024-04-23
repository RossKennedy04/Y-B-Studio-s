using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProject.Code.GameObjects
{
    internal class OrangeEnemy : GameObject
    {
        private bool _targetFound;
        private GameObject _target;
        private int _direction;
        private int _speed;
        private int _waitingToMove;
        Grid myGrid = new Grid();

        public OrangeEnemy() 
        {
            SetSprite("OrangeEnemy");
            _targetFound = false;
            _target = null;
            _direction = 1;
            _speed = 2;
            _waitingToMove = 0;
        }

        public override void Update(float deltaTime)
        {
            /**if (_targetFound == false)
            {
                GameObject[] janitorList = GetScreen().GetAllObjectsOfType<Janitor>();
                Janitor janitor = (Janitor)janitorList[0];
                if (janitor != null)
                {
                    _targetFound = true;
                    _target = janitor;
                    ChangeDirection();
                }
            }**/
            Movement();
        }
        public void Movement()
        { 
            // CHECK IF I WANT TO MOVE RIGHT RANDOMLY
            if (Core.GetRandomNumber(50) == 0)
            {
                int randomMove = _direction;
                while (randomMove == _direction)
                {
                    randomMove = (Core.GetRandomNumber(3) + 1);
                }
                _waitingToMove = randomMove;
            }
            if (_waitingToMove > 0)
            {
                if (_waitingToMove == 1)
                {
                    if (myGrid.GetGridLocked(myGrid.FindGridX((int)GetX() + 1), myGrid.FindGridY((int)GetY())) == false)
                    {
                        _direction = _waitingToMove;
                        _waitingToMove = 0;
                    }
                }
                else  if (_waitingToMove == 2) 
                {
                    if (myGrid.GetGridLocked(myGrid.FindGridX((int)GetX()), myGrid.FindGridY((int)GetY()) - 1) == false)
                    {
                        _direction = _waitingToMove;
                        _waitingToMove = 0;
                    }
                }
                else if (_waitingToMove == 3)
                {
                    if (myGrid.GetGridLocked(myGrid.FindGridX((int)GetX() - 1), myGrid.FindGridY((int)GetY())) == false)
                    {
                        _direction = _waitingToMove;
                        _waitingToMove = 0;
                    }
                }
                else if (_waitingToMove == 4)
                {
                    if (myGrid.GetGridLocked(myGrid.FindGridX((int)GetX()), myGrid.FindGridY((int)GetY()) + 1) == false)
                    {
                        _direction = _waitingToMove;
                        _waitingToMove = 0;
                    }
                }
            }
            switch (_direction)
            {
                case 1: // RIGHT
                    SetPosition(GetX() + _speed, GetY());
                    CollisionDetection();
                    break;
                case 2: // UP 
                    SetPosition(GetX(), GetY() - _speed);
                    CollisionDetection();
                    break;
                case 3: // LEFT
                    SetPosition(GetX() - _speed, GetY());
                    CollisionDetection();
                    break;
                case 4: // DOWN
                    SetPosition(GetX(), GetY() + _speed);
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
