using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BaridaGames.Vault
{
    public static class Vault
    {
        #region Vault Data
        [System.Serializable]
        private class VaultData
        {
            private Dictionary<string, int> intValues;
            private Dictionary<string, float> floatValues;
            private Dictionary<string, double> doubleValues;
            private Dictionary<string, float[]> vectorValues;
            private Dictionary<string, string> stringValues;
            private Dictionary<string, bool> boolValues;
            public VaultData()
            {
                intValues = new Dictionary<string, int>();
                floatValues = new Dictionary<string, float>();
                doubleValues = new Dictionary<string, double>();
                vectorValues = new Dictionary<string, float[]>();
                stringValues = new Dictionary<string, string>();
                boolValues = new Dictionary<string, bool>();
            }
            #region Int Values
            internal int TryGetIntValue(string key, int defaultValue = 0)
            {
                if (intValues.TryGetValue(key, out int value))
                {
                    return value;
                }
                intValues.Add(key, defaultValue);
                return defaultValue;
            }

            internal bool TrySetIntValue(string key, int value)
            {
                if (intValues.ContainsKey(key))
                {
                    intValues[key] = value;
                    return true;
                }
                intValues.Add(key, value);
                return true;
            }

            internal bool DeleteIntKey(string key)
            {
                return intValues.Remove(key);
            }
            #endregion
            #region Float Values
            internal float TryGetFloatValue(string key, float defaultValue = 0)
            {
                if (floatValues.TryGetValue(key, out float value))
                {
                    return value;
                }
                floatValues.Add(key, defaultValue);
                return defaultValue;
            }

            internal bool TrySetFloatValue(string key, float value)
            {
                if (floatValues.ContainsKey(key))
                {
                    floatValues[key] = value;
                    return true;
                }
                floatValues.Add(key, value);
                return true;
            }

            internal bool DeleteFloatKey(string key)
            {
                return floatValues.Remove(key);
            }
            #endregion
            #region Double Values
            internal double TryGetDoubleValue(string key, double defaultValue = 0)
            {
                if (doubleValues.TryGetValue(key, out double value))
                {
                    return value;
                }
                doubleValues.Add(key, defaultValue);
                return defaultValue;
            }

            internal bool TrySetDoubleValue(string key, double value)
            {
                if (doubleValues.ContainsKey(key))
                {
                    doubleValues[key] = value;
                    return true;
                }
                doubleValues.Add(key, value);
                return true;
            }

            internal bool DeleteDoubleKey(string key)
            {
                return doubleValues.Remove(key);
            }
            #endregion
            #region Vector3 Values
            internal Vector3 TryGetVector3Value(string key)
            {
                if (vectorValues.TryGetValue(key, out float[] value))
                {
                    return new Vector3(value[0], value[1], value[2]);
                }

                vectorValues.Add(key, new float[] { 0, 0, 0 });
                return new Vector3();
            }

            internal Vector3 TryGetVector2Value(string key)
            {
                if (vectorValues.TryGetValue(key, out float[] value))
                {
                    return new Vector2(value[0], value[1]);
                }
                vectorValues.Add(key, new float[] { 0, 0 });
                return new Vector2();
            }

            internal bool TrySetVector3Value(string key, Vector3 value)
            {
                float[] _value = new float[] { value.x, value.y, value.z };
                if (vectorValues.ContainsKey(key))
                {
                    vectorValues[key] = _value;
                    return true;
                }
                vectorValues.Add(key, _value);
                return true;
            }

            internal bool TrySetVector2Value(string key, Vector2 value)
            {
                float[] _value = new float[] { value.x, value.y };
                if (vectorValues.ContainsKey(key))
                {
                    vectorValues[key] = _value;
                    return true;
                }
                vectorValues.Add(key, _value);
                return true;
            }

            internal bool DeleteVectorKey(string key)
            {
                return vectorValues.Remove(key);
            }
            #endregion
            #region String Values
            internal string TryGetStringValue(string key, string defaultValue = "")
            {
                if (stringValues.TryGetValue(key, out string value))
                {
                    return value;
                }
                stringValues.Add(key, defaultValue);
                return defaultValue;
            }

            internal bool TrySetStringValue(string key, string value)
            {
                if (stringValues.ContainsKey(key))
                {
                    stringValues[key] = value;
                    return true;
                }
                stringValues.Add(key, value);
                return true;
            }

            internal bool DeleteStringKey(string key)
            {
                return stringValues.Remove(key);
            }
            #endregion
            #region Bool Values
            internal bool TryGetBoolValue(string key, bool defaultValue = false)
            {
                if (boolValues.TryGetValue(key, out bool value))
                {
                    return value;
                }
                boolValues.Add(key, defaultValue);
                return defaultValue;
            }

            internal bool TrySetBoolValue(string key, bool value)
            {
                if (boolValues.ContainsKey(key))
                {
                    boolValues[key] = value;
                    return true;
                }
                boolValues.Add(key, value);
                return true;
            }

            internal bool DeleteBoolKey(string key)
            {
                return boolValues.Remove(key);
            }
            #endregion
        }
        #endregion
        const string FileName = "/barida.vault";
        private static string FilePath = Application.persistentDataPath + FileName;
        private static VaultData data;

        public static void LoadVault()
        {
            FileStream file;
            if (File.Exists(FilePath)) file = File.OpenRead(FilePath);
            else file = File.Create(FilePath);

            // Check if file empty
            if (new FileInfo(FilePath).Length != 0)
            {
                BinaryFormatter bf = new BinaryFormatter();
                data = (VaultData)bf.Deserialize(file);
                file.Close();
            }
            else
            {
                data = new VaultData();
            }
        }

        public static void SaveVault()
        {
            FileStream file;
            if (File.Exists(FilePath)) file = File.OpenRead(FilePath);
            else file = File.Create(FilePath);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            return data.TryGetIntValue(key);
        }

        public static void SetInt(string key, int value)
        {
            data.TrySetIntValue(key, value);
        }

        public static void ModifyInt(string key, int amount)
        {
            data.TrySetIntValue(key, GetInt(key) + amount);
        }

        public static void DeleteInt(string key)
        {
            data.DeleteIntKey(key);
        }
    }
}
