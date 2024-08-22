using ChestSystem.Chests;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestModel
    {
        public ChestSO chestSO;
        public Sprite ChestClosedImage { get; private set; }
        public Sprite ChestOpenImage { get; private set; }
        public ChestType chestType { get; private set; }

        public int CoinsNeeded { get; private set; }
        public int Coins { get; private set; }
        public int Gems { get; private set; }

        public int UnlockDuration { get; private set; }
        // Start is called before the first frame update

        public ChestModel(ChestSO _chestSO)
        {
            this.chestSO = _chestSO;
            this.ChestClosedImage = chestSO.ChestClosedImage;
            this.ChestOpenImage = chestSO.ChestOpenImage;
            this.CoinsNeeded = _chestSO.CoinsNeeded;
            this.Coins = Random.Range(_chestSO.CoinsMin, _chestSO.CoinsMax);
            this.Gems = Random.Range(_chestSO.GemsMin, _chestSO.GemsMax);
            this.chestType = _chestSO.Type;
            this.UnlockDuration = _chestSO.UnlockDuration;
        }
    }
}

