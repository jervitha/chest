using ChestSystem;
using ChestSystem.Chest;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockedState : MonoBehaviour, IChestStates
{

    private ChestController chestController;
    private int unlockDurationMinutes;
    private Button unlockNowButton;
    private RectTransform unlockButtonRectTransform;
    private TextMeshProUGUI unlockText;
    private Button setTimerButton;
    private Vector2 unlockButtonInitialPos;
    private Vector2 centerOfChestPopUp = new Vector2(0, 0);

    public LockedState(ChestController chestController)
    {
        this.chestController = chestController;
        unlockNowButton = UIServices.Instance.UnlockNowButton;
        setTimerButton = UIServices.Instance.SetTimerButton;
        unlockButtonRectTransform = UIServices.Instance.UnlockNowRectTransform;
        unlockButtonInitialPos = UIServices.Instance.UnlockButtonInitialPos;
        unlockText = UIServices.Instance.UnlockText;
    }
    public void OnStateEnable()
    {
        chestController.chestView.StatusText.text = "Locked";
        unlockDurationMinutes = (int)chestController.chestModel.UnlockDuration;
        chestController.chestView.BottomText.text = (unlockDurationMinutes < 60) ?
            unlockDurationMinutes.ToString() + " Min" : (unlockDurationMinutes / 60).ToString() + " Hr";
    }
    public void ChestButtonAction()
    {
        unlockButtonRectTransform.anchoredPosition = unlockButtonInitialPos;
        unlockText.text = "Unlock Now: " + GetRequiredGemsToUnlock().ToString();
        unlockNowButton.gameObject.SetActive(true);

        if (ChestServices.Instance.IsAnyChestUnlocking() == false)
            setTimerButton.gameObject.SetActive(true);
        else
            unlockButtonRectTransform.anchoredPosition = centerOfChestPopUp;

        unlockNowButton.onClick.AddListener(chestController.UnlockNow);
        setTimerButton.onClick.AddListener(chestController.StartUnlocking);
        UIServices.Instance.EnableChestPopUp();
    }
    public void OnStateDisable()
    {
        UIServices.Instance.DisableChestPopUp();
    }
    public ChestStates GetState()
    {
        return ChestStates.Locked;
    }

    public int GetRequiredGemsToUnlock()
    {
        return Mathf.CeilToInt(unlockDurationMinutes * 60 / chestController.TimeSecondsPerGem);
    }
}
