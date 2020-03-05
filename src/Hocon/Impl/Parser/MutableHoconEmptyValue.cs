// -----------------------------------------------------------------------
// <copyright file="HoconEmptyValue.cs" company="Akka.NET Project">
//      Copyright (C) 2013 - 2020 .NET Foundation <https://github.com/akkadotnet/hocon>
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Hocon
{
    /// <summary>
    ///     This class represents an empty <see cref="MutableHoconValue" />,
    ///     it masquerades as all other types and are usually used to represent empty or unresolved substitution.
    /// </summary>
    internal sealed class MutableHoconEmptyValue : MutableHoconValue
    {
        public MutableHoconEmptyValue() : base(null) { }
        
        public MutableHoconEmptyValue(IMutableHoconElement parent) : base(parent)
        {
        }

        public override HoconType Type => HoconType.Empty;

        public override string Raw => "";

        public override void Add(IMutableHoconElement value)
        {
            throw new HoconException($"Can not add new value to {nameof(MutableHoconEmptyValue)}");
        }

        public override void AddRange(IEnumerable<IMutableHoconElement> values)
        {
            throw new HoconException($"Can not add new values to {nameof(MutableHoconEmptyValue)}");
        }

        public override MutableHoconObject GetObject()
        {
            return new MutableHoconObject(Parent);
        }

        public override string GetString()
        {
            return "";
        }

        public override IList<MutableHoconValue> GetArray()
        {
            return new List<MutableHoconValue>();
        }

        public override bool Equals(IMutableHoconElement other)
        {
            if (other is null) return false;
            return other.Type == HoconType.Empty;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override IMutableHoconElement Clone(IMutableHoconElement newParent)
        {
            return new MutableHoconEmptyValue(newParent);
        }
    }
}