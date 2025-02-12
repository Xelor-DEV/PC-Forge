using UnityEngine;

public class PCIeSlotPart : MonoBehaviour
{
    public string requiredTag; // Asignar "GPUSmall" o "GPULarge" en el Inspector
    public MotherboardAssembly motherboardAssembly;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(requiredTag))
        {
            GameObject gpu = collision.gameObject;
            motherboardAssembly.ReportCollision(this, gpu);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(requiredTag))
        {
            GameObject gpu = collision.transform.parent.gameObject;
            motherboardAssembly.ReportCollisionEnd(this, gpu);
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            GameObject gpu = other.transform.parent.gameObject;
            motherboardAssembly.ReportCollisionEnd(this, gpu);
        }
    }
    */
}