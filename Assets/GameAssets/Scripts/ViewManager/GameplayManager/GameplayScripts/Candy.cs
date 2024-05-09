using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    public ColorCode MColorCode;
    public bool HasAlreadyStacked = false;
    [HideInInspector]
    public float UpZAxis;
    [HideInInspector]
    public float length;
    [HideInInspector]
    public float width;
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
        CalculateBounds();
    }

    void Update()
    {

        if (IsBaseCandy)
            FollowLead = null;
        if (IsTrashed || IsBaked) return;

        UpZAxis = FollowLead ?  FollowLead.transform.position.z - width : SpiralRootController.BaseTransform.localPosition.z;
        if (HasAlreadyStacked && !IsBaseCandy && FollowLead)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, 
                new Vector3(FollowLead.transform.localPosition.x, FollowLead.transform.localPosition.y, FollowLead.transform.localPosition.z - width),Time.deltaTime * CandyFollowSpeed);
        }
       
       

    }

    void CalculateBounds()
    {
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            // Get the mesh bounds
            Bounds bounds = meshFilter.sharedMesh.bounds;

            // Calculate width and height in world space
            float width = bounds.size.x * transform.localScale.x;
            float height = bounds.size.z * transform.localScale.z;
            this.width = width;
            this.length = height;
            Debug.Log(this.width + " : width");
          
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
            }
        }

    }
}
