using UnityEngine;

namespace ChestSystem.Chest
{
    [System.Serializable]
    public class ChestRarity
    {
        [SerializeField] private ChestSO chestObject;
        [SerializeField] private int probabilityPercentage;

        private ChestModel chestModel;

        public void SetModel(ChestModel model) => chestModel = model;
        public ChestModel GetModel() => chestModel;
        public ChestSO GetChestObject() => chestObject;
        public int GetProbability() => probabilityPercentage;
    }
}
