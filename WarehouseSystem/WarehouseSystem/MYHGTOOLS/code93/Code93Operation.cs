using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace code93.MYHGTOOLS
{
    /// <summary>
    /// 创建者：刘欢
    /// 创建时间：2014-10-14
    /// 说明：条形码
    /// </summary>
    public class Code93Operation
    {
        List<int> indexs = new List<int>(20);
        char[] chars = {'0','1','2','3','4','5','6','7','8','9',
                           'A','B','C','D','E','F','G','H','I','J',
                           'K','L','M','N','O','P','Q','R','S','T',
                           'U','V','W','X','Y','Z','-','.',' ','$',
                           '/','+','%'};
        string[] codes = {"100010100","101001000","101000100","101000010","100101000",
                             "100100100","100100010","101010000","100010010","100001010" ,
                             "110101000","110100100","110100010","110010100","110010010",
                             "110001010","101101000","101100100","101100010","100110100",
                             "100011010","101011000","101001100","101000110","100101100",
                             "100010110","110110100","110110010","110101100","110100110" ,
                             "110010110","110011010","101101100","101100110","100110110" ,
                             "100111010","100101110","111010100","111010010","111001010" ,
                             "101101110","101101110","110101110","100100110","111011010" ,
                             "111010110","100110010","101011110","1010111101"};
        public string EndodingCode93(string s)
        {
            string code = codes[47];
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '+')
                    return null;
                int y = -1;
                for (int x = 0; x < chars.Length; x++)
                    if (chars[x] == s[i])
                    {
                        y = x;
                        break;
                    }
                if (y == -1)
                    return null;
                code += codes[y];
                indexs.Add(y);
            }
            int m = 0;
            for (int i = indexs.Count - 1; i >= 0; i--)
                m += (((indexs.Count - 1 - i) % 20) + 1) * indexs[i];
            indexs.Add(m % 47);
            int n = 0;
            for (int i = indexs.Count - 1; i >= 0; i--)
                n += (((indexs.Count - 1 - i) % 15) + 1) * indexs[i];
            code += codes[m % 47];
            code += codes[n % 47];
            code += codes[48];
            indexs.Clear();
            return code;
        }
    }
}
