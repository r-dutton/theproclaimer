[web] POST /api/ui/workflow/job-types  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.Create)  [L76–L89] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to JobTypeDto [L88]
  └─ calls JobTypeRepository.Add [L86]
  └─ calls JobTypeRepository.WriteQuery [L79]
  └─ insert JobType [L86]
    └─ reads_from JobTypes
  └─ write JobType [L79]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method WriteQuery [L79]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L88]
      └─ ... (no implementation details available)

