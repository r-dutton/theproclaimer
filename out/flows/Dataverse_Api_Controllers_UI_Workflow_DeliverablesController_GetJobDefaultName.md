[web] GET /api/ui/workflow/deliverables/default-name  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.GetJobDefaultName)  [L74–L83] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request DefaultDeliverableNameQuery [L81]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.DefaultDeliverableNameQueryHandler.Handle [L41–L136]
      └─ maps_to DeliverableTypeDto [L94]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L80]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserId [L80]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L74]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service JobParamsService
        └─ method GetClient [L93]
          └─ implementation Dataverse.ApplicationService.Services.Workflow.JobParamsService.GetClient [L24-L215]
      └─ uses_service IControlledRepository<Job>
        └─ method ReadQuery [L75]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L94]
          └─ ... (no implementation details available)
  └─ sends_request DefaultDeliverableNameQuery [L80]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.DefaultDeliverableNameQueryHandler.Handle [L41–L136]
      └─ maps_to DeliverableTypeDto [L94]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L80]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserId [L80]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L74]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service JobParamsService
        └─ method GetClient [L93]
          └─ implementation Dataverse.ApplicationService.Services.Workflow.JobParamsService.GetClient [L24-L215]
      └─ uses_service IControlledRepository<Job>
        └─ method ReadQuery [L75]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L94]
          └─ ... (no implementation details available)

