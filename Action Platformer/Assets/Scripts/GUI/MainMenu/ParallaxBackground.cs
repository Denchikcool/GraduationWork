using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Vector2 _startPosition;

    [SerializeField]
    private int _moveModifier;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        Vector2 position = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float positionX = Mathf.Lerp(transform.position.x, _startPosition.x + (position.x * _moveModifier), 2.0f * Time.deltaTime);
        float positionY = Mathf.Lerp(transform.position.y, _startPosition.y + (position.y * _moveModifier), 2.0f * Time.deltaTime);

        transform.position = new Vector3(positionX, positionY, 0);
    }
}

