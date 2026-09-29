namespace TiendaTenis.Formularios
{
    partial class FrmCatalogo
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
            btnNuevoFisico = new Button();
            btnNuevoDigital = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            dgv = new DataGridView();
            Codigo = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Categoria = new DataGridViewTextBoxColumn();
            Precio = new DataGridViewTextBoxColumn();
            Peso = new DataGridViewTextBoxColumn();
            Tipo = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // btnNuevoFisico
            // 
            btnNuevoFisico.Location = new Point(80, 28);
            btnNuevoFisico.Name = "btnNuevoFisico";
            btnNuevoFisico.Size = new Size(125, 31);
            btnNuevoFisico.TabIndex = 0;
            btnNuevoFisico.Text = "Nuevo Fisico";
            btnNuevoFisico.UseVisualStyleBackColor = true;
            btnNuevoFisico.Click += btnNuevoFisico_Click;
            // 
            // btnNuevoDigital
            // 
            btnNuevoDigital.Location = new Point(80, 84);
            btnNuevoDigital.Name = "btnNuevoDigital";
            btnNuevoDigital.Size = new Size(125, 31);
            btnNuevoDigital.TabIndex = 1;
            btnNuevoDigital.Text = "Nuevo Digital";
            btnNuevoDigital.UseVisualStyleBackColor = true;
            btnNuevoDigital.Click += btnNuevoDigital_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(80, 148);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(125, 31);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(80, 206);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(125, 31);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Columns.AddRange(new DataGridViewColumn[] { Codigo, Nombre, Categoria, Precio, Peso, Tipo });
            dgv.Location = new Point(80, 269);
            dgv.MultiSelect = false;
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersWidth = 51;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(805, 188);
            dgv.TabIndex = 4;
            // 
            // Codigo
            // 
            Codigo.DataPropertyName = "Codigo";
            Codigo.HeaderText = "Codigo";
            Codigo.MinimumWidth = 6;
            Codigo.Name = "Codigo";
            Codigo.ReadOnly = true;
            Codigo.Width = 125;
            // 
            // Nombre
            // 
            Nombre.DataPropertyName = "Nombre";
            Nombre.HeaderText = "Nombre";
            Nombre.MinimumWidth = 6;
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            Nombre.Width = 125;
            // 
            // Categoria
            // 
            Categoria.DataPropertyName = "Categoria";
            Categoria.HeaderText = "Categoria";
            Categoria.MinimumWidth = 6;
            Categoria.Name = "Categoria";
            Categoria.ReadOnly = true;
            Categoria.Width = 125;
            // 
            // Precio
            // 
            Precio.DataPropertyName = "Precio";
            Precio.HeaderText = "Precio";
            Precio.MinimumWidth = 6;
            Precio.Name = "Precio";
            Precio.ReadOnly = true;
            Precio.Width = 125;
            // 
            // Peso
            // 
            Peso.DataPropertyName = "PesoConUnidad";
            Peso.HeaderText = "Peso";
            Peso.MinimumWidth = 6;
            Peso.Name = "Peso";
            Peso.ReadOnly = true;
            Peso.Width = 125;
            // 
            // Tipo
            // 
            Tipo.DataPropertyName = "TipoDescripcion";
            Tipo.HeaderText = "Tipo";
            Tipo.MinimumWidth = 6;
            Tipo.Name = "Tipo";
            Tipo.ReadOnly = true;
            Tipo.Width = 125;
            // 
            // FrmCatalogo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1412, 487);
            Controls.Add(dgv);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(btnNuevoDigital);
            Controls.Add(btnNuevoFisico);
            Name = "FrmCatalogo";
            Text = "FrmCatalogo";
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnNuevoFisico;
        private Button btnNuevoDigital;
        private Button btnEditar;
        private Button btnEliminar;
        private DataGridView dgv;
        private DataGridViewTextBoxColumn Codigo;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Categoria;
        private DataGridViewTextBoxColumn Precio;
        private DataGridViewTextBoxColumn Peso;
        private DataGridViewTextBoxColumn Tipo;
    }
}