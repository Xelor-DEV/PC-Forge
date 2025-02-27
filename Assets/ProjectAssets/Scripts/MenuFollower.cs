using UnityEngine;

public class MenuFollower : MonoBehaviour
{
    [SerializeField] private GameObject menu; // Referencia al objeto menu
    [SerializeField] private GameObject player; // Referencia al jugador

    private Vector3 initialOffset; // Distancia inicial entre el men� y el jugador

    void Start()
    {
        // Calcular la distancia inicial entre el men� y el jugador (solo en X y Z)
        if (menu != null && player != null)
        {
            initialOffset = menu.transform.position - player.transform.position;
            initialOffset.y = 0; // Ignorar la diferencia en Y
        }
    }

    void Update()
    {
        // Mover el men� de manera relativa al jugador (solo en X y Z)
        if (menu != null && player != null)
        {
            Vector3 newPosition = player.transform.position + initialOffset;
            newPosition.y = menu.transform.position.y; // Mantener la posici�n original en Y
            menu.transform.position = newPosition;
        }
    }

    // M�todo para activar/desactivar el men�
    public void ToggleMenu()
    {
        if (menu != null)
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}