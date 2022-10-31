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
        
    }
}