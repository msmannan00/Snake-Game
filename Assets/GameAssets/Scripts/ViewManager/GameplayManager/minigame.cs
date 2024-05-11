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
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                int candyIndex = Random.Range(0, candyPrefabs.Length);
                Image candy = Instantiate(candyPrefabs[candyIndex], gridLayoutGroup.transform);
                candy.rectTransform.anchoredPosition = new Vector2(i * 100, j * 100); // Adjust based on your actual cell size
                candies[i, j] = candy;
                candyPositions[i, j] = candy.rectTransform.anchoredPosition;

                while ((i >= 2 && candies[i - 1, j].sprite == candy.sprite && candies[i - 2, j].sprite == candy.sprite) ||
                       (j >= 2 && candies[i, j - 1].sprite == candy.sprite && candies[i, j - 2].sprite == candy.sprite))
                {
                    candy.sprite = candyPrefabs[Random.Range(0, candyPrefabs.Length)].sprite;
                }

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
        // Get initial positions
        Vector2 pos1 = candies[x1, y1].rectTransform.anchoredPosition;
        Vector2 pos2 = candies[x2, y2].rectTransform.anchoredPosition;

        // Animate visual movement from pos1 to pos2 and vice versa
        candies[x1, y1].rectTransform.DOAnchorPos(pos2, animationDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            // Reset the position to original after animation to maintain grid integrity
            candies[x1, y1].rectTransform.anchoredPosition = pos1;
        });
        candies[x2, y2].rectTransform.DOAnchorPos(pos1, animationDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            // Reset the position to original after animation to maintain grid integrity
            candies[x2, y2].rectTransform.anchoredPosition = pos2;
        });

        // Play sound effect
    }

    void CheckForMatchesAndReplace(int x, int y)
    {
        // Horizontal check
        int leftCount = 0;
        for (int i = x - 1; i >= 0 && candies[i, y].sprite == candies[x, y].sprite; i--, leftCount++) ;
        int rightCount = 0;
        for (int i = x + 1; i < width && candies[i, y].sprite == candies[x, y].sprite; i++, rightCount++) ;

        int totalHorizontal = 1 + leftCount + rightCount;
        if (totalHorizontal == 3 || totalHorizontal == 4) // Check for exactly three or four candies
        {
            for (int i = x - leftCount; i <= x + rightCount; i++)
            {
                ChangeCandySpriteWithAnimation(candies[i, y]);
            }
        }

        // Vertical check (repeated logic for vertical matches if needed)
        int upCount = 0;
        for (int j = y - 1; j >= 0 && candies[x, j].sprite == candies[x, y].sprite; j--, upCount++) ;
        int downCount = 0;
        for (int j = y + 1; j < height && candies[x, j].sprite == candies[x, y].sprite; j++, downCount++) ;

        int totalVertical = 1 + upCount + downCount;
        if (totalVertical == 3 || totalVertical == 4) // Check for exactly three or four candies
        {
            for (int j = y - upCount; j <= y + downCount; j++)
            {
                ChangeCandySpriteWithAnimation(candies[x, j]);
                StaticAudioManager.Instance.playBonusSound();
            }
        }
    }

    void ChangeCandySpriteWithAnimation(Image candy)
    {
        candy.rectTransform.DOScale(Vector3.zero, 0.25f) // Scale down
            .OnComplete(() =>
            {
                // Change sprite when scaled down
                candy.sprite = candyPrefabs[Random.Range(0, candyPrefabs.Length)].sprite;
                // Scale back up to original size
                candy.rectTransform.DOScale(Vector3.one, 0.25f);
            });
    }

}
