[web] POST /webhooks/v1/webhooks/  (Dataverse.Api.External.Controllers.v1.Core.WebhooksController.Create)  [L100–L109] status=201 [auth=Authentication.CoreRead,Authentication.CoreWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PostSerialisedModel [L107]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PostSerialisedModel [L12-L74]
  └─ uses_service RequestInfoService
    └─ method GetRequestingClientId [L104]
      └─ implementation Dataverse.Services.Features.RequestInfoService.GetRequestingClientId [L11-L92]
      └─ implementation Dataverse.Services.Features.RequestInfoService.GetRequestingClientId [L11-L92]
      └─ implementation Dataverse.Services.Features.RequestInfoService.GetRequestingClientId [L11-L92]

