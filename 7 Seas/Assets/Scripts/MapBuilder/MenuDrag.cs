using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuDrag : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas menu;
    public Button[] menuBtns;

    private Vector2 mousePos;
    private RectTransform transform;

    void Start()
    {
        transform = menu.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
           mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (RectTransformUtility.RectangleContainsScreenPoint(transform, mousePos))
            {
                transform.position = mousePos;
            }
        }
    }

    /*
    public IEnumerator DragOff()
    {
        drag = false;

        yield return new WaitForSeconds(1);

        drag = true;
    }
    */
}
