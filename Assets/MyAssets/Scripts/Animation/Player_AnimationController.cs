using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player_AnimationController : MonoBehaviour
{

    [SerializeField] Animator WeaponAnimator;
    Physics_PlayerMovement playerMovement => GetComponent<Physics_PlayerMovement>();
    [SerializeField] LayerMask layerMask;

    void Start()
    {
        if (WeaponAnimator == null) Debug.LogError("WeaponAnimator is null. Please assign an animator object or component.");
        return;

    }

    void Update()
    {
        WeaponAnimator.SetFloat("_MoveSpeed", playerMovement.GetCurrentSpeed());
        WeaponAnimator.SetFloat("_UpSpeed", playerMovement.GetVerticalVelocity());

    }
    void SetGroundedState(bool currenState)
    {
        WeaponAnimator.SetBool("Ground", currenState);


    }

    void InputValue(Input input)
    {

    }
    void OnEnable()
    {
        try
        {

            GroundDetection.instance.isGroundedEvent += SetGroundedState;
        }
        catch
        {
            Debug.LogError("GroundDetection is null. Please assign a GroundDetection object or component.");
        }

    }

    void OnDisable()
    {
        GroundDetection.instance.isGroundedEvent -= SetGroundedState;
    }

    public void SetCurrentAnimator(Animator _animator)
    {
        WeaponAnimator = _animator;
    }

    public void OnAttack()
    {

        if (WeaponAnimator.GetBool("Fire") == true)
        {
            return;
        }
        else
        {
            StartCoroutine(Shoot());

        }


    }

    IEnumerator Shoot()
    {
        WeaponAnimator.SetBool("_Fire", true);

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {

            if (hit.collider.CompareTag("RaycastHit"))
            {
                hit.collider.gameObject.SetActive(false);
                Debug.Log("Hit object with the correct tag: " + hit.collider.name);

            }
        }
        yield return new WaitForSeconds(0.1f);
        WeaponAnimator.SetBool("_Fire", false);
    }

    public void OnReload()
    {
        if (WeaponAnimator.GetBool("Reload") == true)
        {
            return;
        }
        else
        {
            StartCoroutine(Reload());
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.1f);
        WeaponAnimator.SetBool("_Reload", false);
    }
}
