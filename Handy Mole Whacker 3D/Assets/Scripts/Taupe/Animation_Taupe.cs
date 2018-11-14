using UnityEngine;

public class Animation_Taupe : MonoBehaviour
{
    public static double vitesseAnim;

    Animator mAnim;

    // Use this for initialization
    void Start()
    {
        mAnim = GetComponent<Animator>();
        mAnim.speed = (float)vitesseAnim;
    }

    // Update is called once per frame
    void Update()
    {
        if (mAnim.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash("fin_anim"))
        {
            Spawner.removeTaupe(transform.parent.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }

}