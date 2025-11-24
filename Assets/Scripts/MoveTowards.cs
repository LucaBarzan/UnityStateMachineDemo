using UnityEngine;
using UnityEngine.Events;

public class MoveTowards : MonoBehaviour
{
    private enum MoveState
    {
        Idle,
        Moving
    }

    public UnityEvent OnStartedMoving => onStartedMoving;
    public UnityEvent OnReachedTarget => onReachedTarget;

    [Header("Events")]
    [SerializeField] private UnityEvent onStartedMoving;
    [SerializeField] private UnityEvent onReachedTarget;

    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float stopDistance = 0.1f;

    private MoveState state = MoveState.Idle;

    private void Awake()
    {
        SetTarget(target);
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case MoveState.Moving:
                MoveToTarget();
                break;

            case MoveState.Idle:
                if (!IsAtTarget())
                {
                    onStartedMoving?.Invoke();
                    state = MoveState.Moving;
                }
                break;

        }

    }

    public void SetTarget(Transform newTarget) => target = newTarget;

    private void MoveToTarget()
    {
        if (IsAtTarget())
        {
            onReachedTarget?.Invoke();
            state = MoveState.Idle;
            return;
        }

        switch (_rigidbody2D.bodyType)
        {
            case RigidbodyType2D.Dynamic:
                MoveDynamic();
                break;

                case RigidbodyType2D.Kinematic:
                MoveKinematic();
                break;
        }
    }

    private void MoveDynamic()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        _rigidbody2D.linearVelocity = direction * speed * Time.fixedDeltaTime;
    }

    private void MoveKinematic()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        _rigidbody2D.MovePosition(_rigidbody2D.position + direction * speed * Time.fixedDeltaTime);
    }

    private bool IsAtTarget()
    {
        if (target == null)
            return true;

        return _rigidbody2D.position.IsCloseTo(target.position, stopDistance);
    }
}