using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MedQuizCards
{
    public class UniversityDatabase : MonoBehaviour
    {
        public string fileName = "save.json";
        private string filePath;

        public List<UniversityDTO> Universities = new List<UniversityDTO>();

        private void Awake()
        {
            filePath = Path.Combine(Application.streamingAssetsPath, fileName);
            LoadDatabase();
        }

        // ------------------ CARREGAR ------------------
        public void LoadDatabase()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                // A Unity não desserializa arrays direto em List, então criamos um wrapper
                Universities = JsonUtilityWrapper.FromJson<UniversityDTO>(json);
            }
            else
            {
                Debug.LogWarning($"Arquivo não encontrado em {filePath}, criando novo...");
                Universities = new List<UniversityDTO>();
                SaveDatabase();
            }
        }

        // ------------------ SALVAR ------------------
        public void SaveDatabase()
        {
            string json = JsonUtilityWrapper.ToJson(Universities, true);
            File.WriteAllText(filePath, json);
            Debug.Log("Banco salvo em: " + filePath);
        }

        // ------------------ ADICIONAR ------------------
        public void AddUniversity(UniversityDTO uni)
        {
            Universities.Add(uni);
            SaveDatabase();
        }

        public void AddScoreAndSave(UniversityRanking currentUniversity)
        {
            foreach (var university in Universities)
            {
                if (university.id == currentUniversity.ID.ToString())
                {
                    university.pontos = currentUniversity.Pontos.ToString();
                    break;
                }
            }
            SaveDatabase();
        }

        public void ResetAllScores()
        {
            foreach (var university in Universities)
            {
                university.pontos = "0";
            }
            SaveDatabase();
        }


        private void Update()
        {
            if(Keyboard.current != null && Keyboard.current.f1Key.wasPressedThisFrame)
            {
                ResetAllScores();
            }
        }
    }

    // ------------------ WRAPPER NECESSÁRIO ------------------
    public static class JsonUtilityWrapper
    {
        [System.Serializable]
        private class Wrapper<T>
        {
            public List<T> Items;
        }

        public static List<T> FromJson<T>(string json)
        {
            string newJson = "{\"Items\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.Items;
        }

        public static string ToJson<T>(List<T> list, bool prettyPrint = false)
        {
            Wrapper<T> wrapper = new Wrapper<T> { Items = list };
            string json = JsonUtility.ToJson(wrapper, prettyPrint);
            // Remove o objeto wrapper, deixando só o array
            int start = json.IndexOf(":") + 1;
            int end = json.LastIndexOf("}");
            return json.Substring(start, end - start);
        }
    }
}
