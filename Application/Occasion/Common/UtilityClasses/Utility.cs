using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using EntityLayer.Entities;
using Common.StringsClasses;
using System.Web;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Data.SqlTypes;
using System.Collections;
using System.Xml;
using ideaBubbling.FlvConverter;
using System.Text;

namespace Common.UtilityClasses
{
    public class Utility
    {
        #region Image Operations

        /// <summary>
        /// Resize the given image to be a thumbnail (100*100)
        /// </summary>
        /// <param name="ImageToResizePath">path of the image to resize</param>
        /// <param name="ResizedImagePath">path of the output image</param>
        /// <returns>true if succeeded, false otherwise</returns>
        public static bool ResizeImage(string ImageToResizePath, string ResizedImagePath)
        {
            bool result = false;
            try
            {
                using (Bitmap originalBitmap = Bitmap.FromFile(ImageToResizePath, true) as Bitmap, newbmp = new Bitmap(100, 100))
                {
                    double WidthVsHeightRatio = (float)originalBitmap.Width / (float)originalBitmap.Height;

                    using (Graphics newg = Graphics.FromImage(newbmp))
                    {
                        newg.Clear(Color.White);

                        newg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        newg.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                        newg.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        newg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        newg.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                        if (WidthVsHeightRatio == 1d)
                        {
                            newg.DrawImage(originalBitmap, 0, 0, 100, 100);
                            newg.Save();
                        }

                        else if (WidthVsHeightRatio < 1d) //Image is taller than wider
                        {
                            newg.DrawImage(originalBitmap, new Rectangle(100 * (int)(1 - WidthVsHeightRatio), 0, (int)(100 * WidthVsHeightRatio), 100));
                            newg.Save();
                        }

                        else //Image is wider than taller
                        {
                            double inverse = Math.Pow(WidthVsHeightRatio, -1);
                            newg.DrawImage(originalBitmap, new Rectangle(0, 100 * (int)(1 - inverse), 100, (int)(100 * WidthVsHeightRatio)));
                            newg.Save();
                        }
                    }

                    newbmp.Save(ResizedImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    result = true;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        /// <summary>
        /// Resize the given image to be of the given size
        /// </summary>
        /// <param name="ImageToResizePath">path of the image to resize</param>
        /// <param name="ResizedImagePath">path of the output image</param>
        /// <param name="maxImageSize">the maximum size required</param>
        /// <returns>true if succeeded, false otherwise</returns>
        public static bool ResizeImage(string ImageToResizePath, string ResizedImagePath, int maxImageSize)
        {
            bool result = false;
            System.Drawing.Image originalImage = null;
            System.Drawing.Bitmap thumbnailBitmap = null;
            try
            {
                int thumbnailWidth;
                int thumbnailHeight;

                // Open original image and determine thumbnail size based on image dimensions and the max image size
                originalImage = System.Drawing.Image.FromFile(ImageToResizePath);
                int sourceWidth = originalImage.Width;
                int sourceHeight = originalImage.Height;
                double widthHeightRatio = (double)sourceWidth / (double)sourceHeight;

                // If width greater than height, then width should be max image size, otherwise height should be.
                // Image should keep the same proportions.
                if (widthHeightRatio > 1.0)
                {
                    thumbnailWidth = maxImageSize;
                    thumbnailHeight = (int)(maxImageSize / widthHeightRatio);
                }
                else
                {
                    thumbnailWidth = (int)(maxImageSize * widthHeightRatio);
                    thumbnailHeight = maxImageSize;
                }

                // Create bitmap and graphics objects for the new image
                thumbnailBitmap = new System.Drawing.Bitmap(thumbnailWidth, thumbnailHeight, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(thumbnailBitmap);

                // set graphics parameters to optimize thumbnail image
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Transform image to new size and save thumbnail
                g.DrawImage(originalImage, 0, 0, thumbnailWidth, thumbnailHeight);
                thumbnailBitmap.Save(ResizedImagePath, originalImage.RawFormat);

                result = true;
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                if (originalImage != null)
                {
                    originalImage.Dispose();
                }
                if (thumbnailBitmap != null)
                {
                    thumbnailBitmap.Dispose();
                }
            }
            return result;
        }

        /// <summary>
        /// Resize the given image to be of the given size
        /// </summary>
        /// <param name="ImageToResizePath">path of the image to resize</param>
        /// <param name="ResizedImagePath">path of the output image</param>
        /// <param name="maxImageSize">the maximum size required</param>
        /// <returns>true if succeeded, false otherwise</returns>
        public static bool ResizeImage(string ImageToResizePath, string ResizedImagePath, int maxHeight, int maxWidth)
        {
            bool result = false;
            System.Drawing.Image originalImage = null;
            System.Drawing.Bitmap thumbnailBitmap = null;
            try
            {
                int thumbnailWidth;
                int thumbnailHeight;

                // Open original image and determine thumbnail size based on image dimensions and the max image size
                originalImage = System.Drawing.Image.FromFile(ImageToResizePath);
                int sourceWidth = originalImage.Width;
                int sourceHeight = originalImage.Height;
                double widthHeightRatio = (double)sourceWidth / (double)sourceHeight;

                // If width greater than height, then width should be max image size, otherwise height should be.
                // Image should keep the same proportions.
                if (widthHeightRatio > 1.0)
                {
                    thumbnailWidth = maxWidth;
                    thumbnailHeight = (int)(maxWidth / widthHeightRatio);
                }
                else
                {
                    thumbnailWidth = (int)(maxHeight * widthHeightRatio);
                    thumbnailHeight = maxHeight;
                }

                // Create bitmap and graphics objects for the new image
                thumbnailBitmap = new System.Drawing.Bitmap(thumbnailWidth, thumbnailHeight, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(thumbnailBitmap);

                // set graphics parameters to optimize thumbnail image
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Transform image to new size and save thumbnail
                g.DrawImage(originalImage, 0, 0, thumbnailWidth, thumbnailHeight);
                thumbnailBitmap.Save(ResizedImagePath, originalImage.RawFormat);

                result = true;
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                if (originalImage != null)
                {
                    originalImage.Dispose();
                }
                if (thumbnailBitmap != null)
                {
                    thumbnailBitmap.Dispose();
                }
            }
            return result;
        }

        #endregion

        #region File Operations

        /// <summary>
        /// Deletes the given file
        /// </summary>
        /// <param name="filePath">path of the file to delete</param>
        public static void DeleteFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                file.Delete();
            }
        }

        /// <summary>
        /// Checks if a given file exists
        /// </summary>
        /// <param name="filePath">file path to check</param>
        /// <returns>true if exists, false otherwise</returns>
        public static bool CheckFileExists(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the file name without its extension
        /// </summary>
        /// <param name="file">the file to get its name</param>
        /// <returns>the file name</returns>
        public static string GetFileName(string file)
        {
            string fileName = null;
            try
            {
                fileName = file.Substring(0, file.LastIndexOf(CommonStrings.Dot));
            }
            catch (Exception error)
            {
                throw error;
            }
            return fileName;
        }

        /// <summary>
        /// Gets the file name and file extension
        /// </summary>
        /// <param name="file">file to get its info</param>
        /// <returns>array of strings, the first element is the file name, the second element is the file extension</returns>
        public static string[] GetFileInfo(string file)
        {
            string[] fileInfo = null;
            try
            {
                fileInfo = new string[2];
                fileInfo[0] = GetFileName(file);
                fileInfo[1] = GetFileExtension(file);
            }
            catch (Exception error)
            {
                throw error;
            }
            return fileInfo;
        }

        /// <summary>
        /// Gets the file extension
        /// </summary>
        /// <param name="file">the file to get its extension</param>
        /// <returns>the file extension</returns>
        public static string GetFileExtension(string file)
        {
            string fileExtension = null;
            try
            {
                fileExtension = file.Substring(file.LastIndexOf(CommonStrings.Dot), file.Length - file.LastIndexOf(CommonStrings.Dot));
            }
            catch (Exception error)
            {
                throw error;
            }
            return fileExtension;
        }

        #endregion

        #region Email Operations

        /// <summary>
        /// Sends an Email
        /// </summary>
        /// <param name="From">From Email Address</param>
        /// <param name="To">To Email Address</param>
        /// <param name="Subject">Email Subject</param>
        /// <param name="Body">Email Body</param>
        /// <returns>true if succeeded, false otherwise</returns>
        public static bool SendMail(string From, string To, string Subject, string Body)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(From) && !string.IsNullOrEmpty(To))
                {
                    SmtpClient mailOperator = new SmtpClient();
                    MailMessage message = new MailMessage(From, To, Subject, Body);
                    mailOperator.Send(message);
                    result = true;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        /// <summary>
        /// Sends an Email
        /// </summary>
        /// <param name="From">From Email Address</param>
        /// <param name="To">To Email Address</param>
        /// <param name="Subject">Email Subject</param>
        /// <param name="Body">Email Body</param>
        /// <returns>true if succeeded, false otherwise</returns>
        public static bool SendMail(string From, string To, string Subject, string Body, string smtpServer, int port)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(From) && !string.IsNullOrEmpty(To))
                {
                    SmtpClient mailOperator = new SmtpClient();
                    mailOperator.Host = smtpServer;
                    mailOperator.Port = port;
                    MailMessage message = new MailMessage(From, To, Subject, Body);
                    mailOperator.Send(message);
                    result = true;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        /// <summary>
        /// Sends an Email Template
        /// </summary>
        /// <param name="From">From Email Address</param>
        /// <param name="To">To Email Address</param>
        /// <param name="Subject">Email Subject</param>
        /// <param name="TemplatePath">Template Physical Path</param>
        /// <param name="ItemsList">List of all key/value items to be replaced in the template</param>
        /// <returns>true if succeeded, false otherwise</returns>
        public static bool SendMailTemplate(string From, string To, string Subject, string TemplatePath, List<KeyValue> ItemsList, System.Web.UI.Control Owner)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(From) && !string.IsNullOrEmpty(To) && ItemsList != null && ItemsList.Count > 0)
                {
                    MailDefinition definition = new MailDefinition();

                    definition.BodyFileName = TemplatePath;
                    definition.From = From;
                    definition.Subject = Subject;
                    definition.IsBodyHtml = false;

                    ListDictionary replacements = new ListDictionary();
                    foreach (KeyValue item in ItemsList)
                    {
                        replacements.Add(item.Key, item.Value);
                    }

                    SmtpClient mailOperator = new SmtpClient();
                    mailOperator.Send(definition.CreateMailMessage(To, replacements, Owner));

                    result = true;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        #endregion

        #region Query String Operations

        /// <summary>
        /// Appends a list of parameters to the given url as a query string
        /// </summary>
        /// <param name="path">url</param>
        /// <param name="queryStringList">list of parameters to append</param>
        /// <returns>the result url with the query string</returns>
        public static string AppendQueryString(string path, List<KeyValue> queryStringList)
        {
            if (!string.IsNullOrEmpty(path) && queryStringList != null && queryStringList.Count > 0)
            {
                if (path.Contains(CommonStrings.QuestionSymbol.ToString()))
                {
                    path = string.Concat(path,
                                         CommonStrings.AndSymbol,
                                         queryStringList[0].Key,
                                         CommonStrings.EqualSymbol,
                                         Convert.ToString(queryStringList[0].Value));
                }
                else
                {
                    path = string.Concat(path,
                                         CommonStrings.QuestionSymbol,
                                         queryStringList[0].Key,
                                         CommonStrings.EqualSymbol,
                                         Convert.ToString(queryStringList[0].Value));
                }

                for (int i = 1; i < queryStringList.Count; i++)
                {
                    path = string.Concat(path,
                                         CommonStrings.AndSymbol,
                                         queryStringList[i].Key,
                                         CommonStrings.EqualSymbol,
                                         Convert.ToString(queryStringList[i].Value));
                }

                return path;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Appends a parameter to the given url as a query string
        /// </summary>
        /// <param name="path">url</param>
        /// <param name="queryStringParameter">parameter to append</param>
        /// <returns>url appended with the query string</returns>
        public static string AppendQueryString(string path, KeyValue queryStringParameter)
        {
            if (!string.IsNullOrEmpty(path) && queryStringParameter != null)
            {
                if (path.Contains(CommonStrings.QuestionSymbol.ToString()))
                {
                    return (string.Concat(path,
                                          CommonStrings.AndSymbol,
                                          queryStringParameter.Key,
                                          CommonStrings.EqualSymbol,
                                          Convert.ToString(queryStringParameter.Value)));
                }
                else
                {
                    return (string.Concat(path,
                                          CommonStrings.QuestionSymbol,
                                          queryStringParameter.Key,
                                          CommonStrings.EqualSymbol,
                                          Convert.ToString(queryStringParameter.Value)));
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        #endregion

        #region Download/Upload Operations

        /// <summary>
        /// Downloads the given file on the client's machine
        /// </summary>
        /// <param name="currentResponse">the response object of the current request (always use this.Response)</param>
        /// <param name="FileName">File name</param>
        /// <param name="FilePath">File physical path</param>
        /// <param name="forceDownload">if to force the download dialog box to show</param>
        /// <param name="IsVideo">if the file to be downloaded is a video strem</param>
        public static void DownloadFile(HttpResponse currentResponse, string FileName, string FilePath, bool forceDownload, bool IsVideo)
        {
            string name = Path.GetFileName(FileName);
            string ext = Path.GetExtension(FileName);
            string type = string.Empty;

            // set known types based on file extension
            if (!string.IsNullOrEmpty(ext))
            {
                if (IsVideo)
                {
                    switch (ext.ToLower())
                    {
                        case CommonStrings.Ext_rm:
                            type = CommonStrings.Mime_ramAudio;
                            break;

                        case CommonStrings.Ext_ram:
                            type = CommonStrings.Mime_ramVideo;
                            break;

                        default:
                            type = CommonStrings.Mime_ramAudio;
                            break;
                    }
                }
                else
                {
                    switch (ext.ToLower())
                    {
                        case CommonStrings.Ext_htm:
                        case CommonStrings.Ext_html:
                            type = CommonStrings.Mime_HTML;
                            break;

                        case CommonStrings.Ext_txt:
                            type = CommonStrings.Mime_Text;
                            break;

                        case CommonStrings.Ext_doc:
                        case CommonStrings.Ext_rtf:
                            type = CommonStrings.Mime_MSWord;
                            break;

                        case CommonStrings.Ext_rm:
                            type = CommonStrings.Mime_rmAudio;
                            break;

                        case CommonStrings.Ext_ram:
                            type = CommonStrings.Mime_ramAudio;
                            break;

                        default:
                            type = CommonStrings.Mime_Text;
                            break;
                    }
                }
            }

            if (forceDownload)
            {
                currentResponse.AppendHeader(CommonStrings.ContentDisposition, string.Concat(CommonStrings.Attachment,
                                                                                             CommonStrings.SemiColon,
                                                                                             CommonStrings.Space,
                                                                                             CommonStrings.FileName,
                                                                                             CommonStrings.EqualSymbol,
                                                                                             name));
            }

            if (!string.IsNullOrEmpty(type))
            {
                currentResponse.ContentType = type;
            }

            currentResponse.WriteFile(FilePath);
        }

        /// <summary>
        /// Uploads the specified file to the server, and concatenats now time to the file name to ensure no conflicts with other files 
        /// </summary>
        /// <param name="uploader">FileUpload control which uploads the file</param>
        /// <param name="filePath">the path where to save the file</param>
        /// <param name="savedFilePath">the actual path where the file is saved which is the given path + now time</param>
        /// <param name="savingTime">the time concatenated to the saved file path</param>
        /// <returns>true if the file is uploaded successfully, false otherwise</returns>
        public static bool UploadFile(FileUpload uploader, string folderPath, out string savedFilePath, out string savedFileName)
        {
            savedFileName = string.Concat(Guid.NewGuid().ToString().Replace("-", string.Empty), GetFileExtension(uploader.FileName.Trim()));
            savedFilePath = string.Concat(folderPath, savedFileName);
            bool result = false;
            try
            {
                uploader.SaveAs(savedFilePath);
                result = true;
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        /// <summary>
        /// Converts any video type to flv and saves the converted file to the given folder
        /// </summary>
        /// <param name="currentPage">The page contains the File upload control</param>
        /// <param name="uploader">the file upload control used to upload the video file</param>
        /// <param name="folder">the folder to save the converted file to it</param>
        /// <param name="converterPath">the path of ffmpeg.exe</param>
        /// <param name="savedFlvPath">the path + name of the converted file</param>
        /// <param name="savedFlvName">the name of the converted file</param>
        /// <returns>true if succeeded, otherwise false</returns>
        public static bool SaveFlvVideo(System.Web.UI.Page currentPage, FileUpload uploader, string folder, string converterPath, out string savedFlvPath, out string savedFlvName)
        {
            savedFlvName = string.Empty;
            savedFlvPath = string.Empty;
            bool result = false;
            try
            {
                string savedVideoName;
                string savedVideoPath;
                if (Utility.UploadFile(uploader, folder, out savedVideoPath, out savedVideoName))
                {
                    savedFlvName = savedVideoName.Replace(Utility.GetFileExtension(savedVideoName), ".flv");
                    savedFlvPath = savedVideoPath.Replace(Utility.GetFileExtension(savedVideoPath), ".flv");

                    FlvConverterWebClient.ConvertAsFlv(currentPage, converterPath, savedVideoPath, savedFlvPath);
                    System.Threading.Thread.Sleep(1000);

                    while (FlvConverterWebClient.GetFlvConversionStatus(currentPage) == FlvConverterStatus.BeingConverted)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }

                    if (FlvConverterWebClient.GetFlvConversionStatus(currentPage) == FlvConverterStatus.Completed)
                    {
                        if (Utility.CheckFileExists(savedFlvPath))
                        {
                            DeleteFile(savedVideoPath);
                            result = true;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                if (!string.IsNullOrEmpty(FlvConverterWebClient.GetFlvConversionErrorMsg(currentPage)))
                {
                    throw new Exception(FlvConverterWebClient.GetFlvConversionErrorMsg(currentPage));
                }
                else
                {
                    throw error;
                }
            }
            return result;
        }

        #endregion

        #region SQL Injection Operations

        private static readonly System.Text.RegularExpressions.Regex regSystemThreats =
                new System.Text.RegularExpressions.Regex(@"\s?;\s?|\s?drop\s|\s?grant\s|^'|\s?--|\s?union\s|\s?delete\s|\s?truncate\s|\s?sysobjects\s?|\s?xp_.*?|\s?syslogins\s?|\s?sysremote\s?|\s?sysusers\s?|\s?sysxlogins\s?|\s?sysdatabases\s?|\s?aspnet_.*?|\s?exec\s?|",
                    System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        /// <summary>
        /// A helper method to attempt to discover known SqlInjection attacks.  
        /// For use when using one of the flexible non-parameterized access methods, such as GetPaged()
        /// </summary>
        /// <param name="whereClause">string of the whereClause to check</param>
        /// <returns>true if found, false if not found </returns>
        public static bool DetectSqlInjection(string whereClause)
        {
            return regSystemThreats.IsMatch(whereClause);
        }

        /// <summary>
        /// A helper method to attempt to discover known SqlInjection attacks.  
        /// For use when using one of the flexible non-parameterized access methods, such as GetPaged()
        /// </summary>
        /// <param name="whereClause">string of the whereClause to check</param>
        /// <param name="orderBy">string of the orderBy clause to check</param>
        /// <returns>true if found, false if not found </returns>
        public static bool DetectSqlInjection(string whereClause, string orderBy)
        {
            return regSystemThreats.IsMatch(whereClause) || regSystemThreats.IsMatch(orderBy);
        }

        #endregion

        #region MISC

        public static bool HasArabicLetters(string text)
        {
            bool result = false;
            try
            {
                foreach (char character in text.ToCharArray())
                {
                    if (character >= 0x600 && character <= 0x6ff)
                    {
                        result = true;
                        break;
                    }

                    if (character >= 0x750 && character <= 0x77f)
                    {
                        result = true;
                        break;
                    }

                    if (character >= 0xfb50 && character <= 0xfc3f)
                    {
                        result = true;
                        break;
                    }

                    if (character >= 0xfe70 && character <= 0xfefc)
                    {
                        result = true;
                        break;
                    }
                }

            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }

        #endregion
    }
}
