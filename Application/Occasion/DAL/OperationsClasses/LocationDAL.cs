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
    public class LocationDAL : DataManagment
    {
        #region Operations

        public Location SelectByID(int ID, bool? IsArabic)
        {
            Location info = null;
            try
            {
                info = GetLocation(ID, ProceduresNames.LocationSelectByID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public List<Location> SelectAll(bool? IsArabic)
        {
            List<Location> infoList = null;
            try
            {
                infoList = GetLocationList(ProceduresNames.LocationSelectAll, null, null, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public bool Add(Location info)
        {
            bool result = false;
            try
            {
                result = WriteLocation(ProceduresNames.LocationAdd, info, true);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool Update(Location info)
        {
            bool result = false;
            try
            {
                result = WriteLocation(ProceduresNames.LocationUpdate, info, false);
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
                SqlCommand command = new SqlCommand(ProceduresNames.LocationDelete, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.ID), ID);

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

        private Location GetLocation(int ID, string procedureName, bool? IsArabic)
        {
            Location info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.ID), ID);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.IsArabic), IsArabic);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.IsArabic), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadLocation(reader, IsArabic);
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

        private List<Location> GetLocationList(string procedureName, int? foreignID, string foreignIDName, bool? IsArabic)
        {
            List<Location> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.IsArabic), IsArabic);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.IsArabic), DBNull.Value);

                if (foreignID.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, foreignIDName), foreignID.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Location>();

                    ReadLocationList(reader, infoList, IsArabic);
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

        private Location ReadLocation(SqlDataReader reader, bool? IsArabic)
        {
            Location info = null;
            try
            {
                reader.Read();

                info = new Location();

                info.ID = Convert.ToInt32(reader[Location.CommonColumns.ID]);

                if (!IsArabic.HasValue)
                {
                    info.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);
                    info.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);
                    info.CreationDate = Convert.ToDateTime(reader[Location.CommonColumns.CreationDate]);

                    if (reader[Location.CommonColumns.CreatedBy] != DBNull.Value)
                        info.CreatedBy = (Guid)reader[Location.CommonColumns.CreatedBy];
                    else
                        info.CreatedBy = null;

                    if (reader[Location.CommonColumns.ModificationDate] != DBNull.Value)
                        info.ModificationDate = Convert.ToDateTime(reader[Location.CommonColumns.ModificationDate]);
                    else
                        info.ModificationDate = null;

                    if (reader[Location.CommonColumns.ModifiedBy] != DBNull.Value)
                        info.ModifiedBy = (Guid)reader[Location.CommonColumns.ModifiedBy];
                    else
                        info.ModifiedBy = null;
                }
                else
                {
                    if (IsArabic.Value)
                    {
                        info.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);
                    }
                    else
                    {
                        info.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        private void ReadLocationList(SqlDataReader reader, List<Location> infoList, bool? IsArabic)
        {
            try
            {
                Location info = null;

                if (!IsArabic.HasValue)
                {
                    while (reader.Read())
                    {
                        info = new Location();

                        info.ID = Convert.ToInt32(reader[Location.CommonColumns.ID]);
                        info.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);
                        info.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);
                        info.CreationDate = Convert.ToDateTime(reader[Location.CommonColumns.CreationDate]);

                        if (reader[Location.CommonColumns.CreatedBy] != DBNull.Value)
                            info.CreatedBy = (Guid)reader[Location.CommonColumns.CreatedBy];
                        else
                            info.CreatedBy = null;

                        if (reader[Location.CommonColumns.ModificationDate] != DBNull.Value)
                            info.ModificationDate = Convert.ToDateTime(reader[Location.CommonColumns.ModificationDate]);
                        else
                            info.ModificationDate = null;

                        if (reader[Location.CommonColumns.ModifiedBy] != DBNull.Value)
                            info.ModifiedBy = (Guid)reader[Location.CommonColumns.ModifiedBy];
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
                            info = new Location();

                            info.ID = Convert.ToInt32(reader[Location.CommonColumns.ID]);
                            info.DistrictAr = Convert.ToString(reader[Location.TableColumns.DistrictAr]);

                            infoList.Add(info);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            info = new Location();

                            info.ID = Convert.ToInt32(reader[Location.CommonColumns.ID]);
                            info.DistrictEn = Convert.ToString(reader[Location.TableColumns.DistrictEn]);

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

        private bool WriteLocation(string ProcedureName, Location info, bool IsNew)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProcedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.TableColumns.DistrictAr), info.DistrictAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.TableColumns.DistrictEn), info.DistrictEn);

                if (IsNew)
                {
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.ID), SqlDbType.Int);
                    command.Parameters[string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.ID)].Direction = ParameterDirection.Output;

                    if (info.CreatedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.CreatedBy), info.CreatedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.CreatedBy), DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.ID), info.ID);

                    if (info.ModifiedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.ModifiedBy), info.ModifiedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.ModifiedBy), DBNull.Value);
                }

                this.OpenConnection();

                command.ExecuteNonQuery();

                if (IsNew)
                    info.ID = Convert.ToInt32(command.Parameters[string.Concat(CommonStrings.AtSymbol, Location.CommonColumns.ID)].Value);

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