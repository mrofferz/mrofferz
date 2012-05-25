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
    public class BranchDAL : DataManagment
    {
        #region Operations

        public Branch SelectByID(int ID, bool? IsArabic)
        {
            Branch info = null;
            try
            {
                info = GetBranch(ID, ProceduresNames.SupplierBranchSelectByID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public List<Branch> SelectBySupplierID(int supplierID, bool? IsArabic)
        {
            List<Branch> infoList = null;
            try
            {
                infoList = GetBranchList(ProceduresNames.SupplierBranchSelectBySupplierID, supplierID, Branch.TableColumns.SupplierID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public bool Add(Branch info)
        {
            bool result = false;
            try
            {
                result = WriteBranch(ProceduresNames.SupplierBranchAdd, info, true);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool Update(Branch info)
        {
            bool result = false;
            try
            {
                result = WriteBranch(ProceduresNames.SupplierBranchUpdate, info, false);
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
                SqlCommand command = new SqlCommand(ProceduresNames.SupplierBranchDelete, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.ID), ID);

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

        private Branch GetBranch(int ID, string procedureName, bool? IsArabic)
        {
            Branch info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.ID), ID);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.IsArabic), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadBranch(reader, IsArabic);
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

        private List<Branch> GetBranchList(string procedureName, int? foreignID, string foreignIDName, bool? IsArabic)
        {
            List<Branch> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.IsArabic), DBNull.Value);

                if (foreignID.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, foreignIDName), foreignID.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Branch>();

                    ReadBranchList(reader, infoList, IsArabic);
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

        private Branch ReadBranch(SqlDataReader reader, bool? IsArabic)
        {
            Branch info = null;
            try
            {
                reader.Read();

                info = new Branch();

                info.ID = Convert.ToInt32(reader[Branch.CommonColumns.ID]);
                info.SupplierID = Convert.ToInt32(reader[Branch.CommonColumns.SupplierID]);
                info.BranchLocation.ID = Convert.ToInt32(reader[Branch.TableColumns.LocationID]);

                if (reader[Branch.TableColumns.Phone1] != DBNull.Value)
                    info.Phone1 = Convert.ToString(reader[Branch.TableColumns.Phone1]);

                if (reader[Branch.TableColumns.Phone2] != DBNull.Value)
                    info.Phone2 = Convert.ToString(reader[Branch.TableColumns.Phone2]);

                if (reader[Branch.TableColumns.Phone3] != DBNull.Value)
                    info.Phone3 = Convert.ToString(reader[Branch.TableColumns.Phone3]);

                if (reader[Branch.TableColumns.Mobile1] != DBNull.Value)
                    info.Mobile1 = Convert.ToString(reader[Branch.TableColumns.Mobile1]);

                if (reader[Branch.TableColumns.Mobile2] != DBNull.Value)
                    info.Mobile2 = Convert.ToString(reader[Branch.TableColumns.Mobile2]);

                if (reader[Branch.TableColumns.Mobile3] != DBNull.Value)
                    info.Mobile3 = Convert.ToString(reader[Branch.TableColumns.Mobile3]);

                if (reader[Branch.TableColumns.Fax] != DBNull.Value)
                    info.Fax = Convert.ToString(reader[Branch.TableColumns.Fax]);

                if (reader[Branch.TableColumns.XCoordination] != DBNull.Value)
                    info.XCoordination = Convert.ToDecimal(reader[Branch.TableColumns.XCoordination]);
                else
                    info.XCoordination = null;

                if (reader[Branch.TableColumns.YCoordination] != DBNull.Value)
                    info.YCoordination = Convert.ToDecimal(reader[Branch.TableColumns.YCoordination]);
                else
                    info.YCoordination = null;

                if (reader[Branch.TableColumns.MapZoom] != DBNull.Value)
                    info.MapZoom = Convert.ToInt32(reader[Branch.TableColumns.MapZoom]);
                else
                    info.MapZoom = null;


                if (!IsArabic.HasValue)
                {
                    info.NameAr = Convert.ToString(reader[Branch.TableColumns.NameAr]);
                    info.NameEn = Convert.ToString(reader[Branch.TableColumns.NameEn]);
                    info.AddressAr = Convert.ToString(reader[Branch.TableColumns.AddressAr]);
                    info.AddressEn = Convert.ToString(reader[Branch.TableColumns.AddressEn]);
                    info.BranchLocation.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);
                    info.BranchLocation.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);
                    info.CreationDate = Convert.ToDateTime(reader[Branch.CommonColumns.CreationDate]);

                    if (reader[Branch.CommonColumns.CreatedBy] != DBNull.Value)
                        info.CreatedBy = (Guid)reader[Branch.CommonColumns.CreatedBy];
                    else
                        info.CreatedBy = null;

                    if (reader[Branch.CommonColumns.ModificationDate] != DBNull.Value)
                        info.ModificationDate = Convert.ToDateTime(reader[Branch.CommonColumns.ModificationDate]);
                    else
                        info.ModificationDate = null;

                    if (reader[Branch.CommonColumns.ModifiedBy] != DBNull.Value)
                        info.ModifiedBy = (Guid)reader[Branch.CommonColumns.ModifiedBy];
                    else
                        info.ModifiedBy = null;
                }
                else
                {
                    if (IsArabic.Value)
                    {
                        info.NameAr = Convert.ToString(reader[Branch.TableColumns.NameAr]);
                        info.AddressAr = Convert.ToString(reader[Branch.TableColumns.AddressAr]);
                        info.BranchLocation.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);

                    }
                    else
                    {
                        info.NameEn = Convert.ToString(reader[Branch.TableColumns.NameEn]);
                        info.AddressEn = Convert.ToString(reader[Branch.TableColumns.AddressEn]);
                        info.BranchLocation.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);

                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        private void ReadBranchList(SqlDataReader reader, List<Branch> infoList, bool? IsArabic)
        {
            try
            {
                Branch info = null;

                if (!IsArabic.HasValue)
                {
                    while (reader.Read())
                    {
                        info = new Branch();

                        info.ID = Convert.ToInt32(reader[Branch.CommonColumns.ID]);
                        info.SupplierID = Convert.ToInt32(reader[Branch.CommonColumns.SupplierID]);
                        info.BranchLocation.ID = Convert.ToInt32(reader[Branch.TableColumns.LocationID]);
                        info.NameAr = Convert.ToString(reader[Branch.TableColumns.NameAr]);
                        info.NameEn = Convert.ToString(reader[Branch.TableColumns.NameEn]);
                        info.AddressAr = Convert.ToString(reader[Branch.TableColumns.AddressAr]);
                        info.AddressEn = Convert.ToString(reader[Branch.TableColumns.AddressEn]);
                        info.BranchLocation.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);
                        info.BranchLocation.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);
                        info.CreationDate = Convert.ToDateTime(reader[Branch.CommonColumns.CreationDate]);

                        if (reader[Branch.TableColumns.Phone1] != DBNull.Value)
                            info.Phone1 = Convert.ToString(reader[Branch.TableColumns.Phone1]);

                        if (reader[Branch.TableColumns.Phone2] != DBNull.Value)
                            info.Phone2 = Convert.ToString(reader[Branch.TableColumns.Phone2]);

                        if (reader[Branch.TableColumns.Phone3] != DBNull.Value)
                            info.Phone3 = Convert.ToString(reader[Branch.TableColumns.Phone3]);

                        if (reader[Branch.TableColumns.Mobile1] != DBNull.Value)
                            info.Mobile1 = Convert.ToString(reader[Branch.TableColumns.Mobile1]);

                        if (reader[Branch.TableColumns.Mobile2] != DBNull.Value)
                            info.Mobile2 = Convert.ToString(reader[Branch.TableColumns.Mobile2]);

                        if (reader[Branch.TableColumns.Mobile3] != DBNull.Value)
                            info.Mobile3 = Convert.ToString(reader[Branch.TableColumns.Mobile3]);

                        if (reader[Branch.TableColumns.Fax] != DBNull.Value)
                            info.Fax = Convert.ToString(reader[Branch.TableColumns.Fax]);

                        if (reader[Branch.TableColumns.XCoordination] != DBNull.Value)
                            info.XCoordination = Convert.ToDecimal(reader[Branch.TableColumns.XCoordination]);
                        else
                            info.XCoordination = null;

                        if (reader[Branch.TableColumns.YCoordination] != DBNull.Value)
                            info.YCoordination = Convert.ToDecimal(reader[Branch.TableColumns.YCoordination]);
                        else
                            info.YCoordination = null;

                        if (reader[Branch.TableColumns.MapZoom] != DBNull.Value)
                            info.MapZoom = Convert.ToInt32(reader[Branch.TableColumns.MapZoom]);
                        else
                            info.MapZoom = null;

                        if (reader[Branch.CommonColumns.CreatedBy] != DBNull.Value)
                            info.CreatedBy = (Guid)reader[Branch.CommonColumns.CreatedBy];
                        else
                            info.CreatedBy = null;

                        if (reader[Branch.CommonColumns.ModificationDate] != DBNull.Value)
                            info.ModificationDate = Convert.ToDateTime(reader[Branch.CommonColumns.ModificationDate]);
                        else
                            info.ModificationDate = null;

                        if (reader[Branch.CommonColumns.ModifiedBy] != DBNull.Value)
                            info.ModifiedBy = (Guid)reader[Branch.CommonColumns.ModifiedBy];
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
                            info = new Branch();

                            info.ID = Convert.ToInt32(reader[Branch.CommonColumns.ID]);
                            info.SupplierID = Convert.ToInt32(reader[Branch.CommonColumns.SupplierID]);
                            info.BranchLocation.ID = Convert.ToInt32(reader[Branch.TableColumns.LocationID]);
                            info.NameAr = Convert.ToString(reader[Branch.TableColumns.NameAr]);
                            info.AddressAr = Convert.ToString(reader[Branch.TableColumns.AddressAr]);
                            info.BranchLocation.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);

                            if (reader[Branch.TableColumns.Phone1] != DBNull.Value)
                                info.Phone1 = Convert.ToString(reader[Branch.TableColumns.Phone1]);

                            if (reader[Branch.TableColumns.Phone2] != DBNull.Value)
                                info.Phone2 = Convert.ToString(reader[Branch.TableColumns.Phone2]);

                            if (reader[Branch.TableColumns.Phone3] != DBNull.Value)
                                info.Phone3 = Convert.ToString(reader[Branch.TableColumns.Phone3]);

                            if (reader[Branch.TableColumns.Mobile1] != DBNull.Value)
                                info.Mobile1 = Convert.ToString(reader[Branch.TableColumns.Mobile1]);

                            if (reader[Branch.TableColumns.Mobile2] != DBNull.Value)
                                info.Mobile2 = Convert.ToString(reader[Branch.TableColumns.Mobile2]);

                            if (reader[Branch.TableColumns.Mobile3] != DBNull.Value)
                                info.Mobile3 = Convert.ToString(reader[Branch.TableColumns.Mobile3]);

                            if (reader[Branch.TableColumns.Fax] != DBNull.Value)
                                info.Fax = Convert.ToString(reader[Branch.TableColumns.Fax]);

                            if (reader[Branch.TableColumns.XCoordination] != DBNull.Value)
                                info.XCoordination = Convert.ToDecimal(reader[Branch.TableColumns.XCoordination]);
                            else
                                info.XCoordination = null;

                            if (reader[Branch.TableColumns.YCoordination] != DBNull.Value)
                                info.YCoordination = Convert.ToDecimal(reader[Branch.TableColumns.YCoordination]);
                            else
                                info.YCoordination = null;

                            if (reader[Branch.TableColumns.MapZoom] != DBNull.Value)
                                info.MapZoom = Convert.ToInt32(reader[Branch.TableColumns.MapZoom]);
                            else
                                info.MapZoom = null;

                            infoList.Add(info);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            info = new Branch();

                            info.ID = Convert.ToInt32(reader[Branch.CommonColumns.ID]);
                            info.SupplierID = Convert.ToInt32(reader[Branch.CommonColumns.SupplierID]);
                            info.BranchLocation.ID = Convert.ToInt32(reader[Branch.TableColumns.LocationID]);
                            info.NameEn = Convert.ToString(reader[Branch.TableColumns.NameEn]);
                            info.AddressEn = Convert.ToString(reader[Branch.TableColumns.AddressEn]);
                            info.BranchLocation.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);

                            if (reader[Branch.TableColumns.Phone1] != DBNull.Value)
                                info.Phone1 = Convert.ToString(reader[Branch.TableColumns.Phone1]);

                            if (reader[Branch.TableColumns.Phone2] != DBNull.Value)
                                info.Phone2 = Convert.ToString(reader[Branch.TableColumns.Phone2]);

                            if (reader[Branch.TableColumns.Phone3] != DBNull.Value)
                                info.Phone3 = Convert.ToString(reader[Branch.TableColumns.Phone3]);

                            if (reader[Branch.TableColumns.Mobile1] != DBNull.Value)
                                info.Mobile1 = Convert.ToString(reader[Branch.TableColumns.Mobile1]);

                            if (reader[Branch.TableColumns.Mobile2] != DBNull.Value)
                                info.Mobile2 = Convert.ToString(reader[Branch.TableColumns.Mobile2]);

                            if (reader[Branch.TableColumns.Mobile3] != DBNull.Value)
                                info.Mobile3 = Convert.ToString(reader[Branch.TableColumns.Mobile3]);

                            if (reader[Branch.TableColumns.Fax] != DBNull.Value)
                                info.Fax = Convert.ToString(reader[Branch.TableColumns.Fax]);

                            if (reader[Branch.TableColumns.XCoordination] != DBNull.Value)
                                info.XCoordination = Convert.ToDecimal(reader[Branch.TableColumns.XCoordination]);
                            else
                                info.XCoordination = null;

                            if (reader[Branch.TableColumns.YCoordination] != DBNull.Value)
                                info.YCoordination = Convert.ToDecimal(reader[Branch.TableColumns.YCoordination]);
                            else
                                info.YCoordination = null;

                            if (reader[Branch.TableColumns.MapZoom] != DBNull.Value)
                                info.MapZoom = Convert.ToInt32(reader[Branch.TableColumns.MapZoom]);
                            else
                                info.MapZoom = null;

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

        private bool WriteBranch(string ProcedureName, Branch info, bool IsNew)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProcedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.NameAr), info.NameAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.NameEn), info.NameEn);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.SupplierID), info.SupplierID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.LocationID), info.BranchLocation.ID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.AddressAr), info.AddressAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.AddressEn), info.AddressEn);

                if (!string.IsNullOrEmpty(info.Phone1))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Phone1), info.Phone1);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Phone1), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Phone2))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Phone2), info.Phone2);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Phone2), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Phone3))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Phone3), info.Phone3);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Phone3), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Mobile1))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Mobile1), info.Mobile1);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Mobile1), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Mobile2))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Mobile2), info.Mobile2);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Mobile2), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Mobile3))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Mobile3), info.Mobile3);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Mobile3), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Fax))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.Fax), info.Fax);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.Fax), DBNull.Value);

                if (info.XCoordination.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.XCoordination), info.XCoordination.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.XCoordination), DBNull.Value);

                if (info.YCoordination.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.YCoordination), info.YCoordination.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.YCoordination), DBNull.Value);

                if (info.MapZoom.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.MapZoom), info.MapZoom.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.TableColumns.MapZoom), DBNull.Value);

                if (IsNew)
                {
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.ID), SqlDbType.Int);
                    command.Parameters[string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.ID)].Direction = ParameterDirection.Output;

                    if (info.CreatedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.CreatedBy), info.CreatedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.CreatedBy), DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.ID), info.ID);

                    if (info.ModifiedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.ModifiedBy), info.ModifiedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.ModifiedBy), DBNull.Value);
                }

                this.OpenConnection();

                command.ExecuteNonQuery();

                if (IsNew)
                {
                    info.ID = Convert.ToInt32(command.Parameters[string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.ID)].Value);
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