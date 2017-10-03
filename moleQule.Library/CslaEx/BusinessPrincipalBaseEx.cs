using System;
using System.Collections;
using System.Security.Principal;

using Csla.Security;

namespace moleQule.Library.CslaEx
{
    public interface IIdentityEx : IIdentity
    {
		long Oid { get; }

		string GetName(); 
		string GetPassword();

		bool IsReadable(long elemento);
        bool IsCreable(long elemento);
        bool IsModifiable(long elemento);
        bool IsRemovable(long elemento);

		bool IsSuperUser { get; }
		bool IsAdmin { get; }
		bool IsUser { get; }
		bool IsPartner { get; }
		bool IsClient { get; }
		bool IsProvider { get; }
	}

    [Serializable()]
    public class BusinessPrincipalBaseEx : BusinessPrincipalBase
	{
        /// <summary>
        /// Returns the user's Identity object.
        /// </summary>
		public new IIdentityEx Identity { get { return (IIdentityEx)base.Identity; } set { /*base.Identity = value;*/ } }

        protected BusinessPrincipalBaseEx(IIdentityEx identity) 
			: base(identity) {}

		public override bool IsInRole(string role)
		{
			switch (role)
			{
				case "ADMIN": return Identity.IsAdmin;
				case "SUPERUSER": return Identity.IsSuperUser;
				case "USER": return Identity.IsUser;
				case "PARTNER": return Identity.IsPartner;
				case "CLIENT": return Identity.IsClient;
				case "PROVIDER": return Identity.IsProvider;

				default: return false;
			}
		}

        public virtual bool CanReadObject(long tipo_elemento) { return false; }
        public virtual bool CanCreateObject(long tipo_elemento) { return false; }
        public virtual bool CanModifyObject(long tipo_elemento) { return false; }
        public virtual bool CanRemoveObject(long tipo_elemento) { return false; }

		/*public virtual void ChangeUserSchema() { throw new iQImplementationException("ChangeUserSchema"); }
		public virtual void Close() { throw new iQImplementationException("Close"); }
		public virtual void CloseSettings() { throw new iQImplementationException("CloseSettings"); }
		public virtual void ClearUserContext() { throw new iQImplementationException("ClearUserContext"); }
		public virtual void LoadUserContext() { throw new iQImplementationException("LoadUserContext"); }*/        
	}
}