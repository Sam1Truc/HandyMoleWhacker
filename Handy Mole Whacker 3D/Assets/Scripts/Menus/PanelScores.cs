using Assets.Scripts.GestionJeu;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Assets.Scripts.Menus
{
    public class PanelScores : MonoBehaviour
    {
        public int Count;
        public bool hasChanged;

        GameObject panelMenuAccueil;

        public Dictionary<string, Dictionary<string, double>> DicJoueursDifficulte
        {
            get { return mDicJoueursDifficulte; }
            private set { mDicJoueursDifficulte = value; }
        }

        Dictionary<string, Dictionary<string, double>> mDicJoueursDifficulte;

        private void Start()
        {
            panelMenuAccueil = GameObject.FindGameObjectWithTag("PanelMenu");

            hasChanged = false;
            if (mDicJoueursDifficulte == null)
            {
                CreerDicoJoueurs(Options.Instance);
            }
            TriParScore(Options.Instance.Difficulte);
        }

        private void CreerDicoJoueurs(Options o)
        {
            mDicJoueursDifficulte = new Dictionary<string, Dictionary<string, double>>();
            foreach (string dif in o.ListeDifficulte)
            {
                mDicJoueursDifficulte.Add(dif, new Dictionary<string, double>());
            }
        }

        public void RetourMenu()
        {
            var panelScore = GameObject.FindGameObjectWithTag("PanelScores");
            panelScore.SetActive(false);
            if (panelMenuAccueil != null)
                panelMenuAccueil.SetActive(true);
            else
                Menu.ClicCancelPanelScores();
        }

        public void EnregistrerJoueur(string pseudo, int score, Options opt)
        {
            var mDicoJoueurs = mDicJoueursDifficulte.Single(kvp => kvp.Key == opt.Difficulte).Value;

            if (mDicoJoueurs.Count > 9)
            {
                if (score > mDicoJoueurs.Min(kvp => kvp.Value))
                {
                    mDicoJoueurs.Remove(mDicJoueursDifficulte.Last().Key);
                    mDicoJoueurs.Add(pseudo, score);
                }
            }
            else
            {
                if (mDicoJoueurs.ContainsKey(pseudo))
                {
                    if (mDicoJoueurs[pseudo] < score)
                        mDicoJoueurs[pseudo] = score;
                }
                else
                    mDicoJoueurs.Add(pseudo, score);
            }
            TriParScore(opt.Difficulte);
            hasChanged = true;
        }

        void TriParScore(string difficulte)
        {
            mDicJoueursDifficulte[difficulte] = mDicJoueursDifficulte.Single(kvp => kvp.Key == difficulte).Value
                                .OrderByDescending(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

    }
}
