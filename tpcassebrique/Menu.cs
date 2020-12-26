using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace tpcassebrique
{

    class Menu
    {
        private const Keys TOUCHE_DROITE = Keys.Right;
        private const Keys TOUCHE_GAUCHE = Keys.Left;

       public static Boolean CheckActionDroite()
        {
            Boolean checkActiondown = false;
            KeyboardState keyboard = Keyboard.GetState();

            checkActiondown = keyboard.IsKeyDown(TOUCHE_DROITE);

            return checkActiondown;
        }


        public static Boolean CheckActionGauche()
        {
            Boolean checkActionDown = false;
            KeyboardState keyboard = Keyboard.GetState();

            checkActionDown = keyboard.IsKeyDown(TOUCHE_GAUCHE);

            return checkActionDown;
        }
    }
}
