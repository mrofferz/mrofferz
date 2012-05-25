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
    public class CurrencyDAL : DataManagment
    {
        #region Operations

        public Currency SelectByID(int ID, bool? IsArabic)
        {
            Currency info = null;
            try
            {
                info = GetCurrency(ID, ProceduresNames.CurrencySelectByID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public List<Currency> SelectAll(bool? IsArabic)
        {
            List<Currency> infoList = null;
            try
            {
                infoList = GetCurrencyList(ProceduresNames.CurrencySelectAll, null, null, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public bool Add(Currency info)
        {
            bool result = false;
            try
            {
                result = WriteCurrency(ProceduresNames.CurrencyAdd, info, true);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool Update(Currency info)
        {
            bool result = false;
            try
            {
                result = WriteCurrency(ProceduresNames.CurrencyUpdate, info, false);
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
                SqlCommand command = new SqlCommand(ProceduresNames.CurrencyDelete, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.ID), ID);

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

        private Currency GetCurrency(int ID, string procedureName, bool? IsArabic)
        {
            Currency info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.ID), ID);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.IsArabic), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadCurrency(reader, IsArabic);
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

        private List<Currency> GetCurrencyList(string procedureName, int? foreignID, string foreignIDName, bool? IsArabic)
        {
            List<Currency> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.IsArabic), DBNull.Value);

                if (foreignID.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, foreignIDName), foreignID.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Currency>();

                    ReadCurrencyList(reader, infoList, IsArabic);
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

        private Currency ReadCurrency(SqlDataReader reader, bool? IsArabic)
        {
            Currency info = null;
            try
            {
                reader.Read();

                info = new Currency();

                info.ID = Convert.ToInt32(reader[Currency.CommonColumns.ID]);

                if (!IsArabic.HasValue)
                {
                    info.UnitAr = Convert.ToString(reader[Currency.TableColumns.UnitAr]);
                    info.UnitEn = Convert.ToString(reader[Currency.TableColumns.UnitEn]);
                    info.CreationDate = Convert.ToDateTime(reader[Currency.CommonColumns.CreationDate]);

                    if (reader[Currency.CommonColumns.CreatedBy] != DBNull.Value)
                        info.CreatedBy = (Guid)reader[Currency.CommonColumns.CreatedBy];
                    else
                        info.CreatedBy = null;

                    if (reader[Currency.CommonColumns.ModificationDate] != DBNull.Value)
                        info.ModificationDate = Convert.ToDateTime(reader[Currency.CommonColumns.ModificationDate]);
                    else
                        info.ModificationDate = null;

                    if (reader[Currency.CommonColumns.ModifiedBy] != DBNull.Value)
                        info.ModifiedBy = (Guid)reader[Currency.CommonColumns.ModifiedBy];
                    else
                        info.ModifiedBy = null;
                }
                else
                {
                    if (IsArabic.Value)
                        info.UnitAr = Convert.ToString(reader[Currency.TableColumns.UnitAr]);
                    else
                        info.UnitEn = Convert.ToString(reader[Currency.TableColumns.UnitEn]);
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        private void ReadCurrencyList(SqlDataReader reader, List<Currency> infoList, bool? IsArabic)
        {
            try
            {
                Currency info = null;

                if (!IsArabic.HasValue)
                {
                    while (reader.Read())
                    {
                        info = new Currency();

                        info.ID = Convert.ToInt32(reader[Currency.CommonColumns.ID]);
                        info.UnitAr = Convert.ToString(reader[Currency.TableColumns.UnitAr]);
                        info.UnitEn = Convert.ToString(reader[Currency.TableColumns.UnitEn]);
                        info.CreationDate = Convert.ToDateTime(reader[Currency.CommonColumns.CreationDate]);

                        if (reader[Currency.CommonColumns.CreatedBy] != DBNull.Value)
                            info.CreatedBy = (Guid)reader[Currency.CommonColumns.CreatedBy];
                        else
                            info.CreatedBy = null;

                        if (reader[Currency.CommonColumns.ModificationDate] != DBNull.Value)
                            info.ModificationDate = Convert.ToDateTime(reader[Currency.CommonColumns.ModificationDate]);
                        else
                            info.ModificationDate = null;

                        if (reader[Currency.CommonColumns.ModifiedBy] != DBNull.Value)
                            info.ModifiedBy = (Guid)reader[Currency.CommonColumns.ModifiedBy];
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
                            info = new Currency();

                            info.ID = Convert.ToInt32(reader[Currency.CommonColumns.ID]);
                            info.UnitAr = Convert.ToString(reader[Currency.TableColumns.UnitAr]);

                            infoList.Add(info);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            info = new Currency();

                            info.ID = Convert.ToInt32(reader[Currency.CommonColumns.ID]);
                            info.UnitEn = Convert.ToString(reader[Currency.TableColumns.UnitEn]);

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

        private bool WriteCurrency(string ProcedureName, Currency info, bool IsNew)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProcedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.TableColumns.UnitAr), info.UnitAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.TableColumns.UnitEn), info.UnitEn);

                if (IsNew)
                {
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.ID), SqlDbType.Int);
                    command.Parameters[string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.ID)].Direction = ParameterDirection.Output;

                    if (info.CreatedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.CreatedBy), info.CreatedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.CreatedBy), DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.ID), info.ID);

                    if (info.ModifiedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.ModifiedBy), info.ModifiedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.ModifiedBy), DBNull.Value);
                }

                this.OpenConnection();

                command.ExecuteNonQuery();

                if (IsNew)
                {
                    info.ID = Convert.ToInt32(command.Parameters[string.Concat(CommonStrings.AtSymbol, Currency.CommonColumns.ID)].Value);
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