using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        Dictionary<char, int> CL = new Dictionary<char, int> { { 'A', 0 }, { 'B', 1 }, { 'C', 2 }, { 'D', 3 }, { 'E', 4 }, { 'F', 5 }, { 'G', 6 }, { 'H', 7 }, { 'I', 8 }, { 'J', 9 }, { 'K', 10 }, { 'L', 11 }, { 'M', 12 }, { 'N', 13 }, { 'O', 14 }, { 'P', 15 }, { 'Q', 16 }, { 'R', 17 }, { 'S', 18 }, { 'T', 19 }, { 'U', 20 }, { 'V', 21 }, { 'W', 22 }, { 'X', 23 }, { 'Y', 24 }, { 'Z', 25 } };
        public string Analyse(string plainText, string cipherText)
        {
            Dictionary<char, int> C = new Dictionary<char, int> { { 'A', 0 }, { 'B', 1 }, { 'C', 2 }, { 'D', 3 }, { 'E', 4 }, { 'F', 5 }, { 'G', 6 }, { 'H', 7 }, { 'I', 8 }, { 'J', 9 }, { 'K', 10 }, { 'L', 11 }, { 'M', 12 }, { 'N', 13 }, { 'O', 14 }, { 'P', 15 }, { 'Q', 16 }, { 'R', 17 }, { 'S', 18 }, { 'T', 19 }, { 'U', 20 }, { 'V', 21 }, { 'W', 22 }, { 'X', 23 }, { 'Y', 24 }, { 'Z', 25 } };
            Dictionary<char, int> d = new Dictionary<char, int> { { 'A', 0 }, { 'B', 1 }, { 'C', 2 }, { 'D', 3 }, { 'E', 4 }, { 'F', 5 }, { 'G', 6 }, { 'H', 7 }, { 'I', 8 }, { 'J', 9 }, { 'K', 10 }, { 'L', 11 }, { 'M', 12 }, { 'N', 13 }, { 'O', 14 }, { 'P', 15 }, { 'Q', 16 }, { 'R', 17 }, { 'S', 18 }, { 'T', 19 }, { 'U', 20 }, { 'V', 21 }, { 'W', 22 }, { 'X', 23 }, { 'Y', 24 }, { 'Z', 25 } };
            char[] plain = plainText.ToUpper().ToCharArray();
            char[] cipher = cipherText.ToCharArray();
            char[] key = new char[26];
            for(int i =0;i<plainText.Length;i++)
            {
                int index = CL[plain[i]];
                key[index] = cipher[i];
                d[plain[i]] = -1;
                C[cipher[i]] = -1;
            }
            foreach(KeyValuePair<char,int> item in d)
            {
                if(item.Value!=-1)
                {
                    foreach (KeyValuePair<char, int> i in C)
                    {
                        if(i.Value !=-1){
                            key[item.Value] = i.Key;
                            C[i.Key] = -1;
                            break;
                        }
                    }

                }
            }

            string k = new string(key);
            k = k.ToLower();
            return k;

        }

        public string Decrypt(string cipherText, string key)
        {
            char[] ch = cipherText.ToCharArray();
            char[] k = key.ToUpper().ToCharArray();
            for (int i = 0; i < cipherText.Length; i++)
            {
                int index = Array.IndexOf(k, ch[i]);
                ch[i] = CL.Keys.ElementAt(index);
            }
            string cipher = new string(ch);
            cipher = cipher.ToLower();
            return cipher;
        }

        public string Encrypt(string plainText, string key)
        {
            char[] ch = plainText.ToUpper().ToCharArray();
            char[] k = key.ToUpper().ToCharArray();
            for(int i =0;i<plainText.Length;i++)
            {
                int index = CL[ch[i]];
                ch[i] = k[index];
            }
            string cipher = new string(ch);
            return cipher;
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            string F = "ETAOINSRHLDCUMFPGWYBVKJXQZ";
            string A = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] FC = F.ToCharArray();
            char[] Alph = A.ToCharArray();
            int[] arr = new int [26];
            for(int i =0;i<26;i++)
            {
                int count = 0;
                for(int j =0;j<cipher.Length;j++)
                {
                    if(cipher[j]==Alph[i])
                    {
                        count++;
                    }
                }
                arr[i] = count;
            }
            int temp1;
            char temp2;
            for(int i =0;i <26-1;i++)
            {
                for(int j =i+1;j <26;j++)
                {
                    if (arr[i] < arr[j])
                    {

                    temp1 = arr[i];
                    temp2 = Alph[i];
                    arr[i] = arr[j];
                    Alph[i] = Alph[j];
                    arr[j] = temp1;
                    Alph[j] = temp2;
                } 
                }
            }
            char[] key = new char[26];
                foreach(KeyValuePair<char,int> item in CL)
                {
                    int ind = Array.IndexOf(FC, item.Key);
                            key[item.Value] = Alph[ind];
            
                    
                }
      
            string k = new string(key);
            k = k.ToLower();
            string plaintext = this.Decrypt(cipher, k);
            return plaintext;
        }
    }
}
