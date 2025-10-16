[web] POST /api/internal/job-types  (Dataverse.Api.Controllers.Internal.Workflow.JobTypesController.Create)  [L75–L88] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobTypeDto [L87]
  └─ calls JobTypeRepository.Add [L85]
  └─ calls JobTypeRepository.WriteQuery [L78]
  └─ insert JobType [L85]
    └─ reads_from JobTypes
  └─ write JobType [L78]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method WriteQuery [L78]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L87]
      └─ ... (no implementation details available)

