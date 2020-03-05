// -----------------------------------------------------------------------
// <copyright file="HoconMergedObject.cs" company="Akka.NET Project">
//      Copyright (C) 2013 - 2020 .NET Foundation <https://github.com/akkadotnet/hocon>
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Hocon
{
    internal sealed class MutableHoconMergedObject : MutableHoconObject
    {
        public MutableHoconMergedObject(IMutableHoconElement parent, List<MutableHoconObject> objects) : base(parent)
        {
            Objects = objects;
            foreach (var obj in Objects) base.Merge(obj);
        }

        public List<MutableHoconObject> Objects { get; }

        internal override MutableHoconField TraversePath(HoconPath relativePath)
        {
            var result = Objects.Last().TraversePath(relativePath);
            Clear();
            foreach (var obj in Objects) base.Merge(obj);

            return result;
        }

        internal override MutableHoconField GetOrCreateKey(string key)
        {
            var result = Objects.Last().GetOrCreateKey(key);
            Clear();
            foreach (var obj in Objects) base.Merge(obj);

            return result;
        }

        internal override void SetField(string key, MutableHoconField value)
        {
            Objects.Last().SetField(key, value);
            base.SetField(key, value);
        }

        public override void Merge(MutableHoconObject other)
        {
            var parent = (MutableHoconValue) Parent;
            parent.Add(other);

            Objects.Add(other);
            base.Merge(other);
        }
    }
}