using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner
{
    public static double CoeffJaune;
    public static double CoeffRouge;

    public static IEnumerable<string> ListeTypesTaupes
    {
        get { return mListeTypesTaupes; }
    }

    static List<string> mListeTypesTaupes;
    static Dictionary<int, GameObject> mListeTrousOccupes;
    GameObject mTaupe;
    GameObject mTaupeBonus;
    GameObject mTaupePiege;
    GameObject mTaupeSpawn;

    public static Dictionary<int, GameObject> ListeTrousOccupés
    {
        get { return mListeTrousOccupes; }
    }

    public static void removeTaupe(GameObject t)
    {
        var item = mListeTrousOccupes.First(kvp => kvp.Value == t);
        mListeTrousOccupes.Remove(item.Key);
    }


    /// <summary>
    /// Possibles coordonnées entre
    /// </summary>
    private Dictionary<int,Vector3> mListeCoord = new Dictionary<int, Vector3>
            {
                {1, new Vector3(-3.1f,0,4.2f) },
                {2, new Vector3(0,0,4.2f) },
                {3, new Vector3(3.1f,0,4.2f) },
                {4, new Vector3(-3.1f,0,1.05f) },
                {5, new Vector3(0,0,1.05f) },
                {6, new Vector3(3.1f,0,1.05f) },
                {7, new Vector3(-3.1f,0,-2f) },
                {8, new Vector3(0,0,-2f) },
                {9, new Vector3(3.1f,0,-2f) }
            };

    public Spawner(GameObject taupe, GameObject taupeBonus, GameObject taupePiege)
    {
        mTaupe = taupe;
        mTaupeBonus = taupeBonus;
        mTaupePiege = taupePiege;
        mListeTrousOccupes = new Dictionary<int, GameObject>();
        mListeTypesTaupes = new List<string> { mTaupe.tag, mTaupeBonus.tag, mTaupePiege.tag };
    }

    public void Spawn()
    {
        int trou = Random.Range(1,mListeCoord.Count+1);

        int nbEssais = 0;

        while (!isPlaceVide(trou) && nbEssais < 200)
        {
            trou = Random.Range(1, mListeCoord.Count + 1);
            nbEssais++;
        }
        if (nbEssais == 200)
        {
            return;
        }

        int version=Random.Range(1, 101);

        if (version > (100-(CoeffJaune*100)))
            mTaupeSpawn = mTaupeBonus;
        else
        {
            if (version > (100 - (CoeffRouge * 100)-(CoeffJaune * 100)))
                mTaupeSpawn = mTaupePiege;
            else
                mTaupeSpawn = mTaupe;
        }

        mTaupeSpawn.transform.position = mListeCoord[trou];
        var temp = Object.Instantiate(mTaupeSpawn);
        mListeTrousOccupes.Add(trou, temp);

    }

    bool isPlaceVide(int trou)
    {
        return !mListeTrousOccupes.ContainsKey(trou);
    }

}