using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private int dotsNumber;
    [SerializeField] private GameObject dotsMain;
    [SerializeField] private GameObject dotsPrefab;
    [SerializeField] private float dotSpacing;
    [SerializeField] [Range(0.01f, 0.03f)] private float dotMinScale;
    [SerializeField] [Range(0.3f, 1f)]     private float dotMaxScale;


    private Transform[] m_Dots;

    private Vector2 m_Position;
    private float m_TimeStamp;
    
    
    public void Start()
    {
        Hide();
        PrepareDots();
    }

    private void PrepareDots()
    {
        m_Dots = new Transform[dotsNumber];
        dotsPrefab.transform.localScale = Vector3.one * dotMaxScale;

        var scale = dotMaxScale;

        for (var i = 0; i < dotsNumber; i++)
        {
            m_Dots[i] = Instantiate(dotsPrefab, null).transform;
            m_Dots[i].parent  = dotsMain.transform;

            m_Dots[i].localScale = Vector3.one * scale;
        }
    }

    public void UpdateDots(Vector3 ballPosition, Vector2 forceApplied)
    {
        m_TimeStamp = dotSpacing;
        for (var i = 0; i < dotsNumber; i++)
        {
            m_Position.x = (ballPosition.x + forceApplied.x * m_TimeStamp);
            m_Position.y = (ballPosition.y + forceApplied.y * m_TimeStamp) - (Physics2D.gravity.magnitude * m_TimeStamp * m_TimeStamp) / 2f;
            m_Dots[i].position = m_Position;
            m_TimeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotsMain.SetActive(true);
    }

    public void Hide()
    {
        dotsMain.SetActive(false);
    }
}
