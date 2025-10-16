[web] GET /api/ui/clients/{id}/children/summary  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetChildClientsSummary)  [L179–L207] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ClientSummaryDto [L182]
    └─ automapper.registration DataverseMappingProfile (Client->ClientSummaryDto) [L189]
  └─ calls ClientRepository.ReadQuery [L182]
  └─ calls DocumentRepository.ReadQuery [L197]
  └─ query Document [L197]
    └─ reads_from Documents
  └─ query Client [L182]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L184]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L182]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L197]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L184]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

