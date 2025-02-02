using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public GameObject hand;

    [SerializeField] private Transform heldObject;
    [SerializeField] private bool isGrabing;
    StarterAssetsInputs starterAssetsInputs;

    Rigidbody rb;


    void Awake()
    {
        starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
    }

    void Start()
    {
        isGrabing = false;
    }
    void Update()
    {
        if (starterAssetsInputs.interact && !isGrabing)
        {
            RaycastInput();
        }

        if (starterAssetsInputs.interact && isGrabing)
        {
            DropItem();
        }
    }

    void RaycastInput()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "package")
            {
                heldObject = hit.transform;
                hit.transform.SetParent(hand.transform);
                rb = heldObject.GetComponent<Rigidbody>();

                rb.useGravity = false;
                rb.isKinematic = true;

                hit.transform.position = hand.transform.position;
                hit.transform.eulerAngles = hand.transform.eulerAngles;
                isGrabing = true;
                //hit.transform.position = Vector3.zero;
                
            }

            else if (hit.collider.tag == "stall")
            {
                heldObject = hit.transform;
                hit.transform.SetParent(hand.transform);
                rb = heldObject.GetComponent<Rigidbody>();

                rb.useGravity = false;
                rb.isKinematic = true;

                hit.transform.position = hand.transform.position;
                hit.transform.eulerAngles = hand.transform.eulerAngles;
                isGrabing = true;
            }

            else if (hit.collider.tag == "product")
            {
                heldObject = hit.transform;
                hit.transform.SetParent(hand.transform);
                rb = heldObject.GetComponent<Rigidbody>();

                rb.useGravity = false;
                rb.isKinematic = true;

                hit.transform.position = hand.transform.position;
                hit.transform.eulerAngles = hand.transform.eulerAngles;
                isGrabing = true;
            }
            
            starterAssetsInputs.InteractInput(false);
        }
    }

    void DropItem()
    {
        heldObject.SetParent(null);
        heldObject.position = hand.transform.position + hand.transform.forward * 1f;

        rb.useGravity = true;
        rb.isKinematic = false;

        heldObject = null;
        isGrabing = false;
        starterAssetsInputs.InteractInput(false);
    }
}
