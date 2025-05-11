using System;
using System.Windows.Forms;
using PCSC;
using PCSC.Iso7816;
using PCSC.Monitoring;

namespace Pokladna
{
    public class NfcReader : IDisposable
    {
        private static readonly Lazy<NfcReader> _instance = new(() => new NfcReader());
        public static NfcReader Instance => _instance.Value;

        private readonly ISCardContext _context;
        private ISCardMonitor _monitor;
        private string _readerName;

        public event Action<string> CardUidReceived;

        private NfcReader()
        {
            _context = ContextFactory.Instance.Establish(SCardScope.System);
        }

        public bool Start()
        {
            var readers = _context.GetReaders();
            if (readers == null || readers.Length == 0)
            {
                MessageBox.Show("UPOS: Čtečka karet nenalezena", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            _readerName = readers[0];

            _monitor = MonitorFactory.Instance.Create(SCardScope.System);
            _monitor.CardInserted += (sender, args) => {
                var uid = ReadCardUid();
                if (!string.IsNullOrEmpty(uid))
                {
                    CardUidReceived?.Invoke(uid);
                }
            };

            _monitor.Start(_readerName);
            return true;
        }

        private string ReadCardUid()
        {
            try
            {
                using var isoReader = new IsoReader(_context, _readerName, SCardShareMode.Shared, SCardProtocol.Any, false);

                var apdu = new CommandApdu(IsoCase.Case2Short, isoReader.ActiveProtocol)
                {
                    CLA = 0xFF,
                    INS = 0xCA,
                    P1 = 0x00,
                    P2 = 0x00,
                    Le = 0x00
                };

                var response = isoReader.Transmit(apdu);

                if (response.SW1 == 0x90 && response.SW2 == 0x00)
                {
                    return BitConverter.ToString(response.GetData());
                }
                else
                {
                    Console.WriteLine($"Chyba čtení UID: SW={response.SW1:X2} {response.SW2:X2}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba při čtení UID: " + ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            _monitor?.Cancel();
            _monitor?.Dispose();
            _context.Dispose();
        }
    }

}