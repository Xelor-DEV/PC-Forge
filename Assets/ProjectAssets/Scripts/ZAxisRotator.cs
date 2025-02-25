using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;
using UnityEngine.Events;

public class LatchController : MonoBehaviour
{
    public enum RotationSpace { Local, World }

    [Header("Latch Configuration")]
    [SerializeField] RotationSpace rotationSpace = RotationSpace.Local;
    [SerializeField] float minAngle = 0f;
    [SerializeField] float maxAngle = 360f;
    [SerializeField] float targetAngle = 90f;
    [SerializeField] float closedAngle = 0f;
    [SerializeField] float angleThreshold = 1f;

    [Header("Events")]
    public UnityEvent OnLatchOpened;
    public UnityEvent OnLatchClosed;

    [Header("Components to Lock")]
    [SerializeField] private Grabbable _grabbable;
    [SerializeField] private GrabInteractable _grabInteractable;
    [SerializeField] private HandGrabInteractable _handGrabInteractable;
    [SerializeField] private OneGrabRotateTransformer _oneGrabRotateTransformer;

    private float currentAngle;
    private bool isLocked = false;
    private bool isOpen = false;

    public bool IsLatchOpen => isOpen;
    public bool IsLocked => isLocked;

    void Update()
    {
        if (isLocked) return;

        UpdateRotation();
        CheckTargetAngle();
    }

    private void UpdateRotation()
    {
        currentAngle = rotationSpace == RotationSpace.Local ?
            transform.localEulerAngles.z :
            transform.eulerAngles.z;

        float clampedAngle = Mathf.Repeat(currentAngle, 360f);

        // Forzar rotaci�n a targetAngle cuando est� cerca
        if (Mathf.Abs(clampedAngle - targetAngle) < angleThreshold && !isOpen)
        {
            SetRotation(targetAngle);
            HandleLatchOpened();
            return;
        }

        if (clampedAngle < minAngle || clampedAngle > maxAngle)
        {
            float nearestLimit = (Mathf.Abs(clampedAngle - minAngle) < Mathf.Abs(clampedAngle - maxAngle)) ?
                minAngle :
                maxAngle;

            SetRotation(nearestLimit);
        }
    }

    private void CheckTargetAngle()
    {
        bool wasOpen = isOpen;
        isOpen = Mathf.Abs(currentAngle - targetAngle) < angleThreshold;

        if (!wasOpen && isOpen)
        {
            HandleLatchOpened();
        }
    }

    private void HandleLatchOpened()
    {
        SetRotation(targetAngle);
        LockLatch();
        DeactivateComponents();
        OnLatchOpened.Invoke();
    }

    public void LockLatch()
    {
        isLocked = true;
    }

    public void CloseLatch()
    {
        SetRotation(closedAngle);
        LockLatch();
        DeactivateComponents();
        OnLatchClosed.Invoke();
    }

    private void SetRotation(float angle)
    {
        Vector3 newRotation = rotationSpace == RotationSpace.Local ?
            transform.localEulerAngles :
            transform.eulerAngles;

        newRotation.z = angle;

        if (rotationSpace == RotationSpace.Local)
            transform.localEulerAngles = newRotation;
        else
            transform.eulerAngles = newRotation;
    }

    public void DeactivateComponents()
    {
        if (_grabbable != null) _grabbable.enabled = false;
        if (_grabInteractable != null) _grabInteractable.enabled = false;
        if (_handGrabInteractable != null) _handGrabInteractable.enabled = false;
        if (_oneGrabRotateTransformer != null) _oneGrabRotateTransformer.enabled = false;
    }
}