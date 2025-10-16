[web] GET /api/ui/workflow/deliverables/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.GetDeliverable)  [L64–L72] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableDto [L68]
  └─ calls DeliverableRepository.ReadQuery [L68]
  └─ query Deliverable [L68]
    └─ reads_from Deliverables
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L70]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service DeliverableRepository
    └─ method ReadQuery [L68]
      └─ implementation Dataverse.Data.Repository.Workflow.DeliverableRepository.ReadQuery [L8-L45]
  └─ uses_service UserService
    └─ method GetUserId [L70]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

