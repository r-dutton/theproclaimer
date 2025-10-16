[web] PUT /api/internal/job-statuses/{id}  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.UpdateDetails)  [L68–L76] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateJobStatusCommand [L72]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.CreateOrUpdateJobStatusCommandHandler.Handle [L38–L92]
      └─ maps_to JobStatusDto [L90]
      └─ uses_service IControlledRepository<JobStatus>
        └─ method WriteQuery [L58]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<KanbanColumn>
        └─ method ReadQuery [L71]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L90]
          └─ ... (no implementation details available)

