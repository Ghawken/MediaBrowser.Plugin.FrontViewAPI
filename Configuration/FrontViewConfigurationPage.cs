using System.IO;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Plugins;

namespace MediaBrowser.Plugins.FrontView.Configuration
{
    /// <summary>
    /// Class YatseConfigurationPage
    /// </summary>
    class FrontViewConfigurationPage : IPluginConfigurationPage
    {
        /// <summary>
        /// Gets the type of the configuration page.
        /// </summary>
        /// <value>The type of the configuration page.</value>
        public ConfigurationPageType ConfigurationPageType
        {
            get { return ConfigurationPageType.PluginConfiguration; }
        }

        /// <summary>
        /// Gets the HTML stream.
        /// </summary>
        /// <returns>Stream.</returns>
        public Stream GetHtmlStream()
        {
            return GetType().Assembly.GetManifestResourceStream("MediaBrowser.Plugins.FrontView.Configuration.configPage.html");
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return "FrontView"; }
        }

        public IPlugin Plugin
        {
            get { return FrontView.Plugin.Instance; }
        }
    }
}
