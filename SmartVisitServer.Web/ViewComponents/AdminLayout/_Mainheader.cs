using Application.Abstract.Common;
using Microsoft.AspNetCore.Mvc;
using Persistence.Authentication;

namespace SmartVisitServer.Web.ViewComponents.AdminLayout
{
    public class _Mainheader : ViewComponent
    {
        readonly IUser _currentuser;

        public _Mainheader(IUser currentuser)
        {
            _currentuser = currentuser;
        }

        public IViewComponentResult Invoke()
        {
            
            ViewData["CurrentUser"] = _currentuser.Name;
            ViewData["CurrentCompanyName"] = _currentuser.CompnanyName;
            ViewData["CurrentEmail"] = _currentuser.Email;

            return View();
        }
    }
}
