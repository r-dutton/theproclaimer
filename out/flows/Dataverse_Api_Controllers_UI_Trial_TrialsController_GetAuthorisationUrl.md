[web] GET /api/ui/trials/{type}/authorisation-url  (Dataverse.Api.Controllers.UI.Trial.TrialsController.GetAuthorisationUrl)  [L46–L57] status=200 [AllowAnonymous]
  └─ uses_service ConnectionService
    └─ method GetApiMethods [L56]
      └─ implementation Dataverse.ApplicationService.Services.ConnectionService.GetApiMethods [L9-L19]
  └─ impact_summary
    └─ services 1
      └─ ConnectionService

