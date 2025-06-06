﻿using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using MediatR;

namespace Application.Features.Panel.Queries.ActiveOrPasifUsers
{
    public class UserTotalPasifAndActiveQueryRequest : IRequest<UserTotalPasifAndActiveQueryResponse>/*, ISecuredRequest*/
    {
        // public string[] Roles => new[] { GeneralOperationClaims.Admin };
    }
}
