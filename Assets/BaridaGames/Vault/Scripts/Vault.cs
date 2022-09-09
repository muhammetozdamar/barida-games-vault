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
            internal Vector3 TryGetVector3Value(string key, Vector3 defaultValue = new Vector3())
            {
                if (vectorValues.TryGetValue(key, out float[] value))
                {
                    return new Vector3(value[0], value[1], value[2]);
                }

                vectorValues.Add(key, new float[] { defaultValue.x, defaultValue.y, defaultValue.z });
                return defaultValue;
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
            if (File.Exists(FilePath)) file = File.OpenWrite(FilePath);
            else file = File.Create(FilePath);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }
        #region Int Value
        public static int GetInt(string key, int defaultValue = default)
        {
            return data.TryGetIntValue(key, defaultValue);
        }

        public static bool SetInt(string key, int value)
        {
            return data.TrySetIntValue(key, value);
        }

        public static bool ModifyInt(string key, int amount)
        {
            return data.TrySetIntValue(key, GetInt(key) + amount);
        }

        public static bool DeleteInt(string key)
        {
            return data.DeleteIntKey(key);
        }
        #endregion
        #region Float Value
        public static float GetFloat(string key, float defaultValue = default)
        {
            return data.TryGetFloatValue(key, defaultValue);
        }

        public static bool SetFloat(string key, float value)
        {
            return data.TrySetFloatValue(key, value);
        }

        public static bool ModifyFloat(string key, float amount)
        {
            return data.TrySetFloatValue(key, GetFloat(key) + amount);
        }

        public static bool DeleteFloat(string key)
        {
            return data.DeleteFloatKey(key);
        }
        #endregion
        #region Double Value
        public static double GetDouble(string key, double defaultValue = default)
        {
            return data.TryGetDoubleValue(key, defaultValue);
        }

        public static bool SetDouble(string key, double value)
        {
            return data.TrySetDoubleValue(key, value);
        }

        public static bool ModifyDouble(string key, double amount)
        {
            return data.TrySetDoubleValue(key, GetDouble(key) + amount);
        }

        public static bool DeleteDouble(string key)
        {
            return data.DeleteDoubleKey(key);
        }
        #endregion
        #region Vector Value
        public static Vector3 GetVector3(string key, Vector3 defaultValue = default)
        {
            return data.TryGetVector3Value(key, defaultValue);
        }

        public static bool SetVector3(string key, Vector3 value)
        {
            return data.TrySetVector3Value(key, value);
        }

        public static bool ModifyVector3(string key, Vector3 amount)
        {
            return data.TrySetVector3Value(key, GetVector3(key) + amount);
        }

        public static bool DeleteVectorKey(string key)
        {
            return data.DeleteVectorKey(key);
        }
        #endregion
        #region String Value
        public static string GetString(string key, string defaultValue = default)
        {
            return data.TryGetStringValue(key, defaultValue);
        }

        public static bool SetString(string key, string value)
        {
            return data.TrySetStringValue(key, value);
        }

        public static bool ModifyString(string key, string amount)
        {
            return data.TrySetStringValue(key, GetString(key) + amount);
        }

        public static bool DeleteString(string key)
        {
            return data.DeleteStringKey(key);
        }
        #endregion
        #region Bool Value
        public static bool GetBool(string key, bool defaultValue = default)
        {
            return data.TryGetBoolValue(key, defaultValue);
        }
        public static bool SetBool(string key, bool value)
        {
            return data.TrySetBoolValue(key, value);
        }
        public static bool DeleteBool(string key)
        {
            return data.DeleteBoolKey(key);
        }
        #endregion
    }
}