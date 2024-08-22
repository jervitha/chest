using UnityEngine;
using System.Collections.Generic;

namespace ChestSystem.Chest
{
    public class ChestPoolService : MonoSingleton<ChestPoolService>
    {
        [SerializeField] private ChestView chestPrefab;

        private Queue<ChestView> objectPool = new Queue<ChestView>();
        private int numberOfSlots;

        public ChestView GetFromPool(ChestController chestController)
        {
            ChestView item = null;
            if (objectPool.Count > 0)
            {
                item = objectPool.Dequeue();
                item.gameObject.SetActive(true);
                item.SetChestController(chestController);
                item.Init();
            }
            return item;
        }
        public void ReturnToPool(ChestView item)
        {
            objectPool.Enqueue(item);
            item.gameObject.SetActive(false);
        }

        private void Start()
        {
            CreateChestViews();
        }
        private void CreateChestViews()
        {
            numberOfSlots = SlotServices.Instance.GetNumberOfSlots();
            for (int i = 0; i < numberOfSlots; i++)
            {
                ChestView newView = GameObject.Instantiate<ChestView>(chestPrefab);
                objectPool.Enqueue(newView);
                newView.gameObject.SetActive(false);
            }
        }
    }
}