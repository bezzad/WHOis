using System.Windows.Forms;

namespace WHOis
{
    public class WhoisInfo
    {
        public string Info { get; set; }
        public WhoisEventArgs ErrorLogArgs { get; set; }
        public CheckState ReserveState { get; set; }
    }
}
