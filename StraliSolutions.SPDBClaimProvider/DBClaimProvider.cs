using Microsoft.SharePoint.Administration.Claims;
using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StraliSolutions.SPDBClaimProvider
{
    public class DBClaimProvider : SPClaimProvider
    {
        #region ctor
        public DBClaimProvider(string displayName) : base(displayName) 
        { 
        }
        #endregion

        
        #region Properties
        internal static string ProviderInternalName
        {
            get { return Constants.PROVIDERINTERNALNAME; }
        }

        public override string Name
        {
            get { return ProviderInternalName; }
        }

        internal static string ProviderDisplayName
        {
            get { return Constants.PROIVDERDISPLAYNAME; }
        }

        private static string DBClaimType
        {
            get { return Constants.MAILCLAIMTYPE; }
        }
        private static string DBClaimValueType
        {
            get { return Microsoft.IdentityModel.Claims.ClaimValueTypes.String; }
        }

        internal static string SPTrustedIdentityTokenIssuerName
        {
            //This is the same value returned from:
            //Get-SPTrustedIdentityTokenIssuer | select Name
            get { return DBHelper.propertySPTrustedIdentityTokenIssuerName; }
        }


        public override bool SupportsEntityInformation
        {
            //Not doing claims augmentation
            get { return false; }
        }

        public override bool SupportsHierarchy
        {
            get { return false; }
        }

        public override bool SupportsResolve
        {
            get { return true; }
        }

        public override bool SupportsSearch
        {
            get { return true; }
        }
        #endregion


        protected override void FillClaimTypes(List<string> claimTypes)
        {
            if (claimTypes == null)
                  throw new ArgumentNullException("claimTypes");
   
              // Add our claim type.
              claimTypes.Add(DBClaimType);
        }

        protected override void FillClaimValueTypes(List<string> claimValueTypes)
        {
             if (claimValueTypes == null)
                 throw new ArgumentNullException("claimValueTypes");
 
            // Add our claim value type.
            claimValueTypes.Add(DBClaimValueType);
        }


        protected override void FillSearch(Uri context, string[] entityTypes, string searchPattern, string hierarchyNodeID, int maxCount, Microsoft.SharePoint.WebControls.SPProviderHierarchyTree searchTree)
        {
            List<DBUser> users = DBHelper.Search(searchPattern);
            foreach (var user in users)
            {
                PickerEntity entity = GetPickerEntity(user);
                searchTree.AddEntity(entity);
            }
        }

        protected override void FillEntityTypes(List<string> entityTypes)
        {
            if (null == entityTypes)
             {
                 throw new ArgumentNullException("entityTypes");
             }
             entityTypes.Add(SPClaimEntityTypes.User); 
        }



        protected override void FillResolve(Uri context, string[] entityTypes, string resolveInput, List<Microsoft.SharePoint.WebControls.PickerEntity> resolved)
        {
            DBUser user = DBHelper.FindExact(resolveInput);
            if (null != user)
            {
                PickerEntity entity = GetPickerEntity(user);
                resolved.Add(entity);
            }          
            
        }

       
        private PickerEntity GetPickerEntity(DBUser user)
        {
            PickerEntity entity = CreatePickerEntity();
            entity.Claim = new SPClaim(DBClaimType, user.email, DBClaimValueType, SPOriginalIssuers.Format(SPOriginalIssuerType.TrustedProvider, SPTrustedIdentityTokenIssuerName)); //using ADFS Claim
            //we need to use Windows Authentication Claim instead
            //entity.Claim = new SPClaim(SPClaimTypes.UserLogonName, user.ad_account_name, "http://www.w3.org/2001/XMLSchema#string", SPOriginalIssuers.Format(SPOriginalIssuerType.Windows));
            entity.Description = user.first_name + " " + user.last_name;
            entity.DisplayText = user.first_name + " " + user.last_name;
            entity.EntityData[PeopleEditorEntityDataKeys.DisplayName] = user.first_name + " " + user.last_name;
            entity.EntityData[PeopleEditorEntityDataKeys.Email] = user.email;
            entity.EntityData[PeopleEditorEntityDataKeys.AccountName] = user.ad_account_name;
            entity.EntityType = SPClaimEntityTypes.User;
            entity.IsResolved = true;
            return entity;
        }

        protected override void FillResolve(Uri context, string[] entityTypes, SPClaim resolveInput, List<Microsoft.SharePoint.WebControls.PickerEntity> resolved)
        {
            if (resolveInput.ClaimType == SPClaimTypes.UserLogonName)
                FillResolve(context, entityTypes, resolveInput.Value, resolved);
            else
            {

                //LDAPUser user = LDAPHelper.FindExact(resolveInput.Value);
                //if (null != user)
                //{
                //    PickerEntity entity = GetPickerEntity(user);
                //    resolved.Add(entity);
                //}

                PickerEntity entityADFS = GetPickerEntityADFS(resolveInput.Value);
                resolved.Add(entityADFS);
            }
        }
        private PickerEntity GetPickerEntityADFS(string token)
        {
            PickerEntity entity = CreatePickerEntity();
            entity.Claim = new SPClaim(DBClaimType, token, DBClaimValueType, SPOriginalIssuers.Format(SPOriginalIssuerType.TrustedProvider, SPTrustedIdentityTokenIssuerName)); //using ADFS Claim
            entity.Description = token;
            entity.DisplayText = token;
            entity.EntityData[PeopleEditorEntityDataKeys.DisplayName] = token;
            entity.EntityData[PeopleEditorEntityDataKeys.Email] = token;
            entity.EntityData[PeopleEditorEntityDataKeys.AccountName] = token;
            entity.EntityType = SPClaimEntityTypes.User;
            entity.IsResolved = true;
            return entity;
        }
        #region Not Implemented
        protected override void FillClaimsForEntity(Uri context, SPClaim entity, List<SPClaim> claims)
        {
            throw new NotImplementedException();
        }

        protected override void FillHierarchy(Uri context, string[] entityTypes, string hierarchyNodeID, int numberOfLevels, SPProviderHierarchyTree hierarchy)
        {
            throw new NotImplementedException();
        }
        
        protected override void FillSchema(SPProviderSchema schema)
        {
            schema.AddSchemaElement(new SPSchemaElement(PeopleEditorEntityDataKeys.DisplayName, "Display nem", SPSchemaElementType.Both));
        }
        #endregion

    }
}
