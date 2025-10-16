[web] GET /workflow/v1/job-types/audits  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.GetAuditsQuery)  [L102–L108] status=200
  └─ maps_to EntityAuditDto [L105]
  └─ calls JobTypeRepository.ReadQuery [L105]
  └─ query JobType [L105]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L105]
      └─ ... (no implementation details available)

