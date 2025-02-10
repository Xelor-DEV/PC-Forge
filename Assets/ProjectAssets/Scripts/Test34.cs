using UnityEngine;

public class Test34 : MonoBehaviour
{
    private bool isCollidingWithPlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisionó es el "Player"
        if (other.gameObject.tag == "Player")
        {
            isCollidingWithPlayer = true;
            Debug.Log("entro");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el "Player" deja de colisionar, desactiva la bandera
        if (other.gameObject.tag == "Player")
        {
            isCollidingWithPlayer = false;
            Debug.Log("salio");
        }
    }
}