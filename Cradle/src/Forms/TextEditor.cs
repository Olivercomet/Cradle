using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cradle
{
    
        
    

    public partial class TextEditor : Form
    {
        public Form1 form1;

        public bool continueSearch;
        public int searchStartIndex;

        public bool showingPreviewText;
        public TextEditor()
        {
            this.Icon = Properties.Resources.jenicon;
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;



        }

        public void DisplayText() {
            numericUpDown1.Maximum = Dictionaries.DialogueIDAndDialogue.Keys.Count - 1;
            TextBox.Text = Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].text;
            if (form1.isTranslatedVersion)
            {
                textOffsetLabel.Text = "Offset: " + (0x301000 + Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].offset);
            }
            else
            {
                textOffsetLabel.Text = "Offset: " + (0x020010 + Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].offset);
            }
            
            if (Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].NoSpeaker)
            {
                SpeakerSelector.SelectedItem = "None";
            }
            else
            {
                SpeakerSelector.SelectedIndex = Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].speaker + 1;
            }
            numericUpDown1.Value = form1.currentlySelectedDialogue;
            blueCheckBox.Checked = Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].blue;

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].text = TextBox.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            form1.currentlySelectedDialogue = (int)numericUpDown1.Value;
            DisplayText();
        }

        private void SpeakerSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SpeakerSelector.SelectedIndex > 0)
            {
                Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].speaker = SpeakerSelector.SelectedIndex - 1;
                Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].NoSpeaker = false;
            }
            else
            {
                Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].NoSpeaker = true;
            }
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            continueSearch = false;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
           if (searchBox.Text != "")
                {
                if (!continueSearch)
                    {
                    searchStartIndex = 0;
                    }

                bool success = false;
                int resultIndex = 0;

                for (int i = searchStartIndex+1; i < Dictionaries.DialogueIDAndDialogue.Keys.Count; i++)
                    {
                    if (Dictionaries.DialogueIDAndDialogue[i].text.ToLower().Contains(searchBox.Text.ToLower()))
                        {
                        resultIndex = i;
                        searchStartIndex = i;
                        success = true;
                        break;
                        }
                    }

                if (success)
                    {
                    numericUpDown1.Value = resultIndex;

                    DisplayText();

                    continueSearch = true;
                    }
                else
                    {
                    continueSearch = false;
                    }
                

                
                }
        }


        public async void ShowPreviewText(string text) {

            if (showingPreviewText)
                {
                return;
                }
            else
                {
                showingPreviewText = true;
                }

            int currentPlaceInText = 0;

            PreviewTextBox.Text = "";

            int delaytime = 50;

            int extraDelayTime = 0;

            string colour = "White";

            if (Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].blue)
            {
                colour = "Blue";
            }
            else
            {
                colour = "White";
            }

            while (currentPlaceInText < text.Length)
            {

                if (text[currentPlaceInText] == "["[0])
                {
                    //it's a command

                    currentPlaceInText++;

                    switch (text[currentPlaceInText].ToString() + text[currentPlaceInText + 1].ToString())
                    {
                        case "Wa":
                            //wait
                            int WaitTime = 0;
                            currentPlaceInText += 5;
                            if (text[currentPlaceInText + 1] + "" == "]")
                            {
                                WaitTime = int.Parse(text[currentPlaceInText] + "");
                            }
                            else if (text[currentPlaceInText + 2] + "" == "]")
                            {
                                WaitTime = (int.Parse(text[currentPlaceInText] + "") * 10) + int.Parse(text[currentPlaceInText] + "");
                            }
                            else if (text[currentPlaceInText + 3] + "" == "]")
                            {
                                WaitTime = (int.Parse(text[currentPlaceInText] + "") * 100) + (int.Parse(text[currentPlaceInText] + "") * 10) + int.Parse(text[currentPlaceInText] + "");
                            }

                            extraDelayTime = WaitTime * 350;

                            while (text[currentPlaceInText] + "" != "]")
                            {
                                currentPlaceInText++;
                            }
                            currentPlaceInText++;
                            break;
                        case "Sp":
                            //speed
                            string SpeedString = "";
                            currentPlaceInText += 6;

                            while (text[currentPlaceInText] + "" != "]")
                            {
                                SpeedString += text[currentPlaceInText] + "";
                                currentPlaceInText++;
                            }

                            delaytime = (900 / int.Parse(SpeedString));

                            switch (int.Parse(SpeedString))
                            {
                                case 1:
                                    delaytime = 40;
                                    break;
                                case 2:
                                    delaytime = 100;
                                    break;
                                case 3:
                                    delaytime = 300;
                                    break;
                                case 4:
                                    delaytime = 300;
                                    break;
                                case 5:
                                    delaytime = 130;
                                    break;
                                case 6:
                                    delaytime = 200;
                                    break;
                                case 7:
                                    delaytime = 200;
                                    break;
                                case 10:
                                    delaytime = 300;
                                    break;
                            }


                            currentPlaceInText++;


                            break;
                        case "Em":
                            //emotion
                            string EmotionString = "";
                            currentPlaceInText += 8;
                            while (text[currentPlaceInText] + "" != "]")
                            {
                                EmotionString += text[currentPlaceInText] + "";
                                currentPlaceInText++;
                            }
                            currentPlaceInText++;


                            switch (EmotionString)
                            {
                                case "Default":

                                    break;
                                case "Speak":

                                    break;
                                case "MouthOpen":

                                    break;
                            }

                            break;

                    }

                }
                else
                {
                    await Task.Delay(delaytime + extraDelayTime);

                    if (colour == "Blue")
                        {
                        AppendTextInColour(PreviewTextBox, text[currentPlaceInText]+"", Color.Blue);
                    }
                    else
                        {
                        AppendTextInColour(PreviewTextBox, text[currentPlaceInText]+"", Color.White);
                        }
                    currentPlaceInText++;
                    extraDelayTime = 0;
                }
               

            }

            showingPreviewText = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowPreviewText(Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].text);
        }

        public void AppendTextInColour(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        private void blueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Dictionaries.DialogueIDAndDialogue[form1.currentlySelectedDialogue].blue = blueCheckBox.Checked;
        }
    }


}
