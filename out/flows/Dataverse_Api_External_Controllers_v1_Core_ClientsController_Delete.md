[web] DELETE /core/v1/clients/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.Delete)  [L338–L342] status=200 [auth=Authentication.CoreWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method Delete [L341]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.Delete [L12-L74]

