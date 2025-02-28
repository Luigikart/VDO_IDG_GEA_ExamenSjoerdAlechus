using UnityEngine;
using UnityEngine.SceneManagement;


public class OutBounce : MonoBehaviour
{


    void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Player")) return;
        Rigidbody rb = col.GetComponent<Rigidbody>();
        if(rb == null) return;

        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

}
