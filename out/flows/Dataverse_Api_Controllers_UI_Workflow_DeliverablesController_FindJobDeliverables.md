[web] GET /api/ui/workflow/deliverables/find-job/{jobId:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.FindJobDeliverables)  [L51–L62] [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableLightDto [L55]
  └─ calls DeliverableRepository.ReadQuery [L55]
  └─ queries Deliverable [L55]
    └─ reads_from Deliverables
  └─ uses_service DeliverableRepository
    └─ method ReadQuery [L55]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L57]
  └─ uses_service UserService
    └─ method GetUserId [L57]

