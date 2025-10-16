[web] DELETE /api/ui/imanage/disconnect-user-level  (Dataverse.Api.Controllers.UI.IManageController.DisconnectUserLevel)  [L106–L110] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method DisconnectUserLevel [L109]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.DisconnectUserLevel [L19-L225]
  └─ impact_summary
    └─ services 1
      └─ IDatagetImanageService

