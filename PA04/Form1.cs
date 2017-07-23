//PA04 -Word Search Windows Programming
//    Venus Buccat 815649868
//    Taghreed Alzahrani 817379453



using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PA04
{
    public partial class frmWordSearchProgram : Form
    {
        public frmWordSearchProgram()
        {
            InitializeComponent();
           
        }

        private void lbWordList_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void frmWordSearchProgram_Load(object sender, EventArgs e)
        {
            String[] word_list = Properties.Resources.WordList.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            lbWordList.BeginUpdate();
            lbWordList.Items.Clear();
            foreach (string s in word_list)
                lbWordList.Items.Add(s);
            lbWordList.EndUpdate();
        }

        private bool findText(string text_str)
        {
            bool found = false;

            foreach (object lbItem in lbWordList.Items)
            {
                if (lbItem.ToString().StartsWith(text_str, StringComparison.CurrentCultureIgnoreCase))
                {
                    lbWordList.SelectedIndex = lbWordList.Items.IndexOf(lbItem);
                    found = true;
                    break;
                }
            }

            return found;
        }

        private void btnMorph_Click(object sender, EventArgs e)
        {
            lbResults.BeginUpdate();
            lbResults.Items.Clear();
          
            string keyword = txtUserInput.Text;
            int length = keyword.Length;
            bool found = false;
            lbResults.Items.Clear();
            foreach (string line in lbWordList.Items)
            {
                if (line.Length == length)
                {
                    if (line == keyword) continue;
                    int diff = 0; int same = 0; 

                    for (int i =0; i<length; i++)
                    {
                        if (line[i] == keyword[i]) same++;
                        else diff++; 
                    }

                    if (same == length - 1)
                    {
                        lbResults.Items.Add(line);
                        found = true;
                    }
                }
            }
            if (!found)
            {
                MessageBox.Show("No result found");
                //  lbResults.Items.Clear();
            }
            lbResults.EndUpdate();
        }

        private void btnScrabble_Click(object sender, EventArgs e)
        {
            bool found = false;
            lbResults.BeginUpdate();
            lbResults.Items.Clear();
            string inputTemp;
            string wordTemp;
            string keyword = txtUserInput.Text;

            if (keyword.Length < 3 || keyword.Length > 7)
            {
                string msgStr = String.Format("String '{0}' does not meet the string requirement!\nEnter 3-7 characters ONLY!", txtUserInput.Text);
                MessageBox.Show(msgStr);
            }
            else
            {

                foreach (string line in lbWordList.Items)
                {
                    inputTemp = keyword;
                    wordTemp = line;

                    foreach (char letter in line)
                    {
                        if (inputTemp.ToLower().Contains(letter))
                        {
                            int index = inputTemp.IndexOf(letter);

                            if (index != -1)
                            {
                                inputTemp = inputTemp.Remove(index, 1);
                                index = wordTemp.IndexOf(letter);
                                wordTemp = wordTemp.Remove(index, 1);
                            }
                        }

                    }

                    if (wordTemp.Length == 0)
                    {
                        if (line.Length > 2) lbResults.Items.Add(line);
                        found = true;
                    }
                }

                if (!found)
                {
                    MessageBox.Show("No result found");

                }
                lbResults.EndUpdate();
            }
       
        }

        private void btnRhyme_Click(object sender, EventArgs e)
        {
            lbResults.BeginUpdate();
            lbResults.Items.Clear();
            bool found = false;
            string keyword;
           
            keyword = txtUserInput.Text; 
            foreach (string word in lbWordList.Items)
            {
                if (word.EndsWith(keyword))
                {
                    lbResults.Items.Add(word);
                    found = true;
                }
            }
            if (!found)
            {
                MessageBox.Show("No result found");
                // lbResults.Items.Clear();
            }
            lbResults.EndUpdate();
        }

        private void lbWordList_DoubleClick(object sender, EventArgs e)
        {
            txtUserInput.Text = lbWordList.Text;
        

        }

        private void txtUserInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            
             string msgHelp = String.Format("\n\nDictionary: \nThe Dictionary contains the complete list of words that is available for this program. "
               + "\nUser can also double click a word from the Dictionary and will be assigned asthe user input."
                 + "\n\nMorph: \nMorph Words are words that differ from a specified word in only one letter. "
               + "\nThe user must enter a word into the textbox."
               + "\nBy clicking the Morph Button, all the morph words will be displayed in the Results ListBox."


               + "\n\nSrabble:"
                + "\nScrabble words are constructed from the letters entered by the user"
               + "\nUser must enter a string of scrabble letters."
               + "\nScrabble letters must from 3-7 character ONLY!"
               + "\nIf the input is not in the range, a message box appears to warn the user. Hence, user must make another input. "
               + "\nBy clicking the Srabble Button, all the scrabble words will be displayed in the Results ListBox."

               + "\n\nRhyming:"
               + "\nRhyming Words words that have the same ending string."

               + "\nUser must input a desired ending string."
            + "\nBy clicking the Rhyming Button, all the rhyming words will be displayed in the Results ListBox."


               + "\n\nIf no results are found, the result box will be blank."
              

               );
            MessageBox.Show(msgHelp);
        }
    }
}
