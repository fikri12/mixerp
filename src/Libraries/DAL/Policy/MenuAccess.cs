/********************************************************************************
Copyright (C) MixERP Inc. (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, version 2 of the License.


MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MixERP.Net.DbFactory;
using MixERP.Net.Framework;
using Npgsql;
using PetaPoco;
using Serilog;

namespace MixERP.Net.Schemas.Policy.Data
{
    /// <summary>
    /// Provides simplified data access features to perform SCRUD operation on the database table "policy.menu_access".
    /// </summary>
    public class MenuAccess : DbAccess
    {
        /// <summary>
        /// The schema of this table. Returns literal "policy".
        /// </summary>
	    public override string ObjectNamespace => "policy";

        /// <summary>
        /// The schema unqualified name of this table. Returns literal "menu_access".
        /// </summary>
	    public override string ObjectName => "menu_access";

        /// <summary>
        /// Login id of application user accessing this table.
        /// </summary>
		public long LoginId { get; set; }

        /// <summary>
        /// The name of the database on which queries are being executed to.
        /// </summary>
        public string Catalog { get; set; }

		/// <summary>
		/// Performs SQL count on the table "policy.menu_access".
		/// </summary>
		/// <returns>Returns the number of rows of the table "policy.menu_access".</returns>
		public long Count()
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return 0;
			}

            try
            {
                if (!this.SkipValidation)
                {
                    if (!this.Validated)
                    {
                        this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                    }
                    if (!this.HasAccess)
                    {
                        throw new UnauthorizedException("Access is denied.");
                    }
                }
	
				const string sql = "SELECT COUNT(*) FROM policy.menu_access;";
				return Factory.Scalar<long>(this.Catalog, sql);
            }
            catch (UnauthorizedException ex)
            {
                Log.Error("{Exception} {@Exception}", ex.Message, ex);
                throw new MixERPException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Executes a select query on the table "policy.menu_access" with a where filter on the column "access_id" to return a single instance of the "MenuAccess" class. 
		/// </summary>
		/// <param name="accessId">The column "access_id" parameter used on where filter.</param>
		/// <returns>Returns a non-live, non-mapped instance of "MenuAccess" class mapped to the database row.</returns>
		public MixERP.Net.Entities.Policy.MenuAccess Get(long accessId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            try
            {
                if (!this.SkipValidation)
                {
                    if (!this.Validated)
                    {
                        this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                    }
                    if (!this.HasAccess)
                    {
                        throw new UnauthorizedException("Access is denied.");
                    }
                }
	
				const string sql = "SELECT * FROM policy.menu_access WHERE access_id=@0;";
				return Factory.Get<MixERP.Net.Entities.Policy.MenuAccess>(this.Catalog, sql, accessId).FirstOrDefault();
            }
            catch (UnauthorizedException ex)
            {
                Log.Error("{Exception} {@Exception}", ex.Message, ex);
                throw new MixERPException(ex.Message, ex);
            }
		}

        /// <summary>
        /// Displayfields provide a minimal name/value context for data binding the row collection of policy.menu_access.
        /// </summary>
        /// <returns>Returns an enumerable name and value collection for the table policy.menu_access</returns>
		public IEnumerable<DisplayField> GetDisplayFields()
		{
			List<DisplayField> displayFields = new List<DisplayField>();

			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return displayFields;
			}

            try
            {
                if (!this.SkipValidation)
                {
                    if (!this.Validated)
                    {
                        this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                    }
                    if (!this.HasAccess)
                    {
                        throw new UnauthorizedException("Access is denied.");
                    }
                }
	
				const string sql = "SELECT access_id AS key, access_id as value FROM policy.menu_access;";
				using (NpgsqlCommand command = new NpgsqlCommand(sql))
				{
					using (DataTable table = DbOperation.GetDataTable(this.Catalog, command))
					{
						if (table?.Rows == null || table.Rows.Count == 0)
						{
							return displayFields;
						}

						foreach (DataRow row in table.Rows)
						{
							if (row != null)
							{
								DisplayField displayField = new DisplayField
								{
									Key = row["key"].ToString(),
									Value = row["value"].ToString()
								};

								displayFields.Add(displayField);
							}
						}
					}
				}

				return displayFields;
            }
            catch (UnauthorizedException ex)
            {
                Log.Error("{Exception} {@Exception}", ex.Message, ex);
                throw new MixERPException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Inserts the instance of MenuAccess class on the database table "policy.menu_access".
		/// </summary>
		/// <param name="menuAccess">The instance of "MenuAccess" class to insert.</param>
		public void Add(MixERP.Net.Entities.Policy.MenuAccess menuAccess)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            try
            {
                if (!this.SkipValidation)
                {
                    if (!this.Validated)
                    {
                        this.Validate(AccessTypeEnum.Create, this.LoginId, false);
                    }
                    if (!this.HasAccess)
                    {
                        throw new UnauthorizedException("Access is denied.");
                    }
                }
	
				Factory.Insert(this.Catalog, menuAccess);
            }
            catch (UnauthorizedException ex)
            {
                Log.Error("{Exception} {@Exception}", ex.Message, ex);
                throw new MixERPException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Updates the row of the table "policy.menu_access" with an instance of "MenuAccess" class against the primary key value.
		/// </summary>
		/// <param name="menuAccess">The instance of "MenuAccess" class to update.</param>
		/// <param name="accessId">The value of the column "access_id" which will be updated.</param>
		public void Update(MixERP.Net.Entities.Policy.MenuAccess menuAccess, long accessId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            try
            {
                if (!this.SkipValidation)
                {
                    if (!this.Validated)
                    {
                        this.Validate(AccessTypeEnum.Edit, this.LoginId, false);
                    }
                    if (!this.HasAccess)
                    {
                        throw new UnauthorizedException("Access is denied.");
                    }
                }
	
				Factory.Update(this.Catalog, menuAccess, accessId);
            }
            catch (UnauthorizedException ex)
            {
                Log.Error("{Exception} {@Exception}", ex.Message, ex);
                throw new MixERPException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Deletes the row of the table "policy.menu_access" against the primary key value.
		/// </summary>
		/// <param name="accessId">The value of the column "access_id" which will be deleted.</param>
		public void Delete(long accessId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            try
            {
                if (!this.SkipValidation)
                {
                    if (!this.Validated)
                    {
                        this.Validate(AccessTypeEnum.Delete, this.LoginId, false);
                    }
                    if (!this.HasAccess)
                    {
                        throw new UnauthorizedException("Access is denied.");
                    }
                }
	
				const string sql = "DELETE FROM policy.menu_access WHERE access_id=@0;";
				Factory.NonQuery(this.Catalog, sql, accessId);
            }
            catch (UnauthorizedException ex)
            {
                Log.Error("{Exception} {@Exception}", ex.Message, ex);
                throw new MixERPException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Performs a select statement on table "policy.menu_access" producing a paged result of 25.
		/// </summary>
		/// <returns>Returns the first page of collection of "MenuAccess" class.</returns>
		public IEnumerable<MixERP.Net.Entities.Policy.MenuAccess> GetPagedResult()
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            try
            {
                if (!this.SkipValidation)
                {
                    if (!this.Validated)
                    {
                        this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                    }
                    if (!this.HasAccess)
                    {
                        throw new UnauthorizedException("Access is denied.");
                    }
                }
	
				const string sql = "SELECT * FROM policy.menu_access ORDER BY access_id LIMIT 25 OFFSET 0;";
				return Factory.Get<MixERP.Net.Entities.Policy.MenuAccess>(this.Catalog, sql);
            }
            catch (UnauthorizedException ex)
            {
                Log.Error("{Exception} {@Exception}", ex.Message, ex);
                throw new MixERPException(ex.Message, ex);
            }
		}

		/// <summary>
		/// Performs a select statement on table "policy.menu_access" producing a paged result of 25.
		/// </summary>
		/// <param name="pageNumber">Enter the page number to produce the paged result.</param>
		/// <returns>Returns collection of "MenuAccess" class.</returns>
		public IEnumerable<MixERP.Net.Entities.Policy.MenuAccess> GetPagedResult(long pageNumber)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            try
            {
                if (!this.SkipValidation)
                {
                    if (!this.Validated)
                    {
                        this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                    }
                    if (!this.HasAccess)
                    {
                        throw new UnauthorizedException("Access is denied.");
                    }
                }
	
				long offset = (pageNumber -1) * 25;
				const string sql = "SELECT * FROM policy.menu_access ORDER BY access_id LIMIT 25 OFFSET @0;";
				
				return Factory.Get<MixERP.Net.Entities.Policy.MenuAccess>(this.Catalog, sql, offset);
            }
            catch (UnauthorizedException ex)
            {
                Log.Error("{Exception} {@Exception}", ex.Message, ex);
                throw new MixERPException(ex.Message, ex);
            }
		}
	}
}