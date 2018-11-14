using Leap;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HandPosition : MonoBehaviour {


    Hand hand;
    Controller mControl;
    Text score;
    Text temps;
    Game game;

    float tempsMartAuSol;

    public static float PosY;

    public static bool enable
    {
        get { return hasAnim; }
        set { hasAnim = value; }
    }

    static bool hasAnim;

    static float xBase;
    static float yBase;
    static float zBase;

    Text infoMarteau;


    // Use this for initialization
    void Start () {
        mControl = new Controller();
        score = GameObject.Find("Score").GetComponent<Text>();
        temps = GameObject.Find("Temps").GetComponent<Text>();
        infoMarteau = GameObject.Find("InfoMarteau").GetComponent<Text>();
        infoMarteau.gameObject.SetActive(false);
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        xBase = transform.position.x;
        yBase = transform.position.y;
        zBase = transform.position.z;
    }

    double calcPosition(double realMin, double realMax, double gameMin, double gameMax, double realPos)
    {
        return (realPos - realMin) * (gameMax - gameMin) / (realMax - realMin) + gameMin;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasAnim)
        {
            try
            {
                Frame frame = mControl.Frame();
                hand = frame.Hands[0];
                float x = -hand.StabilizedPalmPosition.x;
                float y = hand.StabilizedPalmPosition.y;
                float z = -hand.PalmPosition.z;
                PosY = y;
                if (y < 130)
                {
                    y = 130F;
                    tempsMartAuSol += Time.deltaTime;
                }
                else
                {
                    tempsMartAuSol = 0;
                    infoMarteau.gameObject.SetActive(false);
                }

                if(tempsMartAuSol > 1)
                {
                    if (temps.text != Game.TECOULE)
                    {
                        score.text = (int.Parse(score.text) - (Collision_Taupe.nbPoints * 3)).ToString();
                        game.tempsLimite -= (float)Collision_Taupe.nbPoints * 2;
                        infoMarteau.gameObject.SetActive(true);
                    }
                    tempsMartAuSol = 0;
                }

                

                //Debug.Log(string.Format("X:{0}/ Y:{1}/ Z:{2}", x, y, z));

                transform.rotation = Quaternion.Euler(0, 0, (float)calcPosition(125, 225, 90, 40, y));
                transform.position =

                    new Vector3(
                        (float)calcPosition(-110, 110, 6, -6, x),
                        (float)calcPosition(225, 125, 4, 1, y),
                        (float)calcPosition(-80, 90, -3, 7, z)
                    );


            }
            catch (Exception e)
            {
                Debug.Log(e);
                transform.position = new Vector3(xBase, yBase, zBase);
                transform.rotation = Quaternion.Euler(0, 0, 40);
            }
        }

	}
}
