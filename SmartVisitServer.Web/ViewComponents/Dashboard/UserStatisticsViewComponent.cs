using Application.Services.Repositories;
using Application.Services.UserService;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using SmartVisitServer.Web.Models.Users;

namespace SmartVisitServer.Web.ViewComponents.Dashboard.UserStatistics
{
    [ViewComponent]
    public class UserStatisticsViewComponent:ViewComponent
    {
        readonly IUserService _userService;

        public UserStatisticsViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var userIstatic = await _userService.UserStateUsersIstaticQueryResponseAsync();

            var model = new UserStatisticsViewModel
            {
                TotalUsers = userIstatic.TotalUsers,
                ActiveUsers = userIstatic.ActiveUsers,
                PassiveUsers = userIstatic.PassiveUsers,
                InactiveUsers = userIstatic.InactiveUsers,
                BlockedUsers = userIstatic.BlockedUsers,
                DeletedUsers = userIstatic.DeletedUsers,
                ActiveUserPercentage = userIstatic.ActiveUserPercentage,
                PassiveUserPercentage = userIstatic.PassiveUserPercentage,
                InactiveUserPercentage = userIstatic.InactiveUserPercentage,
                BlockedUserPercentage = userIstatic.BlockedUserPercentage,
                DeletedUserPercentage = userIstatic.DeletedUserPercentage
            };

            return View("/Views/Dashboard/UserStatistics/Default.cshtml", model);
        }

    }
}
