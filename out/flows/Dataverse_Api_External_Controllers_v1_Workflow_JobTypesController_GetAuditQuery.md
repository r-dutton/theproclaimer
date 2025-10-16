[web] GET /workflow/v1/job-types/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.GetAuditQuery)  [L116–L121] status=200
  └─ maps_to EntityAuditDto [L119]
  └─ calls JobTypeRepository.ReadQuery [L119]
  └─ query JobType [L119]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L119]
      └─ ... (no implementation details available)

