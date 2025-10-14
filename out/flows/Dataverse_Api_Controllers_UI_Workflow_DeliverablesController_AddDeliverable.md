[web] POST /api/ui/workflow/deliverables/  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.AddDeliverable)  [L85–L108] [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableDto [L107]
  └─ calls DeliverableRepository.Add [L105]
  └─ calls DeliverableRepository.WriteQuery [L91]
  └─ writes_to Deliverable [L105]
    └─ reads_from Deliverables
  └─ writes_to Deliverable [L91]
    └─ reads_from Deliverables
  └─ uses_service DeliverableRepository
    └─ method WriteQuery [L91]
  └─ uses_service IMapper
    └─ method Map [L107]
  └─ uses_service JobParamsService
    └─ method GetDeliverableParams [L101]
  └─ sends_request CanIAccessJobQuery [L97]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIAccessJobQueryHandler.Handle [L39–L95]
      └─ uses_service IControlledRepository<Job>
        └─ method ReadQuery [L78]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L68]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L82]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L73]
      └─ uses_service UserService
        └─ method GetUserAsync [L71]
      └─ uses_cache IDistributedCache [L91]
        └─ method SetRecordAsync [write] [L91]
      └─ uses_cache IDistributedCache [L75]
        └─ method DoesRecordExistAsync [access] [L75]
      └─ uses_cache IDistributedCache [L73]
        └─ method CreateAccessKey [write] [L73]

