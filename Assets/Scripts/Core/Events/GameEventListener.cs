using UnityEngine;
using UnityEngine.Events;


public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to listen to")]
    [SerializeField] private GameEventSO gameEvent;

    [Tooltip("Response when event is raised")]
    [SerializeField] private UnityEvent response;

    private void OnEnable()
    {
        if (gameEvent != null)
        {
            gameEvent.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        if (gameEvent != null)
        {
            gameEvent.UnregisterListener(this);
        }
    }

    public void OnEventRaised()
    {
        response?.Invoke();
    }
}