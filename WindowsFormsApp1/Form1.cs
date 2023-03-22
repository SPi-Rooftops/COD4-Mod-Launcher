using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;

namespace WindowsFormsApp1
{




    public partial class Form1 : Form
    {

        public string spmp;
        public string selected_mod;

        public int setting_selected_mod;
        public string setting_radio_spmp;

        public object local_storage;

        public Form1()
        {
            InitializeComponent();
            read_settings();
        }
        
        //################## LOAD #########################
        public void read_settings()
        {
            if (File.Exists("settings.txt"))
            {
                using (StreamReader sr = new StreamReader("settings.txt"))
                {
                    textBox1.Text = sr.ReadLine();
                    setting_radio_spmp = sr.ReadLine();
                    if (setting_radio_spmp == "sp")
                    {
                        radioButton1.Checked = true;
                        radioButton2.Checked = false;
                    }
                    else if (setting_radio_spmp == "mp")
                    {
                        radioButton2.Checked = true;
                        radioButton1.Checked = false;

                    }

                    PopulateListBox();
                }
            }
        }

        //################## SAVE #########################

        public void save_settings()
        {
            using (StreamWriter sw = new StreamWriter("settings.txt"))
            {
                sw.WriteLine(textBox1.Text);
                if (radioButton1.Checked)
                {
                    sw.WriteLine("sp");
                }
                else if (radioButton2.Checked)
                {
                    sw.WriteLine("mp");
                }
            }
        }

        //################## BROWSE ... #########################

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.SelectedPath = "";
            DialogResult result = folderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderPath = folderBrowser.SelectedPath;
                textBox1.Text = folderPath;
                save_settings();
            }
        }

        //###################### SET PATH ####################


        private void button1_Click_1(object sender, EventArgs e)
        {  
            if (Directory.Exists(textBox1.Text))
            {
                //MessageBox.Show("Directory path saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopulateListBox();
            }
            else
            {
                MessageBox.Show("Invalid directory path...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //###################### LIST ############################

        private void PopulateListBox()
        {
            string path = textBox1.Text+"/mods";
            if (Directory.Exists(path))
            {
                listBox1.Items.Clear();
                string[] directories = Directory.GetDirectories(path);
                foreach (string directory in directories)
                {
                    string directoryName = new DirectoryInfo(directory).Name;
                    listBox1.Items.Add(directoryName);
                }
                //listBox1.SelectedIndex = 0; // errors
            }
            else
            {
                listBox1.Items.Clear();
            }
        }

       
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_mod = listBox1.SelectedItem.ToString();
            setting_selected_mod = listBox1.Items.IndexOf(selected_mod);
            Console.WriteLine(setting_selected_mod);
            save_settings();
        }
        

        //###################### START BUTTON ############################

        private void button_start_Click(object sender, EventArgs e)
        {
            string appname = "iw3sp.exe";
            if (spmp == "sp")
                appname = "iw3sp.exe";
            else if (spmp == "mp")
                appname = "iw3mp.exe";

            Process.Start(textBox1.Text + "\\" + appname , " +set fs_game mods/" + listBox1.SelectedItem.ToString());
        }

        //###################### RADIO BUTTONS ############################

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedRadioButton = groupBox1.Controls.OfType<RadioButton>()
                .FirstOrDefault(rb => rb.Checked);
            if (selectedRadioButton != null)
            {
                spmp = "mp";
                Console.WriteLine("MP");
                //save_settings(); // errors
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedRadioButton = groupBox1.Controls.OfType<RadioButton>()
                .FirstOrDefault(rb => rb.Checked);
            if (selectedRadioButton != null)
            {
                spmp = "sp";
                Console.WriteLine("SP");
                //save_settings();// errors
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            groupBox1.Controls.OfType<RadioButton>()
            .ToList()
            .ForEach(rb => rb.CheckedChanged += radioButton1_CheckedChanged);
        }

        //###################### OTHERBUTTONS ############################

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        // Directory
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // string path = textBox1.Text; // Get the directory path entered by the user
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


    }
}