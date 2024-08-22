using ChestSystem;
using ChestSystem.Chest;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockingState : MonoBehaviour, IChestStates
{
    private ChestController chestController;

    private Button unlockNowButton;
    private RectTransform unlockButtonRectTransform;
    private TextMeshProUGUI unlockText;
    private Vector2 centerOfChestPopUp = new Vector2(0, 0);
    private Coroutine countDown;

    public UnlockingState(ChestController chestController)
    {
        this.chestController = chestController;
        unlockNowButton = UIServices.Instance.UnlockNowButton;
        unlockButtonRectTransform = UIServices.Instance.UnlockNowRectTransform;
        unlockText = UIServices.Instance.UnlockText;

    }
    public void OnStateEnable()
    {
        chestController.chestView.StatusText.text = "Unlocking";
        countDown = chestController.chestView.StartCoroutine(chestController.chestView.CountDuration());
    }
    public void ChestButtonAction()
    {
        unlockButtonRectTransform.anchoredPosition = centerOfChestPopUp;
        unlockNowButton.gameObject.SetActive(true);
        unlockText.text = "Unlock Now: " + GetRequiredGemsToUnlock().ToString();
        unlockNowButton.onClick.AddListener(chestController.UnlockNow);
        UIServices.Instance.EnableChestPopUp();
    }
    public void OnStateDisable()
    {
        UIServices.Instance.DisableChestPopUp();

        chestController.chestView.StopCoroutine(countDown);
    }
    public ChestStates GetState()
    {
        return ChestStates.Unlocking;
    }

    public int GetRequiredGemsToUnlock()
    {
        return Mathf.CeilToInt(chestController.chestView.TimeRemainingSeconds / chestController.TimeSecondsPerGem);
    }
}



