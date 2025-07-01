using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class LastGrabbedTracker : NonPersistentSingleton<LastGrabbedTracker>
{
    //public static LastGrabbedTracker Instance { get; private set; }

    [Header("Configuración")]
    [SerializeField] private string _motherboardTag = "Motherboard";
    [SerializeField] private string _coolerTag = "Cooler";

    [SerializeField] private GameObject _lastMotherboard;
    [SerializeField] private GameObject _lastCooler;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Instance = this;
        }
    }

    public void RegisterGrab(GameObject grabbedObject)
    {
        if (grabbedObject.CompareTag(_motherboardTag))
        {
            _lastMotherboard = grabbedObject;
        }
        else if (grabbedObject.CompareTag(_coolerTag))
        {
            _lastCooler = grabbedObject;
        }
    }
}
public class NonPersistentSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var objs = Object.FindObjectsByType(typeof(T), FindObjectsSortMode.None) as T[];
                if (objs.Length > 0)
                    _instance = objs[0];
                if (objs.Length > 1)
                {
                    Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                }
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}