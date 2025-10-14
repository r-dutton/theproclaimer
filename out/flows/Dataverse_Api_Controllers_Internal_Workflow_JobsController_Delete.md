[web] DELETE /api/internal/jobs/{id:guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobsController.Delete)  [L70–L80] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobRepository.WriteQuery [L75]
  └─ writes_to Job [L75]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method WriteQuery [L75]
  └─ sends_request CanIAccessJobQuery [L73]
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

