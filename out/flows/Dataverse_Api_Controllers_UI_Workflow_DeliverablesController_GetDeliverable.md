[web] GET /api/ui/workflow/deliverables/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.GetDeliverable)  [L64–L72] [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableDto [L68]
  └─ calls DeliverableRepository.ReadQuery [L68]
  └─ queries Deliverable [L68]
    └─ reads_from Deliverables
  └─ uses_service DeliverableRepository
    └─ method ReadQuery [L68]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L70]
  └─ uses_service UserService
    └─ method GetUserId [L70]

