using Assets.Scripts.GestionJeu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    class MenuInput : MonoBehaviour
    {
        GameObject panelInput;
        GameObject panelScores;
        Game game;
        Text textNomVide;
        Dropdown dropDownDiff;

        private void Start()
        {
            game = FindObjectOfType<Game>();
            panelInput = GameObject.FindGameObjectWithTag("PanelInput");
            panelScores = GameObject.FindGameObjectWithTag("PanelScores");
            textNomVide = GameObject.FindGameObjectWithTag("TextNomVide").GetComponent<Text>();
            dropDownDiff = GameObject.FindGameObjectWithTag("DropDownDifficulte").GetComponent<Dropdown>();
            textNomVide.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                ClicValiderInput();
        }

        public void ClicAnnulerInput()
        {
            if (panelInput != null)
            {
                panelInput.SetActive(false);
            }
        }

        public void ClicValiderInput()
        {
            PanelScores mPanelScores = panelScores.GetComponent<PanelScores>();
            InputField mInputField = GameObject.FindGameObjectWithTag("InputPlayerName").GetComponent<InputField>();
            mInputField.text = mInputField.text.Trim();
            if (String.IsNullOrEmpty(mInputField.text))
            {
                StartCoroutine(AfficheErreurNom());
                return;
            }

            if (panelScores != null)
            {
                mPanelScores.EnregistrerJoueur(mInputField.text, int.Parse(game.score.text), Options.Instance);
                
                var listOptions = dropDownDiff.options;
                dropDownDiff.value = listOptions.IndexOf(listOptions.Single(k => k.text == Options.Instance.Difficulte));
                Menu.ClicPanelScores();
            }
            ClicAnnulerInput();
        }

        IEnumerator AfficheErreurNom()
        {
            textNomVide.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(2);
            textNomVide.gameObject.SetActive(false);
        }

    }
}
