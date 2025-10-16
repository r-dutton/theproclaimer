[web] GET /api/ui/documents/documents/associations  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetDocumentAssociations)  [L149–L167] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CanIAccessDocumentQuery [L159]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L66]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserAsync [L61]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L58]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L68]
          └─ ... (no implementation details available)
  └─ sends_request GetDocumentAssociations [L163]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentAssociationsHandler.Handle [L39–L148]
      └─ maps_to DeliverableUltraLightDto [L98]
        └─ automapper.registration DataverseMappingProfile (Deliverable->DeliverableUltraLightDto) [L355]
      └─ maps_to JobUltraLightDto [L86]
        └─ automapper.registration DataverseMappingProfile (Job->JobUltraLightDto) [L306]
      └─ uses_client WorkpapersClient [L119]
      └─ uses_service UserService
        └─ method GetUserId [L117]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L114]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service IControlledRepository<Deliverable>
        └─ method ReadQuery [L98]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L74]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Job>
        └─ method ReadQuery [L86]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L88]
          └─ ... (no implementation details available)
      └─ uses_service WorkpapersClient
        └─ method Get [L119]
          └─ ... (no implementation details available)

