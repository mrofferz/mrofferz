using System;
using System.Data.SqlTypes;
using System.Collections;
using Microsoft.SqlServer.Server;

/// <summary>
/// (to be used inside MS-SQL 2005)
/// Contains static functions to split a string - that represents a list - to the list items it contains
/// </summary>
public class SqlSplitList
{
    /// <summary>
    /// Splits a string into an array of substrings based on the specified delimiter
    /// </summary>
    /// <param name="listString">the string to be splited</param>
    /// <param name="delimiter">the delimiter to split based on it</param>
    /// <returns>a collection of the type IEnumerable</returns>
    [SqlFunction(FillRowMethodName = "CharListFillRow")]
    public static IEnumerable SplitCharList(SqlString listString, SqlString delimiter)
    {
        return listString.Value.Split(delimiter.Value.ToCharArray(0, 1));
    }

    /// <summary>
    /// is used to convert each item of the "SplitCharList" collection to string 
    /// </summary>
    /// <param name="item">item to be converted</param>
    /// <param name="itemString">the converted result</param>
    public static void CharListFillRow(object item, out string itemString)
    {
        itemString = item.ToString().Trim();
    }

    /// <summary>
    /// Splits a string into an array of strings based on the specified delimiter
    /// </summary>
    /// <param name="listString">the string to be splited</param>
    /// <param name="delimiter">the delimiter to split based on it</param>
    /// <returns>a collection of the type IEnumerable</returns>
    [SqlFunction(FillRowMethodName = "MultiCharListFillRow")]
    public static ArrayList SplitMultiCharList(SqlString listString, SqlString baseDelimiter, SqlString subDelimiter)
    {
        ArrayList recordsList = new ArrayList();
        string[] baseList = listString.Value.Split(baseDelimiter.Value.ToCharArray(0, 1), StringSplitOptions.RemoveEmptyEntries);
        foreach (string subString in baseList)
        {
            string[] subList = subString.Split(subDelimiter.Value.ToCharArray(0, 1), StringSplitOptions.RemoveEmptyEntries);
            recordsList.Add(subList);
        }

        return recordsList;
    }

    /// <summary>
    /// is used by MS-SQL to fill the row by the passed value 
    /// </summary>
    /// <param name="item">item to be converted</param>
    /// <param name="itemInt">the converted result</param>
    public static void MultiCharListFillRow(object item, out string itemAr, out string itemEn)
    {
        string[] subString = (string[])item;

        itemAr = subString[0].Trim();
        itemEn = subString[1].Trim();
    }

    /// <summary>
    /// Splits a string into an array of integers based on the specified delimiter
    /// </summary>
    /// <param name="listString">the string to be splited</param>
    /// <param name="delimiter">the delimiter to split based on it</param>
    /// <returns>a collection of the type IEnumerable</returns>
    [SqlFunction(FillRowMethodName = "IntListFillRow")]
    public static IEnumerable SplitIntList(SqlString listString, SqlString delimiter)
    {
        return listString.Value.Split(delimiter.Value.ToCharArray(0, 1), StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// is used to convert each item of "SplitIntList" collection to integer
    /// </summary>
    /// <param name="item">item to be converted</param>
    /// <param name="itemInt">the converted result</param>
    public static void IntListFillRow(object item, out int itemInt)
    {
        itemInt = Convert.ToInt32((string)item);
    }

    /// <summary>
    /// Splits a string into an array of integers based on the specified delimiter
    /// </summary>
    /// <param name="listString">the string to be splited</param>
    /// <param name="delimiter">the delimiter to split based on it</param>
    /// <returns>a collection of the type IEnumerable</returns>
    [SqlFunction(FillRowMethodName = "MultiIntListFillRow")]
    public static ArrayList SplitMultiIntList(SqlString listString, SqlString baseDelimiter, SqlString subDelimiter)
    {
        ArrayList recordsList = new ArrayList();
        string[] baseList = listString.Value.Split(baseDelimiter.Value.ToCharArray(0, 1), StringSplitOptions.RemoveEmptyEntries);
        foreach (string subString in baseList)
        {
            string[] subList = subString.Split(subDelimiter.Value.ToCharArray(0, 1), StringSplitOptions.RemoveEmptyEntries);
            recordsList.Add(subList);
        }

        return recordsList;
    }

    /// <summary>
    /// is used by MS-SQL to fill the row by the passed value 
    /// </summary>
    /// <param name="item">item to be converted</param>
    /// <param name="itemInt">the converted result</param>
    public static void MultiIntListFillRow(object item, out int itemInt, out int countInt)
    {
        string[] subString = (string[])item;

        itemInt = Convert.ToInt32((string)subString[0]);
        countInt = Convert.ToInt32((string)subString[1]);
    }

    /// <summary>
    /// Splits a string into an array of integers based on the specified delimiter
    /// </summary>
    /// <param name="listString">the string to be splited</param>
    /// <param name="delimiter">the delimiter to split based on it</param>
    /// <returns>a collection of the type IEnumerable</returns>
    [SqlFunction(FillRowMethodName = "IntDecimalListFillRow")]
    public static ArrayList SplitIntDecimalList(SqlString listString, SqlString baseDelimiter, SqlString subDelimiter)
    {
        ArrayList recordsList = new ArrayList();
        string[] baseList = listString.Value.Split(baseDelimiter.Value.ToCharArray(0, 1), StringSplitOptions.RemoveEmptyEntries);
        foreach (string subString in baseList)
        {
            string[] subList = subString.Split(subDelimiter.Value.ToCharArray(0, 1), StringSplitOptions.RemoveEmptyEntries);
            recordsList.Add(subList);
        }

        return recordsList;
    }

    /// <summary>
    /// is used by MS-SQL to fill the row by the passed value 
    /// </summary>
    /// <param name="item">item to be converted</param>
    /// <param name="itemInt">the converted result</param>
    public static void IntDecimalListFillRow(object item, out int itemInt, out decimal lowCost, out decimal highCost)
    {
        string[] subString = (string[])item;

        itemInt = Convert.ToInt32((string)subString[0]);
        lowCost = Convert.ToDecimal((string)subString[1]);
        highCost = Convert.ToDecimal((string)subString[2]);
    }

    /// <summary>
    /// Splits a string into an array of integers based on the specified delimiter
    /// </summary>
    /// <param name="listString">the string to be splited</param>
    /// <param name="delimiter">the delimiter to split based on it</param>
    /// <returns>a collection of the type IEnumerable</returns>
    [SqlFunction(FillRowMethodName = "MultiIntDecimalListFillRow")]
    public static ArrayList SplitMultiIntDecimalList(SqlString listString, SqlString baseDelimiter, SqlString subDelimiter)
    {
        ArrayList recordsList = new ArrayList();
        string[] baseList = listString.Value.Split(baseDelimiter.Value.ToCharArray(0, 1), StringSplitOptions.RemoveEmptyEntries);
        foreach (string subString in baseList)
        {
            string[] subList = subString.Split(subDelimiter.Value.ToCharArray(0, 1), StringSplitOptions.RemoveEmptyEntries);
            recordsList.Add(subList);
        }

        return recordsList;
    }

    /// <summary>
    /// is used by MS-SQL to fill the row by the passed value 
    /// </summary>
    /// <param name="item">item to be converted</param>
    /// <param name="itemInt">the converted result</param>
    public static void MultiIntDecimalListFillRow(object item, out int typeID, out int valueID, out decimal lowCost, out decimal highCost)
    {
        string[] subString = (string[])item;

        typeID = Convert.ToInt32((string)subString[0]);
        valueID = Convert.ToInt32((string)subString[1]);
        lowCost = Convert.ToDecimal((string)subString[2]);
        highCost = Convert.ToDecimal((string)subString[3]);
    }
}

