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
    public class PollDAL : DataManagment
    {
        #region Operations

        public Poll SelectPollByID(int ID, bool? IsArabic)
        {
            Poll info = null;
            try
            {
                info = GetPoll(ProceduresNames.PollSelectByID, ID, Poll.CommonColumns.ID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public List<Poll> SelectAllPolls(bool? IsArabic)
        {
            List<Poll> infoList = null;
            try
            {
                infoList = GetPollList(ProceduresNames.PollSelectAll, null, null, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Poll> SelectArchivedPolls(bool isArchived, bool? IsArabic)
        {
            List<Poll> infoList = null;
            try
            {
                infoList = GetPollList(ProceduresNames.PollSelectArchived, isArchived, Poll.TableColumns.IsArchived, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Poll> SelectNewPolls(bool isNew, bool? IsArabic)
        {
            List<Poll> infoList = null;
            try
            {
                infoList = GetPollList(ProceduresNames.PollSelectNew, isNew, Poll.TableColumns.IsNew, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public Poll SelectCurrentPoll(bool? IsArabic)
        {
            Poll info = null;
            try
            {
                info = GetPoll(ProceduresNames.PollSelectCurrent, null, null, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public bool AddPoll(Poll info)
        {
            bool result = false;
            try
            {
                result = WritePoll(ProceduresNames.PollAdd, info, true);

                foreach (PollOption option in info.Options)
                {
                    if (result)
                    {
                        option.PollID = info.ID;
                        result = AddOption(option);
                    }
                    else
                    {
                        throw new Exception("Not All Options Added");
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool UpdatePoll(Poll info)
        {
            bool result = false;
            try
            {
                result = WritePoll(ProceduresNames.PollUpdate, info, false);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool DeletePoll(int ID)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.PollDelete, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.ID), ID);

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

        public bool ArchivePoll(int ID, Guid? archivedBy)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.PollArchive, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.ID), ID);

                if (archivedBy != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.TableColumns.ArchivedBy), archivedBy);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.TableColumns.ArchivedBy), DBNull.Value);

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

        public bool SetCurrentPoll(int ID, Guid? currentBy)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.PollSetCurrent, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.ID), ID);

                if (currentBy != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.TableColumns.CurrentBy), currentBy);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.TableColumns.CurrentBy), DBNull.Value);

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

        public bool Vote(int pollID, int optionID)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.PollVote, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.PollID), pollID);

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, PollOption.CommonColumns.PollOptionID), optionID);

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

        public bool AddOption(PollOption info)
        {
            bool result = false;
            try
            {
                result = WritePollOption(ProceduresNames.PollOptionAdd, info, true);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool UpdateOption(PollOption info)
        {
            bool result = false;
            try
            {
                result = WritePollOption(ProceduresNames.PollOptionUpdate, info, false);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool DeleteOptionByID(int ID)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.PollOptionDelete, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, PollOption.CommonColumns.ID), ID);

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

        public bool DeleteOptionByPollID(int pollID)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.PollOptionDeleteByPollID, this.Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, PollOption.TableColumns.PollID), pollID);

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

        public PollOption SelectOptionByID(int ID, bool? IsArabic)
        {
            PollOption info = null;
            try
            {
                info = GetOption(ProceduresNames.PollOptionSelectByID, ID, PollOption.CommonColumns.ID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public List<PollOption> SelectOptionsByPollID(int pollID, bool? IsArabic)
        {
            List<PollOption> infoList = null;
            try
            {
                infoList = GetOptionList(ProceduresNames.PollOptionSelectByID, pollID, PollOption.TableColumns.PollID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        #endregion

        #region Utility Methods

        private Poll GetPoll(string procedureName, object parameter, string parameterName, bool? IsArabic)
        {
            Poll info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parameter != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameterName), parameter);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.IsArabic), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadPoll(reader, IsArabic);

                    if (reader.NextResult())
                    {
                        ReadOptionsList(reader, info.Options, IsArabic);
                    }
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

        private List<Poll> GetPollList(string procedureName, object parameter, string parameterName, bool? IsArabic)
        {
            List<Poll> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.IsArabic), DBNull.Value);

                if (parameter != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameterName), parameter);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Poll>();

                    ReadPollList(reader, infoList, IsArabic);
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

        private PollOption GetOption(string procedureName, object parameter, string parameterName, bool? IsArabic)
        {
            PollOption info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parameter != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameterName), parameter);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.IsArabic), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadOption(reader, IsArabic);
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

        private List<PollOption> GetOptionList(string procedureName, object parameter, string parameterName, bool? IsArabic)
        {
            List<PollOption> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.IsArabic), DBNull.Value);

                if (parameter != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameterName), parameter);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<PollOption>();

                    ReadOptionsList(reader, infoList, IsArabic);
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

        private Poll ReadPoll(SqlDataReader reader, bool? IsArabic)
        {
            Poll info = null;
            try
            {
                reader.Read();

                info = new Poll();

                info.ID = Convert.ToInt32(reader[Poll.CommonColumns.ID]);
                info.TotalVotes = Convert.ToInt32(reader[Poll.TableColumns.TotalVotes]);
                info.IsNew = Convert.ToBoolean(reader[Poll.TableColumns.IsNew]);
                info.IsCurrent = Convert.ToBoolean(reader[Poll.TableColumns.IsCurrent]);
                info.IsArchived = Convert.ToBoolean(reader[Poll.TableColumns.IsArchived]);
                info.CreationDate = Convert.ToDateTime(reader[Poll.CommonColumns.CreationDate]);

                if (reader[Poll.TableColumns.CurrentDate] != DBNull.Value)
                    info.CurrentDate = Convert.ToDateTime(reader[Poll.TableColumns.CurrentDate]);
                else
                    info.CurrentDate = null;

                if (reader[Poll.TableColumns.ArchiveDate] != DBNull.Value)
                    info.ArchiveDate = Convert.ToDateTime(reader[Poll.TableColumns.ArchiveDate]);
                else
                    info.ArchiveDate = null;

                if (!IsArabic.HasValue)
                {
                    info.TitleAr = Convert.ToString(reader[Poll.TableColumns.TitleAr]);
                    info.TitleEn = Convert.ToString(reader[Poll.TableColumns.TitleEn]);

                    if (reader[Poll.CommonColumns.CreatedBy] != DBNull.Value)
                        info.CreatedBy = (Guid)reader[Poll.CommonColumns.CreatedBy];
                    else
                        info.CreatedBy = null;

                    if (reader[Poll.TableColumns.CurrentBy] != DBNull.Value)
                        info.CurrentBy = (Guid)reader[Poll.TableColumns.CurrentBy];
                    else
                        info.CurrentBy = null;

                    if (reader[Poll.TableColumns.ArchivedBy] != DBNull.Value)
                        info.ArchivedBy = (Guid)reader[Poll.TableColumns.ArchivedBy];
                    else
                        info.ArchivedBy = null;

                    if (reader[Poll.CommonColumns.ModificationDate] != DBNull.Value)
                        info.ModificationDate = Convert.ToDateTime(reader[Poll.CommonColumns.ModificationDate]);
                    else
                        info.ModificationDate = null;

                    if (reader[Poll.CommonColumns.ModifiedBy] != DBNull.Value)
                        info.ModifiedBy = (Guid)reader[Poll.CommonColumns.ModifiedBy];
                    else
                        info.ModifiedBy = null;
                }
                else
                {
                    if (IsArabic.Value)
                    {
                        info.TitleAr = Convert.ToString(reader[Poll.TableColumns.TitleAr]);
                    }
                    else
                    {
                        info.TitleEn = Convert.ToString(reader[Poll.TableColumns.TitleEn]);
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        private void ReadPollList(SqlDataReader reader, List<Poll> infoList, bool? IsArabic)
        {
            try
            {
                Poll info = null;

                if (!IsArabic.HasValue)
                {
                    while (reader.Read())
                    {
                        info = new Poll();

                        info.ID = Convert.ToInt32(reader[Poll.CommonColumns.ID]);
                        info.TitleAr = Convert.ToString(reader[Poll.TableColumns.TitleAr]);
                        info.TitleEn = Convert.ToString(reader[Poll.TableColumns.TitleEn]);
                        info.TotalVotes = Convert.ToInt32(reader[Poll.TableColumns.TotalVotes]);
                        info.IsNew = Convert.ToBoolean(reader[Poll.TableColumns.IsNew]);
                        info.IsCurrent = Convert.ToBoolean(reader[Poll.TableColumns.IsCurrent]);
                        info.IsArchived = Convert.ToBoolean(reader[Poll.TableColumns.IsArchived]);
                        info.CreationDate = Convert.ToDateTime(reader[Poll.CommonColumns.CreationDate]);

                        if (reader[Poll.CommonColumns.CreatedBy] != DBNull.Value)
                            info.CreatedBy = (Guid)reader[Poll.CommonColumns.CreatedBy];
                        else
                            info.CreatedBy = null;

                        if (reader[Poll.TableColumns.CurrentBy] != DBNull.Value)
                            info.CurrentBy = (Guid)reader[Poll.TableColumns.CurrentBy];
                        else
                            info.CurrentBy = null;

                        if (reader[Poll.TableColumns.CurrentDate] != DBNull.Value)
                            info.CurrentDate = Convert.ToDateTime(reader[Poll.TableColumns.CurrentDate]);
                        else
                            info.CurrentDate = null;

                        if (reader[Poll.TableColumns.ArchiveDate] != DBNull.Value)
                            info.ArchiveDate = Convert.ToDateTime(reader[Poll.TableColumns.ArchiveDate]);
                        else
                            info.ArchiveDate = null;

                        if (reader[Poll.TableColumns.ArchivedBy] != DBNull.Value)
                            info.ArchivedBy = (Guid)reader[Poll.TableColumns.ArchivedBy];
                        else
                            info.ArchivedBy = null;

                        if (reader[Poll.CommonColumns.ModificationDate] != DBNull.Value)
                            info.ModificationDate = Convert.ToDateTime(reader[Poll.CommonColumns.ModificationDate]);
                        else
                            info.ModificationDate = null;

                        if (reader[Poll.CommonColumns.ModifiedBy] != DBNull.Value)
                            info.ModifiedBy = (Guid)reader[Poll.CommonColumns.ModifiedBy];
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
                            info = new Poll();

                            info.ID = Convert.ToInt32(reader[Poll.CommonColumns.ID]);
                            info.TitleAr = Convert.ToString(reader[Poll.TableColumns.TitleAr]);
                            info.TotalVotes = Convert.ToInt32(reader[Poll.TableColumns.TotalVotes]);
                            info.IsNew = Convert.ToBoolean(reader[Poll.TableColumns.IsNew]);
                            info.IsCurrent = Convert.ToBoolean(reader[Poll.TableColumns.IsCurrent]);
                            info.IsArchived = Convert.ToBoolean(reader[Poll.TableColumns.IsArchived]);
                            info.CreationDate = Convert.ToDateTime(reader[Poll.CommonColumns.CreationDate]);

                            if (reader[Poll.TableColumns.CurrentDate] != DBNull.Value)
                                info.CurrentDate = Convert.ToDateTime(reader[Poll.TableColumns.CurrentDate]);
                            else
                                info.CurrentDate = null;

                            if (reader[Poll.TableColumns.ArchiveDate] != DBNull.Value)
                                info.ArchiveDate = Convert.ToDateTime(reader[Poll.TableColumns.ArchiveDate]);
                            else
                                info.ArchiveDate = null;

                            infoList.Add(info);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            info = new Poll();

                            info.ID = Convert.ToInt32(reader[Poll.CommonColumns.ID]);
                            info.TitleEn = Convert.ToString(reader[Poll.TableColumns.TitleEn]);
                            info.TotalVotes = Convert.ToInt32(reader[Poll.TableColumns.TotalVotes]);
                            info.IsNew = Convert.ToBoolean(reader[Poll.TableColumns.IsNew]);
                            info.IsCurrent = Convert.ToBoolean(reader[Poll.TableColumns.IsCurrent]);
                            info.IsArchived = Convert.ToBoolean(reader[Poll.TableColumns.IsArchived]);
                            info.CreationDate = Convert.ToDateTime(reader[Poll.CommonColumns.CreationDate]);

                            if (reader[Poll.TableColumns.CurrentDate] != DBNull.Value)
                                info.CurrentDate = Convert.ToDateTime(reader[Poll.TableColumns.CurrentDate]);
                            else
                                info.CurrentDate = null;

                            if (reader[Poll.TableColumns.ArchiveDate] != DBNull.Value)
                                info.ArchiveDate = Convert.ToDateTime(reader[Poll.TableColumns.ArchiveDate]);
                            else
                                info.ArchiveDate = null;

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

        private PollOption ReadOption(SqlDataReader reader, bool? IsArabic)
        {
            PollOption option = null;
            try
            {
                reader.Read();

                option.ID = Convert.ToInt32(reader[PollOption.CommonColumns.ID]);
                option.PollID = Convert.ToInt32(reader[PollOption.TableColumns.PollID]);
                option.Votes = Convert.ToInt32(reader[PollOption.TableColumns.Votes]);
                option.Percentage = Convert.ToDecimal(reader[PollOption.TableColumns.Percentage]);

                if (!IsArabic.HasValue)
                {
                    option.TextAr = Convert.ToString(reader[PollOption.TableColumns.TextAr]);
                    option.TextEn = Convert.ToString(reader[PollOption.TableColumns.TextEn]);
                }
                else
                {
                    if (IsArabic.Value)
                    {
                        option.TextAr = Convert.ToString(reader[PollOption.TableColumns.TextAr]);
                    }
                    else
                    {
                        option.TextEn = Convert.ToString(reader[PollOption.TableColumns.TextEn]);
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return option;
        }

        private void ReadOptionsList(SqlDataReader reader, List<PollOption> infoList, bool? IsArabic)
        {
            try
            {
                PollOption option = null;

                if (!IsArabic.HasValue)
                {
                    while (reader.Read())
                    {
                        option = new PollOption();

                        option.ID = Convert.ToInt32(reader[PollOption.CommonColumns.ID]);
                        option.PollID = Convert.ToInt32(reader[PollOption.TableColumns.PollID]);
                        option.TextAr = Convert.ToString(reader[PollOption.TableColumns.TextAr]);
                        option.TextEn = Convert.ToString(reader[PollOption.TableColumns.TextEn]);
                        option.Votes = Convert.ToInt32(reader[PollOption.TableColumns.Votes]);
                        option.Percentage = Convert.ToDecimal(reader[PollOption.TableColumns.Percentage]);

                        infoList.Add(option);
                    }
                }
                else
                {
                    if (IsArabic.Value)
                    {
                        while (reader.Read())
                        {
                            option = new PollOption();

                            option.ID = Convert.ToInt32(reader[PollOption.CommonColumns.ID]);
                            option.PollID = Convert.ToInt32(reader[PollOption.TableColumns.PollID]);
                            option.TextAr = Convert.ToString(reader[PollOption.TableColumns.TextAr]);
                            option.Votes = Convert.ToInt32(reader[PollOption.TableColumns.Votes]);
                            option.Percentage = Convert.ToDecimal(reader[PollOption.TableColumns.Percentage]);

                            infoList.Add(option);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            option = new PollOption();

                            option.ID = Convert.ToInt32(reader[PollOption.CommonColumns.ID]);
                            option.PollID = Convert.ToInt32(reader[PollOption.TableColumns.PollID]);
                            option.TextEn = Convert.ToString(reader[PollOption.TableColumns.TextEn]);
                            option.Votes = Convert.ToInt32(reader[PollOption.TableColumns.Votes]);
                            option.Percentage = Convert.ToDecimal(reader[PollOption.TableColumns.Percentage]);

                            infoList.Add(option);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        private bool WritePoll(string ProcedureName, Poll info, bool IsNew)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProcedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.TableColumns.TitleAr), info.TitleAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.TableColumns.TitleEn), info.TitleEn);

                if (IsNew)
                {
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.ID), SqlDbType.Int);
                    command.Parameters[string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.ID)].Direction = ParameterDirection.Output;

                    if (info.CreatedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.CreatedBy), info.CreatedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.CreatedBy), DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.ID), info.ID);

                    if (info.ModifiedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.ModifiedBy), info.ModifiedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.ModifiedBy), DBNull.Value);
                }


                this.OpenConnection();

                command.ExecuteNonQuery();

                if (IsNew)
                {
                    info.ID = Convert.ToInt32(command.Parameters[string.Concat(CommonStrings.AtSymbol, Poll.CommonColumns.ID)].Value);
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

        private bool WritePollOption(string ProcedureName, PollOption info, bool IsNew)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProcedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, PollOption.TableColumns.TextAr), info.TextAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, PollOption.TableColumns.TextEn), info.TextEn);

                if (IsNew)
                {
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, PollOption.CommonColumns.ID), SqlDbType.Int);
                    command.Parameters[string.Concat(CommonStrings.AtSymbol, PollOption.CommonColumns.ID)].Direction = ParameterDirection.Output;

                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, PollOption.TableColumns.PollID), info.PollID);
                }
                else
                {
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, PollOption.CommonColumns.ID), info.ID);
                }

                this.OpenConnection();

                command.ExecuteNonQuery();

                if (IsNew)
                {
                    info.ID = Convert.ToInt32(command.Parameters[string.Concat(CommonStrings.AtSymbol, PollOption.CommonColumns.ID)].Value);
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