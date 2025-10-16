[web] GET /api/ui/cloud-capcha/access-info  (Dataverse.Api.Controllers.UI.CloudCapchaController.GetAccessInfo)  [L24–L29] status=200 [auth=Authentication.AdminPolicy,Authentication.AdminPolicy]
  └─ uses_service IDataGetCloudCapchaService (AddTransient)
    └─ method GetAccessInfo [L28]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetCloudCapchaService.GetAccessInfo [L13-L49]

