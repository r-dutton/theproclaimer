[web] PUT /api/ui/account-mapping/external-reporting-systems/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.UpdateExternalReportingSystem)  [L73–L78] [auth=Authentication.AdminPolicy]
  └─ sends_request UpdateExternalReportingSystemCommand [L77]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.ExternalReportingSystems.UpdateExternalReportingSystemCommandHandler.Handle [L49–L79]
      └─ maps_to ExternalReportingSystemDto [L76]
      └─ uses_service IControlledRepository<ExternalReportingSystem>
        └─ method WriteQuery [L64]
      └─ uses_service IMapper
        └─ method Map [L76]
      └─ uses_service UserService
        └─ method IsMaster [L67]

