using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using moleQule.Library;

using NHibernate;

namespace moleQule.Library.Common
{
    /// <summary>
    /// Editable Child Business Object
    /// </summary>
    [Serializable()]
    public class Idioma : BusinessBaseEx<Idioma>
    {
        #region Business Methods

        private string _valor = string.Empty;

        public virtual string Valor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _valor;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (_valor != value)
                {
                    _valor = value;
                    PropertyHasChanged();
                }
            }
        }
   
        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="_s">Objeto origen</param>
        protected void CopyValues(Idioma source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _valor = source.Valor;
        }

        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _valor = Format.DataReader.GetString(source, "VALOR");
        }
        
        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, "Valor");
        }

        #endregion

        #region Authorization Rules

        protected override void AddAuthorizationRules()
        {
        }

        public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.AUXILIARES)
                    || AppContext.User.IsAdmin;
        }

        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.AUXILIARES)
                    || AppContext.User.IsAdmin;
        }

        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.AUXILIARES)
                    || AppContext.User.IsAdmin;
        }

        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.AUXILIARES)
                    || AppContext.User.IsAdmin;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        public Idioma()
        {
            MarkAsChild();
            Random r = new Random();
            Oid = (long)r.Next();
        }

        private Idioma(Idioma source)
        {
            MarkAsChild();
            Fetch(source);
        }

        private Idioma(IDataReader reader)
        {
            MarkAsChild();
            Fetch(reader);
        }

        internal static Idioma NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(
                    moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            return new Idioma();
        }

        internal static Idioma GetChild(Idioma source)
        {
            return new Idioma(source);
        }

        internal static Idioma GetChild(IDataReader reader)
        {
            return new Idioma(reader);
        }

        public virtual IdiomaInfo GetInfo()
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(
                  moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            return new IdiomaInfo(Oid, Valor);
        }

        /// <summary>
        /// Borrado aplazado, es posible el undo 
        /// (La función debe ser "no estática")
        /// </summary>
        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(
                    moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
        }

        /// <summary>
        /// No se debe utilizar esta función para guardar. Hace falta el padre.
        /// Utilizar Insert o Update en sustitución de Save.
        /// </summary>
        /// <returns></returns>
        public override Idioma Save()
        {
            throw new iQException(moleQule.Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
        }

        #endregion

        #region Child Data Access

        private void Fetch(IDataReader reader)
        {
            CopyValues(reader);
            MarkOld();
        }

        private void Fetch(Idioma source)
        {
            CopyValues(source);
            MarkOld();
        }

        internal void Insert(Idiomas parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                parent.Session().Save(this);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        internal void Update(Idiomas parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                SessionCode = parent.SessionCode;
                Idioma obj = Session().Get<Idioma>(Oid);
                obj.CopyValues(this);
                Session().Update(obj);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        internal void DeleteSelf(Idiomas parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<Idioma>(Oid));
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkNew();
        }

        #endregion

        #region Commands

        public static bool Exists(string valor)
        {
            ExistsCmd result;
            result = DataPortal.Execute<ExistsCmd>(new ExistsCmd(valor));
            return result.Exists;
        }

        [Serializable()]
        private class ExistsCmd : CommandBase
        {
            private string _valor;
            private bool _exists = false;

            public bool Exists
            {
                get { return _exists; }
            }

            public ExistsCmd(string valor)
            {
                _valor = valor;
            }

            protected override void DataPortal_Execute()
            {
                // Buscar por codigo
                CriteriaEx criteria = Idioma.GetCriteria(Idioma.OpenSession());
                criteria.AddEq("Valor", _valor);
                IdiomaList list = IdiomaList.GetList(criteria);
                _exists = !(list.Count == 0);
            }
        }

        #endregion

    }

    public class IdiomaMap : ClassMapping<Idioma>
    {
        public IdiomaMap()
        {
            Table("IDIOMA");
            Schema("\"COMMON\"");
            Lazy(true);

            Id(x => x.Oid, map => map.Generator(Generators.Sequence,
                      gmap => gmap.Params(new { max_low = 100 })));

            Property(x => x.Valor, map =>
            {
                map.Column("VALOR");
                map.Length(255);
            });
        }
    }
}
