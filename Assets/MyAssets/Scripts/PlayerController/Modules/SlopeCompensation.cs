using UnityEngine;

public class SlopeCompensation : MonoBehaviour
{
    [SerializeField] float slopeForce = 1f;
    [SerializeField] LayerMask hitLayers;
    [SerializeField] bool debugMode = false;
    Rigidbody rb => GetComponent<Rigidbody>();

    void Update()
    {
        if (OnSlope())
        {
            rb.AddForce(Vector3.up * slopeForce, ForceMode.Force);
        }
    }


    bool OnSlope()
    {
        RaycastHit hit;
        float height = GetComponent<CapsuleCollider>().height * 0.5f + 0.5f;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, height, hitLayers))
        {
            if (hit.normal != Vector3.up)
            {
                if (debugMode)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.blue);
                    Debug.DrawRay(hit.point, hit.normal * 1.2f, Color.red);
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        return false;

    }


}
