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
    public class FairDAL : DataManagment
    {
        #region Operations

        public Fair SelectByID(int ID, bool? IsArabic)
        {
            Fair info = null;
            try
            {
                info = GetFair(ID, ProceduresNames.FairSelectByID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public List<Fair> SelectAll(bool? IsArabic, bool? IsActive)
        {
            List<Fair> infoList = null;
            try
            {
                infoList = GetFairList(ProceduresNames.FairSelectAll, null, null, IsArabic, IsActive);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Fair> SelectByCategoryID(int categoryID, bool? IsArabic, bool? IsActive)
        {
            List<Fair> infoList = null;
            try
            {
                infoList = GetFairList(ProceduresNames.FairSelectByCatID, categoryID, Fair.CommonColumns.CategoryID, IsArabic, IsActive);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Fair> SelectByLocationID(int locationID, bool? IsArabic, bool? IsActive)
        {
            List<Fair> infoList = null;
            try
            {
                infoList = GetFairList(ProceduresNames.FairSelectByLocationID, locationID, Fair.TableColumns.LocationID, IsArabic, IsActive);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public bool Add(Fair info)
        {
            bool result = false;
            try
            {
                result = WriteFair(ProceduresNames.FairAdd, info, true);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool Update(Fair info)
        {
            bool result = false;
            try
            {
                result = WriteFair(ProceduresNames.FairUpdate, info, false);
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
                SqlCommand command = new SqlCommand(ProceduresNames.FairDelete, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ID), ID);

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
                SqlCommand command = new SqlCommand(ProceduresNames.FairActivate, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ID), ID);

                if (activatedBy.HasValue)
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.ActivatedBy), activatedBy.Value);
                else
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.ActivatedBy), DBNull.Value);

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
                SqlCommand command = new SqlCommand(ProceduresNames.FairDeactivate, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ID), ID);

                if (deactivatedBy.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.DeactivatedBy), deactivatedBy.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.DeactivatedBy), DBNull.Value);

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

        public bool Like(int ID)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.FairLike, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ID), ID);

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

        public bool Rate(int ID, int rateValue)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.FairRate, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ID), ID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.Value), rateValue);

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

        public bool LinkToCategory(int categoryID, int fairID)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.FairCategoryAdd, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.CategoryID), categoryID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.FairID), fairID);

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

        public bool BreakCategoryLink(int categoryID, int fairID)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.FairCategoryDelete, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.CategoryID), categoryID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.FairID), fairID);

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

        private Fair GetFair(int ID, string procedureName, bool? IsArabic)
        {
            Fair info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ID), ID);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.IsArabic), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadFair(reader, IsArabic);
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

        private List<Fair> GetFairList(string procedureName, int? foreignID, string foreignIDName, bool? IsArabic, bool? IsActive)
        {
            List<Fair> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.IsArabic), DBNull.Value);

                if (IsActive.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.IsActive), IsActive.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.IsActive), DBNull.Value);

                if (foreignID.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, foreignIDName), foreignID.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Fair>();

                    ReadFairList(reader, infoList, IsArabic);
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

        private Fair ReadFair(SqlDataReader reader, bool? IsArabic)
        {
            Fair info = null;
            try
            {
                reader.Read();

                info = new Fair();

                info.ID = Convert.ToInt32(reader[Fair.CommonColumns.ID]);
                info.LocationInfo.ID = Convert.ToInt32(reader[Fair.TableColumns.LocationID]);
                info.StartDate = Convert.ToDateTime(reader[Fair.TableColumns.StartDate]);
                info.EndDate = Convert.ToDateTime(reader[Fair.TableColumns.EndDate]);

                if (reader[Fair.TableColumns.ContactPerson] != DBNull.Value)
                    info.ContactPerson = Convert.ToString(reader[Fair.TableColumns.ContactPerson]);

                if (reader[Fair.TableColumns.ContactPersonMobile] != DBNull.Value)
                    info.ContactPersonMobile = Convert.ToString(reader[Fair.TableColumns.ContactPersonMobile]);

                if (reader[Fair.TableColumns.ContactPersonEmail] != DBNull.Value)
                    info.ContactPersonEmail = Convert.ToString(reader[Fair.TableColumns.ContactPersonEmail]);

                if (reader[Fair.TableColumns.Phone1] != DBNull.Value)
                    info.Phone1 = Convert.ToString(reader[Fair.TableColumns.Phone1]);

                if (reader[Fair.TableColumns.Phone2] != DBNull.Value)
                    info.Phone2 = Convert.ToString(reader[Fair.TableColumns.Phone2]);

                if (reader[Fair.TableColumns.Phone3] != DBNull.Value)
                    info.Phone3 = Convert.ToString(reader[Fair.TableColumns.Phone3]);

                if (reader[Fair.TableColumns.Mobile1] != DBNull.Value)
                    info.Mobile1 = Convert.ToString(reader[Fair.TableColumns.Mobile1]);

                if (reader[Fair.TableColumns.Mobile2] != DBNull.Value)
                    info.Mobile2 = Convert.ToString(reader[Fair.TableColumns.Mobile2]);

                if (reader[Fair.TableColumns.Mobile3] != DBNull.Value)
                    info.Mobile3 = Convert.ToString(reader[Fair.TableColumns.Mobile3]);

                if (reader[Fair.TableColumns.Fax] != DBNull.Value)
                    info.Fax = Convert.ToString(reader[Fair.TableColumns.Fax]);

                if (reader[Fair.TableColumns.Website] != DBNull.Value)
                    info.Website = Convert.ToString(reader[Fair.TableColumns.Website]);

                if (reader[Fair.TableColumns.Email] != DBNull.Value)
                    info.Email = Convert.ToString(reader[Fair.TableColumns.Email]);

                if (reader[Fair.TableColumns.Image] != DBNull.Value)
                    info.Image = Convert.ToString(reader[Fair.TableColumns.Image]);

                if (reader[Fair.TableColumns.Rate] != DBNull.Value)
                    info.Rate = Convert.ToInt32(reader[Fair.TableColumns.Rate]);
                else
                    info.Rate = null;

                if (reader[Fair.TableColumns.RateTotal] != DBNull.Value)
                    info.RateTotal = Convert.ToInt32(reader[Fair.TableColumns.RateTotal]);
                else
                    info.RateTotal = null;

                if (reader[Fair.TableColumns.RateCount] != DBNull.Value)
                    info.RateCount = Convert.ToInt32(reader[Fair.TableColumns.RateCount]);
                else
                    info.RateCount = null;

                if (reader[Fair.TableColumns.Likes] != DBNull.Value)
                    info.Likes = Convert.ToInt32(reader[Fair.TableColumns.Likes]);
                else
                    info.Likes = null;

                if (!IsArabic.HasValue)
                {
                    info.NameAr = Convert.ToString(reader[Fair.TableColumns.NameAr]);
                    info.NameEn = Convert.ToString(reader[Fair.TableColumns.NameEn]);
                    info.LocationInfo.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);
                    info.LocationInfo.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);
                    info.ShortDescriptionAr = Convert.ToString(reader[Fair.TableColumns.ShortDescriptionAr]);
                    info.ShortDescriptionEn = Convert.ToString(reader[Fair.TableColumns.ShortDescriptionEn]);
                    info.AddressAr = Convert.ToString(reader[Fair.TableColumns.AddressAr]);
                    info.AddressEn = Convert.ToString(reader[Fair.TableColumns.AddressEn]);
                    info.IsActive = Convert.ToBoolean(reader[Fair.TableColumns.IsActive]);
                    info.CreationDate = Convert.ToDateTime(reader[Fair.CommonColumns.CreationDate]);

                    if (reader[Fair.TableColumns.DescriptionAr] != DBNull.Value)
                        info.DescriptionAr = Convert.ToString(reader[Fair.TableColumns.DescriptionAr]);

                    if (reader[Fair.TableColumns.DescriptionEn] != DBNull.Value)
                        info.DescriptionEn = Convert.ToString(reader[Fair.TableColumns.DescriptionEn]);

                    if (reader[Fair.TableColumns.ActivationDate] != DBNull.Value)
                        info.ActivationDate = Convert.ToDateTime(reader[Fair.TableColumns.ActivationDate]);
                    else
                        info.ActivationDate = null;

                    if (reader[Fair.TableColumns.ActivatedBy] != DBNull.Value)
                        info.ActivatedBy = (Guid)reader[Fair.TableColumns.ActivatedBy];
                    else
                        info.ActivatedBy = null;

                    if (reader[Fair.TableColumns.DeactivationDate] != DBNull.Value)
                        info.DeactivationDate = Convert.ToDateTime(reader[Fair.TableColumns.DeactivationDate]);
                    else
                        info.DeactivationDate = null;

                    if (reader[Fair.TableColumns.DeactivatedBy] != DBNull.Value)
                        info.DeactivatedBy = (Guid)reader[Fair.TableColumns.DeactivatedBy];
                    else
                        info.DeactivatedBy = null;

                    if (reader[Fair.CommonColumns.CreatedBy] != DBNull.Value)
                        info.CreatedBy = (Guid)reader[Fair.CommonColumns.CreatedBy];
                    else
                        info.CreatedBy = null;

                    if (reader[Fair.CommonColumns.ModificationDate] != DBNull.Value)
                        info.ModificationDate = Convert.ToDateTime(reader[Fair.CommonColumns.ModificationDate]);
                    else
                        info.ModificationDate = null;

                    if (reader[Fair.CommonColumns.ModifiedBy] != DBNull.Value)
                        info.ModifiedBy = (Guid)reader[Fair.CommonColumns.ModifiedBy];
                    else
                        info.ModifiedBy = null;
                }
                else
                {
                    if (IsArabic.Value)
                    {
                        info.NameAr = Convert.ToString(reader[Fair.TableColumns.NameAr]);
                        info.LocationInfo.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);                        
                        info.ShortDescriptionAr = Convert.ToString(reader[Fair.TableColumns.ShortDescriptionAr]);
                        info.AddressAr = Convert.ToString(reader[Fair.TableColumns.AddressAr]);

                        if (reader[Fair.TableColumns.DescriptionAr] != DBNull.Value)
                            info.DescriptionAr = Convert.ToString(reader[Fair.TableColumns.DescriptionAr]);
                    }
                    else
                    {
                        info.NameEn = Convert.ToString(reader[Fair.TableColumns.NameEn]);
                        info.LocationInfo.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);
                        info.ShortDescriptionEn = Convert.ToString(reader[Fair.TableColumns.ShortDescriptionEn]);
                        info.AddressEn = Convert.ToString(reader[Fair.TableColumns.AddressEn]);

                        if (reader[Fair.TableColumns.DescriptionEn] != DBNull.Value)
                            info.DescriptionEn = Convert.ToString(reader[Fair.TableColumns.DescriptionEn]);
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        private void ReadFairList(SqlDataReader reader, List<Fair> infoList, bool? IsArabic)
        {
            try
            {
                Fair info = null;

                if (!IsArabic.HasValue)
                {
                    while (reader.Read())
                    {
                        info = new Fair();

                        info.ID = Convert.ToInt32(reader[Fair.CommonColumns.ID]);
                        info.LocationInfo.ID = Convert.ToInt32(reader[Fair.TableColumns.LocationID]);
                        info.LocationInfo.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);
                        info.LocationInfo.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);
                        info.StartDate = Convert.ToDateTime(reader[Fair.TableColumns.StartDate]);
                        info.EndDate = Convert.ToDateTime(reader[Fair.TableColumns.EndDate]);
                        info.NameAr = Convert.ToString(reader[Fair.TableColumns.NameAr]);
                        info.NameEn = Convert.ToString(reader[Fair.TableColumns.NameEn]);
                        info.ShortDescriptionAr = Convert.ToString(reader[Fair.TableColumns.ShortDescriptionAr]);
                        info.ShortDescriptionEn = Convert.ToString(reader[Fair.TableColumns.ShortDescriptionEn]);
                        info.AddressAr = Convert.ToString(reader[Fair.TableColumns.AddressAr]);
                        info.AddressEn = Convert.ToString(reader[Fair.TableColumns.AddressEn]);
                        info.IsActive = Convert.ToBoolean(reader[Fair.TableColumns.IsActive]);
                        info.CreationDate = Convert.ToDateTime(reader[Fair.CommonColumns.CreationDate]);

                        if (reader[Fair.TableColumns.ContactPerson] != DBNull.Value)
                            info.ContactPerson = Convert.ToString(reader[Fair.TableColumns.ContactPerson]);

                        if (reader[Fair.TableColumns.ContactPersonMobile] != DBNull.Value)
                            info.ContactPersonMobile = Convert.ToString(reader[Fair.TableColumns.ContactPersonMobile]);

                        if (reader[Fair.TableColumns.ContactPersonEmail] != DBNull.Value)
                            info.ContactPersonEmail = Convert.ToString(reader[Fair.TableColumns.ContactPersonEmail]);

                        if (reader[Fair.TableColumns.Phone1] != DBNull.Value)
                            info.Phone1 = Convert.ToString(reader[Fair.TableColumns.Phone1]);

                        if (reader[Fair.TableColumns.Phone2] != DBNull.Value)
                            info.Phone2 = Convert.ToString(reader[Fair.TableColumns.Phone2]);

                        if (reader[Fair.TableColumns.Phone3] != DBNull.Value)
                            info.Phone3 = Convert.ToString(reader[Fair.TableColumns.Phone3]);

                        if (reader[Fair.TableColumns.Mobile1] != DBNull.Value)
                            info.Mobile1 = Convert.ToString(reader[Fair.TableColumns.Mobile1]);

                        if (reader[Fair.TableColumns.Mobile2] != DBNull.Value)
                            info.Mobile2 = Convert.ToString(reader[Fair.TableColumns.Mobile2]);

                        if (reader[Fair.TableColumns.Mobile3] != DBNull.Value)
                            info.Mobile3 = Convert.ToString(reader[Fair.TableColumns.Mobile3]);

                        if (reader[Fair.TableColumns.Fax] != DBNull.Value)
                            info.Fax = Convert.ToString(reader[Fair.TableColumns.Fax]);

                        if (reader[Fair.TableColumns.Website] != DBNull.Value)
                            info.Website = Convert.ToString(reader[Fair.TableColumns.Website]);

                        if (reader[Fair.TableColumns.Email] != DBNull.Value)
                            info.Email = Convert.ToString(reader[Fair.TableColumns.Email]);

                        if (reader[Fair.TableColumns.Image] != DBNull.Value)
                            info.Image = Convert.ToString(reader[Fair.TableColumns.Image]);

                        if (reader[Fair.TableColumns.Rate] != DBNull.Value)
                            info.Rate = Convert.ToInt32(reader[Fair.TableColumns.Rate]);
                        else
                            info.Rate = null;

                        if (reader[Fair.TableColumns.RateTotal] != DBNull.Value)
                            info.RateTotal = Convert.ToInt32(reader[Fair.TableColumns.RateTotal]);
                        else
                            info.RateTotal = null;

                        if (reader[Fair.TableColumns.RateCount] != DBNull.Value)
                            info.RateCount = Convert.ToInt32(reader[Fair.TableColumns.RateCount]);
                        else
                            info.RateCount = null;

                        if (reader[Fair.TableColumns.Likes] != DBNull.Value)
                            info.Likes = Convert.ToInt32(reader[Fair.TableColumns.Likes]);
                        else
                            info.Likes = null;

                        if (reader[Fair.TableColumns.DescriptionAr] != DBNull.Value)
                            info.DescriptionAr = Convert.ToString(reader[Fair.TableColumns.DescriptionAr]);

                        if (reader[Fair.TableColumns.DescriptionEn] != DBNull.Value)
                            info.DescriptionEn = Convert.ToString(reader[Fair.TableColumns.DescriptionEn]);

                        if (reader[Fair.TableColumns.ActivationDate] != DBNull.Value)
                            info.ActivationDate = Convert.ToDateTime(reader[Fair.TableColumns.ActivationDate]);
                        else
                            info.ActivationDate = null;

                        if (reader[Fair.TableColumns.ActivatedBy] != DBNull.Value)
                            info.ActivatedBy = (Guid)reader[Fair.TableColumns.ActivatedBy];
                        else
                            info.ActivatedBy = null;

                        if (reader[Fair.TableColumns.DeactivationDate] != DBNull.Value)
                            info.DeactivationDate = Convert.ToDateTime(reader[Fair.TableColumns.DeactivationDate]);
                        else
                            info.DeactivationDate = null;

                        if (reader[Fair.TableColumns.DeactivatedBy] != DBNull.Value)
                            info.DeactivatedBy = (Guid)reader[Fair.TableColumns.DeactivatedBy];
                        else
                            info.DeactivatedBy = null;

                        if (reader[Fair.CommonColumns.CreatedBy] != DBNull.Value)
                            info.CreatedBy = (Guid)reader[Fair.CommonColumns.CreatedBy];
                        else
                            info.CreatedBy = null;

                        if (reader[Fair.CommonColumns.ModificationDate] != DBNull.Value)
                            info.ModificationDate = Convert.ToDateTime(reader[Fair.CommonColumns.ModificationDate]);
                        else
                            info.ModificationDate = null;

                        if (reader[Fair.CommonColumns.ModifiedBy] != DBNull.Value)
                            info.ModifiedBy = (Guid)reader[Fair.CommonColumns.ModifiedBy];
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
                            info = new Fair();

                            info.ID = Convert.ToInt32(reader[Fair.CommonColumns.ID]);
                            info.LocationInfo.ID = Convert.ToInt32(reader[Fair.TableColumns.LocationID]);
                            info.LocationInfo.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);
                            info.StartDate = Convert.ToDateTime(reader[Fair.TableColumns.StartDate]);
                            info.EndDate = Convert.ToDateTime(reader[Fair.TableColumns.EndDate]);
                            info.NameAr = Convert.ToString(reader[Fair.TableColumns.NameAr]);
                            info.ShortDescriptionAr = Convert.ToString(reader[Fair.TableColumns.ShortDescriptionAr]);
                            info.AddressAr = Convert.ToString(reader[Fair.TableColumns.AddressAr]);

                            if (reader[Fair.TableColumns.ContactPerson] != DBNull.Value)
                                info.ContactPerson = Convert.ToString(reader[Fair.TableColumns.ContactPerson]);

                            if (reader[Fair.TableColumns.ContactPersonMobile] != DBNull.Value)
                                info.ContactPersonMobile = Convert.ToString(reader[Fair.TableColumns.ContactPersonMobile]);

                            if (reader[Fair.TableColumns.ContactPersonEmail] != DBNull.Value)
                                info.ContactPersonEmail = Convert.ToString(reader[Fair.TableColumns.ContactPersonEmail]);

                            if (reader[Fair.TableColumns.Phone1] != DBNull.Value)
                                info.Phone1 = Convert.ToString(reader[Fair.TableColumns.Phone1]);

                            if (reader[Fair.TableColumns.Phone2] != DBNull.Value)
                                info.Phone2 = Convert.ToString(reader[Fair.TableColumns.Phone2]);

                            if (reader[Fair.TableColumns.Phone3] != DBNull.Value)
                                info.Phone3 = Convert.ToString(reader[Fair.TableColumns.Phone3]);

                            if (reader[Fair.TableColumns.Mobile1] != DBNull.Value)
                                info.Mobile1 = Convert.ToString(reader[Fair.TableColumns.Mobile1]);

                            if (reader[Fair.TableColumns.Mobile2] != DBNull.Value)
                                info.Mobile2 = Convert.ToString(reader[Fair.TableColumns.Mobile2]);

                            if (reader[Fair.TableColumns.Mobile3] != DBNull.Value)
                                info.Mobile3 = Convert.ToString(reader[Fair.TableColumns.Mobile3]);

                            if (reader[Fair.TableColumns.Fax] != DBNull.Value)
                                info.Fax = Convert.ToString(reader[Fair.TableColumns.Fax]);

                            if (reader[Fair.TableColumns.Website] != DBNull.Value)
                                info.Website = Convert.ToString(reader[Fair.TableColumns.Website]);

                            if (reader[Fair.TableColumns.Email] != DBNull.Value)
                                info.Email = Convert.ToString(reader[Fair.TableColumns.Email]);

                            if (reader[Fair.TableColumns.Image] != DBNull.Value)
                                info.Image = Convert.ToString(reader[Fair.TableColumns.Image]);

                            if (reader[Fair.TableColumns.Rate] != DBNull.Value)
                                info.Rate = Convert.ToInt32(reader[Fair.TableColumns.Rate]);
                            else
                                info.Rate = null;

                            if (reader[Fair.TableColumns.RateTotal] != DBNull.Value)
                                info.RateTotal = Convert.ToInt32(reader[Fair.TableColumns.RateTotal]);
                            else
                                info.RateTotal = null;

                            if (reader[Fair.TableColumns.RateCount] != DBNull.Value)
                                info.RateCount = Convert.ToInt32(reader[Fair.TableColumns.RateCount]);
                            else
                                info.RateCount = null;

                            if (reader[Fair.TableColumns.Likes] != DBNull.Value)
                                info.Likes = Convert.ToInt32(reader[Fair.TableColumns.Likes]);
                            else
                                info.Likes = null;

                            if (reader[Fair.TableColumns.DescriptionAr] != DBNull.Value)
                                info.DescriptionAr = Convert.ToString(reader[Fair.TableColumns.DescriptionAr]);

                            infoList.Add(info);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            info = new Fair();

                            info.ID = Convert.ToInt32(reader[Fair.CommonColumns.ID]);
                            info.LocationInfo.ID = Convert.ToInt32(reader[Fair.TableColumns.LocationID]);
                            info.LocationInfo.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);
                            info.StartDate = Convert.ToDateTime(reader[Fair.TableColumns.StartDate]);
                            info.EndDate = Convert.ToDateTime(reader[Fair.TableColumns.EndDate]);
                            info.NameEn = Convert.ToString(reader[Fair.TableColumns.NameEn]);
                            info.ShortDescriptionEn = Convert.ToString(reader[Fair.TableColumns.ShortDescriptionEn]);
                            info.AddressEn = Convert.ToString(reader[Fair.TableColumns.AddressEn]);

                            if (reader[Fair.TableColumns.ContactPerson] != DBNull.Value)
                                info.ContactPerson = Convert.ToString(reader[Fair.TableColumns.ContactPerson]);

                            if (reader[Fair.TableColumns.ContactPersonMobile] != DBNull.Value)
                                info.ContactPersonMobile = Convert.ToString(reader[Fair.TableColumns.ContactPersonMobile]);

                            if (reader[Fair.TableColumns.ContactPersonEmail] != DBNull.Value)
                                info.ContactPersonEmail = Convert.ToString(reader[Fair.TableColumns.ContactPersonEmail]);

                            if (reader[Fair.TableColumns.Phone1] != DBNull.Value)
                                info.Phone1 = Convert.ToString(reader[Fair.TableColumns.Phone1]);

                            if (reader[Fair.TableColumns.Phone2] != DBNull.Value)
                                info.Phone2 = Convert.ToString(reader[Fair.TableColumns.Phone2]);

                            if (reader[Fair.TableColumns.Phone3] != DBNull.Value)
                                info.Phone3 = Convert.ToString(reader[Fair.TableColumns.Phone3]);

                            if (reader[Fair.TableColumns.Mobile1] != DBNull.Value)
                                info.Mobile1 = Convert.ToString(reader[Fair.TableColumns.Mobile1]);

                            if (reader[Fair.TableColumns.Mobile2] != DBNull.Value)
                                info.Mobile2 = Convert.ToString(reader[Fair.TableColumns.Mobile2]);

                            if (reader[Fair.TableColumns.Mobile3] != DBNull.Value)
                                info.Mobile3 = Convert.ToString(reader[Fair.TableColumns.Mobile3]);

                            if (reader[Fair.TableColumns.Fax] != DBNull.Value)
                                info.Fax = Convert.ToString(reader[Fair.TableColumns.Fax]);

                            if (reader[Fair.TableColumns.Website] != DBNull.Value)
                                info.Website = Convert.ToString(reader[Fair.TableColumns.Website]);

                            if (reader[Fair.TableColumns.Email] != DBNull.Value)
                                info.Email = Convert.ToString(reader[Fair.TableColumns.Email]);

                            if (reader[Fair.TableColumns.Image] != DBNull.Value)
                                info.Image = Convert.ToString(reader[Fair.TableColumns.Image]);

                            if (reader[Fair.TableColumns.Rate] != DBNull.Value)
                                info.Rate = Convert.ToInt32(reader[Fair.TableColumns.Rate]);
                            else
                                info.Rate = null;

                            if (reader[Fair.TableColumns.RateTotal] != DBNull.Value)
                                info.RateTotal = Convert.ToInt32(reader[Fair.TableColumns.RateTotal]);
                            else
                                info.RateTotal = null;

                            if (reader[Fair.TableColumns.RateCount] != DBNull.Value)
                                info.RateCount = Convert.ToInt32(reader[Fair.TableColumns.RateCount]);
                            else
                                info.RateCount = null;

                            if (reader[Fair.TableColumns.Likes] != DBNull.Value)
                                info.Likes = Convert.ToInt32(reader[Fair.TableColumns.Likes]);
                            else
                                info.Likes = null;

                            if (reader[Fair.TableColumns.DescriptionEn] != DBNull.Value)
                                info.DescriptionEn = Convert.ToString(reader[Fair.TableColumns.DescriptionEn]);

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

        private bool WriteFair(string ProcedureName, Fair info, bool IsNew)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProcedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.NameAr), info.NameAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.NameEn), info.NameEn);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.ShortDescriptionAr), info.ShortDescriptionAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.ShortDescriptionEn), info.ShortDescriptionEn);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.AddressAr), info.AddressAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.AddressEn), info.AddressEn);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.LocationID), info.LocationInfo.ID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.StartDate), info.StartDate);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.EndDate), info.EndDate);

                if (!string.IsNullOrEmpty(info.DescriptionAr))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.DescriptionAr), info.DescriptionAr);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.DescriptionAr), DBNull.Value);

                if (!string.IsNullOrEmpty(info.DescriptionEn))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.DescriptionEn), info.DescriptionEn);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.DescriptionEn), DBNull.Value);

                if (!string.IsNullOrEmpty(info.ContactPerson))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.ContactPerson), info.ContactPerson);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.ContactPerson), DBNull.Value);

                if (!string.IsNullOrEmpty(info.ContactPersonMobile))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.ContactPersonMobile), info.ContactPersonMobile);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.ContactPersonMobile), DBNull.Value);

                if (!string.IsNullOrEmpty(info.ContactPersonEmail))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.ContactPersonEmail), info.ContactPersonEmail);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.ContactPersonEmail), DBNull.Value);

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
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Fax), info.Fax);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Fax), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Website))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Website), info.Website);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Website), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Email))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Email), info.Email);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Email), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Image))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Image), info.Image);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.TableColumns.Image), DBNull.Value);

                if (IsNew)
                {
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ID), SqlDbType.Int);
                    command.Parameters[string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ID)].Direction = ParameterDirection.Output;

                    if (info.CreatedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.CreatedBy), info.CreatedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.CreatedBy), DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ID), info.ID);

                    if (info.ModifiedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ModifiedBy), info.ModifiedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ModifiedBy), DBNull.Value);
                }

                this.OpenConnection();

                command.ExecuteNonQuery();

                if (IsNew)
                {
                    info.ID = Convert.ToInt32(command.Parameters[string.Concat(CommonStrings.AtSymbol, Fair.CommonColumns.ID)].Value);
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