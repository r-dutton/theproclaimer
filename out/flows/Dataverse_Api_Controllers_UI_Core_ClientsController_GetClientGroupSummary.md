[web] GET /api/ui/clients/{id}/group/summary  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetClientGroupSummary)  [L161–L177] [auth=Authentication.UserPolicy]
  └─ maps_to ClientSummaryAsParentDto [L164]
    └─ automapper.registration DataverseMappingProfile (Client->ClientSummaryAsParentDto) [L209]
  └─ calls ClientRepository.ReadQuery [L164]
  └─ calls DocumentRepository.ReadQuery [L171]
  └─ queries Document [L171]
    └─ reads_from Documents
  └─ queries Client [L164]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L166]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L164]
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L171]
  └─ uses_service UserService
    └─ method GetUserId [L166]

