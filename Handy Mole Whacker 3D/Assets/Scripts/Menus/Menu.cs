using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.GestionJeu;
using Assets.Scripts.Menus;

public class Menu : MonoBehaviour
{
    Text valDifficulte;
    Text valChoixDevice;

    public Options Options;

    public static GameObject panelScores;
    public static GameObject panelMenuAccueil;
    public static GameObject panelMenuJeu;
    public static GameObject panelTempsScore;
    Game game;

    // Use this for initialization
    void Start()
    {
        Options = Options.Instance;
        game = FindObjectOfType<Game>();
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("CanvasScoreBoard"));

        var listScoreBoards = GameObject.FindGameObjectsWithTag("CanvasScoreBoard");
        if (listScoreBoards.Length > 1)
            Destroy(listScoreBoards[1]);
        

        panelScores = GameObject.FindGameObjectWithTag("PanelScores");
        if(game==null)
            panelScores.SetActive(false);

        panelMenuAccueil = GameObject.FindGameObjectWithTag("PanelMenu");
        panelMenuJeu = GameObject.FindGameObjectWithTag("PanelFinPartie");
        panelTempsScore = GameObject.FindGameObjectWithTag("PanelTempsScore");

        if (GameObject.FindGameObjectWithTag("TextDifficulte") != null)
        {
            valDifficulte = GameObject.FindGameObjectWithTag("TextDifficulte").GetComponent<Text>();
            valDifficulte.text = Options.Difficulte;
        }
        if (GameObject.FindGameObjectWithTag("TextChoixDevice") != null)
        {
            valChoixDevice = GameObject.FindGameObjectWithTag("TextChoixDevice").GetComponent<Text>();
            valChoixDevice.text = Options.ChoixDevice;
        }
       
    }

    public void ChargerSceneJeu(string nomScene)
    {
        //Instancier la classe Game avec les bons paramètres (Fabrique) puis l'attacher à la scène de jeu.
        if(valDifficulte != null && valChoixDevice != null)
        {
            Options.Difficulte = valDifficulte.text;
            Options.ChoixDevice = valChoixDevice.text;
        }
        panelScores.SetActive(true);
        SceneManager.LoadScene(nomScene);
    }

    public void ChargerScene(string nomScene)
    {
        panelScores.SetActive(true);
        SceneManager.LoadScene(nomScene);
    }

    public void Quitter()
    {
        Application.Quit();
    }

    public void ClicDifficulte()
    {
        var mListeDifficulte = Options.ListeDifficulte;
        if (mListeDifficulte.Contains(valDifficulte.text))
        {
            int indexModeActuel = mListeDifficulte.IndexOf(valDifficulte.text);
            if (indexModeActuel >= mListeDifficulte.Count - 1)
                indexModeActuel = -1;
            valDifficulte.text = mListeDifficulte[indexModeActuel + 1];
        }
    }

    public void ClicChoixDevice()
    {
        var mListeChoixDevice = Options.ListeChoixDevice;
        if (mListeChoixDevice.Contains(valChoixDevice.text))
        {
            int indexModeActuel = mListeChoixDevice.IndexOf(valChoixDevice.text);
            if (indexModeActuel >= mListeChoixDevice.Count - 1)
                indexModeActuel = -1;
            valChoixDevice.text = mListeChoixDevice[indexModeActuel + 1];
        }
    }

    public static void ClicPanelScores()
    {
        if (panelScores != null)
        {
            panelScores.SetActive(true);
            
            if (panelMenuAccueil != null)
                panelMenuAccueil.SetActive(false);
            else
            {
                panelMenuJeu.SetActive(false);
                panelTempsScore.SetActive(false);
            }
        }
        else
            Debug.LogWarning("Pb panlScore nul !");
    }

    public static void ClicCancelPanelScores()
    {
        if (panelMenuAccueil != null)
            panelMenuAccueil.SetActive(true);
        else
        {
            panelTempsScore.SetActive(true);
            panelMenuJeu.SetActive(true);
        }
    }

    public void ClicAffichPanelScores()
    {
        if (panelScores != null)
        {
            panelScores.SetActive(true);
            if (panelMenuAccueil != null)
                panelMenuAccueil.SetActive(false);
            else
            {
                panelMenuJeu.SetActive(false);
                panelTempsScore.SetActive(false);
            }
        }
        else
            Debug.LogWarning("Pb panlScore nul !");
    }

    public void ClicRetourMenu()
    {
        if (panelScores.activeInHierarchy)
            panelScores.SetActive(false);
    }
}
