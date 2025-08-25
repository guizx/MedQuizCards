using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

namespace MedQuizCards
{
    public class UniversityDatabase : MonoBehaviour
    {
        public string fileName = "save.json";

        private string persistentFilePath => Path.Combine(Application.persistentDataPath, fileName);
        private string streamingFilePath => Path.Combine(Application.streamingAssetsPath, fileName);

        public List<UniversityDTO> Universities = new List<UniversityDTO>();

        private void Awake()
        {
            StartCoroutine(LoadDatabase());
        }

        // ------------------ CARREGAR ------------------
        public IEnumerator LoadDatabase()
        {
            // Se não existir em persistentDataPath, copia de StreamingAssets
            if (!File.Exists(persistentFilePath))
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            UnityWebRequest www = UnityWebRequest.Get(streamingFilePath);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao copiar arquivo: " + www.error);
                yield break;
            }

            File.WriteAllText(persistentFilePath, www.downloadHandler.text);
#else
                if (File.Exists(streamingFilePath))
                    File.Copy(streamingFilePath, persistentFilePath, true);
                else
                {
                    Debug.LogWarning($"Arquivo não encontrado em StreamingAssets: {streamingFilePath}");
                    //Universities = new List<UniversityDTO>();
                    //SaveDatabase();
                    yield break;
                }
#endif
            }

            // Agora lê do persistentDataPath
            string json = File.ReadAllText(persistentFilePath);
            Universities = JsonUtilityWrapper.FromJson<UniversityDTO>(json);
            QuizManager.OnDatabaseReload?.Invoke();
            yield break;
        }

        // ------------------ SALVAR ------------------
        public void SaveDatabase()
        {
            string json = JsonUtilityWrapper.ToJson(Universities, true);
            File.WriteAllText(persistentFilePath, json);
            Debug.Log("Banco salvo em: " + persistentFilePath);
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
