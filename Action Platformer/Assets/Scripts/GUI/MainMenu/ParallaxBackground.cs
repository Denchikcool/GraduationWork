using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Vector3 _startPosition;
    private Canvas _canvas;
    private RectTransform _canvasRect;

    [SerializeField, Range(0.1f, 5f)]
    private float _moveSpeed = 2f;

    [SerializeField, Range(0f, 1f)]
    private float _parallaxEffect = 0.5f;

    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _canvasRect = _canvas.GetComponent<RectTransform>();
        _startPosition = transform.localPosition;
    }

    private void Update()
    {
        Vector2 localMousePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, Input.mousePosition, _canvas.worldCamera, out localMousePos);

        Vector2 normalizedMousePos = new Vector2(localMousePos.x / (_canvasRect.rect.width * 0.5f), localMousePos.y / (_canvasRect.rect.height * 0.5f));

        Vector3 targetPosition = _startPosition + new Vector3(normalizedMousePos.x * _parallaxEffect * 100f, normalizedMousePos.y * _parallaxEffect * 100f, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, _moveSpeed * Time.deltaTime);
    }
}