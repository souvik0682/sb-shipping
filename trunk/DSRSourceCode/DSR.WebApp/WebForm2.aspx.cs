using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace DSR.WebApp
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string input = @"£©­¢²©¡€¶Ž€•¡|";
            string output = string.Empty;
            output = Descramble(input);
        }

        private string Descramble(string cInString)
        {
            int nInStrLen = 0;
            char cNextChar;
            string cOutString = string.Empty;

            if (!string.IsNullOrEmpty(cInString))
            {
                nInStrLen = cInString.Length;

                for (int nCounter = 0; nCounter < nInStrLen; nCounter++)
                {
                    cNextChar = Convert.ToChar(cInString.Substring(nCounter, 1));

                    //if ((int)cNextChar > 255)
                    cOutString = cOutString + ConvertUnicodeToAscii(Convert.ToString((char)(cNextChar - 96)));
                }
            }

            //return cOutString;
            return cOutString;
        }

        private string ConvertUnicodeToAscii(string unicodeString)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;
                        
            // Convert the string into a byte[].
            byte[] unicodeBytes = unicode.GetBytes(unicodeString);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string.
            // This is a slightly different approach to converting to illustrate
            // the use of GetCharCount/GetChars.
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);

            // Display the strings created before and after the conversion.
            return asciiString;
        }
    }
}