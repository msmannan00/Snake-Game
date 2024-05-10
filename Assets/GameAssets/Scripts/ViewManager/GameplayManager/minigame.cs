using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;  // Needed for handling input events

public class minigame : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public Image[] candyPrefabs;
    public float swipeThreshold = 20f; // Minimum distance for a swipe to be registered
    public float animationDuration = 0.25f;

    private int width = 3;
    private int height = 4;
    private Image[,] candies;
    private Vector2[,] candyPositions;

    void Start()
    {
        DOTween.Init();
        candies = new Image[width, height];
        candyPositions = new Vector2[width, height];
        InitializeGrid();
    }

    void InitializeGrid()
    {
        // Loop through each row
        for (int j = 0; j < height; j++)
        {
            // For each row, fill all columns
            for (int i = 0; i < width; i++)
            {
                int candyIndex = Random.Range(0, candyPrefabs.Length);
                Image candy = Instantiate(candyPrefabs[candyIndex], gridLayoutGroup.transform);
                // Calculate position: Note that `i * 100` moves the candy horizontally, `j * 100` vertically
                candy.rectTransform.anchoredPosition = new Vector2(i * 100, j * 100); // Adjust based on your actual cell size
                candies[i, j] = candy;
                candyPositions[i, j] = candy.rectTransform.anchoredPosition;

                // Add event listeners for swiping
                AddSwipeEventTriggers(candy, i, j);
            }
        }
    }

    private void AddSwipeEventTriggers(Image candy, int x, int y)
    {
        EventTrigger trigger = candy.gameObject.GetComponent<EventTrigger>() ?? candy.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry beginEntry = new EventTrigger.Entry();
        beginEntry.eventID = EventTriggerType.PointerDown;
        beginEntry.callback.AddListener((data) => { OnBeginDrag((PointerEventData)data, x, y); });

        EventTrigger.Entry endEntry = new EventTrigger.Entry();
        endEntry.eventID = EventTriggerType.PointerUp;
        endEntry.callback.AddListener((data) => { OnEndDrag((PointerEventData)data, x, y); });

        trigger.triggers.Add(beginEntry);
        trigger.triggers.Add(endEntry);
    }

    private Vector2 beginDragPosition;

    private void OnBeginDrag(PointerEventData data, int x, int y)
    {
        beginDragPosition = data.position;
    }

    private void OnEndDrag(PointerEventData data, int x, int y)
    {
        Vector2 endDragPosition = data.position;
        Vector2 direction = endDragPosition - beginDragPosition;

        if (direction.magnitude > swipeThreshold)
        {
            int targetX = x, targetY = y;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    Debug.Log($"Swiped Right on candy at {x}, {y}");
                    targetY = y;  // Keep y the same, change x for horizontal movement
                    targetX = x + 1;  // Move right in the grid
                }
                else
                {
                    Debug.Log($"Swiped Left on candy at {x}, {y}");
                    targetY = y;  // Keep y the same, change x for horizontal movement
                    targetX = x - 1;  // Move left in the grid
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    Debug.Log($"Swiped Up on candy at {x}, {y}");
                    targetX = x;  // Keep x the same, change y for vertical movement
                    targetY = y - 1;  // Move up in the grid
                }
                else
                {
                    Debug.Log($"Swiped Down on candy at {x}, {y}");
                    targetX = x;  // Keep x the same, change y for vertical movement
                    targetY = y + 1;  // Move down in the grid
                }
            }

            if (targetX >= 0 && targetX < width && targetY >= 0 && targetY < height)
            {
                Debug.Log($"Swiped towards index {targetX}, {targetY}");
                SwapCandies(x, y, targetX, targetY);  // Call to swap the candy images
            }
            else
            {
                Debug.Log("Swipe went out of grid bounds.");
            }
        }
    }


    void SwapCandies(int x1, int y1, int x2, int y2)
    {
        // Swap the sprites of the Image components
        Sprite tempSprite = candies[x1, y1].sprite;
        candies[x1, y1].sprite = candies[x2, y2].sprite;
        candies[x2, y2].sprite = tempSprite;

        // Animate the sprite swap
        AnimateSwap(x1, y1, x2, y2);

        // Check for matches along rows and columns for both swapped candies
        CheckForMatchesAndReplace(x1, y1);
        CheckForMatchesAndReplace(x2, y2);
    }

    void AnimateSwap(int x1, int y1, int x2, int y2)
    {
        DOTween.To(() => candies[x1, y1].rectTransform.localScale, x => candies[x1, y1].rectTransform.localScale = x, new Vector3(1.2f, 1.2f, 1), animationDuration / 2)
               .OnComplete(() => DOTween.To(() => candies[x1, y1].rectTransform.localScale, x => candies[x1, y1].rectTransform.localScale = x, Vector3.one, animationDuration / 2));
        DOTween.To(() => candies[x2, y2].rectTransform.localScale, x => candies[x2, y2].rectTransform.localScale = x, new Vector3(1.2f, 1.2f, 1), animationDuration / 2)
               .OnComplete(() => DOTween.To(() => candies[x2, y2].rectTransform.localScale, x => candies[x2, y2].rectTransform.localScale = x, Vector3.one, animationDuration / 2));
        StaticAudioManager.Instance.playBonusSound();
    }

    void CheckForMatchesAndReplace(int x, int y)
    {
        // Horizontal check
        int count = 1;
        // Check to the left, ensuring index is within bounds
        for (int i = x - 1; i >= 0 && candies[i, y].sprite == candies[x, y].sprite; i--, count++) ;
        // Check to the right, ensuring index is within bounds
        for (int i = x + 1; i < width && candies[i, y].sprite == candies[x, y].sprite; i++, count++) ;

        if (count >= 3)
        {
            for (int i = x - count + 1; i <= x + count - 1 && i < width; i++)
                if (i >= 0 && i < width) // Additional boundary check
                    candies[i, y].sprite = candyPrefabs[Random.Range(0, candyPrefabs.Length)].sprite;
        }

        // Vertical check
        count = 1;
        // Check upwards, ensuring index is within bounds
        for (int j = y - 1; j >= 0 && candies[x, j].sprite == candies[x, y].sprite; j--, count++) ;
        // Check downwards, ensuring index is within bounds
        for (int j = y + 1; j < height && candies[x, j].sprite == candies[x, y].sprite; j++, count++) ;

        if (count >= 3)
        {
            for (int j = y - count + 1; j <= y + count - 1 && j < height; j++)
                if (j >= 0 && j < height) // Additional boundary check
                    candies[x, j].sprite = candyPrefabs[Random.Range(0, candyPrefabs.Length)].sprite;
        }
    }


}
