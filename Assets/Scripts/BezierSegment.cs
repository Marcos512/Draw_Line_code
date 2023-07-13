using UnityEngine;

public class BezierSegment : MonoBehaviour
{
    public Transform StartPoint;
    public Transform P1;
    public Transform P2;
    public Transform FinishPoint;

    [SerializeField, Header("Debug")]
    private bool _drawCurve;
    [SerializeField]
    private bool _drawPoints;

    [SerializeField, Min(0)]
    private int _segmentCount = 50;

    public BezierSegment(Transform a1, Transform p1, Transform p2, Transform a2)
    {
        StartPoint = a1;
        P1 = p1;
        P2 = p2;
        FinishPoint = a2;
    }
    private void OnDrawGizmos()
    {
        if (_drawCurve)
        {
            Vector3 preveousePoint = StartPoint.position;

            for (int i = 0; i < _segmentCount + 1; i++)
            {
                float parameter = (float)i / _segmentCount;
                Vector3 point = Bezier.GetPoint(StartPoint.position, P1.position,
                    P2.position, FinishPoint.position, parameter);
                Gizmos.DrawLine(preveousePoint, point);
                preveousePoint = point;
            }
            Gizmos.color = Color.black;
            Gizmos.DrawLine(StartPoint.position, P1.position);
            Gizmos.DrawLine(P2.position, FinishPoint.position);
        }

        if (_drawPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(P1.position, 0.4f);
            Gizmos.DrawSphere(P2.position, 0.4f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(StartPoint.position, 0.4f);
            Gizmos.DrawSphere(FinishPoint.position, 0.4f);
        }
    }
}

