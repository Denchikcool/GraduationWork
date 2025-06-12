using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float _startPosition;
    private float _backgroundLength;

    [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private float _parallaxEffect;

    private void Start()
    {
        _startPosition = transform.position.x;
        _backgroundLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float distance = _camera.transform.position.x * _parallaxEffect;
        float movement = _camera.transform.position.x * (1 - _parallaxEffect);
        transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);

        if(movement > _startPosition + _backgroundLength)
        {
            _startPosition += _backgroundLength;
        }
        else if(movement < _startPosition - _backgroundLength)
        {
            _startPosition -= _backgroundLength;
        }
    }
}

