using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaridaGames.Vault
{
    internal static class VaultBootstrap
    {
        const string SceneName = "BaridaGamesVault Scene";
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadVault()
        {
            Vault.LoadVault();

            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; ++sceneIndex)
            {
                var candidate = SceneManager.GetSceneAt(sceneIndex);
                if (candidate.name == SceneName)
                    return;
            }
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }
    }
}