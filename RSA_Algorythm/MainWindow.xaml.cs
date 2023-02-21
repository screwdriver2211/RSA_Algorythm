using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        int[] encryptedMessage;
        
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            encryptLabel.Content = string.Empty;
            encryptedMessage = rsa.EncryptMessage(encryptMessage.Text, rsa.e, rsa.n);
            foreach (var message in encryptedMessage)
            {
                encryptLabel.Content += message.ToString();
            }
            encryptMessage.Text = string.Empty;
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            decryptMessage.Text = rsa.DecryptMessage(encryptedMessage, rsa.d, rsa.n);
        }
    }
}
