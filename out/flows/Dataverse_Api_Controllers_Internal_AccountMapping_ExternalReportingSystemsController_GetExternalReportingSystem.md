[web] GET /api/internal/account-mapping/external-reporting-systems/{id:guid}  (Dataverse.Api.Controllers.Internal.AccountMapping.ExternalReportingSystemsController.GetExternalReportingSystem)  [L33–L37] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetExternalReportingSystemQuery [L36]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemQueryHandler.Handle [L28–L47]
      └─ maps_to ExternalReportingSystemDto [L43]
        └─ automapper.registration DataverseMappingProfile (ExternalReportingSystem->ExternalReportingSystemDto) [L241]
      └─ uses_service UserService
        └─ method IsMaster [L44]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<ExternalReportingSystem>
        └─ method ReadQuery [L43]
          └─ ... (no implementation details available)

