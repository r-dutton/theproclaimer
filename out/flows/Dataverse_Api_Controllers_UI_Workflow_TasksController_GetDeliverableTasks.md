[web] GET /api/ui/workflow/tasks/for-deliverable/{deliverableId:guid}  (Dataverse.Api.Controllers.UI.Workflow.TasksController.GetDeliverableTasks)  [L74–L86] [auth=Authentication.UserPolicy]
  └─ maps_to WorkflowTaskLightDto [L80]
  └─ calls TaskRepository.ReadQuery [L80]
  └─ uses_service TaskRepository
    └─ method ReadQuery [L80]
  └─ sends_request CanIAccessJobQuery [L78]
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

