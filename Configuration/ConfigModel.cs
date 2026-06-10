using System.Xml.Serialization;

namespace Pokladna.Configuration
{
    [XmlRoot("SystemConfiguration")]
    public class SysConf
    {
        [XmlElement("DatabaseSettings")]
        public DatabaseConfig Database { get; set; }

        [XmlElement("DeviceSettings")]
        public DeviceSettingsConfig Devices { get; set; }

        [XmlElement("StoreSettings")]
        public StoreConfig Store { get; set; }

        [XmlElement("ApplicationSettings")]
        public AppConfig Application { get; set; }
    }

    // --- PODSEKCE PRO HARDWARE ---

    public class DeviceSettingsConfig
    {
        [XmlElement("Printer")]
        public PrinterConfig Printer { get; set; }

        [XmlElement("CustomerDisplay")]
        public CustomerDisplayConfig CustomerDisplay { get; set; }

        [XmlElement("BarcodeScanner")]
        public BarcodeScannerConfig BarcodeScanner { get; set; }
    }

    public class PrinterConfig
    {
        [XmlAttribute("enabled")]
        public bool IsEnabled { get; set; }

        [XmlElement("PrinterName")]
        public string PrinterName { get; set; } // Název USB tiskárny ve Windows (např. Star TSP100)

        [XmlElement("PrinterWidthChars")]
        public int WidthChars { get; set; } // Šířka (32, 40, 42...)

        [XmlElement("CodePage")]
        public string CodePage { get; set; } // "windows-1250"
    }

    public class CustomerDisplayConfig
    {
        [XmlAttribute("enabled")]
        public bool IsEnabled { get; set; }

        [XmlElement("PortName")]
        public string PortName { get; set; } // USB displeje často emulují COM (např. "COM5")

        [XmlElement("BaudRate")]
        public int BaudRate { get; set; } // Typicky 9600 nebo 19200

        [XmlElement("DisplayWidthChars")]
        public int WidthChars { get; set; } // Většinou 20 (pro klasické 2x20 VFD displeje)
    }

    public class BarcodeScannerConfig
    {
        [XmlAttribute("enabled")]
        public bool IsEnabled { get; set; }

        [XmlAttribute("mode")]
        public string Mode { get; set; } // "KeyboardEmulation" (USB HID) nebo "VirtualCOM"

        [XmlElement("PortName")]
        public string PortName { get; set; } // Vyplní se, pouze pokud jede v režimu VirtualCOM
    }

    // --- ZBYTEK KONFIGURACE (Zůstává stejný) ---

    public class DatabaseConfig
    {
        [XmlAttribute("provider")]
        public string Provider { get; set; }
        [XmlAttribute("connectionTimeout")]
        public int Timeout { get; set; }
        [XmlElement("Server")]
        public string Server { get; set; }
        [XmlElement("Port")]
        public int Port { get; set; }
        [XmlElement("DatabaseName")]
        public string DatabaseName { get; set; }
        [XmlElement("User")]
        public string User { get; set; }
        [XmlElement("Password")]
        public string Password { get; set; }
    }

    public class StoreConfig
    {
        [XmlElement("StoreName")]
        public string StoreName { get; set; }
        [XmlElement("CompanyId_ICO")]
        public string CompanyId { get; set; }
        [XmlElement("Street")]
        public string Street { get; set; }
        [XmlElement("City")]
        public string City { get; set; }
        [XmlElement("ReceiptHeaderNote")]
        public string HeaderNote { get; set; }
    }

    public class AppConfig
    {
        [XmlElement("DefaultCashier")]
        public string DefaultCashier { get; set; }
        [XmlElement("DebugMode")]
        public bool DebugMode { get; set; }
        [XmlElement("CurrencySign")]
        public string CurrencySign { get; set; }
    }
}