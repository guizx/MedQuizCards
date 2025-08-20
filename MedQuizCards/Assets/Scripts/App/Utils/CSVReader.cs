using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MedQuizCards
{
    public class CSVReader : MonoBehaviour
    {
        [SerializeField] private TextAsset csvFile;
        private string[][] tabela;


        void ReadCSV()
        {
            string[] linhas = csvFile.text.Split('\n');
            tabela = new string[linhas.Length][];

            for (int i = 0; i < linhas.Length; i++)
            {
                tabela[i] = linhas[i].Split(',');
                for (int j = 0; j < tabela[i].Length; j++)
                    tabela[i][j] = tabela[i][j].Trim();
            }
        }


        public List<string> GetColumn(int columnIndex)
        {
            List<string> columnData = new List<string>();

            string[] lines = csvFile.text.Split('\n');
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                if (values.Length > columnIndex)
                    columnData.Add(values[columnIndex].Trim());
            }

            columnData.RemoveAt(0);

            List<string> columDataDistinct = columnData.Distinct().OrderBy(n => n).ToList();

            return columnData;
        }

        public List<string> GetColuna(string nomeColuna)
        {
            List<string> valores = new List<string>();

            if (tabela == null || tabela.Length == 0)
            {
                Debug.LogError("CSV não foi carregado!");
                return valores;
            }

            // Pega cabeçalho (linha 0)
            string[] cabecalho = tabela[0];
            int indexColuna = System.Array.IndexOf(cabecalho, nomeColuna);

            if (indexColuna == -1)
            {
                Debug.LogError($"Coluna '{nomeColuna}' não encontrada!");
                return valores;
            }

            // Itera a partir da linha 1 (dados, não cabeçalho)
            for (int i = 1; i < tabela.Length; i++)
            {
                if (tabela[i].Length > indexColuna)
                    valores.Add(tabela[i][indexColuna]);
            }

            return valores;
        }
    }
}
