// -----------------------------------------------------------------------
// <copyright file="HoconImmutableExtensions.cs" company="Akka.NET Project">
//      Copyright (C) 2013 - 2020 .NET Foundation <https://github.com/akkadotnet/hocon>
// </copyright>
// -----------------------------------------------------------------------

using Hocon.Immutable.Builder;

namespace Hocon.Immutable.Extensions
{
    public static class HoconImmutableExtensions
    {
        public static HoconImmutableObject ToHoconImmutable(this HoconRoot root)
        {
            return new HoconImmutableObjectBuilder()
                .Merge(root.Value.GetObject())
                .Build();
        }

        public static HoconImmutableElement ToHoconImmutable(this IMutableHoconElement element)
        {
            switch (element)
            {
                case MutableHoconObject o:
                    return o.ToHoconImmutable();
                case MutableHoconArray a:
                    return a.ToHoconImmutable();
                case MutableHoconLiteral l:
                    return l.ToHoconImmutable();
                case MutableHoconValue v:
                    return v.ToHoconImmutable();
                case MutableHoconField f:
                    return f.ToHoconImmutable();
                default:
                    throw new HoconException($"Unknown Hocon element type:{element.GetType().Name}");
            }
        }

        public static HoconImmutableElement ToHoconImmutable(this MutableHoconValue value)
        {
            switch (value.Type)
            {
                case HoconType.Object:
                    return new HoconImmutableObjectBuilder()
                        .Merge(value.GetObject())
                        .Build();
                case HoconType.Array:
                    return new HoconImmutableArrayBuilder()
                        .AddRange(value)
                        .Build();
                case HoconType.Boolean:
                case HoconType.Number:
                case HoconType.String:
                    return new HoconImmutableLiteralBuilder()
                        .Append(value)
                        .Build();
                case HoconType.Empty:
                    return HoconImmutableLiteral.Null;
                default:
                    // Should never reach this line.
                    throw new HoconException($"Unknown Hocon field type:{value.Type}");
            }
        }

        public static HoconImmutableObject ToHoconImmutable(this MutableHoconObject @object)
        {
            return new HoconImmutableObjectBuilder()
                .Merge(@object)
                .Build();
        }

        public static HoconImmutableElement ToHoconImmutable(this MutableHoconField field)
        {
            return field.Value.ToHoconImmutable();
        }

        public static HoconImmutableArray ToHoconImmutable(this MutableHoconArray array)
        {
            return new HoconImmutableArrayBuilder()
                .AddRange(array)
                .Build();
        }

        public static HoconImmutableLiteral ToHoconImmutable(this MutableHoconLiteral literal)
        {
            return literal.LiteralType == HoconLiteralType.Null
                ? null
                : new HoconImmutableLiteralBuilder()
                    .Append(literal)
                    .Build();
        }
    }
}