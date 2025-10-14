[web] POST /api/ui/workflow/job-types  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.Create)  [L76–L89] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to JobTypeDto [L88]
  └─ calls JobTypeRepository.Add [L86]
  └─ calls JobTypeRepository.WriteQuery [L79]
  └─ writes_to JobType [L86]
    └─ reads_from JobTypes
  └─ writes_to JobType [L79]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method WriteQuery [L79]
  └─ uses_service IMapper
    └─ method Map [L88]

