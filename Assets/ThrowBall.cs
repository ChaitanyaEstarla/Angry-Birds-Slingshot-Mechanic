using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public Rigidbody2D ballRigidbody;
    public CircleCollider2D ballCollider;
    
    public Vector3 ballPosition
    {
        get { return transform.position; }
    }
    
    void Awake()
    {
            ballRigidbody = GetComponent<Rigidbody2D>();
            ballCollider = GetComponent<CircleCollider2D>();
    }

    public void Push(Vector2 force)
    {
        ballRigidbody.AddForce(force, ForceMode2D.Impulse);
        
    }

    public void ActivateRigidBody()
    {
        ballRigidbody.isKinematic = false;
    }

    public void DeActivateRigidBody()
    {
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = 0f;
        ballRigidbody.isKinematic = true;
    }
}
