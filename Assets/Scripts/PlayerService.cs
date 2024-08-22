using ChestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class PlayerServices : MonoSingleton<PlayerServices>
    {
        [SerializeField]
        private int gemsInAccount;
        [SerializeField]
        private int coinsInAccount;

        public int GetGemsInAccount() => gemsInAccount;
        public int GetCoinsInAccount() => coinsInAccount;
        public void IncrementGems(int gems)
        {
            gemsInAccount += gems;
            UIServices.Instance.RefreshPlayerStats();
        }
        public void DecrementGems(int gems)
        {
            gemsInAccount -= gems;
            UIServices.Instance.RefreshPlayerStats();
        }
        public void IncrementCoins(int coins)
        {
            coinsInAccount += coins;
            UIServices.Instance.RefreshPlayerStats();
        }
        public void DecrementCoins(int coins)
        {
            coinsInAccount -= coins;
            UIServices.Instance.RefreshPlayerStats();
        }
    }
}

