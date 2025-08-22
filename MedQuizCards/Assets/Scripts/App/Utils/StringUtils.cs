using System.Globalization;
using System.Text;

namespace MedQuizCards
{
    public static class StringUtils
    {
        /// <summary>
        /// Remove acentos/diacríticos e retorna a string só com caracteres ASCII básicos.
        /// </summary>
        public static string RemoveAccents(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            string normalized = texto.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);

                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
