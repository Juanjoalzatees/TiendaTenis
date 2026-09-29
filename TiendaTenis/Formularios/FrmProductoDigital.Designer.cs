namespace TiendaTenis.Formularios
{
    partial class FrmProductoDigital
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            txtCodigo = new TextBox();
            txtUrl = new TextBox();
            txtDescripcion = new TextBox();
            txtNombre = new TextBox();
            nudPeso = new NumericUpDown();
            nudPrecio = new NumericUpDown();
            cboCategoria = new ComboBox();
            cboFormato = new ComboBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudPeso).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(229, 30);
            label1.Name = "label1";
            label1.Size = new Size(61, 20);
            label1.TabIndex = 0;
            label1.Text = "Codigo:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(229, 77);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 1;
            label2.Text = "Nombre:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(229, 120);
            label3.Name = "label3";
            label3.Size = new Size(90, 20);
            label3.TabIndex = 2;
            label3.Text = "Descripcion:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(229, 170);
            label4.Name = "label4";
            label4.Size = new Size(53, 20);
            label4.TabIndex = 3;
            label4.Text = "Precio:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(229, 217);
            label5.Name = "label5";
            label5.Size = new Size(77, 20);
            label5.TabIndex = 4;
            label5.Text = "Categoria:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(229, 268);
            label6.Name = "label6";
            label6.Size = new Size(74, 20);
            label6.TabIndex = 5;
            label6.Text = "Peso(MB):";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(229, 316);
            label7.Name = "label7";
            label7.Size = new Size(68, 20);
            label7.TabIndex = 6;
            label7.Text = "Formato:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(229, 364);
            label8.Name = "label8";
            label8.Size = new Size(38, 20);
            label8.TabIndex = 7;
            label8.Text = "URL:";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(393, 23);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(125, 27);
            txtCodigo.TabIndex = 8;
            // 
            // txtUrl
            // 
            txtUrl.Location = new Point(393, 364);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(125, 27);
            txtUrl.TabIndex = 9;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(393, 117);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(125, 27);
            txtDescripcion.TabIndex = 10;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(393, 74);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(125, 27);
            txtNombre.TabIndex = 11;
            // 
            // nudPeso
            // 
            nudPeso.Location = new Point(394, 268);
            nudPeso.Name = "nudPeso";
            nudPeso.Size = new Size(150, 27);
            nudPeso.TabIndex = 12;
            // 
            // nudPrecio
            // 
            nudPrecio.Location = new Point(393, 163);
            nudPrecio.Maximum = new decimal(new int[] { 5000000, 0, 0, 0 });
            nudPrecio.Name = "nudPrecio";
            nudPrecio.Size = new Size(150, 27);
            nudPrecio.TabIndex = 13;
            // 
            // cboCategoria
            // 
            cboCategoria.FormattingEnabled = true;
            cboCategoria.Location = new Point(393, 217);
            cboCategoria.Name = "cboCategoria";
            cboCategoria.Size = new Size(151, 28);
            cboCategoria.TabIndex = 14;
            // 
            // cboFormato
            // 
            cboFormato.FormattingEnabled = true;
            cboFormato.Location = new Point(394, 316);
            cboFormato.Name = "cboFormato";
            cboFormato.Size = new Size(151, 28);
            cboFormato.TabIndex = 15;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(123, 409);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 16;
            btnGuardar.Text = "Aceptar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(535, 409);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 17;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FrmProductoDigital
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(cboFormato);
            Controls.Add(cboCategoria);
            Controls.Add(nudPrecio);
            Controls.Add(nudPeso);
            Controls.Add(txtNombre);
            Controls.Add(txtDescripcion);
            Controls.Add(txtUrl);
            Controls.Add(txtCodigo);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmProductoDigital";
            Text = "FrmProductoDigital";
            ((System.ComponentModel.ISupportInitialize)nudPeso).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox txtCodigo;
        private TextBox txtUrl;
        private TextBox txtDescripcion;
        private TextBox txtNombre;
        private NumericUpDown nudPeso;
        private NumericUpDown nudPrecio;
        private ComboBox cboCategoria;
        private ComboBox cboFormato;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}