using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Svg.PathsToCircles
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var document = XDocument.Load("test.svg");
            if (document.Root == null) throw new NotImplementedException();
            var root = document.Root;

            var h2Ptcc = new Holland2PathToCircleConverter(root);
            h2Ptcc.Run();

            document.Save("test_New.svg");
            //var paths = root.Descendants().Where( x => x.Name.LocalName == "path");
            //Console.WriteLine(paths.Count());
            //var firstCircle = root.Descendants().First(x => x.Name.LocalName == "circle");
            //var circleName = firstCircle.Name;
            //var r = decimal.Parse(firstCircle.Attribute("r").Value);
            //var cx = firstCircle.Attribute("cx");
            //var cy = firstCircle.Attribute("cy");


            //var paths = root.Descendants().Where(x => x.Name.LocalName == "path");

            //paths.ToList().ForEach(x =>
            //{
            //    var value = x.Attribute("d").Value;

            //    var regexX = new Regex(@"M(.+?)\,");
            //    var m = regexX.Matches(value);
            //    var xVal = decimal.Parse(m[0].Groups[1].Value);

            //    var regex = new Regex(@",(.+?)c");
            //    m = regex.Matches(value);
            //    var yVal = m[0].Groups[1].Value;

            //    x.Name = circleName;
            //    x.Attribute("d").Remove();

            //    x.SetAttributeValue("fill", "#000");
            //    var attributes = x.Attributes().ToList();
            //    attributes.Insert(1, new XAttribute("cx", xVal - r));
            //    attributes.Insert(2, new XAttribute("cy", yVal));
            //    attributes.Insert(3, new XAttribute("r", r));
            //    x.Attributes().Remove();
            //    x.Add(attributes);
            //});
            
            //Console.ReadLine();
        }
    }
}