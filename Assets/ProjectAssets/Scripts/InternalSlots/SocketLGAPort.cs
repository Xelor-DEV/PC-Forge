using UnityEngine;
using UnityEngine.Events;

public class SocketLGAPort : InternalSlot
{
    [Header("Socket Configuration")]
    [SerializeField] private Animator socketAnimator;

    [Header("Socket Events")]
    public UnityEvent OnSocketOpened;
    public UnityEvent OnSocketClosed;

    [Header("CPU Events")]
    public UnityEvent<GameObject> OnCPUAttached = new UnityEvent<GameObject>();

    [Header("External References")]
    [SerializeField] private GameObject triggerContainer; // Objeto con el collider
    [SerializeField] private BoxCollider triggerCollider;

    private bool isSocketOpen = false;
    private bool componentInstalled = false;

    protected override void Start()
    {
        base.Start();
        ConfigureTriggerCollider();
    }

    private void ConfigureTriggerCollider()
    {
        if (triggerCollider != null)
        {
            triggerCollider.isTrigger = true;
        }
    }

    public void HandleTriggerEnter()
    {
        if (componentInstalled == false)
        {
            OpenSocket();
        }    
    }

    private void OpenSocket()
    {
        if (!isSocketOpen)
        {
            isSocketOpen = true;
            PlayAnimation("OnOpenSocket", true);
            OnSocketOpened?.Invoke();
        }
    }

    protected override void CheckAssembly()
    {
        if (isSlotOccupied) return;
        if (isSocketOpen == true && AllSlotsOccupied() == true)
        {
            AttachComponent(currentComponent);
            CloseSocket();
        }
    }

    private bool AllSlotsOccupied()
    {
        for (int i = 0; i < slotsOccupied.Length; ++i)
        {
            if (slotsOccupied[i] == false)
            {
                return false;
            }
        }
        return true;
    }

    protected override void AttachComponent(GameObject component)
    {
        InternalHardware cpu = component.GetComponent<InternalHardware>();
        if (cpu != null)
        {
            isSlotOccupied = true;
            cpu.SnapToCorrectPosition(transform);
            cpu.DeactivateComponents();
            componentInstalled = true;
            OnComponentAttached?.Invoke();
            OnCPUAttached?.Invoke(cpu.gameObject);
        }
    }

    private void CloseSocket()
    {
        isSocketOpen = false;
        PlayAnimation("OnClosedSocket",true);
        OnSocketClosed?.Invoke();
    }

    private void PlayAnimation(string animationName, bool state)
    {
        if (socketAnimator != null)
        {
            socketAnimator.SetBool(animationName, state);
        }
    }

    protected override bool ValidateLatches()
    {
        // No se usan latches en este socket
        return true;
    }
}
