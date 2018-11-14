using UnityEngine;
using UnityEngine.UI;

public class Collision_Taupe : MonoBehaviour {

    Text score;
    Text temps;
    Game game;

    public static double nbPoints;
    public static double nbTempsGagne;

    string tagTaupe;

	// Use this for initialization
	void Start ()
    {
        tagTaupe = gameObject.tag;
        score = GameObject.Find("Score").GetComponent<Text>();
        temps = GameObject.Find("Temps").GetComponent<Text>();
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "marteau")
        {
            if (temps.text != Game.TECOULE)
            {
                if (tagTaupe == "taupePiege")
                {
                    score.text = (int.Parse(score.text) + nbPoints * (-10)).ToString();
                    game.tempsLimite += (float)nbTempsGagne * (-7);
                }
                if (tagTaupe == "taupeBonus")
                {
                    game.tempsLimite += (float)nbTempsGagne * (4);
                    score.text = (int.Parse(score.text) + nbPoints * (2)).ToString();
                }
                if (tagTaupe == "taupe")
                {
                    score.text = (int.Parse(score.text) + nbPoints * (1)).ToString();
                }
            }
            Spawner.removeTaupe(gameObject);
            Destroy(gameObject);
        }
    }
}
