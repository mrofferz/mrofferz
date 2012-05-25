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
    public class BrandDAL : DataManagment
    {
        #region Operations

        public Brand SelectByID(int ID, bool? IsArabic)
        {
            Brand info = null;
            try
            {
                info = GetBrand(ID, ProceduresNames.BrandSelectByID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public List<Brand> SelectAll(bool? IsArabic)
        {
            List<Brand> infoList = null;
            try
            {
                infoList = GetBrandList(ProceduresNames.BrandSelectAll, null, null, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public bool Add(Brand info)
        {
            bool result = false;
            try
            {
                result = WriteBrand(ProceduresNames.BrandAdd, info, true);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool Update(Brand info)
        {
            bool result = false;
            try
            {
                result = WriteBrand(ProceduresNames.BrandUpdate, info, false);
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
                SqlCommand command = new SqlCommand(ProceduresNames.BrandDelete, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.ID), ID);

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

        private Brand GetBrand(int ID, string procedureName, bool? IsArabic)
        {
            Brand info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.ID), ID);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.IsArabic), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadBrand(reader, IsArabic);
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

        private List<Brand> GetBrandList(string procedureName, int? foreignID, string foreignIDName, bool? IsArabic)
        {
            List<Brand> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.IsArabic), DBNull.Value);

                if (foreignID.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, foreignIDName), foreignID.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Brand>();

                    ReadBrandList(reader, infoList, IsArabic);
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

        private Brand ReadBrand(SqlDataReader reader, bool? IsArabic)
        {
            Brand info = null;
            try
            {
                reader.Read();

                info = new Brand();

                info.ID = Convert.ToInt32(reader[Brand.CommonColumns.ID]);
                info.Image = Convert.ToString(reader[Brand.TableColumns.Image]);

                if (!IsArabic.HasValue)
                {
                    info.NameAr = Convert.ToString(reader[Brand.TableColumns.NameAr]);
                    info.NameEn = Convert.ToString(reader[Brand.TableColumns.NameEn]);
                    info.ShortDescriptionAr = Convert.ToString(reader[Brand.TableColumns.ShortDescriptionAr]);
                    info.ShortDescriptionEn = Convert.ToString(reader[Brand.TableColumns.ShortDescriptionEn]);
                    info.CreationDate = Convert.ToDateTime(reader[Brand.CommonColumns.CreationDate]);

                    if (reader[Brand.TableColumns.DescriptionAr] != DBNull.Value)
                        info.DescriptionAr = Convert.ToString(reader[Brand.TableColumns.DescriptionAr]);

                    if (reader[Brand.TableColumns.DescriptionEn] != DBNull.Value)
                        info.DescriptionEn = Convert.ToString(reader[Brand.TableColumns.DescriptionEn]);

                    if (reader[Brand.CommonColumns.CreatedBy] != DBNull.Value)
                        info.CreatedBy = (Guid)reader[Brand.CommonColumns.CreatedBy];
                    else
                        info.CreatedBy = null;

                    if (reader[Brand.CommonColumns.ModificationDate] != DBNull.Value)
                        info.ModificationDate = Convert.ToDateTime(reader[Brand.CommonColumns.ModificationDate]);
                    else
                        info.ModificationDate = null;

                    if (reader[Brand.CommonColumns.ModifiedBy] != DBNull.Value)
                        info.ModifiedBy = (Guid)reader[Brand.CommonColumns.ModifiedBy];
                    else
                        info.ModifiedBy = null;
                }
                else
                {
                    if (IsArabic.Value)
                    {
                        info.NameAr = Convert.ToString(reader[Brand.TableColumns.NameAr]);
                        info.ShortDescriptionAr = Convert.ToString(reader[Brand.TableColumns.ShortDescriptionAr]);

                        if (reader[Brand.TableColumns.DescriptionAr] != DBNull.Value)
                            info.DescriptionAr = Convert.ToString(reader[Brand.TableColumns.DescriptionAr]);
                    }
                    else
                    {
                        info.NameEn = Convert.ToString(reader[Brand.TableColumns.NameEn]);
                        info.ShortDescriptionEn = Convert.ToString(reader[Brand.TableColumns.ShortDescriptionEn]);

                        if (reader[Brand.TableColumns.DescriptionEn] != DBNull.Value)
                            info.DescriptionEn = Convert.ToString(reader[Brand.TableColumns.DescriptionEn]);

                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        private void ReadBrandList(SqlDataReader reader, List<Brand> infoList, bool? IsArabic)
        {
            try
            {
                Brand info = null;

                if (!IsArabic.HasValue)
                {
                    while (reader.Read())
                    {
                        info = new Brand();

                        info.ID = Convert.ToInt32(reader[Brand.CommonColumns.ID]);
                        info.Image = Convert.ToString(reader[Brand.TableColumns.Image]);
                        info.NameAr = Convert.ToString(reader[Brand.TableColumns.NameAr]);
                        info.NameEn = Convert.ToString(reader[Brand.TableColumns.NameEn]);
                        info.ShortDescriptionAr = Convert.ToString(reader[Brand.TableColumns.ShortDescriptionAr]);
                        info.ShortDescriptionEn = Convert.ToString(reader[Brand.TableColumns.ShortDescriptionEn]);
                        info.CreationDate = Convert.ToDateTime(reader[Brand.CommonColumns.CreationDate]);

                        if (reader[Brand.TableColumns.DescriptionAr] != DBNull.Value)
                            info.DescriptionAr = Convert.ToString(reader[Brand.TableColumns.DescriptionAr]);

                        if (reader[Brand.TableColumns.DescriptionEn] != DBNull.Value)
                            info.DescriptionEn = Convert.ToString(reader[Brand.TableColumns.DescriptionEn]);

                        if (reader[Brand.CommonColumns.CreatedBy] != DBNull.Value)
                            info.CreatedBy = (Guid)reader[Brand.CommonColumns.CreatedBy];
                        else
                            info.CreatedBy = null;

                        if (reader[Brand.CommonColumns.ModificationDate] != DBNull.Value)
                            info.ModificationDate = Convert.ToDateTime(reader[Brand.CommonColumns.ModificationDate]);
                        else
                            info.ModificationDate = null;

                        if (reader[Brand.CommonColumns.ModifiedBy] != DBNull.Value)
                            info.ModifiedBy = (Guid)reader[Brand.CommonColumns.ModifiedBy];
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
                            info = new Brand();

                            info.ID = Convert.ToInt32(reader[Brand.CommonColumns.ID]);
                            info.Image = Convert.ToString(reader[Brand.TableColumns.Image]);
                            info.NameAr = Convert.ToString(reader[Brand.TableColumns.NameAr]);
                            info.ShortDescriptionAr = Convert.ToString(reader[Brand.TableColumns.ShortDescriptionAr]);

                            if (reader[Brand.TableColumns.DescriptionAr] != DBNull.Value)
                                info.DescriptionAr = Convert.ToString(reader[Brand.TableColumns.DescriptionAr]);

                            infoList.Add(info);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            info = new Brand();

                            info.ID = Convert.ToInt32(reader[Brand.CommonColumns.ID]);
                            info.Image = Convert.ToString(reader[Brand.TableColumns.Image]);
                            info.NameEn = Convert.ToString(reader[Brand.TableColumns.NameEn]);
                            info.ShortDescriptionEn = Convert.ToString(reader[Brand.TableColumns.ShortDescriptionEn]);

                            if (reader[Brand.TableColumns.DescriptionEn] != DBNull.Value)
                                info.DescriptionEn = Convert.ToString(reader[Brand.TableColumns.DescriptionEn]);

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

        private bool WriteBrand(string ProcedureName, Brand info, bool IsNew)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProcedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.TableColumns.NameAr), info.NameAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.TableColumns.NameEn), info.NameEn);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.TableColumns.ShortDescriptionAr), info.ShortDescriptionAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.TableColumns.ShortDescriptionEn), info.ShortDescriptionEn);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.TableColumns.Image), info.Image);

                if (!string.IsNullOrEmpty(info.DescriptionAr))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.TableColumns.DescriptionAr), info.DescriptionAr);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.TableColumns.DescriptionAr), DBNull.Value);

                if (!string.IsNullOrEmpty(info.DescriptionEn))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.TableColumns.DescriptionEn), info.DescriptionEn);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.TableColumns.DescriptionEn), DBNull.Value);

                if (IsNew)
                {
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.ID), SqlDbType.Int);
                    command.Parameters[string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.ID)].Direction = ParameterDirection.Output;

                    if (info.CreatedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.CreatedBy), info.CreatedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.CreatedBy), DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.ID), info.ID);

                    if (info.ModifiedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.ModifiedBy), info.ModifiedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.ModifiedBy), DBNull.Value);
                }

                this.OpenConnection();

                command.ExecuteNonQuery();

                if (IsNew)
                {
                    info.ID = Convert.ToInt32(command.Parameters[string.Concat(CommonStrings.AtSymbol, Brand.CommonColumns.ID)].Value);
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