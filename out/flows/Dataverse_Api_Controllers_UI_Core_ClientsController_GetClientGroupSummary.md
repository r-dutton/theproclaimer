[web] GET /api/ui/clients/{id}/group/summary  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetClientGroupSummary)  [L161–L177] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ClientSummaryAsParentDto [L164]
    └─ automapper.registration DataverseMappingProfile (Client->ClientSummaryAsParentDto) [L210]
  └─ calls ClientRepository.ReadQuery [L164]
  └─ calls DocumentRepository.ReadQuery [L171]
  └─ query Document [L171]
    └─ reads_from Documents
  └─ query Client [L164]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L166]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L164]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L171]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L166]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

