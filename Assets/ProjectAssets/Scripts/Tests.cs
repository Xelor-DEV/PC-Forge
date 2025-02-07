using UnityEngine;

public class Tests : MonoBehaviour
{
    public GameObject menu; // Referencia al objeto menu
    public GameObject toGenerate; // Referencia al objeto que se generará
    public GameObject spawnPoint; // Referencia al punto de spawn
    public GameObject player; // Referencia al jugador

    private Vector3 initialOffset; // Distancia inicial entre el menú y el jugador

    void Start()
    {
        // Desactivar el menú al inicio
        if (menu != null)
        {
            menu.SetActive(false);
        }

        // Calcular la distancia inicial entre el menú y el jugador (solo en X y Z)
        if (menu != null && player != null)
        {
            initialOffset = menu.transform.position - player.transform.position;
            initialOffset.y = 0; // Ignorar la diferencia en Y
        }
    }

    void Update()
    {
        // Mover el menú de manera relativa al jugador (solo en X y Z)
        if (menu != null && player != null)
        {
            Vector3 newPosition = player.transform.position + initialOffset;
            newPosition.y = menu.transform.position.y; // Mantener la posición original en Y
            menu.transform.position = newPosition;
        }
    }

    // Método para activar/desactivar el menú
    public void ToggleMenu()
    {
        if (menu != null)
        {
            menu.SetActive(!menu.activeSelf);
        }
    }

    // Método para generar un objeto en el spawnPoint
    public void GenerateObject()
    {
        if (toGenerate != null && spawnPoint != null)
        {
            Instantiate(toGenerate, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }
}