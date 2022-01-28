using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataSources
{
    public static class Helpers
    {
        public static void Append<K, V>(this Dictionary<K, V> first, Dictionary<K, V> second)
        {
            second.ToList()
                .ForEach(pair => first[pair.Key] = pair.Value);
        }

        public static T LoadConfigurationFromJson<T>(string jsonFilePath)
        {
            StreamReader sr = null;
            object convertedObject;
            try
            {
                sr = new StreamReader(jsonFilePath);
                string input = sr.ReadToEnd();

                if (input.Length == 0)
                {
                    throw new InvalidDataException($"No data found in file located at: {jsonFilePath}");
                }

                convertedObject = JsonConvert.DeserializeObject<T>(input);
                if (convertedObject == null)
                {
                    throw new ArgumentException("Error converting json data");
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException("There was a problem opening the configuration file. " + ex.Message);
            }
            finally
            {
                sr?.Close();
            }

            return (T)Convert.ChangeType(convertedObject, typeof(T));
        }
    }
}
