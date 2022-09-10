using UnityEngine;

namespace BaridaGames.Vault
{
    public class VaultAutoSave : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject AutoSaveIcon = default;
        [Header("Settings")]
        [SerializeField] private bool AutoSaveEnabled = true;
        [SerializeField] private float AutoSaveInterval = 600;

        private void Start()
        {
            if (!AutoSaveEnabled) return;
            InvokeRepeating("AutoSave", AutoSaveInterval, AutoSaveInterval);
        }

        private async void AutoSave()
        {
            AutoSaveIcon.SetActive(true);
            await Vault.SaveVault();
            AutoSaveIcon.SetActive(false);
        }
    }
}