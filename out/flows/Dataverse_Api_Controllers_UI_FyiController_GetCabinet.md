[web] GET /api/ui/fyi/cabinets/{cabinetId:long}  (Dataverse.Api.Controllers.UI.FyiController.GetCabinet)  [L55–L61] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetCabinetAsync [L58]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetCabinetAsync [L19-L291]

