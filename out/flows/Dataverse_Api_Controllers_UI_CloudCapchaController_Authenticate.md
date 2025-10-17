[web] PUT /api/ui/cloud-capcha/access-info  (Dataverse.Api.Controllers.UI.CloudCapchaController.Authenticate)  [L31–L36] status=200 [auth=Authentication.AdminPolicy,Authentication.AdminPolicy]
  └─ uses_service IDataGetCloudCapchaService (AddTransient)
    └─ method Authenticate [L35]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetCloudCapchaService.Authenticate [L13-L49]
  └─ impact_summary
    └─ services 1
      └─ IDataGetCloudCapchaService

