using UnityEngine;
using System.Collections.Generic;

public class PCIeSlotPart : MonoBehaviour
{
    public string requiredTag;
    public MotherboardAssembly motherboardAssembly;
    public float topThreshold = 0.95f; // Umbral para detección de cara superior

    private BoxCollider boxCollider;
    private HashSet<Collider> validCollisions = new HashSet<Collider>();

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
            Debug.LogError("BoxCollider no encontrado en " + gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag) && IsTopCollision(other))
        {
            validCollisions.Add(other);
            GameObject gpu = other.transform.parent.gameObject;
            motherboardAssembly.ReportCollision(this, gpu);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (validCollisions.Contains(other))
        {
            validCollisions.Remove(other);
            GameObject gpu = other.transform.parent.gameObject;
            motherboardAssembly.ReportCollisionEnd(this, gpu);
        }
    }

    private bool IsTopCollision(Collider other)
    {
        // Calcular punto más cercano en coordenadas mundiales
        Vector3 closestPoint = boxCollider.ClosestPoint(other.transform.position);

        // Calcular dirección desde el centro del collider al punto de colisión
        Vector3 collisionDirection = closestPoint - transform.TransformPoint(boxCollider.center);

        // Obtener la dirección superior normalizada del slot
        Vector3 slotUp = transform.up;

        // Calcular similitud direccional
        float directionSimilarity = Vector3.Dot(collisionDirection.normalized, slotUp);

        return directionSimilarity >= topThreshold;
    }
}