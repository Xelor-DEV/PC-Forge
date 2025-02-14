using UnityEngine;

public class MotherboardAssembly : MonoBehaviour
{
    public PCIeSlotPart smallSlot;
    public PCIeSlotPart largeSlot;

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
        if (smallColliding && largeColliding && currentGPU != null)
        {
            AttachGPU(currentGPU);
        }
    }
    /*
    private void AttachGPU(GameObject gpu)
    {
        // Guardar la escala y rotaci?n original de la GPU
        Vector3 originalScale = gpu.transform.localScale;
        Quaternion originalRotation = gpu.transform.rotation;

        // Hacer que la GPU sea hija de la motherboard
        gpu.transform.SetParent(transform);

        // Restaurar la escala y rotaci?n original
        gpu.transform.localScale = originalScale;
        gpu.transform.rotation = originalRotation;
        GPU tmp = gpu.GetComponent<GPU>();
        tmp.DeactivateComponents();
    }
    */

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