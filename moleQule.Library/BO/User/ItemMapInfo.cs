using System;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
    [Serializable()]
    public class ItemMapInfo : ReadOnlyBaseEx<ItemMapInfo, ItemMap>
    {
        #region Business Methods

        public ItemMapBase _base = new ItemMapBase();

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidItem { get { return _base.Record.OidItem; } }
        public long Privilege { get { return _base.Record.Privilege; } }
        public long OidAssociateItem { get { return _base.Record.OidAssociateItem; } }
        public long AssociatePrivilege { get { return _base.Record.AssociatePrivilege; } }

        public virtual EPrivilege TipoPrivilegio { get{return (EPrivilege)_base.Record.Privilege;}}
        public virtual string TipoPrivilegioLabel { get { return moleQule.Library.EnumTextBase<EPrivilege>.GetLabel(Resources.Enums.ResourceManager, TipoPrivilegio); } }
        public virtual EPrivilege TipoPrivilegioAsociado { get { return (EPrivilege)_base.Record.AssociatePrivilege; } }
        public virtual string TipoPrivilegioAsociadoLabel { get { return moleQule.Library.EnumTextBase<EPrivilege>.GetLabel(Resources.Enums.ResourceManager, TipoPrivilegioAsociado); } }

        #endregion

        #region Factory Methods

        private ItemMapInfo() { /* require use of factory methods */ }
        
        private ItemMapInfo(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
            SessionCode = sessionCode;
            Fetch(reader);
        }

        internal ItemMapInfo(long oid, long oid_item, long privilege, long oid_associate_item, long associate_privilege)
        {
            Oid = oid;
            _base.Record.OidItem = oid_item;
            _base.Record.Privilege = privilege;
            _base.Record.OidAssociateItem = oid_associate_item;
            _base.Record.AssociatePrivilege = associate_privilege;
        }

        public static ItemMapInfo Get(long oid, bool childs = false)
        {
            return ReadOnlyBaseEx<ItemMapInfo, ItemMap>.Get(ItemMap.SELECT(oid, false), childs);
        }
        public static ItemMapInfo GetChild(int sessionCode, IDataReader reader, bool childs = false) { return new ItemMapInfo(sessionCode, reader, childs); }

        public static ItemMapInfo New(long oid = 0) { return new ItemMapInfo() { Oid = oid }; }

        #endregion

        #region Data Access

        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                Oid = 0;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                    }
                }
            }
            catch (Exception ex) { iQExceptionHandler.TreatException(ex, new object[] { criteria.Query }); }
        }

        #endregion
    }
}
