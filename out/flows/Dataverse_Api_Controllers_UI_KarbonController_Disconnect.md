[web] DELETE /api/ui/karbon/disconnect  (Dataverse.Api.Controllers.UI.KarbonController.Disconnect)  [L44–L48] status=200 [auth=Authentication.AdminPolicy]
  └─ uses_service IDatagetKarbonService (AddTransient)
    └─ method Disconnect [L47]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetKarbonService.Disconnect [L13-L53]
  └─ impact_summary
    └─ services 1
      └─ IDatagetKarbonService

