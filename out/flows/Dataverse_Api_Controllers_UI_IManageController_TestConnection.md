[web] GET /api/ui/imanage/test-connection  (Dataverse.Api.Controllers.UI.IManageController.TestConnection)  [L112–L118] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method TestConnection [L115]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.TestConnection [L19-L225]
  └─ impact_summary
    └─ services 1
      └─ IDatagetImanageService

