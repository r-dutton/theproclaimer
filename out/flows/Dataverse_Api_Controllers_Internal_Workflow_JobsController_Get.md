[web] GET /api/internal/jobs/{id:guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobsController.Get)  [L45–L54] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobDto [L50]
    └─ automapper.registration DataverseMappingProfile (Job->JobDto) [L307]
    └─ automapper.registration ExternalApiMappingProfile (Job->JobDto) [L120]
    └─ converts_to JobExportDto [L312]
  └─ calls JobRepository.ReadQuery [L50]
  └─ query Job [L50]
    └─ reads_from Jobs
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L52]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L50]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L52]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
  └─ sends_request CanIAccessJobQuery [L48]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIAccessJobQueryHandler.Handle [L39–L95]
      └─ uses_service UserService
        └─ method GetUserAsync [L71]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L73]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L68]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service IControlledRepository<Job>
        └─ method ReadQuery [L78]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L82]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L91]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L75]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L73]

