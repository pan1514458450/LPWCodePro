using System.Reflection;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.vlcControl1.Play(new FileInfo("C:\\Users\\BayMax\\Desktop\\3a8c95f27d3ad423f06e0787689ebf03.mp4"));
        }

        private void vlcControl1_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
                return;
            if (AssemblyName.GetAssemblyName(currentAssembly.Location).ProcessorArchitecture == ProcessorArchitecture.X86)
                e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", @"win-x86"));
            else
                e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", @"win-x64"));

            if (!e.VlcLibDirectory.Exists)
            {
                var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                folderBrowserDialog.Description = "Select Vlc libraries folder.";
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderBrowserDialog.ShowNewFolderButton = true;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    //D:\Code\LPWCodePro\WinFormsApp1\bin\Debug\net6.0-windows\libvlc\win-x64

                    //D:\Code\LPWCodePro\WinFormsApp1\bin\Debug\net6.0-windows\libvlc\win\x64\
                    e.VlcLibDirectory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.vlcControl1.Play(new FileInfo("C:\\Users\\BayMax\\Desktop\\3a8c95f27d3ad423f06e0787689ebf03.mp4"));
        }
    }
}
