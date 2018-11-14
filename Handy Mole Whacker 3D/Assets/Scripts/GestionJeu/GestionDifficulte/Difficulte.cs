using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GestionJeu
{
    public abstract class Difficulte
    {
        public double VitesseSpawnTaupe
        {
            get;
            protected set;
        }

        public double VitesseAnimTaupe
        {
            get;
            protected set;
        }

        public double ScoreMultiplier
        {
            get;
            protected set;
        }

        public double TimeMultiplier
        {
            get;
            protected set;
        }

        public double CoeffTaupeJaune
        {
            get;
            protected set;
        }

        public double CoeffTaupeRouge
        {
            get;
            protected set;
        }

        public Difficulte(double vSpawn, double vAnim, double scoreMult, double timeMult, double CoeffJaune, double CoeffRouge)
        {
            VitesseSpawnTaupe = vSpawn;
            VitesseAnimTaupe = vAnim;
            ScoreMultiplier = scoreMult;
            TimeMultiplier = timeMult;
            CoeffTaupeJaune = CoeffJaune;
            CoeffTaupeRouge = CoeffRouge;
        }

    }
}
