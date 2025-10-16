[web] POST /api/internal/job-statuses/create-or-update  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.CreateOrUpdate)  [L59–L66] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateJobStatusCommand [L62]
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

