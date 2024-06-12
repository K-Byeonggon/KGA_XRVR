using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Launcher : MonoBehaviour
{
    [SerializeField] GameObject Prefab_Projectiles;
    [SerializeField] Transform Transform_ShootPos;
    [SerializeField] float _projectileSpeed;

    private XRGrabInteractable _grabInteractable;

    private void OnEnable()
    {
        _grabInteractable = this.GetComponent<XRGrabInteractable>();
        _grabInteractable.activated.AddListener(Fire);
    }

    private void OnDisable()
    {
        if(_grabInteractable != null)
        {
            _grabInteractable.activated.RemoveListener(Fire);
        }
    }

    public void Fire(ActivateEventArgs args)
    {
        GameObject newObj = Instantiate(Prefab_Projectiles, Transform_ShootPos.position, Transform_ShootPos.rotation);
        if (newObj.TryGetComponent(out Rigidbody rigidbody)) //TryGetComponent의 성공여부에 따라 true/false 반환
        {
            ApplyForce(rigidbody);
        }
    }

    private void ApplyForce(Rigidbody rigidbody)
    {
        Vector3 force = Transform_ShootPos.forward * _projectileSpeed;
        rigidbody.AddForce(force);
    }
}
