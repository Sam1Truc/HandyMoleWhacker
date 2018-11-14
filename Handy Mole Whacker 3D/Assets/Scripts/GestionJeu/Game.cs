using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GestionJeu;
using System.Collections;
using Assets.Scripts.Menus;

public class Game : MonoBehaviour {

    public float tempsLimite;
    public GameObject taupe;
    public GameObject taupeBonus;
    public GameObject taupePiege;

    public const string TECOULE = "Écoulé";

    bool isPause;
    public Text score;
    public Text temps;
    Text pause;
    GameObject panelFin;
    public GameObject panelScores;
    public GameObject panelInput;
    Spawner spawner;
    Difficulte difficulte;

    const string INFO_PAUSE = "Pause : Touche échap";
    const string INFO_REPRISE = "Reprendre : Touche échap";


    // Use this for initialization
    void Start () {
        isPause = false;
        //Changer Temps et Score en fonction de la difficultée
        ChargementOptions(Options.Instance);
        panelFin = GameObject.FindGameObjectWithTag("PanelFinPartie");
        panelScores = GameObject.FindGameObjectWithTag("PanelScores");
        panelInput = GameObject.FindGameObjectWithTag("PanelInput");
        pause = GameObject.Find("InfoPause").GetComponent<Text>();

        panelInput.SetActive(false);
        Resume();
        score = GameObject.Find("Score").GetComponent<Text>();
        temps = GameObject.Find("Temps").GetComponent<Text>();
        score.text = "0";
        temps.text = tempsLimite.ToString();

        spawner = new Spawner(taupe,taupeBonus,taupePiege);
        InvokeRepeating("CompteurTemps", 0, 0.1f);
        StartCoroutine("Spawn");
    }

    private void ChargementOptions(Options option)
    {
        switch (option.Difficulte)
        {
            case "Facile":
                difficulte = new Facile();
                break;
            case "Normal":
                difficulte = new Normal();
                break;
            case "Difficile":
                difficulte = new Difficile();
                break;
        }
        switch (option.ChoixDevice)
        {
            case "LeapMotion":
                HandPosition.enable = true;
                MousePosition.enable = false;
                break;
            default:
                HandPosition.enable = false;
                MousePosition.enable = true;
                break;
        }
        Animation_Taupe.vitesseAnim = difficulte.VitesseAnimTaupe;
        Collision_Taupe.nbTempsGagne = difficulte.TimeMultiplier;
        Collision_Taupe.nbPoints = difficulte.ScoreMultiplier;
        Spawner.CoeffJaune = difficulte.CoeffTaupeJaune;
        Spawner.CoeffRouge = difficulte.CoeffTaupeRouge;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && temps.text != TECOULE)
            if (!isPause)
                Pause();
            else
                Resume();
    }

    void CompteurTemps()
    {
        float tempsEcoule = tempsLimite;

        tempsEcoule -= Time.timeSinceLevelLoad;
        if (tempsEcoule <= 0)
        {
            temps.text = TECOULE;
            CancelInvoke("CompteurTemps");
            CancelInvoke("Spawn");
            Pause();
            panelInput.SetActive(true);
            pause.gameObject.SetActive(false);
            
        }
        else
        {
            temps.text = tempsEcoule.ToString("0.000");
        }
    }

    IEnumerator Spawn()
    {
        while(temps.text != TECOULE)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range((float)(0.7 + difficulte.VitesseSpawnTaupe), (float)(1.2 + difficulte.VitesseSpawnTaupe)));
            spawner.Spawn();
        }
    }

    public void Pause()
    {
        isPause = true;
        MousePosition.enable = false;
        HandPosition.enable = false;
        Time.timeScale = 0;
        panelFin.SetActive(true);
        //panelScores.SetActive(true);
        Cursor.visible = true;
        pause.text = INFO_REPRISE;
    }

    public void Resume()
    {
        pause.gameObject.SetActive(true);
        isPause = false;
        Cursor.visible = false;
        panelScores.SetActive(false);
        Menu.ClicCancelPanelScores();
        panelFin.SetActive(false);
        Time.timeScale = 1;
        if(Options.Instance.ChoixDevice == "Souris")
            MousePosition.enable = true;
        else
            HandPosition.enable = true;
        pause.text = INFO_PAUSE;
    }
}
