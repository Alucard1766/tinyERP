using System.ComponentModel;

namespace tinyERP.Dal.Types
{
    public enum State
    {
        [Description("Neu")]
        New,
        [Description("In Arbeit")]
        InProgress,
        [Description("Fertig")]
        Completed
    }
}
