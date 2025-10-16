[web] GET /api/ui/workflow/deliverables/find-job/{jobId:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.FindJobDeliverables)  [L51–L62] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableLightDto [L55]
  └─ calls DeliverableRepository.ReadQuery [L55]
  └─ query Deliverable [L55]
    └─ reads_from Deliverables
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L57]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service DeliverableRepository
    └─ method ReadQuery [L55]
      └─ implementation Dataverse.Data.Repository.Workflow.DeliverableRepository.ReadQuery [L8-L45]
  └─ uses_service UserService
    └─ method GetUserId [L57]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

