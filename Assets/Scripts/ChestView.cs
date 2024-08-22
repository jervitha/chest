using ChestSystem.Chests;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.Chest
{
    public class ChestView : MonoBehaviour
    {
        public Button ChestButton { get { return chestButton; } private set { } }
        public TextMeshProUGUI StatusText { get { return statusText; } private set { } }
        public TextMeshProUGUI ChestTypeText { get { return topText; } private set { } }
        public TextMeshProUGUI BottomText { get { return bottomText; } private set { } }
        public Image ChestImage { get { return chestImage; } private set { } }

        [SerializeField] private RectTransform chestRectTransform;
        [SerializeField] private Image chestImage;
        [SerializeField] private Button chestButton;
        [SerializeField] private TextMeshProUGUI topText;
        [SerializeField] private TextMeshProUGUI bottomText;
        [SerializeField] private TextMeshProUGUI statusText;

        private ChestSlot slot;
        private ChestController chestController;

        public int TimeRemainingSeconds { get; internal set; }

        public void SetChestController(ChestController controller)
        {
            chestController = controller;
        }
        public void Init()
        {
            ChestImage.sprite = chestController.chestModel.ChestClosedImage;
            TimeRemainingSeconds = chestController.chestModel.UnlockDuration * 60;
            ChestButton.onClick.AddListener(chestController.ChestButtonAction);
            ChestTypeText.text = Enum.GetName(typeof(ChestType), chestController.GetChestType());
        }

        public void SlotSet(ChestSlot slot)
        {
            this.slot = slot;
            chestRectTransform.anchoredPosition = slot.GetRectTransform().anchoredPosition;
        }
        private void Awake()
        {
            transform.SetParent(ChestServices.Instance.ChestParentTransform);
            chestRectTransform.localScale = new Vector3(1, 1, 1);
        }
        public IEnumerator CountDuration()
        {
            while (TimeRemainingSeconds >= 0)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(TimeRemainingSeconds);
                string timeString = timeSpan.ToString(@"hh\:mm\:ss");
                BottomText.text = timeString;

                TimeRemainingSeconds--;
                yield return new WaitForSeconds(1);
            }
            chestController.UnlockNow();
        }
        internal void DestroyChest()
        {
            slot.SetIsEmpty(true);
            ChestButton.onClick.RemoveAllListeners();
            chestController.RemoveView();
        }

    }

}
