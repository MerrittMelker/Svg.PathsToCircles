using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Svg.PathsToCircles
{
    internal class Holland2PathToCircleConverter : PathToCircleConverter
    {
        protected List<SectionGroup> SectionGroups;

        public Holland2PathToCircleConverter(XElement root) : base(root)
        {
            SectionGroups = new List<SectionGroup>();

            var sg = new SectionGroup {Id = "Orchestra", Sections = new List<Section>()};
            sg.Sections.Add(new Section { Id = "Left", AddRadius = false });
            sg.Sections.Add(new Section { Id = "Center", AddRadius = false });
            sg.Sections.Add(new Section { Id = "Right", AddRadius = true });
            SectionGroups.Add(sg);

            sg = new SectionGroup { Id = "OrchestraBoxes", Sections = new List<Section>() };
            sg.Sections.Add(new Section { Id = "BoxC", AddRadius = false });
            sg.Sections.Add(new Section { Id = "BoxH", AddRadius = true });
            SectionGroups.Add(sg);

            sg = new SectionGroup { Id = "OrchestraCircle", Sections = new List<Section>() };
            sg.Sections.Add(new Section { Id = "Left", AddRadius = false });
            sg.Sections.Add(new Section { Id = "Center", AddRadius = false });
            sg.Sections.Add(new Section { Id = "Right", AddRadius = true });
            SectionGroups.Add(sg);
        }

        public override void Run()
        {
            foreach (var sg in SectionGroups)
            {
                var sgElement = GetChildById(Root, sg.Id);
                foreach (var s in sg.Sections)
                {
                    var sElement = GetChildById(sgElement, s.Id);
                    var paths = sElement.Descendants().Where(x => x.Name.LocalName == "path");
                    ConvertToCircles(paths, s.AddRadius);
                }
            }

            var circles = Root.Descendants().Where(x => x.Name.LocalName == "circle" && x.Attribute("id") == null);
            circles.ToList().ForEach( x => x.Remove());
        }

        protected class SectionGroup
        {
            public string Id { get; set; }
            public List<Section> Sections { get; set; }
        }

        protected class Section
        {
            public string Id { get; set; }
            public bool AddRadius { get; set; }
        }
    }
}