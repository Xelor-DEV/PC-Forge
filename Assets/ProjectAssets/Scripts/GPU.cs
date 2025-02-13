using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
public class GPU : MonoBehaviour
{
    public Rigidbody _compRigidbody;
    public BoxCollider _compBoxCollider;
    public Grabbable _compGrabbable;
    public GrabInteractable _grabInteractable;
    public HandGrabInteractable _handGrabInteractable;

    public void DeactivateComponents()
    {
        if (_compGrabbable != null)
        {
            _compGrabbable.enabled = false;
        }

        if (_grabInteractable != null)
        {
            _grabInteractable.enabled = false;
        }

        if (_handGrabInteractable != null)
        {
            _handGrabInteractable.enabled = false;
        }

        if (_compBoxCollider != null)
        {
            _compBoxCollider.enabled = false;
        }
        

        if (_compRigidbody != null)
        {
            // Desactivar la gravedad
            _compRigidbody.useGravity = false;

            // Congelar la posición y rotación
            _compRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

            // Hacer el Rigidbody cinemático
            _compRigidbody.isKinematic = true;

            // Detener el movimiento y la rotación (aunque esté congelado por los constraints)
            _compRigidbody.velocity = Vector3.zero;
            _compRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
