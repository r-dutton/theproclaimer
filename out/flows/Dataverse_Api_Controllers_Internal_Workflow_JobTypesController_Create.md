[web] POST /api/internal/job-types  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.Create)  [L75–L88] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobTypeDto [L87]
  └─ calls JobTypeRepository.Add [L85]
  └─ calls JobTypeRepository.WriteQuery [L78]
  └─ writes_to JobType [L85]
    └─ reads_from JobTypes
  └─ writes_to JobType [L78]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method WriteQuery [L78]
  └─ uses_service IMapper
    └─ method Map [L87]

