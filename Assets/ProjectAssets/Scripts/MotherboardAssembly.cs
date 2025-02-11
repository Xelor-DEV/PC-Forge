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

    private void AttachGPU(GameObject gpu)
    {
        gpu.transform.parent = this.transform;
        GPU tmp = gpu.GetComponent<GPU>();
        tmp.DeactivateComponents();
    }
}