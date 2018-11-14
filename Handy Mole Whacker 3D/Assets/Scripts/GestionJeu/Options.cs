using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GestionJeu
{
    public class Options
    {
        public List<string> ListeDifficulte { get { return mListeDifficulte; } }
        List<string> mListeDifficulte = new List<string>() { "Facile", "Normal", "Difficile" };

        public List<string> ListeChoixDevice { get { return mListeChoixDevice; } }
        List<string> mListeChoixDevice = new List<string>() { "LeapMotion", "Souris" };

        public string Difficulte
        {
            get { return mDifficulte; }
            set { mDifficulte = value; }
        }
        string mDifficulte;

        public string ChoixDevice
        {
            get { return mChoixDevice; }
            set { mChoixDevice = value; }
        }
        string mChoixDevice;

        private static Options mInstance;

        private Options()
        {
            mDifficulte = mListeDifficulte[0];
            mChoixDevice = mListeChoixDevice[1];
        }

        public static Options Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new Options();
                }
                return mInstance;
            }
        }

    }
}
