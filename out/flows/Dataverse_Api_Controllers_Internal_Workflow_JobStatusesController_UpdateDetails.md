[web] PUT /api/internal/job-statuses/{id}  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.UpdateDetails)  [L68–L76] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateJobStatusCommand [L72]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.CreateOrUpdateJobStatusCommandHandler.Handle [L38–L92]
      └─ maps_to JobStatusDto [L90]
      └─ uses_service IControlledRepository<JobStatus>
        └─ method WriteQuery [L58]
      └─ uses_service IControlledRepository<KanbanColumn>
        └─ method ReadQuery [L71]
      └─ uses_service IMapper
        └─ method Map [L90]

