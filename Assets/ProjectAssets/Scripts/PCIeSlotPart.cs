using UnityEngine;

public class PCIeSlotPart : MonoBehaviour
{
    public string requiredTag; // Asignar "GPUSmall" o "GPULarge" en el Inspector
    public MotherboardAssembly motherboardAssembly;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            // Obtener el objeto raíz de la GPU (ajusta según tu jerarquía)
            GameObject gpu = other.transform.parent.gameObject;
            motherboardAssembly.ReportCollision(this, gpu);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            GameObject gpu = other.transform.parent.gameObject;
            motherboardAssembly.ReportCollisionEnd(this, gpu);
        }
    }
}