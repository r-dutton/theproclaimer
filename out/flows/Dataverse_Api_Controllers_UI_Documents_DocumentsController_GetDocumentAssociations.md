[web] GET /api/ui/documents/documents/associations  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetDocumentAssociations)  [L149–L167] [auth=Authentication.UserPolicy]
  └─ sends_request CanIAccessDocumentQuery [L159]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L66]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L68]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L58]
      └─ uses_service UserService
        └─ method GetUserAsync [L61]
  └─ sends_request GetDocumentAssociations [L163]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentAssociationsHandler.Handle [L39–L148]
      └─ maps_to DeliverableUltraLightDto [L98]
        └─ automapper.registration DataverseMappingProfile (Deliverable->DeliverableUltraLightDto) [L354]
      └─ maps_to JobUltraLightDto [L86]
        └─ automapper.registration DataverseMappingProfile (Job->JobUltraLightDto) [L305]
      └─ uses_client WorkpapersClient [L119]
      └─ uses_service IControlledRepository<Deliverable>
        └─ method ReadQuery [L98]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L74]
      └─ uses_service IControlledRepository<Job>
        └─ method ReadQuery [L86]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L88]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L114]
      └─ uses_service UserService
        └─ method GetUserId [L117]
      └─ uses_service WorkpapersClient
        └─ method Get [L119]

