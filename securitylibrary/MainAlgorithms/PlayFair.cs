using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographicTechnique<string, string>
    {
        /// <summary>
        /// The most common diagrams in english (sorted): TH, HE, AN, IN, ER, ON, RE, ED, ND, HA, AT, EN, ES, OF, NT, EA, TI, TO, IO, LE, IS, OU, AR, AS, DE, RT, VE
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string Analyse(string plainText)
        {
            throw new NotImplementedException();
        }

        public string Analyse(string plainText, string cipherText)
        {
            throw new NotSupportedException();
        }

       public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            cipherText = cipherText.Replace(" ", String.Empty);
            cipherText = cipherText.Replace("j", "i");
            key = key.Replace(" ", String.Empty);
            key = key.Replace("j", "i");
            key = key.ToLower();
            char[,] Table = CreateTable(key);
            string PT = cipherText;
            string text = "";
            for (int i = 0; i < PT.Length; i += 2)
            {
                int[] index1, index2; // index 0 = row / index 1 = column
                index1 = getIndex(Table, PT[i]);
                index2 = getIndex(Table, PT[i + 1]);

                //same row
                if (index1[0] == index2[0])
                {
                    text += Table[index1[0], (index1[1] + 4) % 5];
                    text += Table[index1[0], (index2[1] + 4) % 5];
                }
                //same column
                else if (index1[1] == index2[1])
                {
                    text += Table[(index1[0] + 4) % 5, index1[1]];
                    text += Table[(index2[0] + 4) % 5, index1[1]];
                }
                //square 
                else
                {
                    text += Table[index1[0], index2[1]];
                    text += Table[index2[0], index1[1]];
                }
            }
            return ValidatePTDecrypt(text);
        }
        public string Encrypt(string plainText, string key)
        {
            plainText.ToLower();
            plainText = plainText.Replace(" ", String.Empty);
            plainText = plainText.Replace("j", "i");
            key = key.Replace(" ", String.Empty);
            key = key.Replace("j", "i");
            key = key.ToLower();

            string PT = ValidatePTEncrpy(plainText);
            char[,] Table = CreateTable(key);

            string text = "";
            for (int i = 0; i < PT.Length; i += 2)
            {
                int[] index1, index2; // index 0 = row / index 1 = column
                index1 = getIndex(Table, PT[i]);
                index2 = getIndex(Table, PT[i + 1]);

                //same row
                if (index1[0] == index2[0])
                {
                    text += Table[index1[0], (index1[1] + 1) % 5];
                    text += Table[index1[0], (index2[1] + 1) % 5];
                }
                //same column
                else if (index1[1] == index2[1])
                {
                    text += Table[(index1[0] + 1) % 5, index1[1]];
                    text += Table[(index2[0] + 1) % 5, index1[1]];
                }
                //square 
                else
                {
                    text += Table[index1[0], index2[1]];
                    text += Table[index2[0], index1[1]];
                }
            }
            return text.ToUpper();
        }

        char[,] CreateTable(string Key)
        {

            char[] alphabets = new char[26];
            for (char i = 'a'; i <= 'z'; i++)
                alphabets[Convert.ToInt32(i - 97)] = i;
            alphabets[9] = ' ';
            char[,] table = new char[5, 5];
            int AlphaIndex = 0;
            int KeyIndex = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Key.Length > KeyIndex)
                    {
                        while (Key.Length > KeyIndex && alphabets[Convert.ToInt32(Key[KeyIndex] - 97)] == ' ')
                        {
                            if (alphabets[Convert.ToInt32(Key[KeyIndex] - 97)] == ' ')
                                KeyIndex++;
                        }
                        if (Key.Length == KeyIndex)
                        {
                            j--;
                            continue;
                        }
                        table[i, j] = Key[KeyIndex];
                        alphabets[Convert.ToInt32(Key[KeyIndex] - 97)] = ' ';
                        KeyIndex++;
                    }
                    else
                    {
                        while (alphabets[AlphaIndex] == ' ')
                        {
                            AlphaIndex++;
                        }
                        table[i, j] = alphabets[AlphaIndex];
                        alphabets[AlphaIndex] = ' ';
                        AlphaIndex++;
                    }
                }
            }
            return table;
        }
        string ValidatePTEncrpy(string PT)
        {
            string newPT = "";
            if (PT[0] == 'j')
                newPT += 'i';
            else
                newPT += PT[0];

            for (int i = 1; i < PT.Length; i++)
            {

                if (PT[i] == 'j')
                    newPT += 'i';
                else if (PT[i] == PT[i - 1] && newPT.Length % 2 == 1)
                {
                    newPT = newPT + 'x' + PT[i];
                }
                else
                    newPT += PT[i];
            }
            if (newPT.Length % 2 > 0) newPT += 'x';
            return newPT;
        }
        string ValidatePTDecrypt(string PT)
        {

            string newPT = "";
            newPT += PT[0];

            for (int i = 1; i < PT.Length - 1; i++)
            {

                if (PT[i] == 'x' && i % 2 == 1)
                {
                    if (PT[i - 1] == PT[i + 1])
                        continue;
                    else
                        newPT += PT[i];
                }
                else
                    newPT += PT[i];
            }
            if (PT[PT.Length - 1] != 'x') newPT += PT[PT.Length - 1];
            return newPT.ToUpper();
        }
        int[] getIndex(char[,] Table, char c)
        {
            int[] indexArr = new int[2];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (c == Table[i, j])
                    {
                        indexArr[0] = i;
                        indexArr[1] = j;
                        return indexArr;
                    }
                }
            }
            return new int[2];
        }
    
    }
}
