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
//using MaterialSkin.Controls;
//using MaterialSkin;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public string spmp;
        public int setting_selected_mod;
        public string setting_radio_spmp;



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

                        // saved folder path
                        textBox1.Text = sr.ReadLine();

                        // sp mp checkbox
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
                        spmp = setting_radio_spmp;


                        PopulateListBox();
                        // some errors

                        // selected mod
                        string line3_str = sr.ReadLine();
                        if (line3_str != null)
                        {
                            int line3_int = int.Parse(line3_str);
                            if (listBox1.SelectedIndex != null && line3_int != null && listBox1.Items.Count >= line3_int)
                            {
                                listBox1.SelectedIndex = line3_int;//setting_selected_mod;
                                Console.WriteLine(line3_int);
                            }
                        }


                    }
                }
        }

        //################## SAVE #########################

        public void save_settings()
        {
             using (StreamWriter sw = new StreamWriter("settings.txt"))
            {
                sw.WriteLine(textBox1.Text);
                sw.WriteLine(spmp);
                sw.WriteLine(setting_selected_mod);

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
            }
        }

        //###################### SET PATH ####################

        private void button1_Click_1(object sender, EventArgs e)
        {  
            if (Directory.Exists(textBox1.Text))
            {
                //MessageBox.Show("Directory path saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopulateListBox();
                save_settings();

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
            }
            else
            {
                listBox1.Items.Clear();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_mod = listBox1.SelectedItem.ToString();
            setting_selected_mod = listBox1.SelectedIndex;
            Console.WriteLine(setting_selected_mod);
            //save_settings();

        }
        
        //###################### START BUTTON ############################

        private void button_start_Click(object sender, EventArgs e)
        {
            string appname = "iw3sp.exe";
            if (spmp == "sp")
                appname = "iw3sp.exe";
            else if (spmp == "mp")
                appname = "iw3mp.exe";

            if (listBox1.SelectedItem == null )
                MessageBox.Show("No mod selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                ProcessStartInfo info = new ProcessStartInfo(appname);
                info.FileName = appname;
                info.Arguments = " +set fs_game mods/" + listBox1.SelectedItem.ToString();
                info.WorkingDirectory = textBox1.Text;
                Process.Start(info);
            }
        }

        //###################### RADIO BUTTONS ############################

        private void RadioButton1_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            spmp = "sp";
            save_settings();
        }

        private void RadioButton2_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = true;
            spmp = "mp";
            save_settings();
        }

        //###################### LINKS ############################

        // SPi's Moddb
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.moddb.com/members/spi-hamentsios10/mods");
        }

        // SPi's YouTube
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCYpYA90Nr8C1qQnUrTlbCyA");

        }

        //SPi's Discord
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/Rs68sr4");

        }
        // App's GitHub
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/SPi-Rooftops/COD4-Mod-Launcher");

        }

        //###################### OTHERBUTTONS ############################

        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void label4_Click(object sender, EventArgs e)
        {
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }


    }
}