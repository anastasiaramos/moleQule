using System;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 
using moleQule.Library;

namespace moleQule.Library.Common
{
    /// <summary>
    /// ReadOnly Child Business Object
    /// </summary>
    [Serializable()]
    public class ContactoEmpresaInfo : ReadOnlyBaseEx<ContactoEmpresaInfo>
    {
        #region Business Methods

		public CompanyContactBase _base = new CompanyContactBase();

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public virtual long OidEmpresa
        {
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidEmpresa;
            }
        }
        public virtual string Nombre
        {
            get
            {
                //CanReadProperty(true);
                return _base.Record.Nombre;
            }
        }
        public virtual string Cargo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Cargo;
            }
        }
        public virtual string Dni
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Dni;
            }
        }
        public virtual string Direccion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Direccion;
            }
        }
        public virtual string CodPostal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CodPostal;
            }
        }
        public virtual string Municipio
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Municipio;
            }
        }
        public virtual string Provincia
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Provincia;
            }
        }
        public virtual string Telefonos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Telefonos;
            }
        }

        #endregion

        #region Factory Methods

        private ContactoEmpresaInfo() { /* require use of factory methods */ }

        private ContactoEmpresaInfo(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        internal ContactoEmpresaInfo(long oid,
                                long oidEmpresa,
                                string nombre,
                                string cargo,
                                string dni,
                                string direccion,
                                string codPostal,
                                string municipio,
                                string provincia,
                                string telefonos)
        {
            Oid = oid;
            _base.Record.OidEmpresa = oidEmpresa;
            _base.Record.Nombre = nombre;
            _base.Record.Cargo = cargo;
            _base.Record.Dni = dni;
            _base.Record.Direccion = direccion;
            _base.Record.CodPostal = codPostal;
            _base.Record.Municipio = municipio;
            _base.Record.Provincia = provincia;
            _base.Record.Telefonos = telefonos;
        }

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static ContactoEmpresaInfo Get(IDataReader reader, bool childs)
        {
            return new ContactoEmpresaInfo(reader, childs);
        }

        #endregion

        #region Data Access

        //called to copy data from IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
        }

        #endregion
    }
}
