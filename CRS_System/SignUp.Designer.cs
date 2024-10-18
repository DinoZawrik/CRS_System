namespace CRS_System
{
    partial class SignUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignUp));
            this.FirstName = new System.Windows.Forms.TextBox();
            this.SecondName = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.Registr = new System.Windows.Forms.Button();
            this.BackHome = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FirstName
            // 
            this.FirstName.BackColor = System.Drawing.Color.AntiqueWhite;
            this.FirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FirstName.Location = new System.Drawing.Point(30, 270);
            this.FirstName.Multiline = true;
            this.FirstName.Name = "FirstName";
            this.FirstName.Size = new System.Drawing.Size(300, 60);
            this.FirstName.TabIndex = 0;
            this.FirstName.Text = "Фамилия";
            this.FirstName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SecondName
            // 
            this.SecondName.BackColor = System.Drawing.Color.AntiqueWhite;
            this.SecondName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SecondName.Location = new System.Drawing.Point(350, 270);
            this.SecondName.Multiline = true;
            this.SecondName.Name = "SecondName";
            this.SecondName.Size = new System.Drawing.Size(300, 60);
            this.SecondName.TabIndex = 1;
            this.SecondName.Text = "Имя";
            this.SecondName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Email
            // 
            this.Email.BackColor = System.Drawing.Color.AntiqueWhite;
            this.Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Email.Location = new System.Drawing.Point(200, 370);
            this.Email.Multiline = true;
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(300, 60);
            this.Email.TabIndex = 2;
            this.Email.Text = "Email";
            this.Email.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Password
            // 
            this.Password.BackColor = System.Drawing.Color.AntiqueWhite;
            this.Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Password.Location = new System.Drawing.Point(200, 470);
            this.Password.Multiline = true;
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(300, 60);
            this.Password.TabIndex = 3;
            this.Password.Text = "*********";
            this.Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Registr
            // 
            this.Registr.BackColor = System.Drawing.Color.Teal;
            this.Registr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Registr.ForeColor = System.Drawing.Color.White;
            this.Registr.Location = new System.Drawing.Point(150, 600);
            this.Registr.Name = "Registr";
            this.Registr.Size = new System.Drawing.Size(400, 100);
            this.Registr.TabIndex = 4;
            this.Registr.Text = "Зарегистрироваться";
            this.Registr.UseVisualStyleBackColor = false;
            this.Registr.Click += new System.EventHandler(this.Registr_Click);
            // 
            // BackHome
            // 
            this.BackHome.BackColor = System.Drawing.Color.Teal;
            this.BackHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BackHome.ForeColor = System.Drawing.Color.White;
            this.BackHome.Location = new System.Drawing.Point(12, 12);
            this.BackHome.Name = "BackHome";
            this.BackHome.Size = new System.Drawing.Size(166, 59);
            this.BackHome.TabIndex = 6;
            this.BackHome.Text = "Меню";
            this.BackHome.UseVisualStyleBackColor = false;
            this.BackHome.Click += new System.EventHandler(this.BackHome_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 858);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 37);
            this.label1.TabIndex = 7;
            this.label1.Text = "Уже есть аккаунт?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(359, 858);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 37);
            this.label2.TabIndex = 8;
            this.label2.Text = "Авторизоваться";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(674, 929);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BackHome);
            this.Controls.Add(this.Registr);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.SecondName);
            this.Controls.Add(this.FirstName);
            this.Name = "SignUp";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SignUp";
            this.Load += new System.EventHandler(this.SignUp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FirstName;
        private System.Windows.Forms.TextBox SecondName;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button Registr;
        private System.Windows.Forms.Button BackHome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}