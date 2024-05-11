using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMovController : MonoBehaviour
{
    bool isTouching;
    private bool isMousePressed = false;
    public Transform FocusedObj;
    public float followSpeed = 1;
    [HideInInspector]
    public LayerMask DetectionMask;
    MoveSpiralRoot MoveSpiralController;
    // Start is called before the first frame update
    void Start()
    {
        MoveSpiralController = MoveSpiralRoot.Instance;
    }

    Candy candyObj;
    Vector3 currentFocusedPoint;
    void Update()
    {
        if (MoveSpiralController.StackedCandies.Count == 0) return;

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000,DetectionMask)) 
            {
                GameObject hitObject = hit.collider.gameObject;

                Debug.Log("Hit object: " + hitObject.name);
                isMousePressed = true;

                FocusedObj = MoveSpiralController.baseCandy.transform; 
                currentFocusedPoint = hit.point;
            }
            else
            {
                FocusedObj = null;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
            FocusedObj = null;
            Debug.Log("Mouse button released");
        }

        /////////////////////////detecting touch input
        ///

#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000, DetectionMask))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    Debug.Log("Hit object: " + hitObject.name);
                    isTouching = true;

                    FocusedObj = MoveSpiralController.baseCandy.transform;
                    currentFocusedPoint = hit.point;
                }
                else
                {
                    FocusedObj = null;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isTouching = false;
                FocusedObj = null;
                Debug.Log("Touch ended");
            }
        }
#endif
        if (isMousePressed || isTouching)
        {

            if (FocusedObj)
            {
                Debug.LogError("trying to move : "+ FocusedObj.name);
                FocusedObj.transform.position = Vector3.Lerp(FocusedObj.transform.position,
                    new Vector3(currentFocusedPoint.x,
                    MoveSpiralController.BaseTransform.position.y, MoveSpiralController.BaseTransform.position.z -( MoveSpiralController.CandyGap * (MoveSpiralController.StackedCandies.Count -1)))
                    , Time.deltaTime * followSpeed);
            }
        }
    }
}
