namespace moleQule.Face
{
    partial class ChangeUserUpdate
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Usuario_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Password_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Cancelar_BT = new System.Windows.Forms.Button();
            this.Aceptar_BT = new System.Windows.Forms.Button();
            this.Dominio_TB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Cancelar_BT);
            this.splitContainer1.Panel2.Controls.Add(this.Aceptar_BT);
            this.splitContainer1.Size = new System.Drawing.Size(453, 304);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Dominio_TB);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Usuario_TB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Password_TB);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(116, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 159);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // Usuario_TB
            // 
            this.Usuario_TB.Location = new System.Drawing.Point(23, 35);
            this.Usuario_TB.Name = "Usuario_TB";
            this.Usuario_TB.Size = new System.Drawing.Size(177, 20);
            this.Usuario_TB.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario:";
            // 
            // Password_TB
            // 
            this.Password_TB.Location = new System.Drawing.Point(23, 78);
            this.Password_TB.Name = "Password_TB";
            this.Password_TB.PasswordChar = '*';
            this.Password_TB.Size = new System.Drawing.Size(177, 20);
            this.Password_TB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Contraseña:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(361, 48);
            this.label3.TabIndex = 6;
            this.label3.Text = "El usuario que especifique aqui sera solo usado para instalar la actualizacion. U" +
                "sted seguira trabajando con su usuario actual.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cancelar_BT
            // 
            this.Cancelar_BT.Location = new System.Drawing.Point(232, 10);
            this.Cancelar_BT.Name = "Cancelar_BT";
            this.Cancelar_BT.Size = new System.Drawing.Size(75, 23);
            this.Cancelar_BT.TabIndex = 1;
            this.Cancelar_BT.Text = "Cancelar";
            this.Cancelar_BT.UseVisualStyleBackColor = true;
            this.Cancelar_BT.Click += new System.EventHandler(this.Cancelar_BT_Click);
            // 
            // Aceptar_BT
            // 
            this.Aceptar_BT.Location = new System.Drawing.Point(146, 10);
            this.Aceptar_BT.Name = "Aceptar_BT";
            this.Aceptar_BT.Size = new System.Drawing.Size(75, 23);
            this.Aceptar_BT.TabIndex = 0;
            this.Aceptar_BT.Text = "Aceptar";
            this.Aceptar_BT.UseVisualStyleBackColor = true;
            this.Aceptar_BT.Click += new System.EventHandler(this.Aceptar_BT_Click);
            // 
            // Dominio_TB
            // 
            this.Dominio_TB.Location = new System.Drawing.Point(23, 121);
            this.Dominio_TB.Name = "Dominio_TB";
            this.Dominio_TB.Size = new System.Drawing.Size(177, 20);
            this.Dominio_TB.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Dominio:";
            // 
            // ChangeUserUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 304);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ChangeUserUpdate";
            this.Text = "Cambio de usuario";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Usuario_TB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Password_TB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Cancelar_BT;
        private System.Windows.Forms.Button Aceptar_BT;
        private System.Windows.Forms.TextBox Dominio_TB;
        private System.Windows.Forms.Label label4;

    }
}