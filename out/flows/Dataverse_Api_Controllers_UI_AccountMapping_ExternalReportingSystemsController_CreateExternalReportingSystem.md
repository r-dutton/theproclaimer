[web] POST /api/ui/account-mapping/external-reporting-systems  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.CreateExternalReportingSystem)  [L58–L65] status=201 [auth=Authentication.AdminPolicy]
  └─ sends_request CreateExternalReportingSystemCommand [L62]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.ExternalReportingSystems.CreateExternalReportingSystemCommandHandler.Handle [L41–L70]
      └─ maps_to ExternalReportingSystemDto [L68]
      └─ uses_service UserService
        └─ method IsMaster [L56]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<ExternalReportingSystem>
        └─ method ReadQuery [L58]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L68]
          └─ ... (no implementation details available)

