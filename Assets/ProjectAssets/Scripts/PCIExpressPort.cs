using UnityEngine;

public class PCIExpressPort : InternalSlot
{
    protected override void CheckAssembly()
    {
        if (ValidateLatches() == true && AllSlotsOccupied() == true)
        {
            AttachComponent(currentComponent);
            CloseLatches();
        }
    }

    protected override void AttachComponent(GameObject component)
    {
        InternalHardware gpu = component.GetComponent<InternalHardware>();
        if (gpu != null)
        {
            gpu.SnapToCorrectPosition(transform);
            gpu.DeactivateComponents();
            OnComponentAttached.Invoke();
        }
        else
        {
            Debug.LogError("Componente GPU no encontrado en el objeto adjuntado");
        }
    }

    protected override bool ValidateLatches()
    {
        return latches[0].IsLatchOpen; // Solo necesita un latch
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

    private void CloseLatches()
    {
        for (int i = 0; i < latches.Length; ++i)
        {
            latches[i].CloseLatch();
        }
    }
}