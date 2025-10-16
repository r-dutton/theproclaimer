[web] PUT /documents/v1/documents/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.Update)  [L142–L147] status=200 [auth=Authentication.CoreWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L146]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

