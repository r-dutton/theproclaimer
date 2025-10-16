[web] GET /api/internal/Propagator/workflow-changed-job-status  (Dataverse.Api.Controllers.Internal.PropagatorController.GetWorkflowJobs)  [L150–L169] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls JobRepository.ReadQuery [L153]
  └─ query Job [L153]
    └─ reads_from Jobs
  └─ uses_service JobRepository
    └─ method ReadQuery [L153]
      └─ implementation Dataverse.Data.Repository.Workflow.JobRepository.ReadQuery [L8-L46]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Job writes=0 reads=1
    └─ services 1
      └─ JobRepository

