[web] GET /api/ui/imanage/standard-user-authorisation-url  (Dataverse.Api.Controllers.UI.IManageController.GetAuthorisationUrl)  [L70–L77] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetAuthorisationUrlForStandardUser [L75]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetAuthorisationUrlForStandardUser [L19-L225]

