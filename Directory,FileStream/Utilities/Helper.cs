using System.Text;

namespace Directory_FileStream.Utilities;

public static class Helper
{
    public static string Capitalize(this string text)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < text.Length; i++)
        {
            if (i == 0)
            {
                stringBuilder.Append(char.ToUpper(text[i]));
            }
            else if (char.IsWhiteSpace(text[i]))
            {
                if (!char.IsWhiteSpace(text[i + 1]))
                {
                    stringBuilder.Append(text[i]);
                    stringBuilder.Append(char.ToUpper(text[i + 1]));
                    i++;
                }
            }
            else
            {
                stringBuilder.Append(text[i]);
            }
        }
        return stringBuilder.ToString();
    }
}
