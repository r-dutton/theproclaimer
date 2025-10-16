[web] POST /api/internal/job-statuses/create-or-update  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.CreateOrUpdate)  [L59–L66] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateJobStatusCommand -> CreateOrUpdateJobStatusCommandHandler [L62]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.CreateOrUpdateJobStatusCommandHandler.Handle [L38–L92]
      └─ maps_to JobStatusDto [L90]
      └─ uses_service IControlledRepository<KanbanColumn> (Scoped (inferred))
        └─ method ReadQuery [L71]
          └─ implementation Dataverse.Data.Repository.Workflow.KanbanColumnRepository.ReadQuery
      └─ uses_service IControlledRepository<JobStatus> (Scoped (inferred))
        └─ method WriteQuery [L58]
          └─ implementation Dataverse.Data.Repository.Workflow.JobStatusRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ CreateOrUpdateJobStatusCommand
    └─ handlers 1
      └─ CreateOrUpdateJobStatusCommandHandler
    └─ mappings 1
      └─ JobStatusDto

