[web] GET /api/ui/workflow/deliverables/default-name  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.GetJobDefaultName)  [L74–L83] [auth=Authentication.UserPolicy]
  └─ sends_request DefaultDeliverableNameQuery [L81]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.DefaultDeliverableNameQueryHandler.Handle [L41–L136]
      └─ maps_to DeliverableTypeDto [L94]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L80]
      └─ uses_service IControlledRepository<Job>
        └─ method ReadQuery [L75]
      └─ uses_service IMapper
        └─ method Map [L94]
      └─ uses_service JobParamsService
        └─ method GetClient [L93]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L74]
      └─ uses_service UserService
        └─ method GetUserId [L80]
  └─ sends_request DefaultDeliverableNameQuery [L80]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.DefaultDeliverableNameQueryHandler.Handle [L41–L136]
      └─ maps_to DeliverableTypeDto [L94]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L80]
      └─ uses_service IControlledRepository<Job>
        └─ method ReadQuery [L75]
      └─ uses_service IMapper
        └─ method Map [L94]
      └─ uses_service JobParamsService
        └─ method GetClient [L93]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L74]
      └─ uses_service UserService
        └─ method GetUserId [L80]

