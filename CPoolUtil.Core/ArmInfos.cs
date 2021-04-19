using System.Linq;

namespace CPoolUtil.Core
{
    public class ArmInfo
    {
        public string Name { get; private set; }
        public bool HideForearms { get; private set; }

        private ArmInfo() { } // Only allow specific parsing

        public static ArmInfo Parse(string line)
        {
            string template = line.Split('=')[0];
            string hideForearms = line.Split('=')[1];

            // The boolean logic is a bit "double negativey" here, but as long as we don't find a "!" at the beginning of a prop, that means it's true
            return new ArmInfo
            {
                Name = template,
                HideForearms = !hideForearms.StartsWith("!")
            };
        }
    }
}