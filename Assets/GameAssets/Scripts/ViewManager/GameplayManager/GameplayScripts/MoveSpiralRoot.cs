using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveSpiralRoot : MonoBehaviour
{
    public float moveSpeed = 1;
    public Transform BaseTransform;
    public Candy baseCandy;
    [SerializeField]
    public List<Candy> StackedCandies = new List<Candy>();
    public int QueueLenght = 5;
    public static MoveSpiralRoot Instance;

    public int currentQueueLength = 0;
    
    private void Awake()
    {
        if(Instance == null)
              Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        baseCandy.HasAlreadyStacked = true;
        StackedCandies.Add(baseCandy);
    }

    // Update is called once per frame
    void Update()
    {
        if (StackedCandies.Count == 0) return;
        currentQueueLength =  StackedCandies.Count;
        transform.localPosition -= Vector3.forward * Time.deltaTime * moveSpeed;
        UpdateHead();
    }


    void UpdateHead()
    {
        for (int i = 0; i < StackedCandies.Count; i++)
        {
            if (i == StackedCandies.Count - 1)
                continue;

            StackedCandies[i].FollowLead = StackedCandies[i + 1].transform;
        }

        for (int i = 0; i < StackedCandies.Count; i++)
        {
            if (i == 0)
                continue;

            StackedCandies[i].TailCandy = StackedCandies[i - 1];
        }
    }

    void UpdateZLength()
    {
      
    }

    public void RegisterNewCandy(Candy newCandy)
    {
        if (StackedCandies == null && StackedCandies.Count >= QueueLenght) {

            Debug.LogError("Limit Reached");
            return;
        }
        //  newCandy.UpZAxis = BaseTransform.localPosition.z +  newCandy.width * StackedCandies.Count;
        baseCandy.IsBaseCandy = false;
        newCandy.HasAlreadyStacked = true;
        newCandy.TailCandy = baseCandy;
        baseCandy = newCandy;
        baseCandy.IsBaseCandy = true;
        newCandy.transform.SetParent (this.transform);
        StackedCandies.Add(newCandy);
    }

    public void CandyTrashed(Candy _trashedCandy)
    {
        _trashedCandy.IsTrashed = true;
        if (_trashedCandy.IsBaseCandy)
        {
            if (_trashedCandy.TailCandy)
            {
                _trashedCandy.TailCandy.IsBaseCandy = true;
                baseCandy = _trashedCandy.TailCandy;
            }
                _trashedCandy.FollowLead = null;
        }
        else
        {
            if (_trashedCandy.TailCandy)
            {
                _trashedCandy.TailCandy.FollowLead = _trashedCandy.FollowLead;
            }
        }
        _trashedCandy.IsBaseCandy = false;
        _trashedCandy.transform.parent = null;
        StackedCandies.Remove(_trashedCandy);
    }

    public void CandyBaked(Candy _bakedCandy)
    {
        _bakedCandy.IsBaked = true;
        _bakedCandy.transform.parent = null;
        if (_bakedCandy.IsBaseCandy)
        {
            if (_bakedCandy.TailCandy)
            {
                _bakedCandy.TailCandy.IsBaseCandy = true;
                baseCandy = _bakedCandy.TailCandy;
            }
                _bakedCandy.IsBaseCandy = false;
                _bakedCandy.FollowLead = null;
        }
        else
        {
            if (_bakedCandy.TailCandy)
            {
                _bakedCandy.TailCandy.FollowLead = _bakedCandy.FollowLead;
            }
        }

        _bakedCandy.transform.parent = null;
        StackedCandies.Remove(_bakedCandy);

    }
    

    public int GetCandyIndex(Candy candy)
    {
        for (int i = 0; i < StackedCandies.Count; i++)
        {
            if (StackedCandies[i] == candy)
                return (StackedCandies.Count-1) - i;
        }

        return 1;
    }
    //public Stack<Candy> GetStack()
    //{
    //    return StackedCandies;
    //}

    //[CustomEditor(typeof(MoveSpiralRoot))]
    //public class GameObjectQueueEditor : Editor
    //{
    //    public override void OnInspectorGUI()
    //    {
    //        DrawDefaultInspector();

    //        MoveSpiralRoot queueScript = (MoveSpiralRoot)target;
    //        List<Candy> gameObjectList = new List<Candy>(queueScript.GetStack());

    //        GUILayout.Space(10);
    //        GUILayout.Label("Queue Contents:");

    //        foreach (Candy obj in gameObjectList)
    //        {
    //            EditorGUILayout.ObjectField(obj, typeof(Candy), false);
    //        }
    //    }
    //}

}


