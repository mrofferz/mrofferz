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
    public class CategoryDAL : DataManagment
    {
        #region Operations

        public Category SelectByID(int ID, bool? IsArabic)
        {
            Category info = null;
            try
            {
                info = GetCategory(ID, ProceduresNames.CategorySelectByID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public List<Category> SelectAll(bool? IsArabic)
        {
            List<Category> infoList = null;
            try
            {
                infoList = GetCategoryList(ProceduresNames.CategorySelectAll, null, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Category> SelectByOfferAbility(bool? IsArabic, bool CanHaveOffers)
        {
            List<Category> infoList = null;
            try
            {
                infoList = GetCategoryList(ProceduresNames.CategorySelectByOfferAbility, new KeyValue(Category.TableColumns.CanHaveOffers, CanHaveOffers), IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Category> SelectByParentID(int parentID, bool? IsArabic)
        {
            List<Category> infoList = null;
            try
            {
                infoList = GetCategoryList(ProceduresNames.CategorySelectByParentID, new KeyValue(Category.TableColumns.ParentID, parentID), IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Category> SelectBaseCategories(bool? IsArabic)
        {
            List<Category> infoList = null;
            try
            {
                infoList = GetCategoryList(ProceduresNames.CategorySelectBase, null, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public bool Add(Category info)
        {
            bool result = false;
            try
            {
                result = WriteCategory(ProceduresNames.CategoryAdd, info, true);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool Update(Category info)
        {
            bool result = false;
            try
            {
                result = WriteCategory(ProceduresNames.CategoryUpdate, info, false);
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
                SqlCommand command = new SqlCommand(ProceduresNames.CategoryDelete, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.ID), ID);

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

        private Category GetCategory(int ID, string procedureName, bool? IsArabic)
        {
            Category info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.ID), ID);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.IsArabic), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadCategory(reader, IsArabic);
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

        private List<Category> GetCategoryList(string procedureName, KeyValue parameter, bool? IsArabic)
        {
            List<Category> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.IsArabic), DBNull.Value);

                if (parameter != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameter.Key), parameter.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Category>();

                    ReadCategoryList(reader, infoList, IsArabic);
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

        private Category ReadCategory(SqlDataReader reader, bool? IsArabic)
        {
            Category info = null;
            try
            {
                reader.Read();

                info = new Category();

                info.ID = Convert.ToInt32(reader[Category.CommonColumns.ID]);
                info.HasChildren = Convert.ToBoolean(reader[Category.TableColumns.HasChildren]);
                info.HasOffers = Convert.ToBoolean(reader[Category.TableColumns.HasOffers]);
                info.CanHaveOffers = Convert.ToBoolean(reader[Category.TableColumns.CanHaveOffers]);

                if (reader[Category.TableColumns.ParentID] != DBNull.Value)
                    info.ParentID = Convert.ToInt32(reader[Category.TableColumns.ParentID]);
                else
                    info.ParentID = null;

                if (!IsArabic.HasValue)
                {
                    info.NameAr = Convert.ToString(reader[Category.TableColumns.NameAr]);
                    info.NameEn = Convert.ToString(reader[Category.TableColumns.NameEn]);
                    info.CreationDate = Convert.ToDateTime(reader[Category.CommonColumns.CreationDate]);

                    if (reader[Category.CommonColumns.CreatedBy] != DBNull.Value)
                        info.CreatedBy = (Guid)reader[Category.CommonColumns.CreatedBy];
                    else
                        info.CreatedBy = null;

                    if (reader[Category.CommonColumns.ModificationDate] != DBNull.Value)
                        info.ModificationDate = Convert.ToDateTime(reader[Category.CommonColumns.ModificationDate]);
                    else
                        info.ModificationDate = null;

                    if (reader[Category.CommonColumns.ModifiedBy] != DBNull.Value)
                        info.ModifiedBy = (Guid)reader[Category.CommonColumns.ModifiedBy];
                    else
                        info.ModifiedBy = null;
                }
                else
                {
                    if (IsArabic.Value)
                    {
                        info.NameAr = Convert.ToString(reader[Category.TableColumns.NameAr]);
                    }
                    else
                    {
                        info.NameEn = Convert.ToString(reader[Category.TableColumns.NameEn]);
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        private void ReadCategoryList(SqlDataReader reader, List<Category> infoList, bool? IsArabic)
        {
            try
            {
                Category info = null;

                if (!IsArabic.HasValue)
                {
                    while (reader.Read())
                    {
                        info = new Category();

                        info.ID = Convert.ToInt32(reader[Category.CommonColumns.ID]);
                        info.NameAr = Convert.ToString(reader[Category.TableColumns.NameAr]);
                        info.NameEn = Convert.ToString(reader[Category.TableColumns.NameEn]);
                        info.HasChildren = Convert.ToBoolean(reader[Category.TableColumns.HasChildren]);
                        info.HasOffers = Convert.ToBoolean(reader[Category.TableColumns.HasOffers]);
                        info.CanHaveOffers = Convert.ToBoolean(reader[Category.TableColumns.CanHaveOffers]);
                        info.CreationDate = Convert.ToDateTime(reader[Category.CommonColumns.CreationDate]);

                        if (reader[Category.TableColumns.ParentID] != DBNull.Value)
                            info.ParentID = Convert.ToInt32(reader[Category.TableColumns.ParentID]);
                        else
                            info.ParentID = null;

                        if (reader[Category.CommonColumns.CreatedBy] != DBNull.Value)
                            info.CreatedBy = (Guid)reader[Category.CommonColumns.CreatedBy];
                        else
                            info.CreatedBy = null;

                        if (reader[Category.CommonColumns.ModificationDate] != DBNull.Value)
                            info.ModificationDate = Convert.ToDateTime(reader[Category.CommonColumns.ModificationDate]);
                        else
                            info.ModificationDate = null;

                        if (reader[Category.CommonColumns.ModifiedBy] != DBNull.Value)
                            info.ModifiedBy = (Guid)reader[Category.CommonColumns.ModifiedBy];
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
                            info = new Category();

                            info.ID = Convert.ToInt32(reader[Category.CommonColumns.ID]);
                            info.NameAr = Convert.ToString(reader[Category.TableColumns.NameAr]);
                            info.HasChildren = Convert.ToBoolean(reader[Category.TableColumns.HasChildren]);
                            info.HasOffers = Convert.ToBoolean(reader[Category.TableColumns.HasOffers]);
                            info.CanHaveOffers = Convert.ToBoolean(reader[Category.TableColumns.CanHaveOffers]);

                            if (reader[Category.TableColumns.ParentID] != DBNull.Value)
                                info.ParentID = Convert.ToInt32(reader[Category.TableColumns.ParentID]);
                            else
                                info.ParentID = null;

                            infoList.Add(info);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            info = new Category();

                            info.ID = Convert.ToInt32(reader[Category.CommonColumns.ID]);
                            info.NameEn = Convert.ToString(reader[Category.TableColumns.NameEn]);
                            info.HasChildren = Convert.ToBoolean(reader[Category.TableColumns.HasChildren]);
                            info.HasOffers = Convert.ToBoolean(reader[Category.TableColumns.HasOffers]);
                            info.CanHaveOffers = Convert.ToBoolean(reader[Category.TableColumns.CanHaveOffers]);

                            if (reader[Category.TableColumns.ParentID] != DBNull.Value)
                                info.ParentID = Convert.ToInt32(reader[Category.TableColumns.ParentID]);
                            else
                                info.ParentID = null;

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

        private bool WriteCategory(string ProcedureName, Category info, bool IsNew)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProcedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.TableColumns.NameAr), info.NameAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.TableColumns.NameEn), info.NameEn);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.TableColumns.CanHaveOffers), info.CanHaveOffers);

                if (info.ParentID.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.TableColumns.ParentID), info.ParentID);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.TableColumns.ParentID), DBNull.Value);

                if (IsNew)
                {
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.ID), SqlDbType.Int);
                    command.Parameters[string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.ID)].Direction = ParameterDirection.Output;

                    if (info.CreatedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.CreatedBy), info.CreatedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.CreatedBy), DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.ID), info.ID);

                    if (info.ModifiedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.ModifiedBy), info.ModifiedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.ModifiedBy), DBNull.Value);
                }

                this.OpenConnection();

                command.ExecuteNonQuery();

                if (IsNew)
                {
                    info.ID = Convert.ToInt32(command.Parameters[string.Concat(CommonStrings.AtSymbol, Category.CommonColumns.ID)].Value);
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