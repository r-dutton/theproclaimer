[web] DELETE /api/ui/account-mapping/external-reporting-systems/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.DeleteExternalReportingSystem)  [L85–L92] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request DeleteExternalReportingSystemCommand [L89]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.ExternalReportingSystems.DeleteExternalReportingSystemCommandHandler.Handle [L23–L54]
      └─ uses_service UserService
        └─ method IsMaster [L38]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<ExternalReportingSystem>
        └─ method WriteQuery [L36]
          └─ ... (no implementation details available)

