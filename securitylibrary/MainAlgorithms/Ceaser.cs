using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        Dictionary<char,int> CL = new Dictionary<char,int> { {'A',0}, {'B',1}, {'C',2},{'D',3},{'E',4},{'F',5},{'G',6},{'H',7},{'I',8},{'J',9},{'K',10},{'L',11},{'M',12},{'N',13},{'O',14},{'P',15},{'Q',16},{'R',17},{'S',18},{'T',19},{'U',20},{'V',21},{'W',22},{'X',23},{'Y',24},{'Z',25} };
        public string Encrypt(string plainText, int key)
        {
            char[] ch = plainText.ToUpper().ToCharArray();
            for(int i=0;i<ch.Length;i++)
            {
                int newind =( CL[ch[i]] + key)%26;
                ch[i] = CL.Keys.ElementAt(newind);
            }
            string cipher = new string(ch);
            return cipher;
        }

        public string Decrypt(string cipherText, int key)
        {
            char[] ch = cipherText.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                int newind = (CL[ch[i]] - key) % 26;
                if(newind < 0)
                {
                    newind = 26+newind;
                }
                ch[i] = CL.Keys.ElementAt(newind);
            }
            string cipher = new string(ch);
            cipher = cipher.ToLower();
            return cipher;
        }

        public int Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToUpper();
            for (int i = 0; i < 26;i++)
            {
                string cipher = this.Encrypt(plainText, i);
                if(cipher == cipherText)
                {
                    return i;
                }
            }
                return 0;
        }
    }
}
