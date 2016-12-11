using System;
using MediaBrowser.Model.Plugins;
using System.Collections.Generic;

namespace MediaBrowser.Plugins.FrontView.Configuration
{
    /// <summary>
    /// Class PluginConfiguration
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string FrontViewUserName { get; set; }
        public string Pin { get; set; }
        public bool TimeShift { get; set; }
        public string FrontViewConfigID { get; set; }       // THIS IS Yatse Device ID from Yatse CODE BASE --- needs to be same!
        public Boolean EnableDebugLogging { get; set; }
        public string SelectedDeviceId { get; set; }

        public List<DeviceInfoList> DevicesRunning { get; set; }

        public PluginConfiguration()
        {
            Pin = "0000";
            FrontViewUserName = "FrontView";
            TimeShift = false;
            EnableDebugLogging = false;
            FrontViewConfigID = "9DA94EFB-EFF0-4144-9A18-46B046C450C6";
            SelectedDeviceId = "Dummy";
        }

        public class DeviceInfoList
            {
                public string DeviceName { get; set; }
                public string DeviceId { get; set; }
                public string Client { get; set; }
                public string Id { get; set; }
           
            }


        }
        
        

    
    
    }

