namespace Client_Side
{
    partial class Login
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.l_username = new System.Windows.Forms.Label();
            this.l_password = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.to_register = new System.Windows.Forms.Button();
            this.status_info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // l_username
            // 
            this.l_username.AutoSize = true;
            this.l_username.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.l_username.Location = new System.Drawing.Point(32, 42);
            this.l_username.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.l_username.Name = "l_username";
            this.l_username.Size = new System.Drawing.Size(61, 13);
            this.l_username.TabIndex = 0;
            this.l_username.Text = "Username :";
            // 
            // l_password
            // 
            this.l_password.AutoSize = true;
            this.l_password.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.l_password.Location = new System.Drawing.Point(32, 92);
            this.l_password.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.l_password.Name = "l_password";
            this.l_password.Size = new System.Drawing.Size(59, 13);
            this.l_password.TabIndex = 1;
            this.l_password.Text = "Password :";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(119, 39);
            this.username.Margin = new System.Windows.Forms.Padding(2);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(76, 20);
            this.username.TabIndex = 2;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(119, 89);
            this.password.Margin = new System.Windows.Forms.Padding(2);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(76, 20);
            this.password.TabIndex = 3;
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(35, 156);
            this.btn_login.Margin = new System.Windows.Forms.Padding(2);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(56, 23);
            this.btn_login.TabIndex = 6;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click_1);
            // 
            // to_register
            // 
            this.to_register.Location = new System.Drawing.Point(169, 156);
            this.to_register.Margin = new System.Windows.Forms.Padding(2);
            this.to_register.Name = "to_register";
            this.to_register.Size = new System.Drawing.Size(56, 23);
            this.to_register.TabIndex = 7;
            this.to_register.Text = "Register";
            this.to_register.UseVisualStyleBackColor = true;
            this.to_register.Click += new System.EventHandler(this.to_register_Click);
            // 
            // status_info
            // 
            this.status_info.AutoSize = true;
            this.status_info.Location = new System.Drawing.Point(116, 131);
            this.status_info.Name = "status_info";
            this.status_info.Size = new System.Drawing.Size(0, 13);
            this.status_info.TabIndex = 8;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(251, 190);
            this.Controls.Add(this.status_info);
            this.Controls.Add(this.to_register);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.l_password);
            this.Controls.Add(this.l_username);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label l_username;
        private System.Windows.Forms.Label l_password;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Button to_register;
        private System.Windows.Forms.Label status_info;
    }
}

