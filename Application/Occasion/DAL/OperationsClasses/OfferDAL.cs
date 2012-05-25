using System;

using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

using System.Collections;
using System.Collections.Generic;

using EntityLayer.Entities;
using Common.StringsClasses;
using DAL.Resources;

namespace DAL.OperationsClasses
{
    public class OfferDAL : DataManagment
    {
        #region Operations

        public List<Offer> Search(string SearchWord, bool ByArabic, bool? IsArabic)
        {
            List<Offer> infoList = null;
            try
            {
                List<KeyValue> paramsList = new List<KeyValue>();
                paramsList.Add(new KeyValue("ByArabic", ByArabic));
                paramsList.Add(new KeyValue("SearchWord", SearchWord));

                infoList = GetOffersList(paramsList, ProceduresNames.OfferSimpleSearch, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> Search(string SearchWord, ArrayList categoryList, bool ByArabic, bool? IsArabic)
        {
            List<Offer> infoList = null;
            SqlDataReader reader = null;
            try
            {
                List<SqlDataRecord> categoryIDList = new List<SqlDataRecord>();
                SqlMetaData[] tvpDefinition = { new SqlMetaData("IntID", SqlDbType.Int) };
                SqlDataRecord record = null;
                foreach (int categoryID in categoryList)
                {
                    record = new SqlDataRecord(tvpDefinition);
                    record.SetInt32(0, categoryID);
                    categoryIDList.Add(record);
                }

                SqlCommand command = new SqlCommand(ProceduresNames.OfferSimpleSearchByCat, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SearchWord", SearchWord);
                command.Parameters.AddWithValue("@ByArabic", ByArabic);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                command.Parameters.Add("@CategoryList", SqlDbType.Structured);
                command.Parameters["@CategoryList"].Direction = ParameterDirection.Input;
                command.Parameters["@CategoryList"].TypeName = "IntList";
                command.Parameters["@CategoryList"].Value = categoryIDList;

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Offer>();

                    ReadOffersList(reader, infoList, IsArabic);
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

        public Offer SelectByID(int ID, bool? IsArabic)
        {
            Offer info = null;
            try
            {
                info = GetOffer(new KeyValue(Offer.CommonColumns.ID, ID), ProceduresNames.OfferSelectByID, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        public List<Offer> SelectAll(string offerType, bool? IsArabic, bool? IsActive)
        {
            List<Offer> infoList = null;
            try
            {
                List<KeyValue> paramsList = new List<KeyValue>();

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        paramsList.Add(new KeyValue("AllType", true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, DBNull.Value));
                    }
                    else if (offerType == "IsProduct")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsPackage")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsSale")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, true));
                    }
                }

                infoList = GetOffersList(paramsList, ProceduresNames.OfferSelectAll, IsArabic, IsActive);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> SelectByCategoryID(ArrayList categoryList, string offerType, bool? IsArabic, bool? IsActive)
        {
            List<Offer> infoList = null;
            SqlDataReader reader = null;
            try
            {
                List<SqlDataRecord> categoryIDList = new List<SqlDataRecord>();
                SqlMetaData[] tvpDefinition = { new SqlMetaData("IntID", SqlDbType.Int) };
                SqlDataRecord record = null;
                foreach (int categoryID in categoryList)
                {
                    record = new SqlDataRecord(tvpDefinition);
                    record.SetInt32(0, categoryID);
                    categoryIDList.Add(record);
                }

                SqlCommand command = new SqlCommand(ProceduresNames.OfferSelectByCatID, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        command.Parameters.AddWithValue("@AllType", true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), DBNull.Value);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), DBNull.Value);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), DBNull.Value);
                    }
                    else if (offerType == "IsProduct")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), false);
                    }
                    else if (offerType == "IsPackage")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), false);
                    }
                    else if (offerType == "IsSale")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), true);
                    }
                }

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                if (IsActive.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), IsActive.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), DBNull.Value);

                command.Parameters.Add("@CategoryList", SqlDbType.Structured);
                command.Parameters["@CategoryList"].Direction = ParameterDirection.Input;
                command.Parameters["@CategoryList"].TypeName = "IntList";
                command.Parameters["@CategoryList"].Value = categoryIDList;

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Offer>();

                    ReadOffersList(reader, infoList, IsArabic);
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

        public List<Offer> SelectByCategoryID_SupplierID(ArrayList categoryList, int supplierID, string offerType, bool? IsArabic, bool? IsActive)
        {
            List<Offer> infoList = null;
            SqlDataReader reader = null;
            try
            {
                List<SqlDataRecord> categoryIDList = new List<SqlDataRecord>();
                SqlMetaData[] tvpDefinition = { new SqlMetaData("IntID", SqlDbType.Int) };
                SqlDataRecord record = null;
                foreach (int categoryID in categoryList)
                {
                    record = new SqlDataRecord(tvpDefinition);
                    record.SetInt32(0, categoryID);
                    categoryIDList.Add(record);
                }

                SqlCommand command = new SqlCommand(ProceduresNames.OfferSelectByCatIDSuppID, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.SupplierID), supplierID);

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        command.Parameters.AddWithValue("@AllType", true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), DBNull.Value);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), DBNull.Value);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), DBNull.Value);
                    }
                    else if (offerType == "IsProduct")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), false);
                    }
                    else if (offerType == "IsPackage")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), false);
                    }
                    else if (offerType == "IsSale")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), true);
                    }
                }

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                if (IsActive.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), IsActive.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), DBNull.Value);

                command.Parameters.Add("@CategoryList", SqlDbType.Structured);
                command.Parameters["@CategoryList"].Direction = ParameterDirection.Input;
                command.Parameters["@CategoryList"].TypeName = "IntList";
                command.Parameters["@CategoryList"].Value = categoryIDList;

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Offer>();

                    ReadOffersList(reader, infoList, IsArabic);
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

        public List<Offer> SelectByCategoryID_BrandID(ArrayList categoryList, int brandID, string offerType, bool? IsArabic, bool? IsActive)
        {
            List<Offer> infoList = null;
            SqlDataReader reader = null;
            try
            {
                List<SqlDataRecord> categoryIDList = new List<SqlDataRecord>();
                SqlMetaData[] tvpDefinition = { new SqlMetaData("IntID", SqlDbType.Int) };
                SqlDataRecord record = null;
                foreach (int categoryID in categoryList)
                {
                    record = new SqlDataRecord(tvpDefinition);
                    record.SetInt32(0, categoryID);
                    categoryIDList.Add(record);
                }

                SqlCommand command = new SqlCommand(ProceduresNames.OfferSelectByCatIDBrandID, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.BrandID), brandID);

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        command.Parameters.AddWithValue("@AllType", true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), DBNull.Value);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), DBNull.Value);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), DBNull.Value);
                    }
                    else if (offerType == "IsProduct")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), false);
                    }
                    else if (offerType == "IsPackage")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), false);
                    }
                    else if (offerType == "IsSale")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), true);
                    }
                }

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                if (IsActive.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), IsActive.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), DBNull.Value);

                command.Parameters.Add("@CategoryList", SqlDbType.Structured);
                command.Parameters["@CategoryList"].Direction = ParameterDirection.Input;
                command.Parameters["@CategoryList"].TypeName = "IntList";
                command.Parameters["@CategoryList"].Value = categoryIDList;

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Offer>();

                    ReadOffersList(reader, infoList, IsArabic);
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

        public List<Offer> SelectByCategoryID_SupplierID_BrandID(ArrayList categoryList, int supplierID, int brandID, string offerType, bool? IsArabic, bool? IsActive)
        {
            List<Offer> infoList = null;
            SqlDataReader reader = null;
            try
            {
                List<SqlDataRecord> categoryIDList = new List<SqlDataRecord>();
                SqlMetaData[] tvpDefinition = { new SqlMetaData("IntID", SqlDbType.Int) };
                SqlDataRecord record = null;
                foreach (int categoryID in categoryList)
                {
                    record = new SqlDataRecord(tvpDefinition);
                    record.SetInt32(0, categoryID);
                    categoryIDList.Add(record);
                }

                SqlCommand command = new SqlCommand(ProceduresNames.OfferSelectByCatIDSuppIDBrandID, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.SupplierID), supplierID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.BrandID), brandID);

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        command.Parameters.AddWithValue("@AllType", true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), DBNull.Value);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), DBNull.Value);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), DBNull.Value);
                    }
                    else if (offerType == "IsProduct")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), false);
                    }
                    else if (offerType == "IsPackage")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), true);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), false);
                    }
                    else if (offerType == "IsSale")
                    {
                        command.Parameters.AddWithValue("@AllType", false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), false);
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), true);
                    }
                }

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                if (IsActive.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), IsActive.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), DBNull.Value);

                command.Parameters.Add("@CategoryList", SqlDbType.Structured);
                command.Parameters["@CategoryList"].Direction = ParameterDirection.Input;
                command.Parameters["@CategoryList"].TypeName = "IntList";
                command.Parameters["@CategoryList"].Value = categoryIDList;

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Offer>();

                    ReadOffersList(reader, infoList, IsArabic);
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

        public List<Offer> SelectByBrandID(int brandID, string offerType, bool? IsArabic, bool? IsActive)
        {
            List<Offer> infoList = null;
            try
            {
                List<KeyValue> paramsList = new List<KeyValue>();

                paramsList.Add(new KeyValue(Offer.TableColumns.BrandID, brandID));

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        paramsList.Add(new KeyValue("AllType", true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, DBNull.Value));
                    }
                    else if (offerType == "IsProduct")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsPackage")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsSale")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, true));
                    }
                }

                infoList = GetOffersList(paramsList, ProceduresNames.OfferSelectByBrandID, IsArabic, IsActive);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> SelectBySupplierID(int supplierID, string offerType, bool? IsArabic, bool? IsActive)
        {
            List<Offer> infoList = null;
            try
            {
                List<KeyValue> paramsList = new List<KeyValue>();

                paramsList.Add(new KeyValue(Offer.TableColumns.SupplierID, supplierID));

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        paramsList.Add(new KeyValue("AllType", true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, DBNull.Value));
                    }
                    else if (offerType == "IsProduct")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsPackage")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsSale")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, true));
                    }
                }

                infoList = GetOffersList(paramsList, ProceduresNames.OfferSelectBySuppID, IsArabic, IsActive);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> SelectBySupplierID_BrandID(int supplierID, int brandID, string offerType, bool? IsArabic, bool? IsActive)
        {
            List<Offer> infoList = null;
            try
            {
                List<KeyValue> paramsList = new List<KeyValue>();
                paramsList.Add(new KeyValue(Offer.TableColumns.SupplierID, supplierID));
                paramsList.Add(new KeyValue(Offer.TableColumns.BrandID, brandID));

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        paramsList.Add(new KeyValue("AllType", true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, DBNull.Value));
                    }
                    else if (offerType == "IsProduct")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsPackage")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsSale")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, true));
                    }
                }

                infoList = GetOffersList(paramsList, ProceduresNames.OfferSelectBySuppIDBrandID, IsArabic, IsActive);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> SelectBestDeals(string offerType, bool? IsArabic)
        {
            List<Offer> infoList = null;
            try
            {
                List<KeyValue> paramsList = new List<KeyValue>();

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        paramsList.Add(new KeyValue("AllType", true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, DBNull.Value));
                    }
                    else if (offerType == "IsProduct")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsPackage")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsSale")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, true));
                    }
                }

                infoList = GetOffersList(paramsList, ProceduresNames.OfferSelectBestDeal, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> SelectFeatured(string offerType, bool? IsArabic)
        {
            List<Offer> infoList = null;
            try
            {
                List<KeyValue> paramsList = new List<KeyValue>();

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        paramsList.Add(new KeyValue("AllType", true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, DBNull.Value));
                    }
                    else if (offerType == "IsProduct")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsPackage")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsSale")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, true));
                    }
                }

                infoList = GetOffersList(paramsList, ProceduresNames.OfferSelectFeatured, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> SelectMostLiked(string offerType, bool? IsArabic)
        {
            List<Offer> infoList = null;
            try
            {
                List<KeyValue> paramsList = new List<KeyValue>();

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        paramsList.Add(new KeyValue("AllType", true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, DBNull.Value));
                    }
                    else if (offerType == "IsProduct")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsPackage")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsSale")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, true));
                    }
                }

                infoList = GetOffersList(paramsList, ProceduresNames.OfferSelectMostLiked, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> SelectMostViewed(string offerType, bool? IsArabic)
        {
            List<Offer> infoList = null;
            try
            {
                List<KeyValue> paramsList = new List<KeyValue>();

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        paramsList.Add(new KeyValue("AllType", true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, DBNull.Value));
                    }
                    else if (offerType == "IsProduct")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsPackage")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsSale")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, true));
                    }
                }

                infoList = GetOffersList(paramsList, ProceduresNames.OfferSelectMostViewed, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> SelectNewPackages(bool? IsArabic)
        {
            List<Offer> infoList = null;
            try
            {
                KeyValue parameter = null;
                infoList = GetOffersList(parameter, ProceduresNames.OfferSelectNewPackage, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> SelectNewProducts(bool? IsArabic)
        {
            List<Offer> infoList = null;
            try
            {
                KeyValue parameter = null;
                infoList = GetOffersList(parameter, ProceduresNames.OfferSelectNewProduct, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> SelectNewSales(bool? IsArabic)
        {
            List<Offer> infoList = null;
            try
            {
                KeyValue parameter = null;
                infoList = GetOffersList(parameter, ProceduresNames.OfferSelectNewSale, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public List<Offer> SelectTopRated(string offerType, bool? IsArabic)
        {
            List<Offer> infoList = null;
            try
            {
                List<KeyValue> paramsList = new List<KeyValue>();

                if (!string.IsNullOrEmpty(offerType))
                {
                    if (offerType == "All")
                    {
                        paramsList.Add(new KeyValue("AllType", true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, DBNull.Value));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, DBNull.Value));
                    }
                    else if (offerType == "IsProduct")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsPackage")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, true));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, false));
                    }
                    else if (offerType == "IsSale")
                    {
                        paramsList.Add(new KeyValue("AllType", false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsProduct, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsPackage, false));
                        paramsList.Add(new KeyValue(Offer.TableColumns.IsSale, true));
                    }
                }

                infoList = GetOffersList(paramsList, ProceduresNames.OfferSelectTopRated, IsArabic);
            }
            catch (Exception error)
            {
                throw error;
            }
            return infoList;
        }

        public bool Add(Offer info)
        {
            bool result = false;
            try
            {
                result = WriteOffer(ProceduresNames.OfferAdd, info, true);
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        public bool Update(Offer info)
        {
            bool result = false;
            try
            {
                result = WriteOffer(ProceduresNames.OfferUpdate, info, false);
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
                SqlCommand command = new SqlCommand(ProceduresNames.OfferDelete, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID), ID);

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
                SqlCommand command = new SqlCommand(ProceduresNames.OfferActivate, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID), ID);

                if (activatedBy.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.ActivatedBy), activatedBy.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.ActivatedBy), DBNull.Value);

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
                SqlCommand command = new SqlCommand(ProceduresNames.OfferDeactivate, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID), ID);

                if (deactivatedBy.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.DeactivatedBy), deactivatedBy.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.DeactivatedBy), DBNull.Value);

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
                SqlCommand command = new SqlCommand(ProceduresNames.OfferLike, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID), ID);

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

        public bool View(int ID)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.OfferView, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID), ID);

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
                SqlCommand command = new SqlCommand(ProceduresNames.OfferRate, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID), ID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.Value), rateValue);

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

        public bool LinkToBranch(int offerID, int branchID)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.OfferBranchAdd, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.OfferID), offerID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.BranchID), branchID);

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

        public bool BreakBranchLink(int offerID, int branchID)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.OfferBranchDelete, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.OfferID), offerID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Branch.CommonColumns.BranchID), branchID);

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

        public bool SetBestDeal(int ID, bool value)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.OfferSetBest, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID), ID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.Value), value);

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

        public bool SetFeatured(int ID, bool value)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProceduresNames.OfferSetFeatured, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID), ID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.Value), value);

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

        private Offer GetOffer(KeyValue parameter, string procedureName, bool? IsArabic)
        {
            Offer info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parameter != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameter.Key), parameter.Value);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadOffer(reader, IsArabic);
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

        private Offer GetOffer(KeyValue parameter, string procedureName, bool? IsArabic, bool? IsActive)
        {
            Offer info = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parameter != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameter.Key), parameter.Value);

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                if (IsActive.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), IsActive.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadOffer(reader, IsArabic);
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

        private Offer GetOffer(List<KeyValue> parametersList, string procedureName, bool? IsArabic)
        {
            Offer info = null;
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

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadOffer(reader, IsArabic);
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

        private Offer GetOffer(List<KeyValue> parametersList, string procedureName, bool? IsArabic, bool? IsActive)
        {
            Offer info = null;
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

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                if (IsActive.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), IsActive.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), DBNull.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    info = ReadOffer(reader, IsArabic);
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

        private List<Offer> GetOffersList(KeyValue parameter, string procedureName, bool? IsArabic)
        {
            List<Offer> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                if (parameter != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameter.Key), parameter.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Offer>();

                    ReadOffersList(reader, infoList, IsArabic);
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

        private List<Offer> GetOffersList(KeyValue parameter, string procedureName, bool? IsArabic, bool? IsActive)
        {
            List<Offer> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                if (IsActive.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), IsActive.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), DBNull.Value);

                if (parameter != null)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameter.Key), parameter.Value);

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Offer>();

                    ReadOffersList(reader, infoList, IsArabic);
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

        private List<Offer> GetOffersList(List<KeyValue> parametersList, string procedureName, bool? IsArabic)
        {
            List<Offer> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                if (parametersList != null && parametersList.Count > 0)
                {
                    foreach (KeyValue parameter in parametersList)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameter.Key), parameter.Value);
                }

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Offer>();

                    ReadOffersList(reader, infoList, IsArabic);
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

        private List<Offer> GetOffersList(List<KeyValue> parametersList, string procedureName, bool? IsArabic, bool? IsActive)
        {
            List<Offer> infoList = null;
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(procedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                if (IsArabic.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), IsArabic.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.IsArabic), DBNull.Value);

                if (IsActive.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), IsActive.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsActive), DBNull.Value);

                if (parametersList != null && parametersList.Count > 0)
                {
                    foreach (KeyValue parameter in parametersList)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, parameter.Key), parameter.Value);
                }

                this.OpenConnection();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    infoList = new List<Offer>();

                    ReadOffersList(reader, infoList, IsArabic);
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

        private Offer ReadOffer(SqlDataReader reader, bool? IsArabic)
        {
            Offer info = null;
            try
            {
                reader.Read();

                info = new Offer();

                info.ID = Convert.ToInt32(reader[Offer.CommonColumns.ID]);
                info.CategoryID = Convert.ToInt32(reader[Offer.TableColumns.CategoryID]);
                info.SupplierID = Convert.ToInt32(reader[Offer.TableColumns.SupplierID]);
                info.CurrencyInfo.ID = Convert.ToInt32(reader[Offer.TableColumns.CurrencyID]);
                info.Image = Convert.ToString(reader[Offer.TableColumns.Image]);
                info.IsProduct = Convert.ToBoolean(reader[Offer.TableColumns.IsProduct]);
                info.IsSale = Convert.ToBoolean(reader[Offer.TableColumns.IsSale]);
                info.IsPackage = Convert.ToBoolean(reader[Offer.TableColumns.IsPackage]);
                info.IsBestDeal = Convert.ToBoolean(reader[Offer.TableColumns.IsBestDeal]);
                info.IsFeaturedOffer = Convert.ToBoolean(reader[Offer.TableColumns.IsFeaturedOffer]);
                info.Rate = Convert.ToInt32(reader[Offer.TableColumns.Rate]);
                info.RateCount = Convert.ToInt32(reader[Offer.TableColumns.RateCount]);
                info.RateTotal = Convert.ToInt32(reader[Offer.TableColumns.RateTotal]);
                info.Likes = Convert.ToInt32(reader[Offer.TableColumns.Likes]);
                info.Views = Convert.ToInt32(reader[Offer.TableColumns.Views]);
                info.IsActive = Convert.ToBoolean(reader[Offer.TableColumns.IsActive]);
                info.CreationDate = Convert.ToDateTime(reader[Offer.CommonColumns.CreationDate]);

                if (reader[Offer.TableColumns.BrandID] != DBNull.Value)
                    info.BrandID = Convert.ToInt32(reader[Offer.TableColumns.BrandID]);
                else
                    info.BrandID = null;

                if (reader[Offer.TableColumns.StartDate] != DBNull.Value)
                    info.StartDate = Convert.ToDateTime(reader[Offer.TableColumns.StartDate]);
                else
                    info.StartDate = null;

                if (reader[Offer.TableColumns.EndDate] != DBNull.Value)
                    info.EndDate = Convert.ToDateTime(reader[Offer.TableColumns.EndDate]);
                else
                    info.EndDate = null;

                if (reader[Offer.TableColumns.OldPrice] != DBNull.Value)
                    info.OldPrice = Convert.ToDecimal(reader[Offer.TableColumns.OldPrice]);
                else
                    info.OldPrice = null;

                if (reader[Offer.TableColumns.NewPrice] != DBNull.Value)
                    info.NewPrice = Convert.ToDecimal(reader[Offer.TableColumns.NewPrice]);
                else
                    info.NewPrice = null;

                if (reader[Offer.TableColumns.DiscountPercentage] != DBNull.Value)
                    info.DiscountPercentage = Convert.ToInt32(reader[Offer.TableColumns.DiscountPercentage]);
                else
                    info.DiscountPercentage = null;

                if (reader[Offer.TableColumns.SaleUpTo] != DBNull.Value)
                    info.SaleUpTo = Convert.ToInt32(reader[Offer.TableColumns.SaleUpTo]);
                else
                    info.SaleUpTo = null;

                if (!IsArabic.HasValue)
                {
                    info.NameAr = Convert.ToString(reader[Offer.TableColumns.NameAr]);
                    info.NameEn = Convert.ToString(reader[Offer.TableColumns.NameEn]);
                    info.TitleAr = Convert.ToString(reader[Offer.TableColumns.TitleAr]);
                    info.TitleEn = Convert.ToString(reader[Offer.TableColumns.TitleEn]);
                    info.ShortDescriptionAr = Convert.ToString(reader[Offer.TableColumns.ShortDescriptionAr]);
                    info.ShortDescriptionEn = Convert.ToString(reader[Offer.TableColumns.ShortDescriptionEn]);                    

                    if (reader[Offer.TableColumns.PackageDescriptionAr] != DBNull.Value)
                        info.PackageDescriptionAr = Convert.ToString(reader[Offer.TableColumns.PackageDescriptionAr]);

                    if (reader[Offer.TableColumns.PackageDescriptionEn] != DBNull.Value)
                        info.PackageDescriptionEn = Convert.ToString(reader[Offer.TableColumns.PackageDescriptionEn]);

                    if (reader[Offer.TableColumns.DescriptionAr] != DBNull.Value)
                        info.DescriptionAr = Convert.ToString(reader[Offer.TableColumns.DescriptionAr]);

                    if (reader[Offer.TableColumns.DescriptionEn] != DBNull.Value)
                        info.DescriptionEn = Convert.ToString(reader[Offer.TableColumns.DescriptionEn]);

                    if (reader[Currency.TableColumns.UnitAr] != DBNull.Value)
                        info.CurrencyInfo.UnitAr = Convert.ToString(reader[Currency.TableColumns.UnitAr]);

                    if (reader[Currency.TableColumns.UnitEn] != DBNull.Value)
                        info.CurrencyInfo.UnitEn = Convert.ToString(reader[Currency.TableColumns.UnitEn]);

                    if (reader[Offer.TableColumns.ActivationDate] != DBNull.Value)
                        info.ActivationDate = Convert.ToDateTime(reader[Offer.TableColumns.ActivationDate]);
                    else
                        info.ActivationDate = null;

                    if (reader[Offer.TableColumns.ActivatedBy] != DBNull.Value)
                        info.ActivatedBy = (Guid)reader[Offer.TableColumns.ActivatedBy];
                    else
                        info.ActivatedBy = null;

                    if (reader[Offer.TableColumns.DeactivationDate] != DBNull.Value)
                        info.DeactivationDate = Convert.ToDateTime(reader[Offer.TableColumns.DeactivationDate]);
                    else
                        info.DeactivationDate = null;

                    if (reader[Offer.TableColumns.DeactivatedBy] != DBNull.Value)
                        info.DeactivatedBy = (Guid)reader[Offer.TableColumns.DeactivatedBy];
                    else
                        info.DeactivatedBy = null;

                    if (reader[Offer.CommonColumns.CreatedBy] != DBNull.Value)
                        info.CreatedBy = (Guid)reader[Offer.CommonColumns.CreatedBy];
                    else
                        info.CreatedBy = null;

                    if (reader[Offer.CommonColumns.ModificationDate] != DBNull.Value)
                        info.ModificationDate = Convert.ToDateTime(reader[Offer.CommonColumns.ModificationDate]);
                    else
                        info.ModificationDate = null;

                    if (reader[Offer.CommonColumns.ModifiedBy] != DBNull.Value)
                        info.ModifiedBy = (Guid)reader[Offer.CommonColumns.ModifiedBy];
                    else
                        info.ModifiedBy = null;
                }
                else
                {
                    if (IsArabic.Value)
                    {
                        info.NameAr = Convert.ToString(reader[Offer.TableColumns.NameAr]);
                        info.TitleAr = Convert.ToString(reader[Offer.TableColumns.TitleAr]);
                        info.ShortDescriptionAr = Convert.ToString(reader[Offer.TableColumns.ShortDescriptionAr]);

                        if (reader[Offer.TableColumns.PackageDescriptionAr] != DBNull.Value)
                            info.PackageDescriptionAr = Convert.ToString(reader[Offer.TableColumns.PackageDescriptionAr]);

                        if (reader[Offer.TableColumns.DescriptionAr] != DBNull.Value)
                            info.DescriptionAr = Convert.ToString(reader[Offer.TableColumns.DescriptionAr]);

                        if (reader[Currency.TableColumns.UnitAr] != DBNull.Value)
                            info.CurrencyInfo.UnitAr = Convert.ToString(reader[Currency.TableColumns.UnitAr]);
                    }
                    else
                    {
                        info.NameEn = Convert.ToString(reader[Offer.TableColumns.NameEn]);
                        info.TitleEn = Convert.ToString(reader[Offer.TableColumns.TitleEn]);
                        info.ShortDescriptionEn = Convert.ToString(reader[Offer.TableColumns.ShortDescriptionEn]);

                        if (reader[Offer.TableColumns.PackageDescriptionEn] != DBNull.Value)
                            info.PackageDescriptionEn = Convert.ToString(reader[Offer.TableColumns.PackageDescriptionEn]);

                        if (reader[Offer.TableColumns.DescriptionEn] != DBNull.Value)
                            info.DescriptionEn = Convert.ToString(reader[Offer.TableColumns.DescriptionEn]);

                        if (reader[Currency.TableColumns.UnitEn] != DBNull.Value)
                            info.CurrencyInfo.UnitEn = Convert.ToString(reader[Currency.TableColumns.UnitEn]);
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return info;
        }

        private void ReadOffersList(SqlDataReader reader, List<Offer> infoList, bool? IsArabic)
        {
            try
            {
                Offer info = null;

                if (!IsArabic.HasValue)
                {
                    while (reader.Read())
                    {
                        info = new Offer();

                        info.ID = Convert.ToInt32(reader[Offer.CommonColumns.ID]);
                        info.CategoryID = Convert.ToInt32(reader[Offer.TableColumns.CategoryID]);
                        info.SupplierID = Convert.ToInt32(reader[Offer.TableColumns.SupplierID]);
                        info.CurrencyInfo.ID = Convert.ToInt32(reader[Offer.TableColumns.CurrencyID]);
                        info.Image = Convert.ToString(reader[Offer.TableColumns.Image]);
                        info.IsProduct = Convert.ToBoolean(reader[Offer.TableColumns.IsProduct]);
                        info.IsSale = Convert.ToBoolean(reader[Offer.TableColumns.IsSale]);
                        info.IsPackage = Convert.ToBoolean(reader[Offer.TableColumns.IsPackage]);
                        info.IsBestDeal = Convert.ToBoolean(reader[Offer.TableColumns.IsBestDeal]);
                        info.IsFeaturedOffer = Convert.ToBoolean(reader[Offer.TableColumns.IsFeaturedOffer]);
                        info.Rate = Convert.ToInt32(reader[Offer.TableColumns.Rate]);
                        info.RateCount = Convert.ToInt32(reader[Offer.TableColumns.RateCount]);
                        info.RateTotal = Convert.ToInt32(reader[Offer.TableColumns.RateTotal]);
                        info.Likes = Convert.ToInt32(reader[Offer.TableColumns.Likes]);
                        info.Views = Convert.ToInt32(reader[Offer.TableColumns.Views]);
                        info.IsActive = Convert.ToBoolean(reader[Offer.TableColumns.IsActive]);
                        info.NameAr = Convert.ToString(reader[Offer.TableColumns.NameAr]);
                        info.NameEn = Convert.ToString(reader[Offer.TableColumns.NameEn]);
                        info.TitleAr = Convert.ToString(reader[Offer.TableColumns.TitleAr]);
                        info.TitleEn = Convert.ToString(reader[Offer.TableColumns.TitleEn]);
                        info.ShortDescriptionAr = Convert.ToString(reader[Offer.TableColumns.ShortDescriptionAr]);
                        info.ShortDescriptionEn = Convert.ToString(reader[Offer.TableColumns.ShortDescriptionEn]);
                        info.CreationDate = Convert.ToDateTime(reader[Offer.CommonColumns.CreationDate]);

                        if (reader[Offer.TableColumns.BrandID] != DBNull.Value)
                            info.BrandID = Convert.ToInt32(reader[Offer.TableColumns.BrandID]);
                        else
                            info.BrandID = null;

                        if (reader[Offer.TableColumns.StartDate] != DBNull.Value)
                            info.StartDate = Convert.ToDateTime(reader[Offer.TableColumns.StartDate]);
                        else
                            info.StartDate = null;

                        if (reader[Offer.TableColumns.EndDate] != DBNull.Value)
                            info.EndDate = Convert.ToDateTime(reader[Offer.TableColumns.EndDate]);
                        else
                            info.EndDate = null;

                        if (reader[Offer.TableColumns.OldPrice] != DBNull.Value)
                            info.OldPrice = Convert.ToDecimal(reader[Offer.TableColumns.OldPrice]);
                        else
                            info.OldPrice = null;

                        if (reader[Offer.TableColumns.NewPrice] != DBNull.Value)
                            info.NewPrice = Convert.ToDecimal(reader[Offer.TableColumns.NewPrice]);
                        else
                            info.NewPrice = null;

                        if (reader[Offer.TableColumns.DiscountPercentage] != DBNull.Value)
                            info.DiscountPercentage = Convert.ToInt32(reader[Offer.TableColumns.DiscountPercentage]);
                        else
                            info.DiscountPercentage = null;

                        if (reader[Offer.TableColumns.SaleUpTo] != DBNull.Value)
                            info.SaleUpTo = Convert.ToInt32(reader[Offer.TableColumns.SaleUpTo]);
                        else
                            info.SaleUpTo = null;

                        if (reader[Offer.TableColumns.PackageDescriptionAr] != DBNull.Value)
                            info.PackageDescriptionAr = Convert.ToString(reader[Offer.TableColumns.PackageDescriptionAr]);

                        if (reader[Offer.TableColumns.PackageDescriptionEn] != DBNull.Value)
                            info.PackageDescriptionEn = Convert.ToString(reader[Offer.TableColumns.PackageDescriptionEn]);

                        if (reader[Offer.TableColumns.DescriptionAr] != DBNull.Value)
                            info.DescriptionAr = Convert.ToString(reader[Offer.TableColumns.DescriptionAr]);

                        if (reader[Offer.TableColumns.DescriptionEn] != DBNull.Value)
                            info.DescriptionEn = Convert.ToString(reader[Offer.TableColumns.DescriptionEn]);

                        if (reader[Currency.TableColumns.UnitAr] != DBNull.Value)
                            info.CurrencyInfo.UnitAr = Convert.ToString(reader[Currency.TableColumns.UnitAr]);

                        if (reader[Currency.TableColumns.UnitEn] != DBNull.Value)
                            info.CurrencyInfo.UnitEn = Convert.ToString(reader[Currency.TableColumns.UnitEn]);

                        if (reader[Offer.TableColumns.ActivationDate] != DBNull.Value)
                            info.ActivationDate = Convert.ToDateTime(reader[Offer.TableColumns.ActivationDate]);
                        else
                            info.ActivationDate = null;

                        if (reader[Offer.TableColumns.ActivatedBy] != DBNull.Value)
                            info.ActivatedBy = (Guid)reader[Offer.TableColumns.ActivatedBy];
                        else
                            info.ActivatedBy = null;

                        if (reader[Offer.TableColumns.DeactivationDate] != DBNull.Value)
                            info.DeactivationDate = Convert.ToDateTime(reader[Offer.TableColumns.DeactivationDate]);
                        else
                            info.DeactivationDate = null;

                        if (reader[Offer.TableColumns.DeactivatedBy] != DBNull.Value)
                            info.DeactivatedBy = (Guid)reader[Offer.TableColumns.DeactivatedBy];
                        else
                            info.DeactivatedBy = null;

                        if (reader[Offer.CommonColumns.CreatedBy] != DBNull.Value)
                            info.CreatedBy = (Guid)reader[Offer.CommonColumns.CreatedBy];
                        else
                            info.CreatedBy = null;

                        if (reader[Offer.CommonColumns.ModificationDate] != DBNull.Value)
                            info.ModificationDate = Convert.ToDateTime(reader[Offer.CommonColumns.ModificationDate]);
                        else
                            info.ModificationDate = null;

                        if (reader[Offer.CommonColumns.ModifiedBy] != DBNull.Value)
                            info.ModifiedBy = (Guid)reader[Offer.CommonColumns.ModifiedBy];
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
                            info = new Offer();

                            info.ID = Convert.ToInt32(reader[Offer.CommonColumns.ID]);
                            info.CategoryID = Convert.ToInt32(reader[Offer.TableColumns.CategoryID]);
                            info.SupplierID = Convert.ToInt32(reader[Offer.TableColumns.SupplierID]);
                            info.CurrencyInfo.ID = Convert.ToInt32(reader[Offer.TableColumns.CurrencyID]);
                            info.Image = Convert.ToString(reader[Offer.TableColumns.Image]);
                            info.IsProduct = Convert.ToBoolean(reader[Offer.TableColumns.IsProduct]);
                            info.IsSale = Convert.ToBoolean(reader[Offer.TableColumns.IsSale]);
                            info.IsPackage = Convert.ToBoolean(reader[Offer.TableColumns.IsPackage]);
                            info.IsBestDeal = Convert.ToBoolean(reader[Offer.TableColumns.IsBestDeal]);
                            info.IsFeaturedOffer = Convert.ToBoolean(reader[Offer.TableColumns.IsFeaturedOffer]);
                            info.Rate = Convert.ToInt32(reader[Offer.TableColumns.Rate]);
                            info.RateCount = Convert.ToInt32(reader[Offer.TableColumns.RateCount]);
                            info.RateTotal = Convert.ToInt32(reader[Offer.TableColumns.RateTotal]);
                            info.Likes = Convert.ToInt32(reader[Offer.TableColumns.Likes]);
                            info.Views = Convert.ToInt32(reader[Offer.TableColumns.Views]);
                            info.IsActive = Convert.ToBoolean(reader[Offer.TableColumns.IsActive]);
                            info.NameAr = Convert.ToString(reader[Offer.TableColumns.NameAr]);
                            info.TitleAr = Convert.ToString(reader[Offer.TableColumns.TitleAr]);
                            info.ShortDescriptionAr = Convert.ToString(reader[Offer.TableColumns.ShortDescriptionAr]);
                            info.CreationDate = Convert.ToDateTime(reader[Offer.CommonColumns.CreationDate]);

                            if (reader[Offer.TableColumns.BrandID] != DBNull.Value)
                                info.BrandID = Convert.ToInt32(reader[Offer.TableColumns.BrandID]);
                            else
                                info.BrandID = null;

                            if (reader[Offer.TableColumns.StartDate] != DBNull.Value)
                                info.StartDate = Convert.ToDateTime(reader[Offer.TableColumns.StartDate]);
                            else
                                info.StartDate = null;

                            if (reader[Offer.TableColumns.EndDate] != DBNull.Value)
                                info.EndDate = Convert.ToDateTime(reader[Offer.TableColumns.EndDate]);
                            else
                                info.EndDate = null;

                            if (reader[Offer.TableColumns.OldPrice] != DBNull.Value)
                                info.OldPrice = Convert.ToDecimal(reader[Offer.TableColumns.OldPrice]);
                            else
                                info.OldPrice = null;

                            if (reader[Offer.TableColumns.NewPrice] != DBNull.Value)
                                info.NewPrice = Convert.ToDecimal(reader[Offer.TableColumns.NewPrice]);
                            else
                                info.NewPrice = null;

                            if (reader[Offer.TableColumns.DiscountPercentage] != DBNull.Value)
                                info.DiscountPercentage = Convert.ToInt32(reader[Offer.TableColumns.DiscountPercentage]);
                            else
                                info.DiscountPercentage = null;

                            if (reader[Offer.TableColumns.SaleUpTo] != DBNull.Value)
                                info.SaleUpTo = Convert.ToInt32(reader[Offer.TableColumns.SaleUpTo]);
                            else
                                info.SaleUpTo = null;

                            if (reader[Offer.TableColumns.PackageDescriptionAr] != DBNull.Value)
                                info.PackageDescriptionAr = Convert.ToString(reader[Offer.TableColumns.PackageDescriptionAr]);

                            if (reader[Offer.TableColumns.DescriptionAr] != DBNull.Value)
                                info.DescriptionAr = Convert.ToString(reader[Offer.TableColumns.DescriptionAr]);

                            if (reader[Currency.TableColumns.UnitAr] != DBNull.Value)
                                info.CurrencyInfo.UnitAr = Convert.ToString(reader[Currency.TableColumns.UnitAr]);

                            infoList.Add(info);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            info = new Offer();

                            info.ID = Convert.ToInt32(reader[Offer.CommonColumns.ID]);
                            info.CategoryID = Convert.ToInt32(reader[Offer.TableColumns.CategoryID]);
                            info.SupplierID = Convert.ToInt32(reader[Offer.TableColumns.SupplierID]);
                            info.CurrencyInfo.ID = Convert.ToInt32(reader[Offer.TableColumns.CurrencyID]);
                            info.Image = Convert.ToString(reader[Offer.TableColumns.Image]);
                            info.IsProduct = Convert.ToBoolean(reader[Offer.TableColumns.IsProduct]);
                            info.IsSale = Convert.ToBoolean(reader[Offer.TableColumns.IsSale]);
                            info.IsPackage = Convert.ToBoolean(reader[Offer.TableColumns.IsPackage]);
                            info.IsBestDeal = Convert.ToBoolean(reader[Offer.TableColumns.IsBestDeal]);
                            info.IsFeaturedOffer = Convert.ToBoolean(reader[Offer.TableColumns.IsFeaturedOffer]);
                            info.Rate = Convert.ToInt32(reader[Offer.TableColumns.Rate]);
                            info.RateCount = Convert.ToInt32(reader[Offer.TableColumns.RateCount]);
                            info.RateTotal = Convert.ToInt32(reader[Offer.TableColumns.RateTotal]);
                            info.Likes = Convert.ToInt32(reader[Offer.TableColumns.Likes]);
                            info.Views = Convert.ToInt32(reader[Offer.TableColumns.Views]);
                            info.IsActive = Convert.ToBoolean(reader[Offer.TableColumns.IsActive]);
                            info.NameEn = Convert.ToString(reader[Offer.TableColumns.NameEn]);
                            info.TitleEn = Convert.ToString(reader[Offer.TableColumns.TitleEn]);
                            info.ShortDescriptionEn = Convert.ToString(reader[Offer.TableColumns.ShortDescriptionEn]);
                            info.CreationDate = Convert.ToDateTime(reader[Offer.CommonColumns.CreationDate]);

                            if (reader[Offer.TableColumns.BrandID] != DBNull.Value)
                                info.BrandID = Convert.ToInt32(reader[Offer.TableColumns.BrandID]);
                            else
                                info.BrandID = null;

                            if (reader[Offer.TableColumns.StartDate] != DBNull.Value)
                                info.StartDate = Convert.ToDateTime(reader[Offer.TableColumns.StartDate]);
                            else
                                info.StartDate = null;

                            if (reader[Offer.TableColumns.EndDate] != DBNull.Value)
                                info.EndDate = Convert.ToDateTime(reader[Offer.TableColumns.EndDate]);
                            else
                                info.EndDate = null;

                            if (reader[Offer.TableColumns.OldPrice] != DBNull.Value)
                                info.OldPrice = Convert.ToDecimal(reader[Offer.TableColumns.OldPrice]);
                            else
                                info.OldPrice = null;

                            if (reader[Offer.TableColumns.NewPrice] != DBNull.Value)
                                info.NewPrice = Convert.ToDecimal(reader[Offer.TableColumns.NewPrice]);
                            else
                                info.NewPrice = null;

                            if (reader[Offer.TableColumns.DiscountPercentage] != DBNull.Value)
                                info.DiscountPercentage = Convert.ToInt32(reader[Offer.TableColumns.DiscountPercentage]);
                            else
                                info.DiscountPercentage = null;

                            if (reader[Offer.TableColumns.SaleUpTo] != DBNull.Value)
                                info.SaleUpTo = Convert.ToInt32(reader[Offer.TableColumns.SaleUpTo]);
                            else
                                info.SaleUpTo = null;

                            if (reader[Offer.TableColumns.PackageDescriptionEn] != DBNull.Value)
                                info.PackageDescriptionEn = Convert.ToString(reader[Offer.TableColumns.PackageDescriptionEn]);

                            if (reader[Offer.TableColumns.DescriptionEn] != DBNull.Value)
                                info.DescriptionEn = Convert.ToString(reader[Offer.TableColumns.DescriptionEn]);

                            if (reader[Currency.TableColumns.UnitEn] != DBNull.Value)
                                info.CurrencyInfo.UnitEn = Convert.ToString(reader[Currency.TableColumns.UnitEn]);

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

        private bool WriteOffer(string ProcedureName, Offer info, bool IsNew)
        {
            bool result = false;
            try
            {
                SqlCommand command = new SqlCommand(ProcedureName, this.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.CategoryID), info.CategoryID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.SupplierID), info.SupplierID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.CurrencyID), info.CurrencyInfo.ID);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.NameAr), info.NameAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.NameEn), info.NameEn);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.TitleAr), info.TitleAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.TitleEn), info.TitleEn);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.ShortDescriptionAr), info.ShortDescriptionAr);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.ShortDescriptionEn), info.ShortDescriptionEn);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.Image), info.Image);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsProduct), info.IsProduct);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsSale), info.IsSale);
                command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.IsPackage), info.IsPackage);

                if (info.BrandID.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.BrandID), info.BrandID.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.BrandID), DBNull.Value);

                if (!string.IsNullOrEmpty(info.DescriptionAr))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.DescriptionAr), info.DescriptionAr);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.DescriptionAr), DBNull.Value);

                if (!string.IsNullOrEmpty(info.DescriptionEn))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.DescriptionEn), info.DescriptionEn);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.DescriptionEn), DBNull.Value);

                if (!string.IsNullOrEmpty(info.PackageDescriptionAr))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.PackageDescriptionAr), info.PackageDescriptionAr);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.PackageDescriptionAr), DBNull.Value);

                if (!string.IsNullOrEmpty(info.PackageDescriptionEn))
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.PackageDescriptionEn), info.PackageDescriptionEn);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.PackageDescriptionEn), DBNull.Value);

                if (info.StartDate.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.StartDate), info.StartDate.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.StartDate), DBNull.Value);

                if (info.EndDate.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.EndDate), info.EndDate.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.EndDate), DBNull.Value);

                if (info.OldPrice.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.OldPrice), info.OldPrice.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.OldPrice), DBNull.Value);

                if (info.NewPrice.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.NewPrice), info.NewPrice.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.NewPrice), DBNull.Value);

                if (info.DiscountPercentage.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.DiscountPercentage), info.DiscountPercentage.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.DiscountPercentage), DBNull.Value);

                if (info.SaleUpTo.HasValue)
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.SaleUpTo), info.SaleUpTo.Value);
                else
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.TableColumns.SaleUpTo), DBNull.Value);

                if (IsNew)
                {
                    command.Parameters.Add(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID), SqlDbType.Int);
                    command.Parameters[string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID)].Direction = ParameterDirection.Output;

                    if (info.CreatedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.CreatedBy), info.CreatedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.CreatedBy), DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID), info.ID);

                    if (info.ModifiedBy.HasValue)
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ModifiedBy), info.ModifiedBy.Value);
                    else
                        command.Parameters.AddWithValue(string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ModifiedBy), DBNull.Value);
                }

                this.OpenConnection();

                command.ExecuteNonQuery();

                if (IsNew)
                {
                    info.ID = Convert.ToInt32(command.Parameters[string.Concat(CommonStrings.AtSymbol, Offer.CommonColumns.ID)].Value);
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