[web] DELETE /api/ui/cloud-capcha/disconnect  (Dataverse.Api.Controllers.UI.CloudCapchaController.Disconnect)  [L38–L43] status=200 [auth=Authentication.AdminPolicy,Authentication.AdminPolicy]
  └─ uses_service IDataGetCloudCapchaService (AddTransient)
    └─ method Disconnect [L42]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetCloudCapchaService.Disconnect [L13-L49]

