[web] POST /webhooks/v1/webhooks/  (Dataverse.Api.External.Controllers.v1.Core.WebhooksController.Create)  [L100–L109] [auth=Authentication.CoreRead,Authentication.CoreWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PostSerialisedModel [L107]
  └─ uses_service RequestInfoService
    └─ method GetRequestingClientId [L104]

