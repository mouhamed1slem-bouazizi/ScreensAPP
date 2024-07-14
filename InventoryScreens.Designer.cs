﻿namespace ScreensAPP
{
    partial class InventoryScreens
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Crimson;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1782, 125);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Bottom;
            label1.Font = new Font("Calibri", 22.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(0, 60);
            label1.Margin = new Padding(9);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 0, 0, 11);
            label1.Size = new Size(1782, 65);
            label1.TabIndex = 2;
            label1.Text = "Manage Screens";
            label1.TextAlign = ContentAlignment.BottomCenter;
            label1.UseMnemonic = false;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Calibri", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(1782, 79);
            label2.TabIndex = 1;
            label2.Text = "DMM inventory management";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.UseMnemonic = false;
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(340, 182);
            label3.Name = "label3";
            label3.Size = new Size(204, 28);
            label3.TabIndex = 3;
            label3.Text = "Line A - Counter SA01";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(610, 182);
            label4.Name = "label4";
            label4.Size = new Size(123, 28);
            label4.TabIndex = 4;
            label4.Text = "10.82.130.10";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(774, 182);
            label5.Name = "label5";
            label5.Size = new Size(150, 28);
            label5.TabIndex = 5;
            label5.Text = "e0:73:e7:cf:97:f7";
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(973, 182);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(76, 28);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(1108, 182);
            button1.Name = "button1";
            button1.Size = new Size(94, 28);
            button1.TabIndex = 9;
            button1.Text = "UPtime";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(1220, 182);
            button2.Name = "button2";
            button2.Size = new Size(119, 28);
            button2.TabIndex = 10;
            button2.Text = "FIDS Restart";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(1354, 182);
            button3.Name = "button3";
            button3.Size = new Size(108, 28);
            button3.TabIndex = 11;
            button3.Text = "Memory";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(1482, 182);
            button4.Name = "button4";
            button4.Size = new Size(108, 28);
            button4.TabIndex = 12;
            button4.Text = "Free Memory";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // InventoryScreens
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1782, 1045);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(panel1);
            Name = "InventoryScreens";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "InventoryScreens";
            WindowState = FormWindowState.Maximized;
            Load += InventoryScreens_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label2;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private PictureBox pictureBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}