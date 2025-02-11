using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Common.Domain.Enumerado;

/// <summary>
/// Represents the gender options.
/// </summary>
public enum EGender
{
    /// <summary>
    /// Do not specify.
    /// </summary>
    [Description("Não informar")]
    None = 0,

    /// <summary>
    /// Male gender.
    /// </summary>
    [Description("Male")]
    Male = 1,

    /// <summary>
    /// Female gender.
    /// </summary>
    [Description("Female")]
    Female = 2
}
