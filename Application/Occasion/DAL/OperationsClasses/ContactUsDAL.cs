using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common.StringsClasses;
using DAL.Resources;
using EntityLayer.Entities;

namespace DAL.OperationsClasses
{
    public class ContactUsDAL : DataManagment
    {
        #region Operations

        public ContactUs SelectByID(int ID)
        {
            ContactUs info = null;
            try
            {
                info = GetContactUs(new KeyValue(ContactUs.CommonColumns.ID, ID), ProceduresNames.ContactUsSelectByID);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public List<ContactUs> SelectAll()
        {
            List<ContactUs> infoList = null;
            try
            {
                KeyValue parameter = null;

                infoList = GetContactUsList(parameter, ProceduresNames.ContactUsSelectAll);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<ContactUs> SelectNew()
        {
            List<ContactUs> infoList = null;
            try
            {
                KeyValue parameter = null;

                infoList = GetContactUsList(parameter, ProceduresNames.ContactUsSelectNew);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<ContactUs> SelectReplied()
        {
            List<ContactUs> infoList = null;
            try
            {
                KeyValue parameter = null;

                infoList = GetContactUsList(parameter, ProceduresNames.ContactUsSelectReplied);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<ContactUs> SelectClosed()
        {
            List<ContactUs> infoList = null;
            try
            {
                KeyValue parameter = null;

                infoList = GetContactUsList(parameter, ProceduresNames.ContactUsSelectClosed);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public bool Add(ContactUs info)
        {
            bool result = false;
            try
            {
                result = WriteContactUs(ProceduresNames.ContactUsAdd, info);
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
                SqlCommand command = new SqlCommand(ProceduresNames.ContactUsDelete, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.CommonColumns.ID), ID);

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

        public bool Close(int ID, Guid? closedBy)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.ContactUsClose, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.CommonColumns.ID), ID);

                if (closedBy.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.ClosedBy), closedBy.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.ClosedBy), DBNull.Value);

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

        public bool Reply(int ID, Guid? repliedBy, string reply)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.ContactUsReply, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.CommonColumns.ID), ID);

                if (repliedBy.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.RepliedBy), repliedBy.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.RepliedBy), DBNull.Value);

                if (!string.IsNullOrEmpty(reply))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.Reply), reply);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.Reply), DBNull.Value);

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

        private ContactUs GetContactUs(KeyValue parameter, string procedureName)
        {
            ContactUs info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parameter != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameter.Key), parameter.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadContactUs(reader);
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

        private ContactUs GetContactUs(List<KeyValue> parametersList, string procedureName)
        {
            ContactUs info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parametersList != null && parametersList.Count > 0)
                {
                    foreach (KeyValue parameter in parametersList)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameter.Key), parameter.Value);
                }

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadContactUs(reader);
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

        private List<ContactUs> GetContactUsList(KeyValue parameter, string procedureName)
        {
            List<ContactUs> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parameter != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameter.Key), parameter.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<ContactUs>();

                    ReadContactUsList(reader, infoList);
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

        private List<ContactUs> GetContactUsList(List<KeyValue> parametersList, string procedureName)
        {
            List<ContactUs> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parametersList != null && parametersList.Count > 0)
                {
                    foreach (KeyValue parameter in parametersList)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameter.Key), parameter.Value);
                }

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<ContactUs>();

                    ReadContactUsList(reader, infoList);
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

        private ContactUs ReadContactUs(SqlDataReader reader)
        {
            ContactUs info = null;
            try
            {
                reader.Read();

                info = new ContactUs();

                info.ID = Convert.ToInt32(reader[ContactUs.CommonColumns.ID]);
                info.Title = Convert.ToString(reader[ContactUs.TableColumns.Title]);
                info.Description = Convert.ToString(reader[ContactUs.TableColumns.Description]);
                info.IsNew = Convert.ToBoolean(reader[ContactUs.TableColumns.IsNew]);
                info.CreationDate = Convert.ToDateTime(reader[ContactUs.CommonColumns.CreationDate]);
                info.IsReplied = Convert.ToBoolean(reader[ContactUs.TableColumns.IsReplied]);
                info.IsClosed = Convert.ToBoolean(reader[ContactUs.TableColumns.IsClosed]);

                if (reader[ContactUs.CommonColumns.CreatedBy] != DBNull.Value)
                    info.CreatedBy = (Guid)reader[ContactUs.CommonColumns.CreatedBy];
                else
                    info.CreatedBy = null;

                if (reader[ContactUs.TableColumns.Name] != DBNull.Value)
                    info.Name = Convert.ToString(reader[ContactUs.TableColumns.Name]);

                if (reader[ContactUs.TableColumns.Email] != DBNull.Value)
                    info.Email = Convert.ToString(reader[ContactUs.TableColumns.Email]);

                if (reader[ContactUs.TableColumns.RepliedBy] != DBNull.Value)
                    info.RepliedBy = (Guid)reader[ContactUs.TableColumns.RepliedBy];
                else
                    info.RepliedBy = null;

                if (reader[ContactUs.TableColumns.ReplyDate] != DBNull.Value)
                    info.ReplyDate = Convert.ToDateTime(reader[ContactUs.TableColumns.ReplyDate]);
                else
                    info.ReplyDate = null;

                if (reader[ContactUs.TableColumns.Reply] != DBNull.Value)
                    info.Reply = Convert.ToString(reader[ContactUs.TableColumns.Reply]);

                if (reader[ContactUs.TableColumns.ClosedBy] != DBNull.Value)
                    info.ClosedBy = (Guid)reader[ContactUs.TableColumns.ClosedBy];
                else
                    info.ClosedBy = null;

                if (reader[ContactUs.TableColumns.CloseDate] != DBNull.Value)
                    info.CloseDate = Convert.ToDateTime(reader[ContactUs.TableColumns.CloseDate]);
                else
                    info.CloseDate = null;
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        private void ReadContactUsList(SqlDataReader reader, List<ContactUs> infoList)
        {
            try
            {
                ContactUs info = null;

                while (reader.Read())
                {
                    info = new ContactUs();

                    info.ID = Convert.ToInt32(reader[ContactUs.CommonColumns.ID]);
                    info.Title = Convert.ToString(reader[ContactUs.TableColumns.Title]);
                    info.Description = Convert.ToString(reader[ContactUs.TableColumns.Description]);
                    info.IsNew = Convert.ToBoolean(reader[ContactUs.TableColumns.IsNew]);
                    info.CreationDate = Convert.ToDateTime(reader[ContactUs.CommonColumns.CreationDate]);
                    info.IsReplied = Convert.ToBoolean(reader[ContactUs.TableColumns.IsReplied]);
                    info.IsClosed = Convert.ToBoolean(reader[ContactUs.TableColumns.IsClosed]);

                    if (reader[ContactUs.CommonColumns.CreatedBy] != DBNull.Value)
                        info.CreatedBy = (Guid)reader[ContactUs.CommonColumns.CreatedBy];
                    else
                        info.CreatedBy = null;

                    if (reader[ContactUs.TableColumns.Name] != DBNull.Value)
                        info.Name = Convert.ToString(reader[ContactUs.TableColumns.Name]);

                    if (reader[ContactUs.TableColumns.Email] != DBNull.Value)
                        info.Email = Convert.ToString(reader[ContactUs.TableColumns.Email]);

                    if (reader[ContactUs.TableColumns.RepliedBy] != DBNull.Value)
                        info.RepliedBy = (Guid)reader[ContactUs.TableColumns.RepliedBy];
                    else
                        info.RepliedBy = null;

                    if (reader[ContactUs.TableColumns.ReplyDate] != DBNull.Value)
                        info.ReplyDate = Convert.ToDateTime(reader[ContactUs.TableColumns.ReplyDate]);
                    else
                        info.ReplyDate = null;

                    if (reader[ContactUs.TableColumns.Reply] != DBNull.Value)
                        info.Reply = Convert.ToString(reader[ContactUs.TableColumns.Reply]);

                    if (reader[ContactUs.TableColumns.ClosedBy] != DBNull.Value)
                        info.ClosedBy = (Guid)reader[ContactUs.TableColumns.ClosedBy];
                    else
                        info.ClosedBy = null;

                    if (reader[ContactUs.TableColumns.CloseDate] != DBNull.Value)
                        info.CloseDate = Convert.ToDateTime(reader[ContactUs.TableColumns.CloseDate]);
                    else
                        info.CloseDate = null;

                    infoList.Add(info);
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        private bool WriteContactUs(string ProcedureName, ContactUs info)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProcedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, ContactUs.CommonColumns.ID), SqlDbType.Int);
                command.Parameters[string.Concat(CommonStrings.AtSymbol, ContactUs.CommonColumns.ID)].Direction = ParameterDirection.Output;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.Title), info.Title);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.Description), info.Description);

                if (!string.IsNullOrEmpty(info.Name))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.Name), info.Name);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.Name), DBNull.Value);

                if (!string.IsNullOrEmpty(info.Email))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.Email), info.Email);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.TableColumns.Email), DBNull.Value);

                if (info.CreatedBy.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.CommonColumns.CreatedBy), info.CreatedBy.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, ContactUs.CommonColumns.CreatedBy), DBNull.Value);

                this.OpenConnection();

                command.ExecuteNonQuery();

                info.ID = Convert.ToInt32(command.Parameters[string.Concat(CommonStrings.AtSymbol, ContactUs.CommonColumns.ID)].Value);

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