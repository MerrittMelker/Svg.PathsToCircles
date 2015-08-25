using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Svg.PathsToCircles
{
    internal abstract class PathToCircleConverter
    {
        protected XName CircleName;
        protected Decimal Radius;
        protected XElement Root;

        protected PathToCircleConverter(XElement root)
        {
            Root = root;

            var firstCircle = root.Descendants().First(x => x.Name.LocalName == "circle");
            CircleName = firstCircle.Name;
            Radius = decimal.Parse(firstCircle.Attribute("r").Value);
            //var cx = firstCircle.Attribute("cx");
            //var cy = firstCircle.Attribute("cy");
        }

        public abstract void Run();

        protected static XElement GetChildById(XContainer root, string gId)
        {
            return root.Elements().Single(x => x.Attribute("id") != null && x.Attribute("id").Value == gId);
        }

        protected void ConvertToCircles(IEnumerable<XElement> paths, bool addRadius)
        {
            paths.ToList().ForEach(x =>
            {
                var value = x.Attribute("d").Value;

                var regexX = new Regex(@"M(.+?)\,");
                var m = regexX.Matches(value);
                var xVal = decimal.Parse(m[0].Groups[1].Value);

                var regex = new Regex(@",(.+?)c");
                m = regex.Matches(value);
                var yVal = m[0].Groups[1].Value;

                x.Name = CircleName;
                x.Attribute("d").Remove();

                x.SetAttributeValue("fill", "#000");
                var attributes = x.Attributes().ToList();

                attributes.Insert(1,
                    addRadius ? new XAttribute("cx", xVal + Radius) : new XAttribute("cx", xVal - Radius));

                attributes.Insert(2, new XAttribute("cy", yVal));
                attributes.Insert(3, new XAttribute("r", Radius));
                x.Attributes().Remove();
                x.Add(attributes);
            });
        }
    }
}