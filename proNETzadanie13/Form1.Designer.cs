using System.Windows.Forms;

namespace SymmetricEncryptionBenchmark
{
    partial class MainForm
    {
        private Button btnStartBenchmark;
        private DataGridView dataGridViewResults;

        private void InitializeComponent()
        {
            this.btnStartBenchmark = new System.Windows.Forms.Button();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartBenchmark
            // 
            this.btnStartBenchmark.Location = new System.Drawing.Point(12, 12);
            this.btnStartBenchmark.Name = "btnStartBenchmark";
            this.btnStartBenchmark.Size = new System.Drawing.Size(160, 23);
            this.btnStartBenchmark.TabIndex = 0;
            this.btnStartBenchmark.Text = "Start Benchmark";
            this.btnStartBenchmark.UseVisualStyleBackColor = true;
            this.btnStartBenchmark.Click += new System.EventHandler(this.btnStartBenchmark_Click);
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResults.Location = new System.Drawing.Point(12, 41);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.Size = new System.Drawing.Size(760, 480);
            this.dataGridViewResults.TabIndex = 1;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 533);
            this.Controls.Add(this.dataGridViewResults);
            this.Controls.Add(this.btnStartBenchmark);
            this.Name = "MainForm";
            this.Text = "Symmetric Encryption Benchmark";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
