using System;

using System.Data;
using System.Data.SqlClient;

using System.Collections;
using System.Collections.Generic;

using EntityLayer.Entities;
using Common.StringsClasses;
using DAL.Resources;

namespace DAL.OperationsClasses
{
    public class SupplierDAL : DataManagment
    {
        #region Operations

        public Supplier SelectByID(int ID, bool? IsArabic)
        {
            Supplier info = null;
            try
            {
                info = GetSupplier(ID, ProceduresNames.SupplierSelectByID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public List<Supplier> SelectAll(bool? IsArabic, bool? IsActive)
        {
            List<Supplier> infoList = null;
            try
            {
                infoList = GetSupplierList(ProceduresNames.SupplierSelectAll, null, null, IsArabic, IsActive);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Supplier> SelectByBranchID(int branchID, bool? IsArabic, bool? IsActive)
        {
            List<Supplier> infoList = null;
            try
            {
                infoList = GetSupplierList(ProceduresNames.SupplierSelectByBranchID, branchID, Supplier.CommonColumns.BranchID, IsArabic, IsActive);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public bool Add(Supplier info)
        {
            bool result = false;
            try
            {
                result = WriteSupplier(ProceduresNames.SupplierAdd, info, true);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool Update(Supplier info)
        {
            bool result = false;
            try
            {
                result = WriteSupplier(ProceduresNames.SupplierUpdate, info, false);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool Delete(int ID)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.SupplierDelete, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.ID), ID);

                this.OpenConnection();
                command.ExecuteNonQuery();

                result = true;
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        public bool Activate(int ID, Guid? activatedBy)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.SupplierActivate, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.ID), ID);

                if (activatedBy.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.ActivatedBy), activatedBy.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.ActivatedBy), DBNull.Value);


                this.OpenConnection();
                command.ExecuteNonQuery();

                result = true;
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        public bool Deactivate(int ID, Guid? deactivatedBy)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.SupplierDeactivate, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.ID), ID);

                if (deactivatedBy.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.DeactivatedBy), deactivatedBy);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.DeactivatedBy), DBNull.Value);

                this.OpenConnection();
                command.ExecuteNonQuery();

                result = true;
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        #endregion

        #region Utility Methods

        private Supplier GetSupplier(int ID, string procedureName, bool? IsArabic)
        {
            Supplier info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.ID), ID);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.IsArabic), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadSupplier(reader, IsArabic);
                    ReadBranchesList(reader, info, IsArabic);
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                if (reader != null)
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                this.CloseConnection();
            }
            return info;
        }

        private List<Supplier> GetSupplierList(string procedureName, int? foreignID, string foreignIDName, bool? IsArabic, bool? IsActive)
        {
            List<Supplier> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.IsArabic), DBNull.Value);

                if (IsActive.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.IsActive), IsActive.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.IsActive), DBNull.Value);

                if (foreignID.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, foreignIDName), foreignID.Value);


                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Supplier>();

                    ReadSupplierList(reader, infoList, IsArabic);
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                if (reader != null)
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                this.CloseConnection();
            }
            return infoList;
        }

        private Supplier ReadSupplier(SqlDataReader reader, bool? IsArabic)
        {
            Supplier info = null;
            try
            {
                reader.Read();

                info = new Supplier();

                info.ID = Convert.ToInt32(reader[Supplier.CommonColumns.ID]);

                if (reader[Supplier.TableColumns.Website] != DBNull.Value)
                    info.Website = Convert.ToString(reader[Supplier.TableColumns.Website]);

                if (reader[Supplier.TableColumns.Email] != DBNull.Value)
                    info.Email = Convert.ToString(reader[Supplier.TableColumns.Email]);

                if (reader[Supplier.TableColumns.HotLine] != DBNull.Value)
                    info.HotLine = Convert.ToString(reader[Supplier.TableColumns.HotLine]);

                if (reader[Supplier.TableColumns.Image] != DBNull.Value)
                    info.Image = Convert.ToString(reader[Supplier.TableColumns.Image]);

                if (!IsArabic.HasValue)
                {
                    info.NameAr = Convert.ToString(reader[Supplier.TableColumns.NameAr]);
                    info.NameEn = Convert.ToString(reader[Supplier.TableColumns.NameEn]);
                    info.ShortDescriptionAr = Convert.ToString(reader[Supplier.TableColumns.ShortDescriptionAr]);
                    info.ShortDescriptionEn = Convert.ToString(reader[Supplier.TableColumns.ShortDescriptionEn]);
                    info.ContactPerson = Convert.ToString(reader[Supplier.TableColumns.ContactPerson]);
                    info.IsActive = Convert.ToBoolean(reader[Supplier.TableColumns.IsActive]);
                    info.CreationDate = Convert.ToDateTime(reader[Supplier.CommonColumns.CreationDate]);

                    if (reader[Supplier.TableColumns.DescriptionAr] != DBNull.Value)
                        info.DescriptionAr = Convert.ToString(reader[Supplier.TableColumns.DescriptionAr]);

                    if (reader[Supplier.TableColumns.DescriptionEn] != DBNull.Value)
                        info.DescriptionEn = Convert.ToString(reader[Supplier.TableColumns.DescriptionEn]);

                    if (reader[Supplier.TableColumns.ContactPersonMobile] != DBNull.Value)
                        info.ContactPersonMobile = Convert.ToString(reader[Supplier.TableColumns.ContactPersonMobile]);

                    if (reader[Supplier.TableColumns.ContactPersonEmail] != DBNull.Value)
                        info.ContactPersonEmail = Convert.ToString(reader[Supplier.TableColumns.ContactPersonEmail]);

                    if (reader[Supplier.TableColumns.ActivationDate] != DBNull.Value)
                        info.ActivationDate = Convert.ToDateTime(reader[Supplier.TableColumns.ActivationDate]);
                    else
                        info.ActivationDate = null;

                    if (reader[Supplier.TableColumns.ActivatedBy] != DBNull.Value)
                        info.ActivatedBy = (Guid)reader[Supplier.TableColumns.ActivatedBy];
                    else
                        info.ActivatedBy = null;

                    if (reader[Supplier.TableColumns.DeactivationDate] != DBNull.Value)
                        info.DeactivationDate = Convert.ToDateTime(reader[Supplier.TableColumns.DeactivationDate]);
                    else
                        info.DeactivationDate = null;

                    if (reader[Supplier.TableColumns.DeactivatedBy] != DBNull.Value)
                        info.DeactivatedBy = (Guid)reader[Supplier.TableColumns.DeactivatedBy];
                    else
                        info.DeactivatedBy = null;

                    if (reader[Supplier.CommonColumns.CreatedBy] != DBNull.Value)
                        info.CreatedBy = (Guid)reader[Supplier.CommonColumns.CreatedBy];
                    else
                        info.CreatedBy = null;

                    if (reader[Supplier.CommonColumns.ModificationDate] != DBNull.Value)
                        info.ModificationDate = Convert.ToDateTime(reader[Supplier.CommonColumns.ModificationDate]);
                    else
                        info.ModificationDate = null;

                    if (reader[Supplier.CommonColumns.ModifiedBy] != DBNull.Value)
                        info.ModifiedBy = (Guid)reader[Supplier.CommonColumns.ModifiedBy];
                    else
                        info.ModifiedBy = null;
                }
                else
                {
                    if (IsArabic.Value)
                    {
                        info.NameAr = Convert.ToString(reader[Supplier.TableColumns.NameAr]);
                        info.ShortDescriptionAr = Convert.ToString(reader[Supplier.TableColumns.ShortDescriptionAr]);

                        if (reader[Supplier.TableColumns.DescriptionAr] != DBNull.Value)
                            info.DescriptionAr = Convert.ToString(reader[Supplier.TableColumns.DescriptionAr]);
                    }
                    else
                    {
                        info.NameEn = Convert.ToString(reader[Supplier.TableColumns.NameEn]);
                        info.ShortDescriptionEn = Convert.ToString(reader[Supplier.TableColumns.ShortDescriptionEn]);

                        if (reader[Supplier.TableColumns.DescriptionEn] != DBNull.Value)
                            info.DescriptionEn = Convert.ToString(reader[Supplier.TableColumns.DescriptionEn]);
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        private void ReadSupplierList(SqlDataReader reader, List<Supplier> infoList, bool? IsArabic)
        {
            try
            {
                Supplier info = null;

                if (!IsArabic.HasValue)
                {
                    while (reader.Read())
                    {
                        info = new Supplier();

                        info.ID = Convert.ToInt32(reader[Supplier.CommonColumns.ID]);
                        info.NameAr = Convert.ToString(reader[Supplier.TableColumns.NameAr]);
                        info.NameEn = Convert.ToString(reader[Supplier.TableColumns.NameEn]);
                        info.ShortDescriptionAr = Convert.ToString(reader[Supplier.TableColumns.ShortDescriptionAr]);
                        info.ShortDescriptionEn = Convert.ToString(reader[Supplier.TableColumns.ShortDescriptionEn]);
                        info.ContactPerson = Convert.ToString(reader[Supplier.TableColumns.ContactPerson]);
                        info.IsActive = Convert.ToBoolean(reader[Supplier.TableColumns.IsActive]);
                        info.CreationDate = Convert.ToDateTime(reader[Supplier.CommonColumns.CreationDate]);

                        if (reader[Supplier.TableColumns.Website] != DBNull.Value)
                            info.Website = Convert.ToString(reader[Supplier.TableColumns.Website]);

                        if (reader[Supplier.TableColumns.Email] != DBNull.Value)
                            info.Email = Convert.ToString(reader[Supplier.TableColumns.Email]);

                        if (reader[Supplier.TableColumns.HotLine] != DBNull.Value)
                            info.HotLine = Convert.ToString(reader[Supplier.TableColumns.HotLine]);

                        if (reader[Supplier.TableColumns.Image] != DBNull.Value)
                            info.Image = Convert.ToString(reader[Supplier.TableColumns.Image]);

                        if (reader[Supplier.TableColumns.DescriptionAr] != DBNull.Value)
                            info.DescriptionAr = Convert.ToString(reader[Supplier.TableColumns.DescriptionAr]);

                        if (reader[Supplier.TableColumns.DescriptionEn] != DBNull.Value)
                            info.DescriptionEn = Convert.ToString(reader[Supplier.TableColumns.DescriptionEn]);

                        if (reader[Supplier.TableColumns.ContactPersonMobile] != DBNull.Value)
                            info.ContactPersonMobile = Convert.ToString(reader[Supplier.TableColumns.ContactPersonMobile]);

                        if (reader[Supplier.TableColumns.ContactPersonEmail] != DBNull.Value)
                            info.ContactPersonEmail = Convert.ToString(reader[Supplier.TableColumns.ContactPersonEmail]);

                        if (reader[Supplier.TableColumns.ActivationDate] != DBNull.Value)
                            info.ActivationDate = Convert.ToDateTime(reader[Supplier.TableColumns.ActivationDate]);
                        else
                            info.ActivationDate = null;

                        if (reader[Supplier.TableColumns.ActivatedBy] != DBNull.Value)
                            info.ActivatedBy = (Guid)reader[Supplier.TableColumns.ActivatedBy];
                        else
                            info.ActivatedBy = null;

                        if (reader[Supplier.TableColumns.DeactivationDate] != DBNull.Value)
                            info.DeactivationDate = Convert.ToDateTime(reader[Supplier.TableColumns.DeactivationDate]);
                        else
                            info.DeactivationDate = null;

                        if (reader[Supplier.TableColumns.DeactivatedBy] != DBNull.Value)
                            info.DeactivatedBy = (Guid)reader[Supplier.TableColumns.DeactivatedBy];
                        else
                            info.DeactivatedBy = null;

                        if (reader[Supplier.CommonColumns.CreatedBy] != DBNull.Value)
                            info.CreatedBy = (Guid)reader[Supplier.CommonColumns.CreatedBy];
                        else
                            info.CreatedBy = null;

                        if (reader[Supplier.CommonColumns.ModificationDate] != DBNull.Value)
                            info.ModificationDate = Convert.ToDateTime(reader[Supplier.CommonColumns.ModificationDate]);
                        else
                            info.ModificationDate = null;

                        if (reader[Supplier.CommonColumns.ModifiedBy] != DBNull.Value)
                            info.ModifiedBy = (Guid)reader[Supplier.CommonColumns.ModifiedBy];
                        else
                            info.ModifiedBy = null;

                        infoList.Add(info);
                    }
                }
                else
                {
                    if (IsArabic.Value)
                    {
                        while (reader.Read())
                        {
                            info = new Supplier();

                            info.ID = Convert.ToInt32(reader[Supplier.CommonColumns.ID]);
                            info.NameAr = Convert.ToString(reader[Supplier.TableColumns.NameAr]);
                            info.ShortDescriptionAr = Convert.ToString(reader[Supplier.TableColumns.ShortDescriptionAr]);

                            if (reader[Supplier.TableColumns.Website] != DBNull.Value)
                                info.Website = Convert.ToString(reader[Supplier.TableColumns.Website]);

                            if (reader[Supplier.TableColumns.Email] != DBNull.Value)
                                info.Email = Convert.ToString(reader[Supplier.TableColumns.Email]);

                            if (reader[Supplier.TableColumns.HotLine] != DBNull.Value)
                                info.HotLine = Convert.ToString(reader[Supplier.TableColumns.HotLine]);

                            if (reader[Supplier.TableColumns.Image] != DBNull.Value)
                                info.Image = Convert.ToString(reader[Supplier.TableColumns.Image]);

                            if (reader[Supplier.TableColumns.DescriptionAr] != DBNull.Value)
                                info.DescriptionAr = Convert.ToString(reader[Supplier.TableColumns.DescriptionAr]);

                            infoList.Add(info);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            info = new Supplier();

                            info.ID = Convert.ToInt32(reader[Supplier.CommonColumns.ID]);
                            info.NameEn = Convert.ToString(reader[Supplier.TableColumns.NameEn]);
                            info.ShortDescriptionEn = Convert.ToString(reader[Supplier.TableColumns.ShortDescriptionEn]);

                            if (reader[Supplier.TableColumns.Website] != DBNull.Value)
                                info.Website = Convert.ToString(reader[Supplier.TableColumns.Website]);

                            if (reader[Supplier.TableColumns.Email] != DBNull.Value)
                                info.Email = Convert.ToString(reader[Supplier.TableColumns.Email]);

                            if (reader[Supplier.TableColumns.HotLine] != DBNull.Value)
                                info.HotLine = Convert.ToString(reader[Supplier.TableColumns.HotLine]);

                            if (reader[Supplier.TableColumns.Image] != DBNull.Value)
                                info.Image = Convert.ToString(reader[Supplier.TableColumns.Image]);

                            if (reader[Supplier.TableColumns.DescriptionEn] != DBNull.Value)
                                info.DescriptionEn = Convert.ToString(reader[Supplier.TableColumns.DescriptionEn]);

                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        private void ReadBranchesList(SqlDataReader reader, Supplier info, bool? IsArabic)
        {
            try
            {
                if (reader.NextResult())
                {
                    Branch suppBranch = null;

                    if (!IsArabic.HasValue)
                    {
                        while (reader.Read())
                        {
                            suppBranch = new Branch();

                            suppBranch.ID = Convert.ToInt32(reader[Branch.CommonColumns.ID]);
                            suppBranch.SupplierID = Convert.ToInt32(reader[Branch.CommonColumns.SupplierID]);
                            suppBranch.BranchLocation.ID = Convert.ToInt32(reader[Branch.TableColumns.LocationID]);
                            suppBranch.NameAr = Convert.ToString(reader[Branch.TableColumns.NameAr]);
                            suppBranch.NameEn = Convert.ToString(reader[Branch.TableColumns.NameEn]);
                            suppBranch.AddressAr = Convert.ToString(reader[Branch.TableColumns.AddressAr]);
                            suppBranch.AddressEn = Convert.ToString(reader[Branch.TableColumns.AddressEn]);
                            suppBranch.BranchLocation.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);
                            suppBranch.BranchLocation.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);
                            suppBranch.CreationDate = Convert.ToDateTime(reader[Branch.CommonColumns.CreationDate]);

                            if (reader[Branch.TableColumns.Phone1] != DBNull.Value)
                                suppBranch.Phone1 = Convert.ToString(reader[Branch.TableColumns.Phone1]);

                            if (reader[Branch.TableColumns.Phone2] != DBNull.Value)
                                suppBranch.Phone2 = Convert.ToString(reader[Branch.TableColumns.Phone2]);

                            if (reader[Branch.TableColumns.Phone3] != DBNull.Value)
                                suppBranch.Phone3 = Convert.ToString(reader[Branch.TableColumns.Phone3]);

                            if (reader[Branch.TableColumns.Mobile1] != DBNull.Value)
                                suppBranch.Mobile1 = Convert.ToString(reader[Branch.TableColumns.Mobile1]);

                            if (reader[Branch.TableColumns.Mobile2] != DBNull.Value)
                                suppBranch.Mobile2 = Convert.ToString(reader[Branch.TableColumns.Mobile2]);

                            if (reader[Branch.TableColumns.Mobile3] != DBNull.Value)
                                suppBranch.Mobile3 = Convert.ToString(reader[Branch.TableColumns.Mobile3]);

                            if (reader[Branch.TableColumns.Fax] != DBNull.Value)
                                suppBranch.Fax = Convert.ToString(reader[Branch.TableColumns.Fax]);

                            if (reader[Branch.TableColumns.XCoordination] != DBNull.Value)
                                suppBranch.XCoordination = Convert.ToDecimal(reader[Branch.TableColumns.XCoordination]);
                            else
                                suppBranch.XCoordination = null;

                            if (reader[Branch.TableColumns.YCoordination] != DBNull.Value)
                                suppBranch.YCoordination = Convert.ToDecimal(reader[Branch.TableColumns.YCoordination]);
                            else
                                suppBranch.YCoordination = null;

                            if (reader[Branch.TableColumns.MapZoom] != DBNull.Value)
                                suppBranch.MapZoom = Convert.ToInt32(reader[Branch.TableColumns.MapZoom]);
                            else
                                suppBranch.MapZoom = null;

                            if (reader[Branch.CommonColumns.CreatedBy] != DBNull.Value)
                                suppBranch.CreatedBy = (Guid)reader[Branch.CommonColumns.CreatedBy];
                            else
                                suppBranch.CreatedBy = null;

                            if (reader[Branch.CommonColumns.ModificationDate] != DBNull.Value)
                                suppBranch.ModificationDate = Convert.ToDateTime(reader[Branch.CommonColumns.ModificationDate]);
                            else
                                suppBranch.ModificationDate = null;

                            if (reader[Branch.CommonColumns.ModifiedBy] != DBNull.Value)
                                suppBranch.ModifiedBy = (Guid)reader[Branch.CommonColumns.ModifiedBy];
                            else
                                suppBranch.ModifiedBy = null;

                            info.BranchList.Add(suppBranch);
                        }
                    }
                    else
                    {
                        if (IsArabic.Value)
                        {
                            while (reader.Read())
                            {
                                suppBranch = new Branch();

                                suppBranch.ID = Convert.ToInt32(reader[Branch.CommonColumns.ID]);
                                suppBranch.SupplierID = Convert.ToInt32(reader[Branch.CommonColumns.SupplierID]);
                                suppBranch.BranchLocation.ID = Convert.ToInt32(reader[Branch.TableColumns.LocationID]);
                                suppBranch.NameAr = Convert.ToString(reader[Branch.TableColumns.NameAr]);
                                suppBranch.AddressAr = Convert.ToString(reader[Branch.TableColumns.AddressAr]);
                                suppBranch.BranchLocation.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);

                                if (reader[Branch.TableColumns.Phone1] != DBNull.Value)
                                    suppBranch.Phone1 = Convert.ToString(reader[Branch.TableColumns.Phone1]);

                                if (reader[Branch.TableColumns.Phone2] != DBNull.Value)
                                    suppBranch.Phone2 = Convert.ToString(reader[Branch.TableColumns.Phone2]);

                                if (reader[Branch.TableColumns.Phone3] != DBNull.Value)
                                    suppBranch.Phone3 = Convert.ToString(reader[Branch.TableColumns.Phone3]);

                                if (reader[Branch.TableColumns.Mobile1] != DBNull.Value)
                                    suppBranch.Mobile1 = Convert.ToString(reader[Branch.TableColumns.Mobile1]);

                                if (reader[Branch.TableColumns.Mobile2] != DBNull.Value)
                                    suppBranch.Mobile2 = Convert.ToString(reader[Branch.TableColumns.Mobile2]);

                                if (reader[Branch.TableColumns.Mobile3] != DBNull.Value)
                                    suppBranch.Mobile3 = Convert.ToString(reader[Branch.TableColumns.Mobile3]);

                                if (reader[Branch.TableColumns.Fax] != DBNull.Value)
                                    suppBranch.Fax = Convert.ToString(reader[Branch.TableColumns.Fax]);

                                if (reader[Branch.TableColumns.XCoordination] != DBNull.Value)
                                    suppBranch.XCoordination = Convert.ToDecimal(reader[Branch.TableColumns.XCoordination]);
                                else
                                    suppBranch.XCoordination = null;

                                if (reader[Branch.TableColumns.YCoordination] != DBNull.Value)
                                    suppBranch.YCoordination = Convert.ToDecimal(reader[Branch.TableColumns.YCoordination]);
                                else
                                    suppBranch.YCoordination = null;

                                if (reader[Branch.TableColumns.MapZoom] != DBNull.Value)
                                    suppBranch.MapZoom = Convert.ToInt32(reader[Branch.TableColumns.MapZoom]);
                                else
                                    suppBranch.MapZoom = null;


                                info.BranchList.Add(suppBranch);
                            }
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                suppBranch = new Branch();

                                suppBranch.ID = Convert.ToInt32(reader[Branch.CommonColumns.ID]);
                                suppBranch.SupplierID = Convert.ToInt32(reader[Branch.CommonColumns.SupplierID]);
                                suppBranch.BranchLocation.ID = Convert.ToInt32(reader[Branch.TableColumns.LocationID]);
                                suppBranch.NameEn = Convert.ToString(reader[Branch.TableColumns.NameEn]);
                                suppBranch.AddressEn = Convert.ToString(reader[Branch.TableColumns.AddressEn]);
                                suppBranch.BranchLocation.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);

                                if (reader[Branch.TableColumns.Phone1] != DBNull.Value)
                                    suppBranch.Phone1 = Convert.ToString(reader[Branch.TableColumns.Phone1]);

                                if (reader[Branch.TableColumns.Phone2] != DBNull.Value)
                                    suppBranch.Phone2 = Convert.ToString(reader[Branch.TableColumns.Phone2]);

                                if (reader[Branch.TableColumns.Phone3] != DBNull.Value)
                                    suppBranch.Phone3 = Convert.ToString(reader[Branch.TableColumns.Phone3]);

                                if (reader[Branch.TableColumns.Mobile1] != DBNull.Value)
                                    suppBranch.Mobile1 = Convert.ToString(reader[Branch.TableColumns.Mobile1]);

                                if (reader[Branch.TableColumns.Mobile2] != DBNull.Value)
                                    suppBranch.Mobile2 = Convert.ToString(reader[Branch.TableColumns.Mobile2]);

                                if (reader[Branch.TableColumns.Mobile3] != DBNull.Value)
                                    suppBranch.Mobile3 = Convert.ToString(reader[Branch.TableColumns.Mobile3]);

                                if (reader[Branch.TableColumns.Fax] != DBNull.Value)
                                    suppBranch.Fax = Convert.ToString(reader[Branch.TableColumns.Fax]);

                                if (reader[Branch.TableColumns.XCoordination] != DBNull.Value)
                                    suppBranch.XCoordination = Convert.ToDecimal(reader[Branch.TableColumns.XCoordination]);
                                else
                                    suppBranch.XCoordination = null;

                                if (reader[Branch.TableColumns.YCoordination] != DBNull.Value)
                                    suppBranch.YCoordination = Convert.ToDecimal(reader[Branch.TableColumns.YCoordination]);
                                else
                                    suppBranch.YCoordination = null;

                                if (reader[Branch.TableColumns.MapZoom] != DBNull.Value)
                                    suppBranch.MapZoom = Convert.ToInt32(reader[Branch.TableColumns.MapZoom]);
                                else
                                    suppBranch.MapZoom = null;

                                info.BranchList.Add(suppBranch);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        private bool WriteSupplier(string ProcedureName, Supplier info, bool IsNew)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProcedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.NameAr), info.NameAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.NameEn), info.NameEn);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.ContactPerson), info.ContactPerson);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.ShortDescriptionAr), info.ShortDescriptionAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.ShortDescriptionEn), info.ShortDescriptionEn);

                if (!string.IsNullOrEmpty(info.DescriptionAr))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.DescriptionAr), info.DescriptionAr);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.DescriptionAr), DBNull.Value);

                if (!string.IsNullOrEmpty(info.DescriptionEn))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.DescriptionEn), info.DescriptionEn);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.DescriptionEn), DBNull.Value);

                if (!string.IsNullOrEmpty(info.ContactPersonMobile))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.ContactPersonMobile), info.ContactPersonMobile);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.ContactPersonMobile), DBNull.Value);

                if (!string.IsNullOrEmpty(info.ContactPersonEmail))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.ContactPersonEmail), info.ContactPersonEmail);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.ContactPersonEmail), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Image))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.Image), info.Image);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.Image), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Website))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.Website), info.Website);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.Website), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Email))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.Email), info.Email);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.Email), DBNull.Value);

                if (!string.IsNullOrEmpty(info.HotLine))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.HotLine), info.HotLine);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.TableColumns.HotLine), DBNull.Value);

                if (IsNew)
                {
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.ID), SqlDbType.Int);
                    command.Parameters[string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.ID)].Direction = ParameterDirection.Output;

                    if (info.CreatedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.CreatedBy), info.CreatedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.CreatedBy), DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.ID), info.ID);

                    if (info.ModifiedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.ModifiedBy), info.ModifiedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.ModifiedBy), DBNull.Value);
                }

                this.OpenConnection();

                command.ExecuteNonQuery();

                if (IsNew)
                {
                    info.ID = Convert.ToInt32(command.Parameters[string.Concat(CommonStrings.AtSymbol, Supplier.CommonColumns.ID)].Value);
                }

                result = true;
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        #endregion
    }
}