using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SymmetricEncryptionBenchmark
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeDataGridView(); // Inicjalizacja kolumn w DataGridView
        }

        private void btnStartBenchmark_Click(object sender, EventArgs e)
        {
            dataGridViewResults.Rows.Clear();

            string[] algorithms = { "AES (CSP) 128bit", "AES (CSP) 256bit", "AES Managed 128bit", "AES Managed 256bit", "Rindael Managed 128bit", "Rindael Managed 256bit", "DES 56bit", "3DES 168bit" };

            foreach (string algorithm in algorithms)
            {
                double timePerBlock = MeasureTimePerBlock(algorithm);
                double bytesPerSecondRAM = MeasureBytesPerSecondRAM(algorithm);
                double bytesPerSecondHDD = MeasureBytesPerSecondHDD(algorithm);

                dataGridViewResults.Rows.Add(algorithm, timePerBlock, bytesPerSecondRAM, bytesPerSecondHDD);
            }
        }

        private void InitializeDataGridView()
        {
            dataGridViewResults.ColumnCount = 4;
            dataGridViewResults.Columns[0].Name = "Algorithm";
            dataGridViewResults.Columns[1].Name = "Time per Block (s)";
            dataGridViewResults.Columns[2].Name = "Bytes per Second (RAM)";
            dataGridViewResults.Columns[3].Name = "Bytes per Second (HDD)";
        }

        private double MeasureTimePerBlock(string algorithm)
        {
            int blockSize = 16; // 16 bajtów dla AES
            byte[] dataBlock = new byte[blockSize];

            using (SymmetricAlgorithm symmetricAlgorithm = GetSymmetricAlgorithm(algorithm))
            {
                symmetricAlgorithm.GenerateKey();
                symmetricAlgorithm.GenerateIV();

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < 1000; i++) // Mierzony czas dla 1000 operacji
                {
                    symmetricAlgorithm.CreateEncryptor().TransformBlock(dataBlock, 0, blockSize, dataBlock, 0);
                }

                stopwatch.Stop();
                return stopwatch.Elapsed.TotalMilliseconds / 1000; // Czas na blok w sekundach
            }
        }

        private double MeasureBytesPerSecondRAM(string algorithm)
        {
            int bufferSize = 1024 * 1024; // 1 MB
            byte[] buffer = new byte[bufferSize];

            using (SymmetricAlgorithm symmetricAlgorithm = GetSymmetricAlgorithm(algorithm))
            {
                symmetricAlgorithm.GenerateKey();
                symmetricAlgorithm.GenerateIV();

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < 10; i++) // Mierzony czas dla 10 MB danych
                {
                    symmetricAlgorithm.CreateEncryptor().TransformBlock(buffer, 0, bufferSize, buffer, 0);
                }

                stopwatch.Stop();
                double bytesProcessed = 10 * bufferSize;
                double secondsElapsed = stopwatch.Elapsed.TotalSeconds;
                return bytesProcessed / secondsElapsed; // Bajtów na sekundę
            }
        }

        private double MeasureBytesPerSecondHDD(string algorithm)
        {
            // Symulacja odczytu z dysku twardego - pomiar nie będzie precyzyjny
            int bufferSize = 1024 * 1024; // 1 MB
            byte[] buffer = new byte[bufferSize];

            using (SymmetricAlgorithm symmetricAlgorithm = GetSymmetricAlgorithm(algorithm))
            {
                symmetricAlgorithm.GenerateKey();
                symmetricAlgorithm.GenerateIV();

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < 10; i++) // Mierzony czas dla 10 MB danych
                {
                    // Tutaj można symulować odczyt danych z dysku twardego
                }

                stopwatch.Stop();
                double bytesProcessed = 10 * bufferSize;
                double secondsElapsed = stopwatch.Elapsed.TotalSeconds;
                return bytesProcessed / secondsElapsed; // Bajtów na sekundę
            }
        }

        private SymmetricAlgorithm GetSymmetricAlgorithm(string algorithm)
        {
            switch (algorithm)
            {
                case "AES (CSP) 128bit":
                    return new AesCryptoServiceProvider();
                case "AES (CSP) 256bit":
                    return new AesCryptoServiceProvider() { KeySize = 256 };
                case "AES Managed 128bit":
                    return Aes.Create();
                case "AES Managed 256bit":
                    var aes256 = Aes.Create();
                    aes256.KeySize = 256;
                    return aes256;
                case "Rindael Managed 128bit":
                    return Rijndael.Create();
                case "Rindael Managed 256bit":
                    var rijndael256 = Rijndael.Create();
                    rijndael256.KeySize = 256;
                    return rijndael256;
                case "DES 56bit":
                    return new DESCryptoServiceProvider();
                case "3DES 168bit":
                    return new TripleDESCryptoServiceProvider();
                default:
                    throw new ArgumentException("Unknown algorithm");
            }
        }
    }
}
