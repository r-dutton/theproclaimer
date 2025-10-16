[web] GET /workflow/v1/job-types/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.FullQuery)  [L88–L95] status=200
  └─ calls JobTypeRepository.ReadQuery [L92]
  └─ query JobType [L92]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L92]
      └─ ... (no implementation details available)

