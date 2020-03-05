// -----------------------------------------------------------------------
// <copyright file="HoconLiteral.cs" company="Akka.NET Project">
//      Copyright (C) 2013 - 2020 .NET Foundation <https://github.com/akkadotnet/hocon>
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Hocon
{
    /// <summary>
    ///     This class represents a literal element in a HOCON (Human-Optimized Config Object Notation)
    ///     configuration string.
    ///     <code>
    /// akka {  
    ///   actor {
    ///     provider = "Akka.Remote.RemoteActorRefProvider, Akka.Remote"
    ///   }
    /// }
    /// </code>
    /// </summary>
    internal abstract class MutableHoconLiteral : IMutableHoconElement
    {
        protected MutableHoconLiteral(IMutableHoconElement parent, string value)
        {
            Parent = parent;
            Value = value;
        }

        public abstract HoconLiteralType LiteralType { get; }

        /// <summary>
        ///     Gets or sets the value of this element.
        /// </summary>
        public virtual string Value { get; }

        public IMutableHoconElement Parent { get; }
        public abstract HoconType Type { get; }

        /// <summary>
        ///     Retrieves the raw string representation of this element.
        /// </summary>
        /// <returns>The raw value of this element.</returns>
        public virtual string Raw => Value;

        /// <inheritdoc />
        /// <exception cref="T:Hocon.HoconException">
        ///     This element is a string literal. It is not an object.
        ///     Therefore this method will throw an exception.
        /// </exception>
        public MutableHoconObject GetObject()
        {
            throw new HoconException("Hocon literal could not be converted to object.");
        }

        /// <inheritdoc />
        public string GetString()
        {
            return Value;
        }

        /// <summary>
        ///     Retrieves a list of elements associated with this element.
        /// </summary>
        /// <returns>
        ///     A list of elements associated with this element.
        /// </returns>
        /// <exception cref="HoconException">
        ///     This element is a string literal. It is not an array.
        ///     Therefore this method will throw an exception.
        /// </exception>
        public IList<MutableHoconValue> GetArray()
        {
            throw new HoconException("Hocon literal could not be converted to array.");
        }

        /// <inheritdoc />
        public string ToString(int indent, int indentSize)
        {
            return Raw;
        }

        public abstract IMutableHoconElement Clone(IMutableHoconElement newParent);

        public bool Equals(IMutableHoconElement other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type && string.Equals(Value, other.GetString());
        }

        /// <summary>
        ///     Returns the string representation of this element.
        /// </summary>
        /// <returns>The value of this element.</returns>
        public override string ToString()
        {
            return Raw;
        }

        internal static MutableHoconLiteral Create(IMutableHoconElement owner, Token token)
        {
            switch (token.LiteralType)
            {
                case TokenLiteralType.Null:
                    return new MutableHoconNull(owner);

                case TokenLiteralType.Bool:
                    return new MutableHoconBool(owner, token.Value);

                case TokenLiteralType.Whitespace:
                    return new MutableHoconWhitespace(owner, token.Value);

                case TokenLiteralType.UnquotedLiteralValue:
                    return new MutableHoconUnquotedString(owner, token.Value);

                case TokenLiteralType.QuotedLiteralValue:
                    return new MutableHoconQuotedString(owner, token.Value);

                case TokenLiteralType.TripleQuotedLiteralValue:
                    return new MutableHoconTripleQuotedString(owner, token.Value);

                case TokenLiteralType.Long:
                    return new MutableHoconLong(owner, token.Value);

                case TokenLiteralType.Double:
                    return new MutableHoconDouble(owner, token.Value);

                case TokenLiteralType.Hex:
                    return new MutableHoconHex(owner, token.Value);

                case TokenLiteralType.Octal:
                    return new MutableHoconOctal(owner, token.Value);

                default:
                    throw new HoconException($"Unknown token literal type: {token.Value}");
            }
        }

        public override bool Equals(object obj)
        {
            // Needs to be cast to IHoconElement because there are cases 
            // where a HoconLiteral can be the same as a HoconValue
            return obj is IMutableHoconElement other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }

        public static bool operator ==(MutableHoconLiteral left, MutableHoconLiteral right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MutableHoconLiteral left, MutableHoconLiteral right)
        {
            return !Equals(left, right);
        }
    }

    internal sealed class MutableHoconNull : MutableHoconLiteral
    {
        public MutableHoconNull(IMutableHoconElement parent) : base(parent, "null")
        {
        }

        public override HoconType Type => HoconType.String;
        public override HoconLiteralType LiteralType => HoconLiteralType.Null;
        public override string Raw => "null";
        public override string Value => null;

        public override IMutableHoconElement Clone(IMutableHoconElement newParent)
        {
            return new MutableHoconNull(newParent);
        }
    }

    internal sealed class MutableHoconBool : MutableHoconLiteral
    {
        public MutableHoconBool(IMutableHoconElement parent, string value) : base(parent, value)
        {
        }

        public override HoconType Type => HoconType.Boolean;
        public override HoconLiteralType LiteralType => HoconLiteralType.Bool;

        public override IMutableHoconElement Clone(IMutableHoconElement newParent)
        {
            return new MutableHoconBool(newParent, Value);
        }
    }

    internal sealed class MutableHoconDouble : MutableHoconLiteral
    {
        public MutableHoconDouble(IMutableHoconElement parent, string value) : base(parent, value)
        {
        }

        public override HoconType Type => HoconType.Number;
        public override HoconLiteralType LiteralType => HoconLiteralType.Double;

        public override IMutableHoconElement Clone(IMutableHoconElement newParent)
        {
            return new MutableHoconDouble(newParent, Value);
        }
    }

    internal sealed class MutableHoconLong : MutableHoconLiteral
    {
        public MutableHoconLong(IMutableHoconElement parent, string value) : base(parent, value)
        {
        }

        public override HoconType Type => HoconType.Number;
        public override HoconLiteralType LiteralType => HoconLiteralType.Long;

        public override IMutableHoconElement Clone(IMutableHoconElement newParent)
        {
            return new MutableHoconLong(newParent, Value);
        }
    }

    internal sealed class MutableHoconHex : MutableHoconLiteral
    {
        public MutableHoconHex(IMutableHoconElement parent, string value) : base(parent, value)
        {
        }

        public override HoconType Type => HoconType.Number;
        public override HoconLiteralType LiteralType => HoconLiteralType.Hex;

        public override IMutableHoconElement Clone(IMutableHoconElement newParent)
        {
            return new MutableHoconHex(newParent, Value);
        }
    }

    internal sealed class MutableHoconOctal : MutableHoconLiteral
    {
        public MutableHoconOctal(IMutableHoconElement parent, string value) : base(parent, value)
        {
        }

        public override HoconLiteralType LiteralType => HoconLiteralType.Long;

        public override HoconType Type => HoconType.Number;

        public override IMutableHoconElement Clone(IMutableHoconElement newParent)
        {
            return new MutableHoconOctal(newParent, Value);
        }
    }

    internal sealed class MutableHoconUnquotedString : MutableHoconLiteral
    {
        public MutableHoconUnquotedString(IMutableHoconElement parent, string value) : base(parent, value)
        {
        }

        public override HoconType Type => HoconType.String;
        public override HoconLiteralType LiteralType => HoconLiteralType.UnquotedString;

        public override IMutableHoconElement Clone(IMutableHoconElement newParent)
        {
            return new MutableHoconUnquotedString(newParent, Value);
        }
    }

    internal sealed class MutableHoconQuotedString : MutableHoconLiteral
    {
        public MutableHoconQuotedString(IMutableHoconElement parent, string value) : base(parent, value)
        {
        }

        public override HoconType Type => HoconType.String;
        public override HoconLiteralType LiteralType => HoconLiteralType.QuotedString;
        public override string Raw => "\"" + Value + "\"";

        public override IMutableHoconElement Clone(IMutableHoconElement newParent)
        {
            return new MutableHoconQuotedString(newParent, Value);
        }
    }

    internal sealed class MutableHoconTripleQuotedString : MutableHoconLiteral
    {
        public MutableHoconTripleQuotedString(IMutableHoconElement parent, string value) : base(parent, value)
        {
        }

        public override HoconType Type => HoconType.String;
        public override HoconLiteralType LiteralType => HoconLiteralType.TripleQuotedString;
        public override string Raw => "\"\"\"" + Value + "\"\"\"";

        public override IMutableHoconElement Clone(IMutableHoconElement newParent)
        {
            return new MutableHoconTripleQuotedString(newParent, Value);
        }
    }

    internal sealed class MutableHoconWhitespace : MutableHoconLiteral
    {
        public MutableHoconWhitespace(IMutableHoconElement parent, string value) : base(parent, value)
        {
        }

        public override HoconType Type => HoconType.String;
        public override HoconLiteralType LiteralType => HoconLiteralType.Whitespace;

        public override IMutableHoconElement Clone(IMutableHoconElement newParent)
        {
            return new MutableHoconWhitespace(newParent, Value);
        }
    }
}