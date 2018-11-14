using UnityEngine;

public class MousePosition : MonoBehaviour {

    Animator monMarteauAnim;

    public static bool enable
    {
        get { return hasAnim; }
        set { hasAnim = value; }
    }

    private static bool hasAnim;

    // Use this for initialization
    void Start () {
        monMarteauAnim = GameObject.FindGameObjectWithTag("marteau").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAnim)
        {
            Vector3 v = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            v.Scale(new Vector3(10, 10, 1));
            v.x -= 4;
            v.z = v.y - 4;
            v.y = 0.7F;
            transform.position = v;


            if (Input.GetMouseButtonDown(0))
                monMarteauAnim.SetTrigger("taper");
        }
    }

}
