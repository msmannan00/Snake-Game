using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    public ColorCode MColorCode;
    public bool HasAlreadyStacked = false;
  
    public float CandyFollowSpeed = 3;
    public bool IsBaseCandy = false;
    public Transform FollowLead;
    MeshFilter meshFilter;
    public bool IsCandyRoten = false;

    MoveSpiralRoot SpiralRootController;
    public bool IsTrashed = false;
    public bool IsBaked = false;
    public Candy TailCandy;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        SpiralRootController = MoveSpiralRoot.Instance;
       
    }

    void Update()
    {

        if (IsBaseCandy)
            FollowLead = null;
        if (IsTrashed || IsBaked) return;

       // UpZAxis = FollowLead ?  FollowLead.transform.position.z - width : SpiralRootController.BaseTransform.localPosition.z;
        if (HasAlreadyStacked && !IsBaseCandy && FollowLead)
        {
            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(FollowLead.transform.position.x, SpiralRootController.BaseTransform.position.y, FollowLead.transform.position.z + SpiralRootController.CandyGap),Time.deltaTime * CandyFollowSpeed);
        }
       
       

    }

   

    private void OnTriggerEnter(Collider other)
    {
        Candy _candy;
        if(_candy = other.GetComponent<Candy>())
        {
            if (!_candy.HasAlreadyStacked)
            {
                SpiralRootController.RegisterNewCandy(_candy);
                StaticAudioManager.Instance.playEatingSound();
            }
        }

    }
}
