namespace Proyecto_Integrador.View
{
    partial class FormRegistroAlumno
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
            lblId = new Label();
            txtId = new TextBox();
            lblNombreCompleto = new Label();
            txtNombreCompleto = new TextBox();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblConfirmar = new Label();
            txtConfirmar = new TextBox();
            btnRegistrar = new Button();
            SuspendLayout();
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(52, 23);
            lblId.Name = "lblId";
            lblId.Size = new Size(123, 25);
            lblId.TabIndex = 0;
            lblId.Text = "ID / Matrícula:";
            // 
            // txtId
            // 
            txtId.Location = new Point(221, 21);
            txtId.Name = "txtId";
            txtId.Size = new Size(220, 31);
            txtId.TabIndex = 1;
            // 
            // lblNombreCompleto
            // 
            lblNombreCompleto.AutoSize = true;
            lblNombreCompleto.Location = new Point(52, 57);
            lblNombreCompleto.Name = "lblNombreCompleto";
            lblNombreCompleto.Size = new Size(163, 25);
            lblNombreCompleto.TabIndex = 2;
            lblNombreCompleto.Text = "Nombre completo:";
            // 
            // txtNombreCompleto
            // 
            txtNombreCompleto.Location = new Point(221, 58);
            txtNombreCompleto.Name = "txtNombreCompleto";
            txtNombreCompleto.Size = new Size(220, 31);
            txtNombreCompleto.TabIndex = 3;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(81, 98);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(76, 25);
            lblUsuario.TabIndex = 4;
            lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(221, 95);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(220, 31);
            txtUsuario.TabIndex = 5;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(52, 137);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(105, 25);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(221, 137);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(220, 31);
            txtPassword.TabIndex = 7;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirmar
            // 
            lblConfirmar.AutoSize = true;
            lblConfirmar.Location = new Point(10, 188);
            lblConfirmar.Name = "lblConfirmar";
            lblConfirmar.Size = new Size(186, 25);
            lblConfirmar.TabIndex = 8;
            lblConfirmar.Text = "Confirmar contraseña:";
            // 
            // txtConfirmar
            // 
            txtConfirmar.Location = new Point(221, 188);
            txtConfirmar.Name = "txtConfirmar";
            txtConfirmar.Size = new Size(220, 31);
            txtConfirmar.TabIndex = 9;
            txtConfirmar.UseSystemPasswordChar = true;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(160, 237);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(100, 40);
            btnRegistrar.TabIndex = 10;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            // 
            // FormRegistroAlumno
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRegistrar);
            Controls.Add(txtConfirmar);
            Controls.Add(lblConfirmar);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsuario);
            Controls.Add(lblUsuario);
            Controls.Add(txtNombreCompleto);
            Controls.Add(lblNombreCompleto);
            Controls.Add(txtId);
            Controls.Add(lblId);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormRegistroAlumno";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registro de alumno";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblId;
        private TextBox txtId;
        private Label lblNombreCompleto;
        private TextBox txtNombreCompleto;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmar;
        private TextBox txtConfirmar;
        private Button btnRegistrar;
    }
}