using UnityEngine;
using UnityEngine.SceneManagement;


public class GunPickup : MonoBehaviour
{

    [SerializeField] GameObject GunInHand;
    [SerializeField] GameObject GunOnGround;
    void Awake()
    {
        GunInHand.SetActive(false);
        GunOnGround.SetActive(true);
    }

    void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Player")) return;
        Rigidbody rb = col.GetComponent<Rigidbody>();
        if(rb == null) return;

        GunInHand.SetActive(true);
        GunOnGround.SetActive(false);

    }

}
