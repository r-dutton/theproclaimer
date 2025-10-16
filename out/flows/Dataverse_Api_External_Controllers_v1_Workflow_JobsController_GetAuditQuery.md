[web] GET /workflow/v1/jobs/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.GetAuditQuery)  [L118–L123] status=200
  └─ maps_to EntityAuditDto [L121]
  └─ calls JobRepository.ReadQuery [L121]
  └─ query Job [L121]
    └─ reads_from Jobs
  └─ uses_service IControlledRepository<Job>
    └─ method ReadQuery [L121]
      └─ ... (no implementation details available)

