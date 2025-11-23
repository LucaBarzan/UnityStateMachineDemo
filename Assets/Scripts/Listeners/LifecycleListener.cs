using UnityEngine;
using UnityEngine.Events;

public class LifecycleListener : MonoBehaviour
{
    public UnityEvent OnAwake;
    public UnityEvent _OnEnable;
    public UnityEvent OnStart;
    public UnityEvent _OnDisable;
    public UnityEvent _OnDestroy;

    private void Awake() => OnAwake?.Invoke();

    private void OnEnable() => _OnEnable?.Invoke();

    private void Start() => OnStart?.Invoke();


    private void OnDisable() => _OnDisable?.Invoke();

    private void OnDestroy() => _OnDestroy?.Invoke();
}