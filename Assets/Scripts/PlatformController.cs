using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Transform target;
    [SerializeField] private float speedMultiplier = 32f;

    private Material[] materials;
    private Vector3 mOffset;
    private float mZCoord;

    private void Start()
    {
        target.SetParent(null, true);
        rb2D.bodyType = RigidbodyType2D.Static;
        gameObject.layer = 8;
        materials = GetComponent<Renderer>().materials;
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

        SetMaterialsAlpha(.5f);       
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
        StartCoroutine(SetStatic());
    }

    private IEnumerator SetStatic()
    {
        gameObject.layer = 8;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        target.position = transform.position;

        rb2D.velocity = Vector2.zero;

        rb2D.bodyType = RigidbodyType2D.Static;

        SetMaterialsAlpha(1f);

        yield return null;
    }

    private void SetMaterialsAlpha(float alpha)
    {
        foreach (Material material in materials)
        {
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
        }
    }
}
