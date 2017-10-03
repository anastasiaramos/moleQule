using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;

using Csla;

namespace moleQule.Library.CslaEx
{
    public interface IPrincipalEx : IPrincipal
	{
		#region Properties

		new IIdentityEx Identity { get; set; }

		ISchemaInfo ActiveSchema { get; set; }

		HashOidList Branches { get; set; }

        SecureItemList SecureItems { get; set; }

		#endregion

		#region Permissions

		bool CanReadObject(long elemento);
        bool CanCreateObject(long elemento);
        bool CanModifyObject(long elemento);
        bool CanRemoveObject(long elemento);

		#endregion 

		#region Business

		void ChangeUserSchema(ISchemaInfo schema);
		void ChangeUserSchema(ISchemaInfo schema, bool forceChangeSessionFactory);
		void ChangeUserSchema(string schema, bool forceChangeSessionFactory);
		void ClearUserContext();
		void Close();
		void CloseSettings();
		void LoadSettings(User user);
		void LoadUserContext();
		void Logout();
		void ReloadUser();
		void ReloadUser(long oid, long oidSchema);
		void SaveSettings();

		#endregion
	}
}
