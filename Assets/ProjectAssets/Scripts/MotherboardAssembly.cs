using UnityEngine;

public class MotherboardAssembly : MonoBehaviour
{
    public PCIeSlotPart smallSlot;
    public PCIeSlotPart largeSlot;

    [Header("Latches")]
    [SerializeField] private LatchController pcieLatch;
    [SerializeField] private LatchController leftDIIMLatch;
    [SerializeField] private LatchController RightDIIMLatch;

    private GameObject currentGPU;
    private bool smallColliding;
    private bool largeColliding;

    public void ReportCollision(PCIeSlotPart slot, GameObject gpu)
    {
        if (slot == smallSlot)
        {
            smallColliding = true;
            currentGPU = gpu;
        }
        else if (slot == largeSlot)
        {
            largeColliding = true;
            currentGPU = gpu;
        }

        CheckAssembly();
    }

    public void ReportCollisionEnd(PCIeSlotPart slot, GameObject gpu)
    {
        if (slot == smallSlot) smallColliding = false;
        else if (slot == largeSlot) largeColliding = false;

        if (!smallColliding && !largeColliding) currentGPU = null;
    }

    private void CheckAssembly()
    {
        if (pcieLatch.IsLatchOpen && smallColliding && largeColliding && currentGPU != null)
        {
            AttachGPU(currentGPU);
            pcieLatch.CloseLatch(); // Cierra el latch al colocar la GPU
        }
    }


    private void AttachGPU(GameObject gpu)
    {
        GPU gpuComponent = gpu.GetComponent<GPU>();
        if (gpuComponent != null)
        {
            // Aplicar posición y rotación correcta
            gpuComponent.SnapToCorrectPosition(transform);

            // Desactivar componentes
            gpuComponent.DeactivateComponents();
        }
        else
        {
            Debug.LogError("Componente GPU no encontrado en el objeto adjuntado");
        }
    }
}