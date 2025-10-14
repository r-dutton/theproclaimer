[web] POST /api/internal/deliverables/  (Dataverse.Api.Controllers.Internal.Workflow.DeliverablesController.Create)  [L96–L119] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to DeliverableDto [L118]
  └─ calls DeliverableRepository.Add [L116]
  └─ calls DeliverableRepository.WriteQuery [L102]
  └─ writes_to Deliverable [L116]
    └─ reads_from Deliverables
  └─ writes_to Deliverable [L102]
    └─ reads_from Deliverables
  └─ uses_service IControlledRepository<Deliverable>
    └─ method WriteQuery [L102]
  └─ uses_service IMapper
    └─ method Map [L118]
  └─ uses_service JobParamsService
    └─ method GetDeliverableParams [L112]
  └─ sends_request CanIAccessJobQuery [L108]
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

