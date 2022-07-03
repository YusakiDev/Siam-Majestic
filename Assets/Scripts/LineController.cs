using UnityEngine;

public class LineController : Singleton<LineController>
{
    [SerializeField] GameObject linePrefab;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform[] _points;
    private LineConnection _lineConnection;

    private void Awake()
    {
        _lineConnection = LineConnection.Instance;
    }

    public void SetUpLine(Transform[] points)
    {
        foreach (var var in _lineConnection.lines)
        {
            var go =Instantiate(linePrefab, transform.position, Quaternion.identity);
            _lineRenderer = go.GetComponent<LineRenderer>();
            _points = points;
            _lineRenderer.positionCount = _points.Length;
            for (int i = 0; i < _points.Length; i++)
            {
                _lineRenderer.SetPosition(i, _points[i].position);
            }
            
        }
    }
    
}
