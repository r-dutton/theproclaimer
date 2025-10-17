[web] DELETE /api/ui/imanage/disconnect  (Dataverse.Api.Controllers.UI.IManageController.Disconnect)  [L99–L104] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method Disconnect [L103]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.Disconnect [L19-L225]
  └─ impact_summary
    └─ services 1
      └─ IDatagetImanageService

