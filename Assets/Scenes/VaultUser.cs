using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaridaGames.Vault;

public class VaultUser : MonoBehaviour
{
    private void Awake()
    {
        print("Access int: " + Vault.GetInt("0", 1));
        print("Access float: " + Vault.GetFloat("0", 69f));
        print("Access double: " + Vault.GetDouble("0", 666d));
        print("Access vector3: " + Vault.GetVector3("0", Vector3.forward + Vector3.up));
        print("Access string: " + Vault.GetString("0", "string 234"));
        print("Access bool: " + Vault.GetBool("0", false));
    }

    private void Start()
    {
        Vault.SaveVault();
    }
}
