using Assets.Scripts.GestionJeu;
using Assets.Scripts.Menus;
using UnityEngine;
using UnityEngine.UI;

public class ListeScoresJoeurs : MonoBehaviour
{

    public GameObject mLigneJoueurPrefab;
    Text choixAffichDiff;

    PanelScores mPanel;

    // Use this for initialization
    void Start()
    {
        mPanel = FindObjectOfType<PanelScores>();
        choixAffichDiff = GameObject.FindGameObjectWithTag("ContentDropDownDifficulte").GetComponent<Text>();
        if (choixAffichDiff != null)
        {
            choixAffichDiff.text = Options.Instance.Difficulte;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mPanel.hasChanged && choixAffichDiff != null)
        {
            ChargementTabScores();
            mPanel.hasChanged = false;
        }
    }

    public void ChargementTabScores()
    {
        if (mPanel == null)
        {
            Debug.LogError("Le script PanelScores ne figure dans aucun gameObject de la scène !");
            return;
        }

        while (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            child.SetParent(null);
            DestroyObject(child.gameObject);
        }
        int i = 0;
        foreach (var p in mPanel.DicJoueursDifficulte[choixAffichDiff.text])
        {
            GameObject joueur = (GameObject)Instantiate(mLigneJoueurPrefab);
            joueur.transform.SetParent(transform);
            joueur.transform.localScale = new Vector3(1, 1, 1);
            joueur.transform.Find("Position").GetComponent<Text>().text = (++i).ToString();
            joueur.transform.Find("Pseudo").GetComponent<Text>().text = p.Key;
            joueur.transform.Find("ScoreJoueur").GetComponent<Text>().text = p.Value.ToString();
        }
    }
}
