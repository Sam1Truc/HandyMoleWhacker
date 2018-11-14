using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GestionJeu
{
    public class Facile : Difficulte
    {
        public Facile() 
            : base(0, 1, 1, 2,0.2,0.2)
        {
        }
    }
}
