using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField, Tooltip("ћинимальное рассто€ние между точками")]
    private float _minDistPoint;

    [SerializeField]
    private bool _oneLineDrawMod;

    [SerializeField, Tooltip("Ќомер сло€ где нельз€ рисовать линию")]
    private int _layerMask;

    [SerializeField]
    private GameObject _linePrefab;

    private LineRenderer _lineRenderer;

    private EdgeCollider2D _edgeCollider;

    private List<Vector2> _linePath;

    private GameObject _line;

    private Vector2? _direction;
    private Vector2? _lastPoint;

    public bool LineStartDraw { get => _lineStartDraw; }
    private bool _lineStartDraw = false;
    private bool _lineReady = false;


    private void Initialaze(GameObject line)
    {
        _lineRenderer = line.GetComponent<LineRenderer>();
        _edgeCollider = line.GetComponent<EdgeCollider2D>();

        _linePath = new List<Vector2>();
        _direction = null;
        _lastPoint = null;
    }

    public void TryStartDrawLine()
    {
        if (!_lineReady && PointerOnDrawZone(_layerMask, out RaycastHit2D[] hit))
        {
            StartDrawLine(hit[0].point);
            _lineStartDraw = true;
        }
    }

    private void StartDrawLine(Vector2 startPoint)
    {
        _line = Instantiate(_linePrefab);
        Initialaze(_line);
        AddNextPoint(startPoint);
    }

    public void TryAddNextPoint()
    {
        if (PointerOnDrawZone(_layerMask, out RaycastHit2D[] hit))
        {
            var nextPoint = hit[0].point;
            var direction = nextPoint - _lastPoint;
            if (_direction != direction && Vector3.Distance(_lastPoint.Value, nextPoint) >= _minDistPoint)
            {
                _direction = direction;
                AddNextPoint(nextPoint);
            }
        }
        else
            TryFinishDrawLine();
    }

    private void AddNextPoint(Vector2 point)
    {
        var countPoins = _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(countPoins, point);
        _lastPoint = point;
        _linePath.Add(point);
    }

    public bool TryFinishDrawLine()
    {
        _lineStartDraw = false;

        if (_lineRenderer.positionCount <= 1)
        {
            Destroy(_line);
            return false;
        }
        _edgeCollider.points = _linePath.ToArray();

        if (_oneLineDrawMod)
            _lineReady = true;
        return true;
    }

    private bool PointerOnDrawZone(LayerMask DontDrawlayer, out RaycastHit2D[] hits)
    {
        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hits = Physics2D.RaycastAll(ray, Vector2.zero);
        if (hits != null)
        {
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.layer == DontDrawlayer)
                {
                    return false;
                }
            }
        }
        return hits.Length > 0;
    }
}
