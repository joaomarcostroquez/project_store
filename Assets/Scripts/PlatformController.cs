using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Transform target;
    [SerializeField] private float speedMultiplier = 16f;

    private Material material;
    private Vector3 mOffset;
    private float mZCoord;

    private void Start()
    {
        target.SetParent(null, true);
        rb2D.bodyType = RigidbodyType2D.Static;
        gameObject.layer = 0;
        material = GetComponent<Renderer>().material;
    }

    private void FixedUpdate()
    {
        rb2D.velocity = ((Vector2)target.transform.position - rb2D.position) * speedMultiplier;
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(target.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = target.transform.position - GetMouseAsWorldPoint();

        rb2D.bodyType = RigidbodyType2D.Dynamic;

        gameObject.layer = 7;

        material.color = new Color(material.color.r, material.color.g, material.color.b, 0.5f);
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        target.transform.position = GetMouseAsWorldPoint() + mOffset;
    }

    private void OnMouseUp()
    {
        target.position = transform.position;

        rb2D.velocity = Vector2.zero;

        rb2D.bodyType = RigidbodyType2D.Static;

        gameObject.layer = 0;

        material.color = new Color(material.color.r, material.color.g, material.color.b, 1f);
    }
}
