[web] GET /api/ui/fyi/cabinets  (Dataverse.Api.Controllers.UI.FyiController.GetCabinets)  [L47–L53] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetCabinetsAsync [L50]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetCabinetsAsync [L19-L291]

