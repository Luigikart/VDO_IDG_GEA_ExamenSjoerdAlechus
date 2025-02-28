using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class GroundDetection : MonoBehaviour
{

    [SerializeField] LayerMask hitLayers;
    [SerializeField] float hitDistance;
    [SerializeField] float hitRadius = 0.5f;
    [SerializeField] bool debugMode = false;
    static public GroundDetection instance;
    Vector3 debugPostion;

    public UnityAction<bool> isGroundedEvent;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool isGrounded(Vector3 position)

    {
        if (Physics.SphereCast(position, hitRadius, Vector3.down, out RaycastHit hit, hitDistance, hitLayers))
        {
            if (debugMode)
            {
                Debug.DrawRay(transform.position, Vector3.down * hitDistance, Color.green, 0.1f);
                debugPostion = hit.point;
            }
            isGroundedEvent?.Invoke(true);
            return true;

        }
        if (debugMode)


        {
            debugPostion = position;
            Debug.DrawRay(transform.position, Vector3.down * hitDistance, Color.red, 0.1f);

        }
        isGroundedEvent?.Invoke(false);
        return false;




#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (!debugMode) return;
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Gizmos.DrawSphere(debugPostion, hitRadius);
        }
#endif

    }   
}    

