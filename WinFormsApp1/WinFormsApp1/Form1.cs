using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        //Encrypts the text
        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string plainText = textBoxText.Text; //Get the plain text from the textbox
                string key = textBoxKey.Text; //Get the key from the key textbox
                string function = comboBoxFunctions.Text; //Get the text from the functions combo box
                string cypherText = ""; //The cyphertext being returned
                if (function == "Caesar Cypher")
                {
                    int caesarKey = int.Parse(key);
                    cypherText = CaesarCypher(plainText, caesarKey, true);
                }
                textBoxText.Text = cypherText;
            }
            catch
            {
                MessageBox.Show("Validate your inputs");
            }
        }
        //Decrypts the text
        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string cypherText = textBoxText.Text; //Get the plain text from the textbox
                string key = textBoxKey.Text; //Get the key from the key textbox
                string function = comboBoxFunctions.Text; //Get the text from the functions combo box
                string plainText = ""; //The cyphertext being returned
                if (function == "Caesar Cypher")
                {
                    int caesarKey = int.Parse(key);
                    plainText = CaesarCypher(cypherText, caesarKey, false);
                }
                textBoxText.Text = plainText;
            }
            catch
            {
                MessageBox.Show("Validate your inputs");
            }
        }
        //Casear text
        private string CaesarCypher(string text, int key, bool isEncryption)
        {
            StringBuilder caesarText = new StringBuilder();
            key = key % 26; //Modulo 26
            if (isEncryption)
            {
                //Go through each character of the plain text
                for (int i = 0; i < text.Length; i++)
                {
                    char letter = text[i];
                    int number = (int)letter; //Select the ASCII number of the capital letter
                    //If they are capital
                    if (letter >= 65 && letter <= 90)
                    {
                        number -= 65; //Subtract by 65
                        number += key; //Add the key
                        number %= 26; //Modulo 26
                        number += 97; //Add 97 to transform into small
                    }
                    else if (letter >= 97 && letter <= 122) //If the letter is small
                    {
                        number -= 97; //Subtract by 97
                        number += key; //Add the key
                        number %= 26; //Modulo 26
                        number += 65; //Add 65 to transform into capital
                    }
                    caesarText.Append((char)number);
                }
            }
            else
            {
                //Go through each character of the plain text
                for (int i = 0; i < text.Length; i++)
                {
                    char letter = text[i];
                    int number = (int)letter; //Select the ASCII number of the capital letter
                    //If they are capital
                    if (letter >= 65 && letter <= 90)
                    {
                        number -= 65; //Subtract by 65
                        number -= key; //Add the key
                        if (number < 0) //If the number is a negative
                        {
                            number += 26; //Convert to positive
                        }
                        number %= 26; //Add 26
                        number += 97; //Add 97 to transform into small
                    }
                    else if (letter >= 97 && letter <= 122) //If the letter is small
                    {
                        number -= 97; //Subtract by 97
                        number -= key; //Add the key
                        if (number < 0) //If the number is a negative
                        {
                            number += 26; //Convert to positive
                        }
                        number %= 26; //Add 26
                        number += 65; //Add 65 to transform into capital
                    }
                    caesarText.Append((char)number);
                }
            }
            return caesarText.ToString();
        }
        //Find key for the encrypted message
        private void buttonFindKey_Click(object sender, EventArgs e)
        {
            try
            {
                string function = comboBoxFunctions.Text; //Get the selected function
                string cypherText = textBoxText.Text; //The text in the text box
                string plainText;
                string lowerText;
                List<string> keyList = new List<string>();
                StringBuilder builder = new StringBuilder();
                if (function == "Caesar Cypher")
                {
                    for (int i = 0; i < 26; i++)
                    {
                        plainText = CaesarCypher(cypherText, i, false);
                        lowerText = plainText.ToLower(); //lower case for checking
                        if (lowerText.Contains("the") || lowerText.Contains("is") || lowerText.Contains("or")) //Contains any essentiall words
                        {
                            keyList.Add(i.ToString());
                        }
                    }
                }
                builder.Append("Possible keys for " + function + " is : ");
                foreach (string key in keyList)
                {
                    builder.Append(key + ",");
                }
                textBoxLog.Text = builder.ToString();
            }
            catch
            {
                MessageBox.Show("Validate inputs");
            }
        }
    }
}
