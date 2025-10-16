[web] DELETE /core/v1/teams/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.Delete)  [L158–L162] status=200 [auth=Authentication.CoreWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method Delete [L161]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.Delete [L12-L74]

