using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            Thread t = new Thread(new ThreadStart(SplashScreenStart));
            t.Start();
            Thread.Sleep(5000);

            InitializeComponent();

            t.Abort();

        }

        public void SplashScreenStart()
        {
            Application.Run(new Form2());
        }

        public string OpenFile()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    return openFileDialog.FileName;
                }
            }
            return "0";
        }

        public void SaveFile(string input)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string name = saveFileDialog.FileName;
                File.WriteAllText(name, input);
            }
        }

        private void buttonImporta_Click(object sender, EventArgs e)
        {

            int i = 0;
            String line;
            int num;

            //textArea.Text = "";

            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(OpenFile());

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {

                    try
                    {
                        //textArea.Text += line + "\n";
                        num = System.Convert.ToInt32(line);

                        Vector.getInstance().addOrd(num);

                        i++;
                        line = sr.ReadLine();
                    }
                    catch (Exception a)
                    {

                    }

                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception a)
            {
                MessageBox.Show("Exception: " + a.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            listValoriArray.DataSource = Vector.getInstance().getArray();

        }

        private void buttonInserisci_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBoxValori.Text);
            Vector.getInstance().addOrd(num);
            listValoriArray.DataSource = Vector.getInstance().getArray();
        }

        private void buttonEsporta_Click(object sender, EventArgs e)
        {

            string path = OpenFile();
            System.IO.File.WriteAllText(path, "");

            for (int i = 0; i < Vector.getInstance().getArray().Length; i++)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
                {
                    file.WriteLine(Vector.getInstance().getArray()[i].ToString());
                }
            }
            
        }

        private void textArea_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        

        }
    }
}
