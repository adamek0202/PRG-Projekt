using Serilog;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Pokladna.Configuration
{
    internal static class ConfigManager
    {
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");

        public static SysConf Values { get; private set; }

        public static bool Initialize()
        {
            if (!File.Exists(ConfigPath))
            {
                return false;
            }
            try
            {
                var serializer = new XmlSerializer(typeof(SysConf));
                using(var reader = new StreamReader(ConfigPath))
                {
                    var loadedConfig = (SysConf)serializer.Deserialize(reader);
                    Values = EnsureSubsectionsNotNull(loadedConfig);
                    return true;
                }
            }
            catch(Exception ex)
            {
                Log.Fatal($"Nastala kritická chyba při načítání konfigurace: {ex.Message}");
                return false;
            }
        }

        private static SysConf EnsureSubsectionsNotNull(SysConf config)
        {
            if (config.Database == null) config.Database = new DatabaseConfig();
            if (config.Devices == null) config.Devices = new DeviceSettingsConfig();
            if (config.Devices.Printer == null) config.Devices.Printer = new PrinterConfig();
            if (config.Devices.CustomerDisplay == null) config.Devices.CustomerDisplay = new CustomerDisplayConfig();
            if (config.Devices.BarcodeScanner == null) config.Devices.BarcodeScanner = new BarcodeScannerConfig();
            if (config.Store == null) config.Store = new StoreConfig();
            if (config.Application == null) config.Application = new AppConfig();
            return config;
        }
    }
}
