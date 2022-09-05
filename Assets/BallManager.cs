using System;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;
    
    public bool isDragging = false;
    [SerializeField]
    public float pushForce = 4f;
    
    public ThrowBall ball;
    public Camera mainCamera;
    public Trajectory trajectory;

    private Vector2 m_StartingPoint;
    private Vector2 m_EndPoint;
    private Vector2 m_Direction;
    private Vector2 m_Force;
    private float m_Distance;
    
    private void Start()
    {
        mainCamera = Camera.main;
        //We don't want the ball to fall down immediately after the game starts
        ball.DeActivateRigidBody();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if (isDragging)
        {
            OnDrag();
        }
    }

    private void OnDragStart()
    {
        ball.DeActivateRigidBody();
        m_StartingPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        trajectory.Show();
    }
    
    private void OnDragEnd()
    {
        ball.ActivateRigidBody();
        ball.Push(m_Force);
        trajectory.Hide();
    }
    
    private void OnDrag()
    {
        m_EndPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        m_Distance = Vector2.Distance(m_StartingPoint, m_EndPoint);
        m_Direction = (m_StartingPoint - m_EndPoint).normalized;
        m_Force = m_Direction * m_Distance * pushForce;
        
        Debug.DrawLine(m_StartingPoint, m_EndPoint);
        
        trajectory.UpdateDots(ball.ballPosition, m_Force);
    }
}
