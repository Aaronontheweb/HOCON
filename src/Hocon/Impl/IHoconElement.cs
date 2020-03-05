// -----------------------------------------------------------------------
// <copyright file="IHoconElement.cs" company="Petabridge, LLC">
//      Copyright (C) 2015 - 2020 Petabridge, LLC <https://petabridge.com>
// </copyright>
// -----------------------------------------------------------------------

// -----------------------------------------------------------------------
// <copyright file="IHoconElement.cs" company="Petabridge, LLC">
//      Copyright (C) 2015 - 2020 Petabridge, LLC <https://petabridge.com>
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Hocon
{
    internal interface IHoconElement : IEquatable<IHoconElement>
    {
        HoconType Type { get; }

        string Raw { get; }

        /// <summary>
        ///     Retrieves the string representation of this element.
        /// </summary>
        /// <returns>The string representation of this element.</returns>
        /// <remarks>
        /// NOTE: this returns an unquoted string. If you want the raw, underlying string
        /// including quotes call <see cref="object.ToString()"/> instead.
        /// </remarks>
        string GetString();

        /// <summary>
        ///     Retrieves the string representation of this element, indented for pretty printing.
        /// </summary>
        /// <param name="indent">The number indents this element.</param>
        /// <param name="indentSize">The number of spaces for each indent.</param>
        /// <returns>A pretty printed HOCON string representation of this element.</returns>
        string ToString(int indent, int indentSize);
    }
}