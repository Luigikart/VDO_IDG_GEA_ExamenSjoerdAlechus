using UnityEngine;

public class FallAccelration : MonoBehaviour
{
   [SerializeField] float fallAccelration = 1f;
    Rigidbody rb => GetComponent<Rigidbody>();
    Vector3 oldPos;
    void LateUpdate()
    {

        if  (rb.linearVelocity.y < 0f)
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallAccelration * Time.deltaTime;
         
    }
}
