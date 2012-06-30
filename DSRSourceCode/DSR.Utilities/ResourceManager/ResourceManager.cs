using System.Configuration;
using System.Globalization;
using System.Reflection;

namespace DSR.Utilities.ResourceManager
{
    /// <summary>
    /// Represents a class for Retrieving Localized string / objects from the Resource File for displaying error message.
    /// </summary>
    /// <createdby>Amit Kumar Chandra</createdby>
    /// <createddate>30/06/2012</createddate>
    public static class ResourceManager
    {
        #region Private Static Variables

        private static string _resourceFileNamespace = "DSR.Utilities.ResourceManager.Resource";
        private static string _resourceFileBaseName = "Resource";
        private static string _defaultCultureName = "en-GB";
        private static System.Resources.ResourceManager _manager = new System.Resources.ResourceManager(_resourceFileNamespace + "." + _resourceFileBaseName, Assembly.GetExecutingAssembly(), null);

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets the namespace of the resource file.
        /// </summary>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string ResourceFileNamespace
        {
            get { return _resourceFileNamespace; }
        }

        /// <summary>
        /// Gets the base name of the resource file.
        /// </summary>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string ResourceFileBaseName
        {
            get { return _resourceFileBaseName; }
        }

        /// <summary>
        /// Gets default culture name.
        /// </summary>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string DefaultCultureName
        {
            get { return _defaultCultureName; }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets the value of the specified <see cref="System.String"/> resource.
        /// </summary>
        /// <param name="name">The name of the resource to get.</param>
        /// <returns>The <see cref="System.String"/> object containing the value of the specified resource.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string GetString(string name)
        {
            CultureInfo culture = new CultureInfo(_defaultCultureName);

            if (_manager == null)
                _manager = new System.Resources.ResourceManager(_resourceFileNamespace + "." + _resourceFileBaseName, Assembly.GetExecutingAssembly(), null);

            return name.Replace("ERR", "Error ") + ": " + _manager.GetString(name, culture);
        }

        /// <summary>
        /// Gets the value of the specified <see cref="System.String"/> resource localized for the specified culture.
        /// </summary>
        /// <param name="name">The name of the resource to get.</param>
        /// <param name="cultureName">The culture name in the format "[languagecode2]-[country/regioncode2]".</param>
        /// <returns>The <see cref="System.String"/> object containing the value of the specified resource.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string GetString(string name, string cultureName)
        {
            if (string.IsNullOrEmpty(cultureName))
                cultureName = _defaultCultureName;

            CultureInfo culture = new CultureInfo(cultureName);

            if (_manager == null)
                _manager = new System.Resources.ResourceManager(_resourceFileNamespace + "." + _resourceFileBaseName, Assembly.GetExecutingAssembly(), null);

            return name.Replace("ERR", "Error ") + ": " + _manager.GetString(name, culture);
        }

        /// <summary>
        /// Gets the value of the specified <see cref="System.String"/> resource without the validation code.
        /// </summary>
        /// <param name="name">The name of the resource to get.</param>
        /// <returns>The <see cref="System.String"/> object containing the value of the specified resource.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string GetStringWithoutName(string name)
        {
            CultureInfo culture = new CultureInfo(_defaultCultureName);

            if (_manager == null)
                _manager = new System.Resources.ResourceManager(_resourceFileNamespace + "." + _resourceFileBaseName, Assembly.GetExecutingAssembly(), null);

            return _manager.GetString(name, culture);
        }

        /// <summary>
        /// Gets the value of the specified <see cref="System.String"/> resource without the validation code, localized for the specified culture.
        /// </summary>
        /// <param name="name">The name of the resource to get.</param>
        /// <param name="cultureName">The culture name in the format "[languagecode2]-[country/regioncode2]".</param>
        /// <returns>The <see cref="System.String"/> object containing the value of the specified resource.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string GetStringWithoutName(string name, string cultureName)
        {
            if (string.IsNullOrEmpty(cultureName))
                cultureName = _defaultCultureName;

            CultureInfo culture = new CultureInfo(cultureName);

            if (_manager == null)
                _manager = new System.Resources.ResourceManager(_resourceFileNamespace + "." + _resourceFileBaseName, Assembly.GetExecutingAssembly(), null);

            return _manager.GetString(name, culture);
        }

        /// <summary>
        /// Gets the value of the specified <see cref="System.Object"/> resource.
        /// </summary>
        /// <param name="name">The name of the resource to get.</param>
        /// <returns>The <see cref="System.Object"/> for the specified resource.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static object GetObject(string name)
        {
            CultureInfo culture = new CultureInfo(_defaultCultureName);

            if (_manager == null)
                _manager = new System.Resources.ResourceManager(_resourceFileNamespace + "." + _resourceFileBaseName, Assembly.GetExecutingAssembly(), null);

            return _manager.GetObject(name, culture);
        }

        /// <summary>
        /// Gets the value of the <see cref="System.Object"/> resource localized for the specified culture.
        /// </summary>
        /// <param name="name">The name of the resource to get.</param>
        /// <param name="cultureName">The culture name in the format "[languagecode2]-[country/regioncode2]".</param>
        /// <returns>The <see cref="System.Object"/> for the specified resource.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static object GetObject(string name, string cultureName)
        {
            if (string.IsNullOrEmpty(cultureName))
                cultureName = _defaultCultureName;

            CultureInfo culture = new CultureInfo(cultureName);

            if (_manager == null)
                _manager = new System.Resources.ResourceManager(_resourceFileNamespace + "." + _resourceFileBaseName, Assembly.GetExecutingAssembly(), null);

            return _manager.GetObject(name, culture);
        }

        #endregion
    }
}
