using TMPro;
using UnityEngine;


public class BookController : MonoBehaviour
{
    Animator anim;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float range;
    [SerializeField] private GameObject Target;
    private Transform playerPos;
    private float distance;
    private TMP_Text hintText;


    void Start()
    {

        anim = gameObject.GetComponent<Animator>();

        //get player transform
        playerPos = Target.transform;

        //get hint text
        hintText = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, playerPos.position);
        if (distance < range)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            anim.SetTrigger("OpenBook");
            hintText.gameObject.SetActive(true);
        }
        else
        {
            hintText.gameObject.SetActive(false);
            //spin
            var angle = spinSpeed * Time.deltaTime;
            var axis = new Vector3(0.0f, 1.0f, 0.0f);
            transform.localRotation *= Quaternion.AngleAxis(angle, axis);
        }
    }
}
