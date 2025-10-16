[web] DELETE /core/v1/offices/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.Delete)  [L159–L163] status=200 [auth=Authentication.CoreWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method Delete [L162]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.Delete [L12-L74]

