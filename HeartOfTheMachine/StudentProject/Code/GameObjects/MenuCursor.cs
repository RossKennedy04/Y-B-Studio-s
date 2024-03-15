using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StudentProject.Code.GameObjects
{
    internal class MenuCursor : GameObject
    {
        public MenuCursor()
        {
            SetSprite("MenuCursor");
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetScale(0.9f, 0.9f);
        }

        public override void Update(float deltaTime)
        {
            SetPosition(GameInput.GetMousePosition());
        }
    }
}
