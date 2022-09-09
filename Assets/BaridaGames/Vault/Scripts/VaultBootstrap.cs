using UnityEngine;

namespace BaridaGames.Vault
{
    internal static class VaultBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadVault()
        {
            Vault.LoadVault();
        }
    }
}
