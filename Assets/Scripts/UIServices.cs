using ChestSystem.Chest;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem
{

    public class UIServices : MonoSingleton<UIServices>
    {

        public RectTransform UnlockNowRectTransform { get { return unlockNowRectTransform; } private set { } }

        public Vector2 UnlockButtonInitialPos { get; private set; }
        public Button UnlockNowButton { get { return unlockNowButton; } private set { } }
        public Button SetTimerButton { get { return setTimerButton; } private set { } }
        public GameObject GiftMessage { get { return giftMessage; } private set { } }
        public TextMeshProUGUI GiftCoinText { get { return giftCoinText; } private set { } }
        public TextMeshProUGUI GiftGemText { get { return giftGemText; } private set { } }
        public TextMeshProUGUI UnlockText { get { return unlockText; } private set { } }

        [Header("Chest Related Buttons")]
        [SerializeField] private Button createChestButton;
        [SerializeField] private GameObject OverLayImage;
        [SerializeField] private GameObject chestSlotsFullPopUp;
        [SerializeField] private Button closeChestSlotsFull;

        [Header("Chest Pop Up")]
        [SerializeField] private GameObject chestPopUp;
        [SerializeField] private Button closeChestPopUp;
        [SerializeField] private Button unlockNowButton;
        [SerializeField] private RectTransform unlockNowRectTransform;
        [SerializeField] private TextMeshProUGUI unlockText;
        [SerializeField] private Button setTimerButton;
        [SerializeField] private GameObject giftMessage;
        [SerializeField] private TextMeshProUGUI giftCoinText;
        [SerializeField] private TextMeshProUGUI giftGemText;

        [Header("Player Stats")]
        [SerializeField] private TextMeshProUGUI coins;
        [SerializeField] private TextMeshProUGUI gems;


        public static event Action OnChestPopUpClosed;

        private void Start()
        {
            OverLayImage.SetActive(false);
            chestSlotsFullPopUp.SetActive(false);
            chestPopUp.SetActive(false);
            unlockNowButton.gameObject.SetActive(false);
            setTimerButton.gameObject.SetActive(false);
            giftMessage.gameObject.SetActive(false);
            UnlockButtonInitialPos = unlockNowRectTransform.anchoredPosition;

            createChestButton.onClick.AddListener(ChestServices.Instance.SpawnRandomChest);
            closeChestSlotsFull.onClick.AddListener(DisableSlotsFullPopUp);
            closeChestPopUp.onClick.AddListener(DisableChestPopUp);

            RefreshPlayerStats();
        }
        public void RefreshPlayerStats()
        {
            coins.text = PlayerServices.Instance.GetCoinsInAccount().ToString();
            gems.text = PlayerServices.Instance.GetGemsInAccount().ToString();
        }
        public void EnableChestPopUp()
        {
            OverLayImage.SetActive(true);
            chestPopUp.SetActive(true);
        }
        public void DisableChestPopUp()
        {
            OverLayImage.SetActive(false);
            chestPopUp.SetActive(false);
            unlockNowButton.gameObject.SetActive(false);
            setTimerButton.gameObject.SetActive(false);
            giftMessage.gameObject.SetActive(false);

            OnChestPopUpClosed?.Invoke();
            unlockNowButton.onClick.RemoveAllListeners();
            setTimerButton.onClick.RemoveAllListeners();
        }
        public void EnableSlotsFullPopUp()
        {
            OverLayImage.SetActive(true);
            chestSlotsFullPopUp.SetActive(true);
        }
        private void DisableSlotsFullPopUp()
        {
            OverLayImage.SetActive(false);
            chestSlotsFullPopUp.SetActive(false);
        }
    }

}

