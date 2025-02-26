using UnityEngine;
using UnityEngine.Events;

public abstract class InternalSlot : MonoBehaviour
{
    [Header("Base Port Configuration")]
    [SerializeField] protected SlotCollider[] slots;
    [SerializeField] protected LatchController[] latches;
    [SerializeField] protected float alignmentThreshold = 0.9f;

    [Header("Events")]
    public UnityEvent OnComponentAttached = new UnityEvent();

    protected GameObject currentComponent;
    protected bool[] slotsOccupied;

    protected virtual void Start()
    {
        slotsOccupied = new bool[slots.Length];
        for(int i = 0; i < slots.Length; ++i)
        {
            slots[i].SlotIndex = i;
        }
    }

    public virtual void ReportCollision(int slotIndex, GameObject component)
    {
        slotsOccupied[slotIndex] = true;
        currentComponent = component;
        CheckAssembly();
    }

    public virtual void ReportCollisionEnd(int slotIndex, GameObject component)
    {
        slotsOccupied[slotIndex] = false;
        if (AllSlotsEmpty() == true)
        {
            currentComponent = null;
        }
    }

    protected virtual bool AllSlotsEmpty()
    {
        for (int i = 0; i < slotsOccupied.Length; ++i)
        {
            if (slotsOccupied[i] == true)
            {
                return false;
            }

        }
        return true;
    }

    protected abstract void CheckAssembly();
    protected abstract void AttachComponent(GameObject component);
    protected abstract bool ValidateLatches();
}