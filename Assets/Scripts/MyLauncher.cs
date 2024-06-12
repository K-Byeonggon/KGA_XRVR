using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MyLauncher : MonoBehaviour
{
    [SerializeField] GameObject Prefab_Projectiles;
    [SerializeField] Transform Transform_ShootPos;
    [SerializeField] float _projectileSpeed;

    private XRGrabInteractable _grabInteractable;

    private void OnEnable()
    {
        _grabInteractable = this.GetComponent<XRGrabInteractable>();
        if( _grabInteractable as Object == null)
        {
            Debug.LogError("Launcher need GrabInteractable");
            return;
        }

        _grabInteractable.activated.AddListener(Fire);
    }

    private void OnDisable()
    {
        _grabInteractable.activated.RemoveListener(Fire);
    }

    public void Fire(ActivateEventArgs args)
    {
        GameObject newObj = Instantiate(Prefab_Projectiles, Transform_ShootPos.position, Transform_ShootPos.rotation);
        if(newObj.TryGetComponent(out Rigidbody rigidbody))
        {
            ApplyForce(rigidbody);
        }
    }

    private void ApplyForce(Rigidbody rigidbody)
    {
        Vector3 force = Transform_ShootPos.up * _projectileSpeed;
        rigidbody.AddForce(force);
    }
}
