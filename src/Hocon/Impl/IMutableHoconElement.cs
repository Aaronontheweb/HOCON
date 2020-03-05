// -----------------------------------------------------------------------
// <copyright file="IHoconElement.cs" company="Akka.NET Project">
//      Copyright (C) 2013 - 2020 .NET Foundation <https://github.com/akkadotnet/hocon>
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Hocon
{
    internal interface IHoconElement : IEquatable<IHoconElement>
    {

        HoconType Type { get; }

        string Raw { get; }
    }

        /// <summary>
        ///     This interface defines the contract needed to implement
        ///     a HOCON (Human-Optimized Config Object Notation) element.
        /// </summary>
        internal interface IMutableHoconElement : IEquatable<IMutableHoconElement>
    {
        IMutableHoconElement Parent { get; }

        HoconType Type { get; }

        string Raw { get; }

        /// <summary>
        ///     Retrieves the HOCON object representation of this element.
        /// </summary>
        /// <returns>The HOCON object representation of this element.</returns>
        MutableHoconObject GetObject();

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
        ///     Retrieves a list of elements associated with this element.
        /// </summary>
        /// <returns>A list of elements associated with this element.</returns>
        IList<MutableHoconValue> GetArray();

        /// <summary>
        ///     Do deep copy of this element.
        /// </summary>
        /// <returns>A deep company of this element.</returns>
        IMutableHoconElement Clone(IMutableHoconElement newParent);

        /// <summary>
        ///     Retrieves the string representation of this element, indented for pretty printing.
        /// </summary>
        /// <param name="indent">The number indents this element.</param>
        /// <param name="indentSize">The number of spaces for each indent.</param>
        /// <returns>A pretty printed HOCON string representation of this element.</returns>
        string ToString(int indent, int indentSize);
    }
}