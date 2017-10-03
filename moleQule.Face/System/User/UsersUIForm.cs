using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Library.CslaEx;
using moleQule.Library;

namespace moleQule.Face
{
    public partial class UsersUIForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "UserUIForm";
        public static Type Type { get { return typeof(UsersUIForm); } }

        /// <summary>
        /// Se trata de la Schema actual, que contiene usuarios y permisos que se van a editar.
        /// </summary>
        private Users _usuarios;

        public Users Usuarios
        {
            get { return _usuarios; }
            set { _usuarios = value; }
        }

        public long UsuariosActiveOID()
        {
            if (Datos.Current != null)
                return ((User)Datos.Current).Oid;
            else
                return -1;
        }

        public long PermisosActiveOID()
        {
            if (Datos_Permisos.Current != null)
                return ((Privilege)(Datos_Permisos.Current)).Oid;
            else
                return -1;
        }

        #endregion

        #region Factory Methods

        public UsersUIForm()
        {
            InitializeComponent();
            SetFormData();
        }

		public UsersUIForm(Form parent)
			: this(null, parent) {}

        public UsersUIForm(ISchemaInfo schema, Form parent)
            : base(-1, new object[1] { schema}, true, parent)
        {
            InitializeComponent();
            SetFormData();
        }

		protected override void GetFormSourceData()
		{
			_usuarios = Users.NewList();
			_usuarios.BeginEdit();
		}

        protected override void GetFormSourceData(object[] parameters)
        {
			ISchemaInfo schema = parameters[0] as ISchemaInfo;
			_usuarios = Users.GetList(schema);
			_usuarios.BeginEdit();
        }

        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(Resources.Messages.SAVING))
            {
                // Comprobamos los nuevos usuarios para meterlos en la lista de usuarios-Schemas
                foreach (User usuario in _usuarios)
                {
                    if (usuario.IsNew)
                    {
                        SchemaUser user = SchemaUser.NewChild(usuario);
                        user.OidSchema = AppContext.ActiveSchema.Oid;
                        usuario.Schemas.Add(user);
                    }
                }
                this.Datos.RaiseListChangedEvents = false;
                this.Datos_Permisos.RaiseListChangedEvents = false;

                Users temp = _usuarios.Clone();
                temp.ApplyEdit();

                // do the save
                try
                {
                    _usuarios = temp.Save();
                    _usuarios.ApplyEdit();
                    return true;
                }
                catch (iQValidationException ex)
                {
                    PgMng.ShowInfoException(ex);
                    return false;
                }
                catch (iQPersistentException ex)
                {
					PgMng.ShowInfoException(ex);
                    return false;
                }
                catch (Csla.Validation.ValidationException ex)
                {
					PgMng.ShowInfoException(ex);
                    return false;
                }
                finally
                {
                    this.Datos.RaiseListChangedEvents = true;
                    this.Datos_Permisos.RaiseListChangedEvents = true;
                }
            }
        }

        #endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Usuario.Tag = 1;

			cols.Add(Item);

			ControlsMng.MaximizeColumns(Users_DGW, cols);

			cols.Clear();

			cols = new List<DataGridViewColumn>();
			Item.Tag = 1;

			cols.Add(Item);

			ControlsMng.MaximizeColumns(Privileges_DGW, cols);
		}

		public override void FormatControls()
		{
			MaximizeForm(Width, 700);
			base.FormatControls();
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;

            User item = row.DataBoundItem as User;

			ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		protected void MarkAsActiva(DataGridViewRow row, string column, bool activa)
		{
			Privilege item = row.DataBoundItem as Privilege;

			if (activa)
				((DataGridViewButtonCell)row.Cells[column]).Value = moleQule.Library.Resources.Labels.SET_PRIVILEGES;
			else
				((DataGridViewButtonCell)row.Cells[column]).Value = moleQule.Library.Resources.Labels.UNSET_PRIVILEGES;
		}

		#endregion

		#region Source
		
        protected override void RefreshMainData()
        {
			Datos_Usuarios.DataSource = _usuarios;
			Datos = Datos_Usuarios;
        }
        
        protected override void SetUnlinkedGridValues(string grid_name)
        {
            foreach (DataGridViewRow row in Privileges_DGW.Rows)
            {
                Privilege privilegio = row.DataBoundItem as Privilege;

                MarkAsActiva(row, Read.Name, privilegio.Read);
                MarkAsActiva(row, Create.Name, privilegio.Create);
                MarkAsActiva(row, Modify.Name, privilegio.Modify);
                MarkAsActiva(row, Remove.Name, privilegio.Remove);
            }
        }

        #endregion

		#region Business Methods

		public void ReadUserGrants()
		{
			if (Datos.Current == null) return;

            Datos_Permisos.DataSource = ((User)Datos.Current).Licences;
			SetUnlinkedGridValues(Privileges_DGW.Name);
		}

		private void AsignarPermisos(bool todos)
		{
			if (Datos.Current == null) return;

            Datos_Permisos.DataSource = ((User)Datos.Current).Licences;

			foreach (Privilege permiso in Datos_Permisos)
			{
				permiso.Create = todos;
				permiso.Modify = todos;
				permiso.Remove = todos;
				permiso.Read = todos;
			}

			Privileges_DGW.Refresh();
		}

		#endregion

        #region Actions

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected virtual void SetPrivilegesAction(Privilege privilegio, EPrivilege permiso)
        {
            if (Datos.Current != null)
                Privileges.AssociatePerms((User)Datos.Current, privilegio, permiso); 
        }

        protected virtual bool CheckAssociatedPrivilegesAction(Privilege privilegio, EPrivilege permiso)
        {
            if (Datos.Current != null)
                return Privileges.CheckPerms((User)Datos.Current, privilegio, permiso);
            return false;
        }

        #endregion

        #region Buttons

        private void NewUser_TI_Click(object sender, EventArgs e)
        {
            UserPasswordAddForm form = new UserPasswordAddForm(true);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                User nuevo = _usuarios.NewItem();
                nuevo.Licences = Privileges.CreatePerms(nuevo);
                nuevo.Name = form.Nombre;
				nuevo.PlainPwd = form.Pass;
            }
        }

        private void EditUser_TI_Click(object sender, EventArgs e)
        {
            User item = (User)Datos.Current;
            
            if (Datos.Current != null)
            {
                UserPasswordAddForm form = new UserPasswordAddForm(((User)Datos.Current).Name, true);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    ((User)Datos.Current).Name = form.Nombre;
                    ((User)Datos.Current).PlainPwd = form.Pass;
                }
            }
            else
            {
                 PgMng.ShowInfoException(Resources.Messages.NO_SELECTED);
            }
        }

		private void DeleteUser_TI_Click(object sender, EventArgs e)
		{
			if (Datos.Current == null) return;

            if (((User)Datos.Current).Name == AppContext.User.Name)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_DELETE_USER);
				return;
			}

            User item = (User)Datos.Current;

			if (item.Oid == 1)
			{
				PgMng.ShowInfoException(String.Format(moleQule.Library.Resources.Messages.DELETE_USER_NOT_ALLOWED, item.Name));
				return;
			}

			if (ProgressInfoMng.ShowQuestion(Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
			{
				_usuarios.Remove(item.Oid);
			}
		}

		private void AttachUser_TI_Click(object sender, EventArgs e)
		{
			UserSelectForm form = new UserSelectForm();
			form.ShowDialog(this);

			if (form.DialogResult == DialogResult.OK)
			{
				UserInfo user = (UserInfo)form.Selected;
			
				_usuarios.AttachItem(user, AppContext.ActiveSchema);

				SetGridFormat(Users_DGW);
			}
		}

		private void DettachUser_TI_Click(object sender, EventArgs e)
		{
            User item = (User)Datos.Current;

			_usuarios.DettachItem(item, AppContext.ActiveSchema);

			SetGridFormat(Users_DGW);
			Datos_Usuarios.ResetBindings(false);
			Datos_Permisos.ResetBindings(false);
		}

		private void AdminUser_TI_Click(object sender, EventArgs e)
		{
			if (Datos.Current == null) return;
            User item = (User)Datos.Current;

			if (item.IsSuperUser) return;

			// Para no permitir que el usuario actual se quite permisos
			if (item.Name == AppContext.User.Name) return;

			item.IsAdmin = !item.IsAdmin;

			if (item.IsAdmin) AsignarPermisos(true);
		}

        private void QuitarPermisos_Button_Click(object sender, EventArgs e)
        {
            if (((User)Datos.Current).IsAdmin) return;
            AsignarPermisos(false);
        }

        private void AsignarPermisos_Button_Click(object sender, EventArgs e)
        {
            AsignarPermisos(true);
        }

        #endregion

        #region Events

        private void UsuarioUIForm_Shown(object sender, EventArgs e)
        {
            ReadUserGrants();
            PgMng.FillUp();
        }

        private void UsuarioUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _usuarios.CloseSession();
            Cerrar();
        }

		private void Users_DGW_SelectionChanged(object sender, EventArgs e)
        {
            ReadUserGrants();
        }

        private void Privileges_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Privileges_DGW.CurrentRow == null) return;
            if (e.ColumnIndex == -1) return;

            EPrivilege permiso = EPrivilege.Read;

            switch (Privileges_DGW.Columns[e.ColumnIndex].Name)
            {
                case "Read":
                    permiso = EPrivilege.Read;
                    break;
                case "Create":
                    permiso = EPrivilege.Create;
                    break;
                case "Modify":
                    permiso = EPrivilege.Modify;
                    break;
                case "Remove":
                    permiso = EPrivilege.Delete;
                    break;
            }

            DataGridViewRow row = Privileges_DGW.CurrentRow;
            Privilege item = row.DataBoundItem as Privilege;

            if (row.Cells[e.ColumnIndex].Value.ToString() == moleQule.Library.Resources.Labels.UNSET_PRIVILEGES)
            {
                SetPrivilegesAction(item, permiso);

                switch(Privileges_DGW.Columns[e.ColumnIndex].DataPropertyName)
                {
                    case "CanRead":
                        item.Read = true;
                        break;
                    case "CanCreate":
                        item.Create = true;
                        break;
                    case "CanModify":
                        item.Modify = true;
                        break;
                    case "CanDelete":
                        item.Remove = true;
                        break;
                }
            }
            else
            {
                if (CheckAssociatedPrivilegesAction(item, permiso))
                {
                    switch (Privileges_DGW.Columns[e.ColumnIndex].DataPropertyName)
                    {
                        case "CanRead":
                            item.Read = false;
                            break;
                        case "CanCreate":
                            item.Create = false;
                            break;
                        case "CanModify":
                            item.Modify = false;
                            break;
                        case "CanDelete":
                            item.Remove = false;
                            break;
                    }
                }
            }

            //SetUnlinkedGridValues(Permisos_Grid.Name);
            //ReadUserGrants();
            Privileges_DGW.Refresh();
        }

        #endregion
    }
}

