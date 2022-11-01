using Test4Net.Logging.Interfaces;
using Test4Net.QACode.Attributes;
using Test4Net.QACode.Constants;
using Test4Net.UI.POM.Browser.Interfaces;
using Test4Net.UI.POM.Page;

namespace Test4Net.QACode.Fakes;

[Platform(Pf.Windows)]
[ViewPort(1024, 768)]
public class FakePageModel : AbstractPage
{
    public FakePageModel(IBrowser browser, ILog log) : base(browser, log)
    {
        
    }
}