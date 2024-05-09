using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour, PageController
{
    int mCurrentLevel = 1;
    public GridLayoutGroup gridLayoutGroup;
    public GameObject aScrollViewContent;


    public void onInit(Dictionary<string, object> data)
    {
        mCurrentLevel = PlayerPrefs.GetInt("current_level", 1);
        UpdateCellSize();
        onInitScroll();
    }

    void onInitScroll()
    {
        for (int index = 0; index < userSessionManager.Instance.totalLevel; index++)
        {
            GameObject mLevel = Instantiate(Resources.Load<GameObject>("Prefabs/level/levelItem"));
            mLevel.name = "Category_" + index;
            mLevel.transform.SetParent(aScrollViewContent.transform, false);
            LevelItemController mLevelItemController = mLevel.GetComponent<LevelItemController>();
            mLevelItemController.InitCategory(mCurrentLevel, index, gameObject);
        }
    }

    void UpdateCellSize()
    {
        //int size = gridLayoutGroup.cellSize.y;
        //gridLayoutGroup.cellSize = new Vector2(size*1.2f, size * 1.2f);
    }

}
