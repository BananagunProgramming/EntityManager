namespace EntityManager
{
    public class HelperPage : System.Web.WebPages.HelperPage
    {
        public static System.Web.Mvc.HtmlHelper MvcHtmlHelper
        {
            get { return ((System.Web.Mvc.WebViewPage) System.Web.WebPages.WebPageContext.Current.Page).Html; }
        }
    }
}
