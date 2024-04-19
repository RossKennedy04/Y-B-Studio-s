using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProject.Code.GameObjects
{
    internal class WallCorner : GameObject
    {
        public WallCorner() 
        {
            SetSprite("Level1WallCorner");
        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}
