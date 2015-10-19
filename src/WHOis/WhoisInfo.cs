using System.Windows.Forms;

namespace WHOis
{
    public class WhoisInfo
    {
        public string Info { get; set; }
        public WhoisErrorEventArgs ErrorLogArgs { get; set; }
        public CheckState ReserveState { get; set; }
        public DataGridViewCell Cell { get; set; }
    }
}
