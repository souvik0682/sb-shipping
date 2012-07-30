using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using DSR.Utilities.Cryptography;

namespace DSR.Utilities
{
    public class GeneralFunctions
    {
        #region Public Methods

        /// <summary>
        /// Serialize a custom object to XML string.
        /// </summary>
        /// <param name="objSerialize">Object that is to be serialized to XML.</param>
        /// <returns>Serialized XML string.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string Serialize(object objSerialize)
        {
            XmlSerializer xs = null;
            MemoryStream memoryStream = new MemoryStream();

            xs = new XmlSerializer(objSerialize.GetType());
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Unicode);

            if (xs != null)
            {
                xs.Serialize(xmlTextWriter, objSerialize);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            }

            return UTF16ByteArrayToString(memoryStream.ToArray());
        }

        /// <summary>
        /// Re construct an object from serialized xml string.
        /// </summary>
        /// <param name="xmlDocument">Serialized xml string.</param>
        /// <param name="type">The type of the object to be de-serialized.</param>
        /// <returns>De-serialized object.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static object Deserialize(string xmlDocument, Type type)
        {
            XmlSerializer xs = new XmlSerializer(type);
            MemoryStream memoryStream = new MemoryStream(StringToUTF16ByteArray(xmlDocument));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Unicode);

            return xs.Deserialize(memoryStream);
        }

        /// <summary>
        /// Encrypt query string.
        /// </summary>
        /// <param name="value">The value to be encrypted.</param>
        /// <returns>Encrypted query string.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string EncryptQueryString(string value)
        {
            string encryptedString = string.Empty;

            encryptedString = Encryption.Encrypt(value);

            if (encryptedString.Contains("+"))
            {
                encryptedString = encryptedString.Replace("+", "%2B");
            }

            if (encryptedString.Contains("/"))
            {
                encryptedString = encryptedString.Replace("/", "%2F");
            }

            if (encryptedString.Contains("&"))
            {
                encryptedString = encryptedString.Replace("&", "%24");
            }

            if (encryptedString.Contains("#"))
            {
                encryptedString = encryptedString.Replace("#", "%23");
            }

            return encryptedString;
        }

        /// <summary>
        /// Decrypt query string.
        /// </summary>
        /// <param name="value">The value to be decrypted.</param>
        /// <returns>Decrypted query string.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string DecryptQueryString(string value)
        {
            string decryptedString = string.Empty;
            string encryptedString = value;

            if (encryptedString.Contains("%2B"))
            {
                encryptedString = encryptedString.Replace("%2B", "+");
            }

            if (encryptedString.Contains("%2F"))
            {
                encryptedString = encryptedString.Replace("%2F", "/");
            }

            if (encryptedString.Contains("%24"))
            {
                encryptedString = encryptedString.Replace("%24", "&");
            }

            if (encryptedString.Contains("%23"))
            {
                encryptedString = encryptedString.Replace("%23", "#");
            }

            decryptedString = Encryption.Decrypt(encryptedString);

            return decryptedString;
        }

        /// <summary>
        /// Writes exception detail to the log file.
        /// </summary>
        /// <param name="path">The path of the error log file.</param>
        /// <param name="message">The message to be written.</param>
        /// <returns>True->If successfully logged, False->Otherwise.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static bool WriteErrorLog(string path, string message)
        {
            bool isSuccess = false;
            string directoryPath = string.Empty;
            bool isFileExists = File.Exists(path);

            if (!string.IsNullOrEmpty(path))
                directoryPath = path.Substring(0, path.LastIndexOf(@"\"));

            if (CreateDirectory(directoryPath))
            {
                using (StreamWriter streamWriter = (isFileExists) ? File.AppendText(path) : new StreamWriter(path))
                {
                    // Write to the file:
                    streamWriter.WriteLine(message);
                    streamWriter.WriteLine("---------------------------------");

                    // Close the stream:
                    streamWriter.Flush();
                    streamWriter.Close();
                    isSuccess = true;
                }
            }

            return isSuccess;
        }

        /// <summary>
        /// Creates a directory, if not exists, with the specified path.
        /// </summary>
        /// <param name="path">The path of the directory to be created.</param>
        /// <returns>True->If directory already exists or the directory is successfully created, False->If directory creation fails.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static bool CreateDirectory(string path)
        {
            bool isSuccess = false;

            DirectoryInfo di = new DirectoryInfo(path);

            if (!di.Exists)
            {
                //Directory not exists, so create it.
                di.Create();
                isSuccess = true;
            }
            else
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        /// <summary>
        /// Checks whether the specied object is empty or not.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to be checked.</param>
        /// <returns>True->if empty, False->otherwise.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static bool IsNullOrEmpty(object obj)
        {
            bool bReturn = false;

            if (obj == null || obj.ToString() == string.Empty || obj.ToString().Trim() == string.Empty)
                bReturn = true;

            return bReturn;
        }

        /// <summary>
        /// Checks whether the specified date is valid or not.
        /// </summary>
        /// <param name="dt">Date to check</param>
        /// <returns>True->If valid date, False->otherwise.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static bool IsValidDate(DateTime dt)
        {
            bool bIsValid = false;

            if (DateTime.Compare(dt, DateTime.MinValue) > 0 && DateTime.Compare(dt, DateTime.MaxValue) < 0)
                bIsValid = true;

            return bIsValid;
        }

        /// <summary>
        /// Selects an item in the <see cref="System.Web.UI.WebControls.DropDownList"/> control that contains the specified value.
        /// </summary>
        /// <param name="ddl">The <see cref="System.Web.UI.WebControls.DropDownList"/> control.</param>
        /// <param name="value">Selected Value.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static void SelectDropDownListItem(DropDownList ddl, string value)
        {
            ListItem lstItem;
            lstItem = ddl.Items.FindByValue(value);

            if (lstItem != null)
                ddl.SelectedValue = value;
            else
                ddl.SelectedValue = Constants.DROPDOWNLIST_DEFAULT_VALUE;
        }

        /// <summary>
        /// Select an item from <see cref="System.Web.UI.WebControls.DropDownList" /> control corresponding to the given text.
        /// </summary>
        /// <param name="ddl">The <see cref="System.Web.UI.WebControls.DropDownList"/> control.</param>
        /// <param name="text">Selected text</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static void SelectDropDownListItemByText(DropDownList ddl, string text)
        {
            ListItem lstItem;
            lstItem = ddl.Items.FindByText(text);

            if (lstItem != null)
                lstItem.Selected = true;
        }

        /// <summary>
        /// Loads the DropDownList control with the specified data source.
        /// </summary>
        /// <param name="ddl">The <see cref="System.Web.UI.WebControls.DropDownList"/> to be populated.</param>
        /// <param name="ds">The <see cref="System.Data.DataSet"/> representing the data source.</param>
        public static void PopulateDropDownList(DropDownList ddl, DataSet ds)
        {
            PopulateDropDownList(ddl, ds, true);
        }

        public static void PopulateDropDownList(DropDownList ddl, DataSet ds, bool includeDefaultValue)
        {
            ddl.DataSource = ds;
            ddl.DataValueField = Constants.DATA_VALUE_FIELD;
            ddl.DataTextField = Constants.DATA_TEXT_FIELD;
            ddl.DataBind();

            if (includeDefaultValue)
                ddl.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        public static void PopulateDropDownList<T>(DropDownList ddl, List<T> lstSource, string dataValueField, string dataTextField, bool includeDefaultValue)
        {
            ddl.DataSource = lstSource;
            ddl.DataValueField = dataValueField;
            ddl.DataTextField = dataTextField;
            ddl.DataBind();

            if (includeDefaultValue)
                ddl.Items.Insert(0, new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        public static void PopulateDropDownList<T>(DropDownList ddl, List<T> lstSource, string dataValueField, string dataTextField, string customDefaultValue)
        {
            ddl.DataSource = lstSource;
            ddl.DataValueField = dataValueField;
            ddl.DataTextField = dataTextField;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(customDefaultValue, Constants.DROPDOWNLIST_DEFAULT_VALUE));
        }

        /// <summary>
        /// Registers the startup alert script with the <see cref="System.Web.UI.Page"/> object with the message.
        /// </summary>
        /// <param name="page">The <see cref="System.Web.UI.Page"/> object.</param>
        /// <param name="message">The message to be displayed.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static void RegisterAlertScript(Page page, string message)
        {
            page.ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script>javascript:void alert('" + message + "');</script>");
        }

        public static void RegisterErrorAlertScript(Page page, string message)
        {
            RegisterAlertScript(page, "An Error has occured: \\r\\n Error Message: " + message);
        }

        public static void ClosePopupandRefreshParent(Page page)
        {
            page.ClientScript.RegisterStartupScript(typeof(Page), "CloseRefresh", "<script>javascript:window.close();window.opener.location.reload();</script>");
        }
        public static void ClosePopup(Page page)
        {
            page.ClientScript.RegisterStartupScript(typeof(Page), "CloseRefresh", "<script>javascript:window.close();</script>");
        }
        /// <summary>
        /// Formats the alert message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Formatted alert message.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string FormatAlertMessage(string message)
        {
            return message + "\\r\\n";
        }

        /// <summary>
        /// Formats the alert message.
        /// </summary>
        /// <param name="slNo">The sl no.</param>
        /// <param name="message">The message.</param>
        /// <returns>Formatted alert message.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string FormatAlertMessage(int slNo, string message)
        {
            return slNo.ToString() + ". " + message + "\\r\\n";
        }

        public static decimal ConvertLatLongToDecimal(decimal? degree, decimal? minute, decimal? second)
        {
            decimal value = 0.00M;

            if (degree.HasValue)
                value = degree.Value;

            if (minute.HasValue)
                value += minute.Value / 60.00M;

            if (second.HasValue)
                value += second.Value / 3600.00M;

            return Math.Round(value, 6);
        }

        /// <summary>
        /// Formats the latitude longitude.
        /// </summary>
        /// <param name="degree">The degree.</param>
        /// <param name="minute">The minute.</param>
        /// <param name="second">The second.</param>
        /// <param name="sign">The sign.</param>
        /// <returns></returns>
        public static string FormatLatitudeLongitude(string degree, string minute, string second, string sign)
        {
            string latLong = string.Empty;

            if (!string.IsNullOrEmpty(degree))
            {
                latLong = degree + "°";
            }
            if (!string.IsNullOrEmpty(minute))
            {
                latLong += minute + "'";
            }
            if (!string.IsNullOrEmpty(second))
            {
                latLong += second + "\"";
            }
            if (!string.IsNullOrEmpty(sign))
            {
                latLong += " " + sign;
            }

            return (string.IsNullOrEmpty(latLong)) ? Constants.NOT_AVAILABLE_TEXT : latLong;
        }

        /// <summary>
        /// Clears the application cache.
        /// </summary>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static void ClearApplicationCache()
        {
            System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
        }

        public static void ApplyGridViewAlternateItemStyle(GridViewRow row, int cellCount)
        {
            if (row.RowIndex % 2 != 0)
            {
                for (int index = 0; index < cellCount; index++)
                {
                    row.Cells[index].CssClass = "gridviewalternateitem";
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// To convert a Byte Array of Unicode charaters (UTF-16 encoded) to a complete string.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to string.</param>
        /// <returns>String converted from Unicode Byte Array.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        private static string UTF16ByteArrayToString(byte[] characters)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            return encoding.GetString(characters);
        }

        /// <summary>
        /// Converts the String to UTF16 Byte array and is used in de-serialization.
        /// </summary>
        /// <param name="xmlDocument">String to be converted in byte array.</param>
        /// <returns>Unicode Byte Array converted from string.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        private static byte[] StringToUTF16ByteArray(string xmlDocument)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            Byte[] byteArray = encoding.GetBytes(xmlDocument);
            return byteArray;
        }

        #endregion
    }
}
