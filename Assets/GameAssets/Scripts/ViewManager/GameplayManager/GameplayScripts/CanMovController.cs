using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMovController : MonoBehaviour
{
    private bool isMousePressed = false;
    public Transform FocusedObj;
    public float followSpeed = 1;
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


        if (isMousePressed)
        {

            if (FocusedObj && !userSessionManager.Instance.mIsMenuOpened)
            {
                Debug.LogError("trying to move object");
                FocusedObj.transform.localPosition = Vector3.Lerp(FocusedObj.transform.localPosition,
                    new Vector3(-currentFocusedPoint.x, 
                    FocusedObj.transform.localPosition.y, MoveSpiralController.BaseTransform.localPosition.z +( MoveSpiralController.baseCandy.width * (MoveSpiralController.StackedCandies.Count -1)))
                    , Time.deltaTime * followSpeed);
            }
        }
    }
}
