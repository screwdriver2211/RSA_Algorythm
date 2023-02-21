using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RSA_Algorythm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RSA rsa = new();
        //string message = "";
        List<byte> encryptMessageBytes = new List<byte>();
        List<byte> decryptMessageBytes = new List<byte>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            encryptMessageBytes = rsa.Encrypt(encryptMessage.Text);
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            decryptMessageBytes = rsa.Decrypt(encryptMessageBytes);
            decryptMessage.Text = Encoding.Default.GetString(encryptMessageBytes.ToArray());        }
    }
}
