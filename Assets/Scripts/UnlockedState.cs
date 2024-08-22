using ChestSystem;
using ChestSystem.Chest;
using TMPro;
using UnityEngine;

public class UnlockedState : MonoBehaviour, IChestStates
{
    private ChestController chestController;
    private GameObject giftMessage;
    private TextMeshProUGUI giftCoinText;
    private TextMeshProUGUI giftGemText;

    public UnlockedState(ChestController chestController)
    {
        this.chestController = chestController;
        giftMessage = UIServices.Instance.GiftMessage;
        giftCoinText = UIServices.Instance.GiftCoinText;
        giftGemText = UIServices.Instance.GiftGemText;
    }
    public void OnStateEnable()
    {
        chestController.chestView.StatusText.text = "Unlocked";
        chestController.chestView.BottomText.text = "OPEN";
        chestController.chestView.ChestImage.sprite = chestController.chestModel.ChestOpenImage;

    }
    public void ChestButtonAction()
    {
        UIServices.OnChestPopUpClosed += DestroyChest;
        giftMessage.SetActive(true);
        SetGifts();
        UIServices.Instance.EnableChestPopUp();
    }
    public void OnStateDisable()
    {
        UIServices.Instance.DisableChestPopUp();
    }
    public ChestStates GetState()
    {
        return ChestStates.Unlocked;
    }
    public int GetRequiredGemsToUnlock()
    {
        return 0;
    }

    private void SetGifts()
    {
        int giftCoins = chestController.chestModel.Coins;
        int giftGems = chestController.chestModel.Gems;

        giftCoinText.text = "You got " + giftCoins.ToString();
        giftGemText.text = "You got " + giftGems.ToString();

        PlayerServices.Instance.IncrementCoins(giftCoins);
        PlayerServices.Instance.IncrementGems(giftGems);
    }
    private void DestroyChest()
    {
        UIServices.OnChestPopUpClosed -= DestroyChest;
        OnStateDisable();
        chestController.chestView.DestroyChest();
    }
}
