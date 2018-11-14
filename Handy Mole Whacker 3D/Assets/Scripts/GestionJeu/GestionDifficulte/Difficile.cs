using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GestionJeu
{
    public class Difficile : Difficulte
    {
        public Difficile() 
            : base(-0.4, 2, 3, 0.75,0.05,0.25)
        {
        }
    }
}
