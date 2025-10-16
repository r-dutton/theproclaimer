[web] GET /workflow/v1/job-types/  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.MinimalQuery)  [L69–L76] status=200
  └─ calls JobTypeRepository.ReadQuery [L73]
  └─ query JobType [L73]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method ReadQuery [L73]
      └─ ... (no implementation details available)

