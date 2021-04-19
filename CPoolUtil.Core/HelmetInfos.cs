using System.Collections.Generic;
using System.Linq;

namespace CPoolUtil.Core
{
    public class HelmetInfo
    {
        public string Name { get; private set; }
        public bool HideHair { get; private set; }
        public bool HideUpperFaceProps { get; private set; }
        public bool HideLowerFaceProps { get; private set; }
        public bool HideFacialHair { get; private set; }

        private HelmetInfo() { } // Only allow specific parsing

        public static HelmetInfo Parse(string line)
        {
            string template = line.Split('=')[0];
            string[] props = line.Split('=')[1].Split(',');

            // The boolean logic is a bit "double negativey" here, but as long as we don't find a "!" at the beginning of a prop, that means it's true
            return new HelmetInfo
            {
                Name = template,
                HideHair = !props.First(p => p.EndsWith("HideHair")).StartsWith('!'),
                HideUpperFaceProps = !props.First(p => p.EndsWith("HideUpperFaceProps")).StartsWith('!'),
                HideLowerFaceProps = !props.First(p => p.EndsWith("HideLowerFaceProps")).StartsWith('!'),
                HideFacialHair = !props.First(p => p.EndsWith("HideFacialHair")).StartsWith('!')
            };
        }
    }
}