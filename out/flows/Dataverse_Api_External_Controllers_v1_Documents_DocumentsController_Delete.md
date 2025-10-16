[web] DELETE /documents/v1/documents/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.Delete)  [L158–L162] status=200 [auth=Authentication.CoreWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method Delete [L161]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.Delete [L12-L74]

