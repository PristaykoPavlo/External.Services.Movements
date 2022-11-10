using System.ComponentModel;
using System.Runtime.Serialization;
using YamlDotNet.Core.Tokens;

namespace External.Services.Movements.WebApi.Models
{
    public class PagedMovements
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<Movement>? Movements { get; set; }

    }

    public class Movement
    {
        public int MovementId { get; set; }
        public string? Account { get; set; }
        public EnumMovementType MovementType { get; set; }
        public decimal Amount { get; set; }
        public string? AccountFrom { get; set; }
        public string? AccountTo { get; set; }
    }

    public enum EnumMovementType
    {
        [Description("Unknown"), EnumMember(Value = "Unknown")]
        Unknown = 0,
        [Description("Fee"), EnumMember(Value = "Fee")]
        Fee = 1,
        [Description("Interest"), EnumMember(Value = "Interest")]
        Interest = 2,
        [Description("Tax"), EnumMember(Value = "Tax")]
        Tax = 3,
        [Description("Fiscal transfer"), EnumMember(Value = "Fiscal transfer")]
        FiscalTransfer = 4,
        [Description("Incoming"), EnumMember(Value = "Incoming")]
        Incoming = 5,
        [Description("Outgoing"), EnumMember(Value = "Outgoing")]
        Outgoing = 6
    }
}