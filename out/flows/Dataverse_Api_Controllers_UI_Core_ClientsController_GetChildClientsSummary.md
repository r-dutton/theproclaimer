[web] GET /api/ui/clients/{id}/children/summary  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetChildClientsSummary)  [L179–L207] [auth=Authentication.UserPolicy]
  └─ maps_to ClientSummaryDto [L182]
    └─ automapper.registration DataverseMappingProfile (Client->ClientSummaryDto) [L188]
  └─ calls ClientRepository.ReadQuery [L182]
  └─ calls DocumentRepository.ReadQuery [L197]
  └─ queries Document [L197]
    └─ reads_from Documents
  └─ queries Client [L182]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L184]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L182]
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L197]
  └─ uses_service UserService
    └─ method GetUserId [L184]

