using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TrashDetector : MonoBehaviour
{

    MoveSpiralRoot MoveSplineRoot;
    public Transform trashPoint;
    // Start is called before the first frame update
    void Start()
    {
        MoveSplineRoot = MoveSpiralRoot.Instance;
    }

    // Update is called once per frame



    private void OnTriggerEnter(Collider other)
    {
        Candy _candy = other.GetComponent<Candy>();

        if (_candy)
        {
            if (_candy.IsCandyRoten)
            {
                MoveSplineRoot.CandyTrashed(_candy);
                _candy.transform.DOMove(trashPoint.transform.position, 0.5f);
            }
        }
        

    }
}
