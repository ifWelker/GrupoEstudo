using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CalculoBonus
{
    public enum Cargo
    {
        [EnumMember(Value = "Junior")]
        Junior = 0,
        [EnumMember(Value = "Pleno")]
        Pleno,
        [EnumMember(Value = "Senior")]
        Senior,
        [EnumMember(Value = "Especialista")]
        Especialista,
        [EnumMember(Value = "Coordenador")]
        Coordenador,
        [EnumMember(Value = "Gerente")]
        Gerente,
        [EnumMember(Value = "Diretor")]
        Diretor
    }
}
