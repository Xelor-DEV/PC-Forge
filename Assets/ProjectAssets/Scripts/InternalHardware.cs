using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine.Events;

public class InternalHardware : MonoBehaviour
{
    [Header("Components to Deactivate")]
    [SerializeField] private Rigidbody _compRigidbody;
    [SerializeField] private BoxCollider _compBoxCollider;
    [SerializeField] private Grabbable _compGrabbable;
    [SerializeField] private GrabInteractable _grabInteractable;
    [SerializeField] private HandGrabInteractable _handGrabInteractable;

    [Header("Attachment Settings")]
    [SerializeField] private Vector3 correctPosition; // Posición relativa a la motherboard
    [SerializeField] private Vector3 correctRotation; // Rotación en ángulos de Euler

    [Header("Events")]
    public UnityEvent OnComponentInstalled; // El evento público

    public void SnapToCorrectPosition(Transform motherboard)
    {
        // Configurar transformación relativa
        transform.SetParent(motherboard);
        transform.localPosition = correctPosition;
        transform.localRotation = Quaternion.Euler(correctRotation);
    }

    [ContextMenu("Set Correct Position And Rotation")]
    public void SetCorrectPositionAndRotation()
    {
        correctPosition = transform.transform.localPosition;
        correctRotation = transform.transform.localRotation.eulerAngles;
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

            // Congelar la posición y rotación
            _compRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

            // Hacer el Rigidbody cinemático
            _compRigidbody.isKinematic = true;

            // Detener el movimiento y la rotación (aunque esté congelado por los constraints)
            _compRigidbody.velocity = Vector3.zero;
            _compRigidbody.angularVelocity = Vector3.zero;
        }
        OnComponentInstalled?.Invoke();
    }
}
