[web] GET /api/internal/Propagator/workflow-tasks  (Dataverse.Api.Controllers.Internal.PropagatorController.GetWorkflowTasks)  [L133–L148] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TaskRepository.ReadQuery [L136]
  └─ uses_service TaskRepository
    └─ method ReadQuery [L136]
      └─ implementation Dataverse.Data.Repository.Workflow.TaskRepository.ReadQuery [L8-L40]

