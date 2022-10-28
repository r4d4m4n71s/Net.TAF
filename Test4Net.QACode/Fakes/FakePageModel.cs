using System.Drawing;
using Test4Net.Logging.Interfaces;
using Test4Net.UI.Browser.Interfaces;
using Test4Net.UI.Page;
using Test4Net.UITest.Constants;
using Test4Net.UITest.Models.Attributes;

namespace Net.TAF.QACode.Fakes;

[Platform(Pf.Windows)]
[ViewPort(1024, 768)]
public class FakePageModel : AbstractPage
{
    public FakePageModel(IBrowser browser, ILog log) : base(browser, log)
    {
        Rules.Add(new Rule("MaxViewPortSizeAllowed", () =>
        {
            var viewPorts = (ViewPortAttribute[])typeof(FakePageModel).GetCustomAttributes(typeof(ViewPortAttribute), true);

            // Available for all view ports
            if (viewPorts.Length == 0)
                return true;

            var faults = new List<Size>();
            var browserViewPort = browser.Driver.Manage().Window.Size;
            foreach (var viewPort in viewPorts)
            {
                if (viewPort.Size.Width > browserViewPort.Width
                    || viewPort.Size.Height > browserViewPort.Height)
                    faults.Add(viewPort.Size);
            }

            if(faults.Any())
                Log.Warn($"The view ports : {string.Join(",", faults.Select(pt=> $"[{pt.Width}{(pt.Height > -1 ? ","+pt.Height : string.Empty)}]"))}" +
                         $" are not supported for [{browserViewPort.Width+","+browserViewPort.Height}] browser setup");

            return !faults.Any();
        }, "Determines a max browser window size(with, height)px for this page."));
    }
}