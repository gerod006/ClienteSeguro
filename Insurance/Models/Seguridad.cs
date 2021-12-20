using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
 namespace Insurance.Models
{
    public class Seguridad
    {
        public static string GetSqlConnection(string connectionStringName = "DefaultConnection")
        {
            // optionally defaults to "DefaultConnection" if no connection string name is inputted
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            string passPhrase = "prueba2111111111";
            // decrypt password
            string password = get_prase_after_word(connectionString, "password=", ";");
            //connectionString =  StringCipher.StringCipher.Encrypt("", passPhrase);

            connectionString = connectionString.Replace(password, StringCipher.StringCipher.Decrypt(password, passPhrase));
            return connectionString;
        }
        public static string get_prase_after_word(string search_string_in, string word_before_in, string word_after_in)
        {
            int myStartPos = 0;
            string myWorkString = "";

            // get position where phrase "word_before_in" ends

            if (!string.IsNullOrEmpty(word_before_in))
            {
                myStartPos = search_string_in.ToLower().IndexOf(word_before_in) + word_before_in.Length;

                // extract remaining text
                myWorkString = search_string_in.Substring(myStartPos, search_string_in.Length - myStartPos).Trim();

                if (!string.IsNullOrEmpty(word_after_in))
                {
                    // get position where phrase starts in the working string
                    myWorkString = myWorkString.Substring(0, myWorkString.IndexOf(word_after_in)).Trim();

                }
            }
            else
            {
                myWorkString = string.Empty;
            }
            return myWorkString.Trim();
        }
    }
}