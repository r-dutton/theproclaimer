[web] GET /api/internal/jobs/{id:guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobsController.Get)  [L45–L54] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobDto [L50]
    └─ automapper.registration DataverseMappingProfile (Job->JobDto) [L306]
    └─ automapper.registration ExternalApiMappingProfile (Job->JobDto) [L120]
    └─ converts_to JobExportDto [L311]
  └─ calls JobRepository.ReadQuery [L50]
  └─ queries Job [L50]
    └─ reads_from Jobs
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L52]
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L50]
  └─ uses_service UserService
    └─ method GetUserId [L52]
  └─ sends_request CanIAccessJobQuery [L48]
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

