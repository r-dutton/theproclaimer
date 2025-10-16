[web] PUT /webhooks/v1/webhooks/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.WebhooksController.Update)  [L119–L129] status=200 [auth=Authentication.CoreRead,Authentication.CoreWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L127]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

