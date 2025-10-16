[web] GET /api/ui/cloud-capcha/test-connection  (Dataverse.Api.Controllers.UI.CloudCapchaController.TestConnection)  [L45–L50] status=200 [auth=Authentication.AdminPolicy,Authentication.AdminPolicy]
  └─ uses_service IDataGetCloudCapchaService (AddTransient)
    └─ method TestConnection [L49]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetCloudCapchaService.TestConnection [L13-L49]

