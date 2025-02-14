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

    [Header("Attachment Settings")]
    public Vector3 correctPosition; // Posici�n relativa a la motherboard
    public Vector3 correctRotation; // Rotaci�n en �ngulos de Euler

    public void SnapToCorrectPosition(Transform motherboard)
    {
        // Configurar transformaci�n relativa
        transform.SetParent(motherboard);
        transform.localPosition = correctPosition;
        transform.localRotation = Quaternion.Euler(correctRotation);
    }


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

            // Congelar la posici�n y rotaci�n
            _compRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

            // Hacer el Rigidbody cinem�tico
            _compRigidbody.isKinematic = true;

            // Detener el movimiento y la rotaci�n (aunque est� congelado por los constraints)
            _compRigidbody.velocity = Vector3.zero;
            _compRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
